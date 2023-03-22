using System.Diagnostics;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using WorkitemTst.Models;

namespace WorkitemTst.Facade
{

    public class ProcessOutput
    {
        public string Output { get; set; }
        public string Error { get; set; }
    }


    public class WorkitemTypeInput
    {
        public string Name { get; set; }
        public List<string> Fields { get; set; }

        public string Value { get; set; }
    }



    public class Tfs
    {

        public const string azCredential = "en25man5qo4t4rjypxcvtc4b6wgrtmnxe2ebs4twlea45vr4xaha";
        public const string urlCollection = "https://vmsys-tfsd02/DefaultCollection";
        public const string projectName = "New VTXRM";
        public const string projectGuid = "4d9b6282-36c4-4570-b8c8-088bdad876ac";
        public const string witadminPath = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\Common7\\IDE\\CommonExtensions\\Microsoft\\TeamFoundation\\Team Explorer";

        public Tfs()
        {

        }





        public async Task<T> GetWitClient<T>() where T : VssHttpClientBase
        {
            VssConnection connection = new VssConnection(
                new Uri(urlCollection),
                new VssBasicCredential("", azCredential)
            );

            // Get the work item tracking client
            T witClient = connection.GetClient<T>();
            return witClient;
        }

        private ProcessOutput ProcessCommand(string command, string workingDirectory = "")
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = workingDirectory;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"/C {command}";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return new ProcessOutput()
            {
                Output = output,
                Error = error
            };
        }


        private ProcessOutput ExecuteWitadmin(string operation, string args)
        {
            var command = $"witadmin.exe {operation} /collection:\"{urlCollection}\" /p:\"{projectName}\" {args}";
            var workingDirectory = witadminPath;
            var result = ProcessCommand(command, workingDirectory);
            if (!string.IsNullOrEmpty(result.Error))
            {
                Console.Error.WriteLine(result.Error);
                throw new Exception("Algo ocorreu na chamada witadmin");
            }
            return result;
        }




        public List<string> GetWorkitemTypeList()
        {
            var result = ExecuteWitadmin("listwitd", "");

            return result.Output.Replace("\r", "").Split("\n").Where( entry =>  !string.IsNullOrEmpty(entry)  ).ToList();
        }


        public XmlWorkitemType GetWorkitemType(string wit)
        {
            var result = ExecuteWitadmin("exportwitd", $"/n:\"{wit}\"");

            var xmlWorkitem = result.Output;
            XmlWorkitemType workitemType;

            XmlSerializer serializer = new XmlSerializer(typeof(XmlWorkitemType));
            using (TextReader reader = new StringReader(xmlWorkitem))
            {
                XmlSerializer xs = new XmlSerializer(typeof(XmlWorkitemType));
                workitemType = (XmlWorkitemType)serializer.Deserialize(reader);
            }

            return workitemType;
        }


        public string CreateOrUpdateWorkitemType(string wit, WorkitemTypeInput workitem)
        {
            XmlWorkitemType inputXml = new XmlWorkitemType()
            {
                Version = "1.0",
                WorkItemType = new WorkItemType()
                {
                    Name = wit,
                    Description = $"description for {wit}",
                    Fields = workitem.Fields.Select(f => new Field()
                    {
                        Name = f,
                        Reference = $"vtxrm.ref{f}",
                        HelpText = $"help text for {f}, with value: {workitem.Value}",
                        Type = "String"
                    }).ToArray(),
                    Form = new Form()
                    {
                        LayoutXml = @"
                                    <Group>
                                        <Column PercentWidth='75'>
                                            <Control FieldName='System.Title' Type='FieldControl' Label='&amp;Titulo:' LabelPosition='Left' />
                                        </Column>
                                    </Group>
                                ",
                        WebLayoutXml = $@"
                                <Page Label=""Details"" LayoutMode=""FirstColumnWide"">
                                  <Section>
                                    <Group Label=""Description2 {workitem.Value}"">
                                      <Control Type=""HtmlFieldControl"" FieldName=""System.Description"" />
                                    </Group>
                                  </Section>
                                </Page>                                
                            ",



                    },
                    Workflow = new Workflow2()
                    {
                        States = new List<State>() {
                            new State() {
                                Value = "Active",
                            },
                            new State() {
                                Value = "New",
                            }
                        }.ToArray(),
                        Transitions = new List<Transition2>() {
                                                      new Transition2(){
                                From = "",
                                To = "New",
                                Reasons= new List<Reason>()
                                {
                                    new DefaultReason(){
                                    Value = "Just Default"
                                    }

                                }.ToArray()

                            },
                            new Transition2(){
                                From = "New",
                                To = "Active",
                                Reasons= new List<Reason>()
                                {
                                    new ReasonItem() {
                                        Value = "Just Because"
                                        //DefaultReason = "Just because"
                                    },
                                    new DefaultReason(){
                                    Value = "Just Default"
                                    }

                                }.ToArray()

                            }
                        }.ToArray(),
                    }
                }
            };

            var tempFilePath = Path.GetTempFileName();
            using (var writer = new StreamWriter(tempFilePath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(XmlWorkitemType));
                xs.Serialize(writer, inputXml);
            }

            var result = ExecuteWitadmin("importwitd", $"/f:\"{tempFilePath}\"");

            return result.Output;
        }



        public string DeleteWorkitemType(string wit)
        {
            var result = ExecuteWitadmin("destroywitd", $"/n:\"{wit}\" /noprompt");

            return result.Output;
        }




    }
}
