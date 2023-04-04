using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using FieldType = Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models.FieldType;
using System.Diagnostics;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.Text;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;
using System.Xml.Serialization;
using WorkitemTst.Models;
using System.Reflection.PortableExecutable;
using Microsoft.VisualStudio.Services.Users;
using WorkItemType = WorkitemTst.Models.WorkItemType;
using WorkitemTst.Entitys;
using System.Runtime.Serialization;
using System.Xml;
using WorkitemTst.Facade;
using Microsoft.TeamFoundation.Build.WebApi;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TfsController : ControllerBase
    {


        public const string azCredential = "en25man5qo4t4rjypxcvtc4b6wgrtmnxe2ebs4twlea45vr4xaha";
        public const string urlCollection = "https://vmsys-tfsd02/DefaultCollection";
        public const string projectName = "New VTXRM";
        public const string projectGuid = "4d9b6282-36c4-4570-b8c8-088bdad876ac";
        public const string witadminPath = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\Common7\\IDE\\CommonExtensions\\Microsoft\\TeamFoundation\\Team Explorer";

        private readonly Tfs _tfs;

        public TfsController(
            Tfs tfs 
            )
        {
            _tfs = tfs;
            }

        //public class ProcessOutput
        //{
        //    public string Output { get; set; }
        //    public string Error { get; set; }
        //}


        //public class WorkitemTypeInput
        //{
        //    public string Name { get; set; }
        //    public List<string> Fields { get; set; }

        //    public string Value { get; set; }
        //}


        private async Task<IEnumerable<WorkItem>> GetWorkitemsByIds(IEnumerable<int> ids)
        {
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();
            var workItems = await witClient.GetWorkItemsAsync(null, ids.Select(id => (int)id));
            return workItems;
        }


        //private async Task<T> GetWitClient<T>() where T : VssHttpClientBase {
        //    VssConnection connection = new VssConnection(
        //        new Uri(urlCollection),
        //        new VssBasicCredential("", azCredential)
        //    );

        //    // Get the work item tracking client
        //    T witClient = connection.GetClient<T>();
        //    return witClient;
        //}

        //private ProcessOutput ProcessCommand(string command, string workingDirectory = "")
        //{
        //    Process p = new Process();
        //    p.StartInfo.UseShellExecute = false;
        //    p.StartInfo.RedirectStandardOutput = true;
        //    p.StartInfo.WorkingDirectory = workingDirectory;
        //    p.StartInfo.RedirectStandardError = true;
        //    p.StartInfo.FileName = "cmd.exe";
        //    p.StartInfo.Arguments = $"/C {command}";
        //    p.Start();
        //    string output = p.StandardOutput.ReadToEnd();
        //    string error = p.StandardError.ReadToEnd();
        //    p.WaitForExit();
        //    return new ProcessOutput()
        //    {
        //        Output = output,
        //        Error = error
        //    };
        //}

        //private ProcessOutput ExecuteWitadmin(string operation, string args) {
        //    var command = $"witadmin.exe {operation} /collection:\"{urlCollection}\" /p:\"{projectName}\" {args}";
        //    var workingDirectory = witadminPath;
        //    var result = ProcessCommand(command, workingDirectory);
        //    if (!string.IsNullOrEmpty(result.Error))
        //    {
        //        Console.Error.WriteLine(result.Error);
        //        throw new Exception("Algo ocorreu na chamada witadmin");
        //    }
        //    return result;
        //} 


        /// <summary>
        /// Gets a list of WorkItems
        /// </summary>
        [HttpGet("workitem")]
        public async Task<IEnumerable<WorkItem>> GetWorkitems() {
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();
            Wiql wiql = new Wiql()
            {
                Query = @"Select [System.Id], [System.Title], [System.State] 
                            From WorkItems 
                            Where 
                                [System.WorkItemType] = 'Task'
                                AND [State] = 'Active'
                                AND [Assigned To] = @Me"
            };
            var queryResults = await witClient.QueryByWiqlAsync(wiql);
            var workitems = await GetWorkitemsByIds(queryResults.WorkItems.Select(wi => wi.Id));
            return workitems;
        }


        /// <summary>
        /// Updates a field in a Process Workitem Type (experimental)
        /// </summary>
        [HttpPut("workitem")]
        public async Task<string> UpdateProcessWorkItemType([FromQuery] string fieldname)
        {
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();

            Guid projectId = new Guid(projectGuid);
            string workItemType = "Bug";
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType wit = witClient.GetWorkItemTypeAsync(projectName, workItemType).Result;

            // Define the new field
            WorkItemTypeFieldInstance newField = new WorkItemTypeFieldInstance
            {
                Name = $"new_{fieldname}",
                ReferenceName = $"vtxrm.{fieldname}",
                HelpText = "cenas",
                Field = new WorkItemFieldReference() { 
                    Name= "fieldName",
                    ReferenceName = "ref.fieldname"
                }
            };

            // Add the new field to the work item type
            List<WorkItemTypeFieldInstance> fields = wit.Fields.ToList();
            fields.Add(newField);
            wit.Fields = fields;

            // Update the work item type

            var witProcessClient = await _tfs.GetWitClient<WorkItemTrackingProcessHttpClient>();

            //WorkItemTrackingProcessHttpClient witProcessClient = connection.GetClient<WorkItemTrackingProcessHttpClient>();
            witProcessClient.UpdateProcessWorkItemTypeAsync(new UpdateProcessWorkItemTypeRequest(), projectId, wit.ReferenceName).Wait();


            WorkItemField newField2 = new WorkItemField()
            {
                Name = "vtxrm_custom01",
                ReferenceName = "vtxrm.custom1",
                Type = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.FieldType.String,
            };
            ;
            var newFieldCreated = await witClient.CreateFieldAsync(newField2);


            return "WorkItemType Updated";
        }


        /// <summary>
        /// Gets a list of WorkItem Types of a project
        /// </summary>
        [HttpGet("workitemType")]
        public List<string> GetWorkitemTypeList()
        {
            return _tfs.GetWorkitemTypeList();
            //var result = ExecuteWitadmin("listwitd", "");
            //return result.Output.Replace("\r", "").Split("\n").ToList();
        }


        /// <summary>
        /// Gets the detail of a WorkItem Type
        /// </summary>
        [HttpGet("workitemType/{wit}")]
        public XmlWorkitemType GetWorkitemType(string wit) {
            return _tfs.GetWorkitemType(wit);
            //var result = ExecuteWitadmin("exportwitd", $"/n:\"{wit}\"");

            //var xmlWorkitem = result.Output;
            //XmlWorkitemType workitemType;

            //XmlSerializer serializer = new XmlSerializer(typeof(XmlWorkitemType));
            //using (TextReader reader = new StringReader(xmlWorkitem))
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(XmlWorkitemType));
            //    workitemType = (XmlWorkitemType)serializer.Deserialize(reader);
            //}
            
            //return workitemType;
        }


        /// <summary>
        /// Creates or Updates a WorkItem Type in a project scope
        /// </summary>
        [HttpPost("workitemType/{wit}")]
        public string CreateOrUpdateWorkitemType(string wit, [FromBody] WorkitemTypeInput workitem)
        {
            return this._tfs.CreateOrUpdateWorkitemType(wit, workitem);

            //XmlWorkitemType inputXml = new XmlWorkitemType()
            //{
            //    Version = "1.0",
            //    WorkItemType = new WorkItemType() { 
            //        Name= wit,
            //        Description= $"description for {wit}",
            //        Fields= workitem.Fields.Select( f => new Field() { 
            //            Name = f,
            //            Reference= $"vtxrm.ref{f}",
            //            HelpText = $"help text for {f}, with value: {workitem.Value}",
            //            Type = "String"
            //            } ).ToArray() ,
            //        Form = new Form()
            //        {
            //            LayoutXml = @"
            //                        <Group>
            //                            <Column PercentWidth='75'>
            //                                <Control FieldName='System.Title' Type='FieldControl' Label='&amp;Titulo:' LabelPosition='Left' />
            //                            </Column>
            //                        </Group>
            //                    ",
            //            WebLayoutXml = $@"
            //                    <Page Label=""Details"" LayoutMode=""FirstColumnWide"">
            //                      <Section>
            //                        <Group Label=""Description2 {workitem.Value}"">
            //                          <Control Type=""HtmlFieldControl"" FieldName=""System.Description"" />
            //                        </Group>
            //                      </Section>
            //                    </Page>                                
            //                ",



            //        },
            //        Workflow = new Workflow2() {
            //            States = new List<State>() {
            //                new State() {
            //                    Value = "Active",
            //                },
            //                new State() {
            //                    Value = "New",
            //                }
            //            }.ToArray() ,
            //            Transitions = new List<Transition2>() {
            //                                          new Transition2(){
            //                    From = "",
            //                    To = "New",
            //                    Reasons= new List<Reason>()
            //                    {
            //                        new DefaultReason(){
            //                        Value = "Just Default"
            //                        }

            //                    }.ToArray()

            //                },
            //                new Transition2(){ 
            //                    From = "New",
            //                    To = "Active",
            //                    Reasons= new List<Reason>()
            //                    {
            //                        new ReasonItem() {
            //                            Value = "Just Because"
            //                            //DefaultReason = "Just because"
            //                        },
            //                        new DefaultReason(){
            //                        Value = "Just Default"
            //                        }

            //                    }.ToArray()

            //                }
            //            }.ToArray(),
            //        }
            //    }
            //};
            
            //var tempFilePath = Path.GetTempFileName();
            //using (var writer = new StreamWriter(tempFilePath))
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(XmlWorkitemType));
            //    xs.Serialize(writer, inputXml);
            //}

            //var result = ExecuteWitadmin("importwitd", $"/f:\"{tempFilePath}\"");

            //return result.Output;
        }



        [HttpPost("workitemType/upload")]
        public async Task<string> UploadWit([FromForm] IFormFile file)
        {
            return this._tfs.UploadWorkitemType(file);
        }


        /// <summary>
        /// Deletes a WorkItem Type
        /// </summary>
        [HttpDelete("workitemType/{wit}")]
        public string DeleteWorkitemType(string wit)
        {
            return  this._tfs.DeleteWorkitemType(wit);
            //var result = ExecuteWitadmin("destroywitd", $"/n:\"{wit}\" /noprompt");
            //return result.Output;
        }



        /// <summary>
        /// Creates a new WorkItem
        /// </summary>
        [HttpPost("workitem")]
        public async  Task<WorkItem> CreateWorkitem(  [FromQuery] string title, [FromQuery] string type = "Task")
        {
            Guid projectId = new Guid(projectGuid);
            string witName = type;
            JsonPatchDocument document = new JsonPatchDocument() { 
                new JsonPatchOperation( ){ 
                    From= null,
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Value = title,
                    Path = "/fields/System.Title"
                },
                //new JsonPatchOperation( ){
                //    From= null,
                //    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                //    Value = 30,
                //    Path = "/fields/Microsoft.VSTS.Scheduling.RemainingWork"
                //}
            };
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();
            var newWi = await witClient.CreateWorkItemAsync(document, projectId, witName);
            return newWi;
        }


        [HttpGet("linktype")]
        public async Task<IEnumerable<dynamic>> GetLinkTypes()
        { 
            return _tfs.GetLinkTypes();
        }


        [HttpGet("globallist/list")]
        public async Task<IEnumerable<dynamic>> GetGlobalLists()
        {
            return _tfs.GetGlobalLists();
        }

        [HttpGet("globallists")]
        public async Task<dynamic> ExportGlobalLists()
        {
            return _tfs.ExportGlobalLists();
        }


        [HttpDelete("globallists")]
        public async Task<dynamic> DestroyGlobalLists([FromQuery] string globallistName)
        {
            return _tfs.DestroyGlobalList(globallistName);
        }


        [HttpGet("globalworkflow")]
        public async Task<dynamic> ExportGlobalWorkflow([FromQuery] string? project)
        {
            return _tfs.ExportGlobalWorkflow(project);
        }


        [HttpPost("globalworkflow")]
        public async Task<dynamic> ImportGlobalWorkflow([FromQuery] string? project)
        {
            return _tfs.ImportGlobalWorkflow(project);
        }




    }
}
