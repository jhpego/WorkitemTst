using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkitemTst.Entitys;
using WorkitemTst.Models;

namespace WorkitemTst.Controllers
{
    public class ManagementController : Controller
    {

        readonly AppDBContext _appDBContext;
        public ManagementController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


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


        [HttpGet("types")]
        public IEnumerable<dynamic> GetWorkitemTypes()
        {
            var typesList = _appDBContext.WorkitemType.Include(type => type.Fields)
                //.Include(type => type.Relations)
                .ToList();

            var complete = typesList.Select((type) => new
            {
                TypeId = type.Id,
                TypeName = type.Name,
                //Relations = type.Relations.Select(relation => new
                //{
                //    TypeName = relation.TargetWIType.Name,
                //    TypeId = relation.TargetWIType.Id,
                //    Relation = Enum.GetName(typeof(WorkitemRelationKind), relation.Relation)

                //})
            });
            return complete.ToList();
        }


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




        [HttpGet("instances")]
        public IEnumerable<Workitem> GetWorkitemInstances()
        {
            var instancesList = _appDBContext.Workitem
                .Include(wi => wi.Values)
                //.Include(wi => wi.Relations)
                .Include(wi => wi.WorkitemType)
                    .ThenInclude(type => type.Fields)
                .ToList();
            return instancesList;
        }

    }
}
