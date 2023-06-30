using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using WorkitemTst.Facade;
using WorkitemTst.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;





// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureAPIController : ControllerBase
    {

        readonly AppDBContext _appDBContext;
        private readonly Tfs _tfs;
        private readonly AppOptions _appOptions;

        public AzureAPIController(AppDBContext appDBContext, Tfs tfs, AppOptions appOptions)
        {
            _appDBContext = appDBContext;
            _tfs = tfs;
            _appOptions = appOptions;   
        }



        #region PRIVATE

        private async Task<IEnumerable<WorkItem>> GetWorkitemsByIds(IEnumerable<int> ids)
        {
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();
            var workItems = await witClient.GetWorkItemsAsync(null, ids.Select(id => (int)id));
            return workItems;
        }

        #endregion PRIVATE







        /// <summary>
        /// Querys a list of Workitems (active, task, @Me)
        /// </summary>
        [HttpGet("workitem")]
        public async Task<IEnumerable<WorkItem>> GetWorkitems()
        {
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

            Guid projectId = new Guid(_appOptions.ProjectGuid);
            string workItemType = "Bug";
            Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemType wit = witClient.GetWorkItemTypeAsync(_appOptions.ProjectName, workItemType).Result;

            // Define the new field
            WorkItemTypeFieldInstance newField = new WorkItemTypeFieldInstance
            {
                Name = $"new_{fieldname}",
                ReferenceName = $"vtxrm.{fieldname}",
                HelpText = "cenas",
                Field = new WorkItemFieldReference()
                {
                    Name = "fieldName",
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
        /// Gets the list of workitem Types of one project
        /// </summary>
        [HttpGet("workitemType")]
        public async Task<dynamic> GetWorkitemtypes()
        {
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();

            Guid projectId = new Guid(_appOptions.ProjectGuid);

            var workitemTypes = witClient.GetWorkItemTypesAsync(projectId).Result;

            return workitemTypes.Select(wiT => new { 
                Name = wiT.Name,
                ReferenceName = wiT.ReferenceName,
                Description = wiT.Description,
                Url = wiT.Url,
                Form = wiT.XmlForm,
            });
        }



        /// <summary>
        /// Creates a new WorkItem Task, with provided title
        /// </summary>
        [HttpPost("workitem")]
        public async Task<WorkItem> CreateWorkitem([FromQuery] string title, [FromQuery] string type = "Task")
        {
            Guid projectId = new Guid(_appOptions.ProjectGuid);
            string witName = type;
            JsonPatchDocument document = new JsonPatchDocument() {
                new JsonPatchOperation( ){
                    From= null,
                    Operation = Microsoft.VisualStudio.Services.WebApi.Patch.Operation.Add,
                    Value = title,
                    Path = "/fields/System.Title"
                },
            };
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();
            var newWi = await witClient.CreateWorkItemAsync(document, projectId, witName);
            return newWi;
        }




    }
}
