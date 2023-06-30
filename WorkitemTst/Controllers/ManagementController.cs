using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using WorkitemTst.Entitys;
using WorkitemTst.Facade;
using WorkitemTst.Models;
using Transition = WorkitemTst.Entitys.Transition;
using Workflow = WorkitemTst.Entitys.Workflow;
using System.Linq;
using Microsoft.VisualStudio.Services.Common;
using System.Security.Policy;
using Microsoft.Extensions.Options;

namespace WorkitemTst.Controllers
{
    public class ManagementController : Controller
    {

        readonly AppDBContext _appDBContext;
        private readonly Tfs _tfs;
        private readonly AppOptions _appOptions;

        public ManagementController(
            AppDBContext appDBContext,
            Tfs tfs,
            AppOptions appOptions
            )
        {
            _appDBContext = appDBContext;
            this._tfs = tfs;
            _appOptions = appOptions;
        }

        /// <summary>
        /// Creates Initial instances in Database
        /// </summary>
        [HttpPost("data")]
        public string CreateData()
        {

            #region INTERACTION

            var mainInteraction = new Iteration() {
                Name = "2022",
                Active = false,
                StartDate = new DateTime(2023, 1, 1, 0, 0, 0),
                EndDate = new DateTime(2023, 12, 31, 0, 0, 0),
            };

            var childInteractions = new List<Iteration>() {
                 new Iteration(){
                    Name = "January",
                    Active= false,
                    StartDate = new DateTime(2023, 1, 1, 0, 0, 0),
                    EndDate= new DateTime(2023, 1, 31, 0, 0, 0),
                    Parent = mainInteraction
                },
                new Iteration(){
                    Name = "February",
                    Active= true,
                    StartDate = new DateTime(2023, 2, 1, 0, 0, 0),
                    EndDate= new DateTime(2023, 2, 28, 0, 0, 0),
                    Parent = mainInteraction
                }
            };

            _appDBContext.Iteration.Add(mainInteraction);
            _appDBContext.Iteration.AddRange(childInteractions);
            _appDBContext.SaveChanges();

            #endregion INTERACTION


            #region WORKFLOW


            var wfUserStory = new Workflow()
            {
                Name = "WorkflowUserStory"
            };

            var US_statusNew = new Status() {
                Code = "NEW",
                Name = "New",
                Workflow = wfUserStory
            };

            var US_statusAnalysis = new Status()
            {
                Code = "ANALISYS",
                Name = "Analysis",
                Workflow = wfUserStory
            };

            var US_statusActive = new Status()
            {
                Code = "ACTIVE",
                Name = "Active",
                Workflow = wfUserStory
            };

            var US_statusClosed = new Status()
            {
                Code = "CLOSED",
                Name = "Closed",
                Workflow = wfUserStory
            };

            var US_transitions = new List<Transition>() {
                new Transition(){
                Name = "initial",
                InitialStatus = null,
                NextStatus= US_statusNew,
                },
                new Transition(){
                Name = "new2analysis",
                InitialStatus = US_statusNew,
                NextStatus= US_statusAnalysis,
                },
                new Transition(){
                Name = "analysis2active",
                InitialStatus = US_statusAnalysis,
                NextStatus= US_statusActive,
                },
                new Transition(){
                Name = "active2closed",
                InitialStatus = US_statusActive,
                NextStatus= US_statusClosed,
                },
            };

            _appDBContext.Transition.AddRange(US_transitions);

            var wfTask = new Workflow()
            {
                Name = "WorkflowTask"
            };


            var TSK_statusNew = new Status()
            {
                Code = "NEW",
                Name = "New",
                Workflow = wfTask
            };

            var TSK_statusPending = new Status()
            {
                Code = "PENDING",
                Name = "Pending",
                Workflow = wfTask
            };

            var TSK_statusActive = new Status()
            {
                Code = "ACTIVE",
                Name = "Active",
                Workflow = wfTask
            };

            var TSK_statusClosed = new Status()
            {
                Code = "CLOSED",
                Name = "Closed",
                Workflow = wfTask
            };

            var TSK_transitions = new List<Transition>() {
                new Transition(){
                Name = "initial",
                InitialStatus = null,
                NextStatus= TSK_statusNew,
                },
                new Transition(){
                Name = "new2aactive",
                InitialStatus = TSK_statusNew,
                NextStatus= TSK_statusActive,
                },
                new Transition(){
                Name = "active2pending",
                InitialStatus = TSK_statusActive,
                NextStatus= TSK_statusPending,
                },
                new Transition(){
                Name = "pending2active",
                InitialStatus = TSK_statusPending,
                NextStatus= TSK_statusActive,
                },
                new Transition(){
                Name = "active2closed",
                InitialStatus = TSK_statusActive,
                NextStatus= TSK_statusClosed,
                },
            };

            _appDBContext.Transition.AddRange(TSK_transitions);

            _appDBContext.SaveChanges();

            #endregion WORKFLOW


            #region CREATE WITYPES

            var tskfields = new List<WorkitemField>() {
            new WorkitemField()
                {
                    Order= 0,
                    FieldMode = WorkitemFieldKind.Text,
                    Name = "campo de texto"
                },
                new WorkitemField()
                {
                    Order= 1,
                    FieldMode = WorkitemFieldKind.Dropdown,
                    Name = "dropdown field"
                }
            };
            var usFields = new List<WorkitemField>() {
            new WorkitemField()
                {
                    Order= 0,
                    FieldMode = WorkitemFieldKind.Text,
                    Name = "campo de texto us"
                },
                new WorkitemField()
                {
                    Order= 1,
                    FieldMode = WorkitemFieldKind.Dropdown,
                    Name = "dropdown field us"
                }
            };
            var wiType = new WorkitemType()
            {
                Name = "Task",
                //Relations = new List<WITypeRelation>(),
                Fields = tskfields,
                Workflow = wfTask,
            };
            _appDBContext.WorkitemType.Add(wiType);
            var wiType2 = new WorkitemType()
            {
                Name = "UserStory",
                //Relations = new List<WITypeRelation>(),
                Fields = usFields,
                Workflow = wfUserStory,
                
            };
            _appDBContext.WorkitemType.Add(wiType2);
            _appDBContext.SaveChanges();

            #endregion CREATE WITYPES


            #region CREATE RELATIONS
            var wiTaskType = _appDBContext.WorkitemType.Where(witype => witype.Name == "Task").OrderBy(witype => witype.Id).Last();
            var wiUSType = _appDBContext.WorkitemType.Where(witype => witype.Name == "UserStory").OrderBy(witype => witype.Id).Last();

            var wiUSTypeRelations = new List<WorkitemTypeRelation> {
                new WorkitemTypeRelation()
                {
                    SourcetWorkitemType = wiTaskType,
                    RelationMode = WorkitemRelationKind.Parent,
                    TargetWorkitemType= wiUSType ,

                },
                new WorkitemTypeRelation()
                {
                    SourcetWorkitemType = wiUSType,
                    RelationMode = WorkitemRelationKind.Related,
                    TargetWorkitemType= wiUSType,
                }
            };
            _appDBContext.WorkitemTypeRelation.AddRange(wiUSTypeRelations);

            _appDBContext.SaveChanges();

            #endregion CREATE RELATIONS


            #region CREATE WORKITEM

            var wiTaskTypeType = _appDBContext.WorkitemType.Where(witype => witype.Name == "Task").Include(witype => witype.Fields).OrderBy(witype => witype.Id).Last();
            var taskFields = wiTaskTypeType.Fields;
            var values = taskFields.Select(field => new WorkitemValue()
            {
                Field = field,
                Value = $"task value {field.Name}"
            });
            var wiTask = new Workitem()
            {
                Name = "task 01",
                WorkitemType = wiTaskTypeType,
                Values = values.ToList(),
            };
            _appDBContext.Workitem.Add(wiTask);

            var wiUSTypeType = _appDBContext.WorkitemType.Where(witype => witype.Name == "UserStory").Include(witype => witype.Fields).OrderBy(witype => witype.Id).Last();
            var ustoryFields = wiUSTypeType.Fields;
            var usValues = ustoryFields.Select(field => new WorkitemValue()
            {
                Field = field,
                Value = $"us value {field.Name}"
            });
            var wiUstory = new Workitem()
            {
                Name = "UserStory 01",
                WorkitemType = wiUSTypeType,
                Values = usValues.ToList(),
                //Relations = usRelations,
            };

            var usRelations = new List<WorkitemRelation>() {
            new WorkitemRelation(){
                SourceWorkitem = wiTask,
                TargetWorkitem = wiUstory,
                Relation = wiUSTypeRelations.Where( rel => rel.RelationMode == WorkitemRelationKind.Parent ).FirstOrDefault()
            }
            };

            _appDBContext.Workitem.Add(wiUstory);
            _appDBContext.WorkitemRelation.AddRange(usRelations);
            _appDBContext.SaveChanges();

            #endregion CREATE WORKITEM


            return "saved Data";
        }



