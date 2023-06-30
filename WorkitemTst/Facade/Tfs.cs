using System.Diagnostics;
using System.IO;
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
        private readonly AppOptions _appOptions;

        public Tfs(AppOptions appOptions)
        {
            _appOptions = appOptions;   
        }





        public async Task<T> GetWitClient<T>() where T : VssHttpClientBase
        {
            VssConnection connection = new VssConnection(
                new Uri(_appOptions.UrlCollection),
                new VssBasicCredential("", _appOptions.AzureCredentials)
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
            var command = $"witadmin.exe {operation} /collection:\"{_appOptions.UrlCollection}\" {args}";
            var workingDirectory = _appOptions.PathWitAdmin;
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
            var result = ExecuteWitadmin("listwitd", $"/p:\"{_appOptions.ProjectName}\"");

            return result.Output.Replace("\r", "").Split("\n").Where( entry =>  !string.IsNullOrEmpty(entry)  ).ToList();
        }


        public XmlWorkitemType GetWorkitemType(string wit)
        {
            var result = ExecuteWitadmin("exportwitd", $"/p:\"{_appOptions.ProjectName}\" /n:\"{wit}\"");

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
                    //GlobalLists = new GlobalList[] { 
                    //    new GlobalList(){ 
                    //        ListItems = new ListItem[]{
                    //            new ListItem(){
                    //                Value = "item A"
                    //            },
                    //            new ListItem(){
                    //                Value = "item B"
                    //            },
                    //        }
                    //    }
                    //},
                    Fields =  workitem.Fields
                        .Select(f => new Field()
                        {
                            Name = f,
                            Reference = $"vtxrm.ref{f}",
                            HelpText = $"help text for {f}, with value: {workitem.Value}",
                            Type = "String",
                       
                        })
                        .Concat(new Field[] {
                            new Field()
                            {
                             Name = $"other03",
                             Reference = $"vtxrm.jhpField03",
                             //HelpText = $"help text for jhpField02",
                             Type = "String",
                             //AllowedValues = new ListItem[] {
                             //   new ListItem() { Value = "allow01" },
                             //   new ListItem() { Value = "allow02" },
                             //   new ListItem() { Value = "allow03" },
                             //}
                            }
                        })
                        .ToArray(),
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
                                    
                                    <Group Label=""Field 3"">
                                        <Control FieldName=""vtxrm.jhpField03"" Type=""FieldControl"" Label=""&amp;JHP Field 3:"" LabelPosition=""Left"" />
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

            var result = ExecuteWitadmin("importwitd", $"/p:\"{_appOptions.ProjectName}\" /f:\"{tempFilePath}\"");

            return result.Output;
        }

        public string RenameWorkitemType(string wit, string witNewName)
        {
            var result = ExecuteWitadmin("renamewitd", $"/p:\"{_appOptions.ProjectName}\" /n:\"{wit}\" /new:\"{witNewName}\" /noprompt");
            return result.Output;
        }




        public string UploadWorkitemType(IFormFile file)
        {
            if (file.Length <= 0)
            {
                throw new Exception("Invalid file");
            }

            string tempFilePath = Path.GetTempFileName();
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            var result = ExecuteWitadmin("importwitd", $"/p:\"{_appOptions.ProjectName}\" /f:\"{tempFilePath}\"");

            return result.Output;

        }



        public string DeleteWorkitemType(string wit)
        {
            var result = ExecuteWitadmin("destroywitd", $"/p:\"{_appOptions.ProjectName}\" /n:\"{wit}\" /noprompt");

            return result.Output;
        }


        public IEnumerable<dynamic> GetLinkTypes() {
            var result = ExecuteWitadmin("listlinktypes", $"");
            return result.Output.Split("\r\n\r\n")
                .Where( row => !string.IsNullOrEmpty(row))
                .Select( row => {
                    var parts = row.Split("\r\n").Where( row => !string.IsNullOrEmpty(row));
                    return new { 
                        Reference = parts.ElementAt(0).Split(":")[1].Trim(),
                        Names = parts.ElementAt(1).Split(":")[1].Split(",").Select( row => row.Trim() ), 
                        Topology = parts.ElementAt(2).Split(":")[1].Trim(),
                        Active = parts.ElementAt(3).Split(":")[1].Trim(),
                    }; 

                } );
        }



        public IEnumerable<dynamic> GetGlobalLists()
        {
            var result = ExecuteWitadmin("listgloballist", $"");
            return result.Output.Split("\r\n")
                .Where(row => !string.IsNullOrEmpty(row));
        }

        public dynamic ExportGlobalLists()
        {
            var tempFilePath = Path.GetTempFileName();
            var result = ExecuteWitadmin("exportgloballist", $" /f:\"{tempFilePath}\"");
            string readText = File.ReadAllText(tempFilePath);
            return readText;
        }

        public dynamic DestroyGlobalList(string globallistName)
        {
            var tempFilePath = Path.GetTempFileName();
            var result = ExecuteWitadmin("destroygloballist", $" /n:\"{globallistName}\" /noprompt /force");
            string readText = File.ReadAllText(tempFilePath);
            return readText;
        }

        //witadmin destroygloballist /collection:CollectionURL /n:GlobalListName[/ noprompt][/ force]


        public dynamic ExportGlobalWorkflow(string project)
        {
            var tempFilePath = Path.GetTempFileName();
            //var witAdminArgs = $" /f:\"{tempFilePath}\" /exportgloballists ";
            var witAdminArgs = $" /f:\"{tempFilePath}\" ";
            if (!string.IsNullOrEmpty(project)) {
                witAdminArgs = $" {witAdminArgs} /p:\"{_appOptions.ProjectName}\"";
            }
            
            //var result = ExecuteWitadmin("exportglobalworkflow ", $" /f:\"{tempFilePath}\" /p:\"{projectName}\" /exportgloballists ");
            var result = ExecuteWitadmin("exportglobalworkflow ", $"{witAdminArgs}");

            string readText = File.ReadAllText(tempFilePath);
            return readText;
        }




        public dynamic ImportGlobalWorkflow(string project) {
            var tempFilePath = Path.GetTempFileName();
            var xmlFields = @$"
                <FIELD name=""other03"" refname=""vtxrm.jhpField03"" type=""String"">
                    <ALLOWEDVALUES expanditems=""true"">
                        <LISTITEM value=""allow01jhpOk"" />
                        <LISTITEM value=""allow02jhpOk"" />
                        <LISTITEM value=""allow03jhpOk"" />
                    </ALLOWEDVALUES>      
                </FIELD>";
            var xmlContent = @$"<?xml version=""1.0"" encoding=""utf-8""?>
                <GLOBALWORKFLOW>
                   <FIELDS>
                      {xmlFields}
                   </FIELDS>
                </GLOBALWORKFLOW>";
            File.WriteAllText(tempFilePath, xmlContent);

            var witAdminArgs = $" /f:\"{tempFilePath}\"";
            if (!string.IsNullOrEmpty(project))
            {
                witAdminArgs = $" {witAdminArgs} /p:\"{_appOptions.ProjectName}\"";
            }


            var result = ExecuteWitadmin("importglobalworkflow ", $"{witAdminArgs}");
            //var result = ExecuteWitadmin("importglobalworkflow ", $" /f:\"{tempFilePath}\" ");

            return result.Output;
        }




    }
}
