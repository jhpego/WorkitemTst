using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            #region CREATE WITYPES

            var tskfields = new List<WIField>() {
            new WIField()
                {
                    Order= 0,
                    FieldType = WIFieldType.Text,
                    Name = "campo de texto"
                },
                new WIField()
                {
                    Order= 1,
                    FieldType = WIFieldType.Dropdown,
                    Name = "dropdown field"
                }
            };
            var usFields = new List<WIField>() {
            new WIField()
                {
                    Order= 0,
                    FieldType = WIFieldType.Text,
                    Name = "campo de texto us"
                },
                new WIField()
                {
                    Order= 1,
                    FieldType = WIFieldType.Dropdown,
                    Name = "dropdown field us"
                }
            };
            //var form = new WIForm()
            //{
            //    Fields = fields
            //};
            //var relations = new List<WITypeRelation>();
            var wiType = new WIType()
            {
                InternalCode = "task",
                Name = "Task",
                Relations = new List<WITypeRelation>(),
                Fields = tskfields
            };
            _appDBContext.WorkitemTypes.Add(wiType);
            var wiType2 = new WIType()
            {
                InternalCode = "userStory",
                Name = "UserStory",
                Relations = new List<WITypeRelation>(),
                Fields = usFields
            };
            _appDBContext.WorkitemTypes.Add(wiType2);
            _appDBContext.SaveChanges();

            #endregion CREATE WITYPES


            #region CREATE RELATIONS
            var wiTaskType = _appDBContext.WorkitemTypes.Where(witype => witype.Name == "Task").OrderBy(witype => witype.Id).Last();
            var wiUSType = _appDBContext.WorkitemTypes.Where(witype => witype.Name == "UserStory").OrderBy(witype => witype.Id).Last();

            wiUSType.Relations = new List<WITypeRelation> {
                new WITypeRelation()
                {
                    Relation = WIRelationTypeEnum.Child,
                    TargetWIType= wiTaskType,

                },
                            new WITypeRelation()
                {
                    Relation = WIRelationTypeEnum.Related,
                    TargetWIType= wiUSType,
                            }
            };

            _appDBContext.SaveChanges();

            #endregion CREATE RELATIONS


            #region CREATE WORKITEM

            var wiTaskTypeType = _appDBContext.WorkitemTypes.Where(witype => witype.Name == "Task").Include(witype => witype.Fields).OrderBy(witype => witype.Id).Last();
            var taskFields = wiTaskTypeType.Fields;
            var values = taskFields.Select(field => new WIValue()
            {
                Field = field,
                Value = $"task value {field.Name}"
            });
            var wiTask = new Workitem()
            {
                Title = "task 01",
                WIType = wiTaskTypeType,
                Values = values.ToList(),
            };
            _appDBContext.Workitems.Add(wiTask);

            var wiUSTypeType = _appDBContext.WorkitemTypes.Where(witype => witype.Name == "UserStory").Include(witype => witype.Fields).OrderBy(witype => witype.Id).Last();
            var ustoryFields = wiUSTypeType.Fields;
            var usValues = ustoryFields.Select(field => new WIValue()
            {
                Field = field,
                Value = $"us value {field.Name}"
            });
            var usRelations = new List<WIRelation>() {
            new WIRelation(){
                TargetWorkitem = wiTask,
                Relation = WIRelationTypeEnum.Child
            }
            };
            var wiUstory = new Workitem()
            {
                Title = "UserStory 01",
                WIType = wiUSTypeType,
                Values = usValues.ToList(),
                Relations = usRelations,
            };
            _appDBContext.Workitems.Add(wiUstory);

            _appDBContext.SaveChanges();

            #endregion CREATE WORKITEM


            return "saved Data";
        }


        [HttpGet("types")]
        public IEnumerable<dynamic> GetWITypes()
        {
            var typesList = _appDBContext.WorkitemTypes.Include(type => type.Fields).Include(type => type.Relations).ToList();

            var complete = typesList.Select((type) => new
            {
                TypeId = type.Id,
                TypeName = type.Name,
                Relations = type.Relations.Select(relation => new
                {
                    TypeName = relation.TargetWIType.Name,
                    TypeId = relation.TargetWIType.Id,
                    Relation = Enum.GetName(typeof(WIRelationTypeEnum), relation.Relation)

                })
            });
            return complete.ToList();
        }


        [HttpDelete("data")]
        public string DeleteData()
        {
            _appDBContext.Fields.RemoveRange(_appDBContext.Fields);
            _appDBContext.WIRelations.RemoveRange(_appDBContext.WIRelations);
            _appDBContext.Workitems.RemoveRange(_appDBContext.Workitems);
            _appDBContext.TypeRelations.RemoveRange(_appDBContext.TypeRelations);
            _appDBContext.WorkitemTypes.RemoveRange(_appDBContext.WorkitemTypes);
            _appDBContext.SaveChanges();
            return "deleted";
        }




        [HttpGet("instances")]
        public IEnumerable<Workitem> GetWIInstances()
        {
            var instancesList = _appDBContext.Workitems
                .Include(wi => wi.Values)
                //.Include(wi => wi.Relations)
                .Include(wi => wi.WIType)
                    .ThenInclude(type => type.Fields)
                .ToList();
            return instancesList;
        }

    }
}