        /// <summary>
        /// Deletes all data instances on Database
        /// </summary>
        [HttpDelete("data")]
        public string DeleteData()
        {
            _appDBContext.Fields.RemoveRange(_appDBContext.Fields);
            _appDBContext.WorkitemRelation.RemoveRange(_appDBContext.WorkitemRelation);
            _appDBContext.Workitem.RemoveRange(_appDBContext.Workitem);
            _appDBContext.WorkitemTypeRelation.RemoveRange(_appDBContext.WorkitemTypeRelation);
            _appDBContext.WorkitemType.RemoveRange(_appDBContext.WorkitemType);
            _appDBContext.SaveChanges();
            return "deleted";
        }


        /// <summary>
        /// Syncs WorkitemTypes between Database and Azure (listwitd)
        /// </summary>
        [HttpGet("syncwit")]
        public IEnumerable<string> SyncWorkitemTypes()
        {
            var output = new List<string>();
            var listOfInsightWorkitemTypes = _appDBContext.SimpleWit.ToList();

            var listOfAzureWorkitemTypes = _tfs.GetWorkitemTypeList();
            foreach (var type in listOfAzureWorkitemTypes) { 
            
                var typeDetail =  _tfs.GetWorkitemType(type);
                var hashString = typeDetail.GetHashCode().ToString();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlWorkitemType));
                var xmlContent = "";

                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xmlSerializer.Serialize(writer, typeDetail);
                        xmlContent = sww.ToString(); // Your XML
                    }
                }

   


                if (hashString != "0")
                {
                    var foundInsightWorkitem = listOfInsightWorkitemTypes.Where(wi =>
                        typeDetail.WorkItemType.Name == wi.Name
                        && wi.Hash == hashString
                    ).FirstOrDefault();
                    if (foundInsightWorkitem == null)
                    {
                        _appDBContext.SimpleWit.Add(
                            new SimpleWit()
                            {
                                Name = typeDetail.WorkItemType.Name, //internalCode
                                Hash = hashString,
                                Content = xmlContent
                            }
                            );
                        output.Add(typeDetail.WorkItemType.Name);
                    }
                    else
                    {
                        if (foundInsightWorkitem.Hash != hashString)
                        {
                            foundInsightWorkitem = new SimpleWit()
                            {
                                Name = typeDetail.WorkItemType.Name,
                                Hash = hashString,
                                Content = xmlContent
                            };
                            output.Add(typeDetail.WorkItemType.Name);
                        }
                    }

                }
            }

            // detectar as que foram apagadas

            _appDBContext.SaveChanges();


            
            return output;
        }


        /// <summary>
        /// Syncs WorkitemTypes between Database and Azure (listwitd) - 2nd approach
        /// </summary>
        [HttpGet("syncwit2")]
        public dynamic SyncWorkitemTypes2()
        {

            var itemsWitInsight = _appDBContext.SimpleWit.ToList();

            //var itemsWitAzure = _tfs.GetWorkitemTypeList().Select(witName =>
            //{
            //    var witDetail = _tfs.GetWorkitemType(witName);
            //    return new SimpleWit()
            //    {
            //        Name = witDetail.WorkItemType.Name,
            //        Content = XmlWorkitemType.ToXmlString(witDetail),
            //        Hash = witDetail.GenerateHash().ToString(),
            //    };
            //}).ToList();

            var witList = _tfs.GetWorkitemTypeList();

            List<SimpleWit> itemsWitAzureNew = new List<SimpleWit>();


            ParallelOptions options = new()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            ParallelLoopResult result =  Parallel.ForEach(witList, options, witName =>
            {
                var witDetail = _tfs.GetWorkitemType(witName);
                var newItem = new SimpleWit()
                {
                    Name = witDetail.WorkItemType.Name,
                    Content = XmlWorkitemType.ToXmlString(witDetail),
                    Hash = witDetail.GenerateHash(),
                };
                itemsWitAzureNew.Add(newItem);
            });

            var itemsAdded = itemsWitAzureNew.Except(itemsWitInsight);              //items que existem no azure, mas que ainda não existem no insight
            var itemsRemoved = itemsWitInsight.Except(itemsWitAzureNew);            //items que existem no insight, mas que já não existem no azure
            var itemsCommonInsight = itemsWitInsight.Intersect(itemsWitAzureNew);   //items que existem no insight e no azure
            var azureDict = itemsWitAzureNew.Intersect(itemsWitInsight)             //hash de nomes de wits do azure, para posterior acesso rápido
                    .ToDictionary(item => item.Name, item => item);
            
            var itemsChanged = itemsCommonInsight                                   //items que foram alterados, com base nas propriedades hash, novas e anteriores
                .Where(currItem => azureDict[currItem.Name].Hash != currItem.Hash)
                .ToList();
            
            foreach (var cItem in itemsChanged)
            {
                var azureItem = azureDict[cItem.Name];
                cItem.Hash = azureItem.Hash;
                cItem.Content = azureItem.Content;
            }

            _appDBContext.SimpleWit.AddRange(itemsAdded);
            _appDBContext.SimpleWit.RemoveRange(itemsRemoved);

            _appDBContext.SaveChanges();
             
            return new
            {
                Added = itemsAdded.Select( el => el.Name ),
                Removed = itemsRemoved.Select(el => el.Name),
                Changed = itemsChanged.Select(el => el.Name),
            };

        }



        /// <summary>
        /// Migrates Workitems between Projects
        /// </summary>
        [HttpGet("migratewi")]
        public async Task<IEnumerable<string>> MigrateWorkitems()
        {
            var output = new List<string>();
            var witClient = await _tfs.GetWitClient<WorkItemTrackingHttpClient>();

            var sourceProjectId = _appOptions.ProjectGuidNewVtxrm;
            var targetProjectId = _appOptions.ProjectGuidWits2;


            var latestRevisions = await witClient.ReadReportingRevisionsGetAsync(sourceProjectId, null, null, null, null, null, null, null, true );
            var workitemsList = latestRevisions.Values;
            _appDBContext.SimpleWi.AddRange(
                workitemsList.Select( wi => new SimpleWi() { 
                    Name = wi.Fields.Where( f => f.Key == "System.Title" )?.FirstOrDefault().Value?.ToString() ?? "defaultTitle",
                    Content = JsonSerializer.Serialize(wi),
                    Hash = wi.Id.ToString(),
                })
                );
            _appDBContext.SaveChanges();


            var listDoNotUpdate = new List<string>() {
            "System.BoardColumn","System.BoardColumnDone", "System.State"
            };

            foreach (  var workitem in workitemsList )
            {
                WorkItem wiToClone = witClient.GetWorkItemAsync((int)workitem.Id).Result;

                var migrateDate = DateTime.Now;

                var patchOperations = wiToClone.Fields
                    .Where( f => !listDoNotUpdate.Contains(f.Key))
                    .Select(f => new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = $"/fields/{f.Key}",
                            Value = f.Value
                            //Value = f.Key == "System.Title" ? $" {f.Value} (migrated in {DateTime.Now})" : f.Value 
                        }) ; 


                JsonPatchDocument patchDocument = new JsonPatchDocument();
                patchDocument.AddRange(patchOperations);

                string title = workitem.Fields.Where(f => f.Key == "System.Title").FirstOrDefault().Value.ToString();
                string workitemType = workitem.Fields.Where( f => f.Key == "System.WorkItemType").FirstOrDefault().Value.ToString();

                try
                {
                    //if (workitemType == "Feature")
                    //{

                        var res = await witClient.CreateWorkItemAsync(patchDocument, targetProjectId, workitemType, null, true);

                    var updPatchDocument = new JsonPatchDocument() { 
                        new JsonPatchOperation(){
                                Operation = Operation.Replace,
                                Path = $"/fields/System.Title",
                                Value = $"{wiToClone.Fields["System.Title"]} (migrated in {DateTime.Now})"
                        }
                    };

                        var res2 = await witClient.UpdateWorkItemAsync(updPatchDocument, (int)res.Id, null, true);
                    //}

                }
                catch (Exception ex)
                {

                    throw;
                }
                
            };

            //witClient.CreateWorkItemBatchRequest(projectIdServicesTests)

            output = workitemsList.Select(wi => wi.Id.ToString()).ToList();
            return output;
        }


    }
}
