using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkitemTst.Entitys;
using WorkitemTst.Models;
using Transition = WorkitemTst.Entitys.Transition;
using WorkitemType = WorkitemTst.Entitys.WorkitemType;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class WorkitemController : Controller
    {

        readonly AppDBContext _appDBContext;
        public WorkitemController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpPost("workitem")]
        public ActionResult<string> CreateWorkitem([FromBody] WorkitemDTO workitem)
        {
            var wiType = _appDBContext.WorkitemType.Where( witype => witype.Id == workitem.TypeId ).FirstOrDefault();
            var newWorkitem = new Workitem() { 
                Name = workitem.Name,
                WorkitemType = wiType
            };
            _appDBContext.Workitem.Add(newWorkitem);
            _appDBContext.SaveChanges();
            return "workitem created";
        }


        [HttpGet("workitem/{id}")]
        public ActionResult<Workitem> GetWorkitem(int id)
        {
            var wi = _appDBContext.Workitem.Include(wi => wi.WorkitemType).Where(wi => wi.Id == id).FirstOrDefault();
            if (wi == null)
            {
                return NotFound();
            }
            return wi;
        }


        [HttpGet("workitems")]
        public ActionResult<IEnumerable<Workitem>> GetWorkitem()
        {
            var wi = _appDBContext.Workitem
                .Include(wi => wi.Values)
                .Include(wi => wi.WorkitemType)
                .ToList();
            return wi;
        }


        [HttpGet("workitemTypes")]
        public ActionResult<IEnumerable<WorkitemType>> GetWorkitemTypes()
        {
            var witypes = _appDBContext.WorkitemType.ToList();
            return witypes;
        }


        [HttpGet("relation/{id}")]
        public ActionResult<IEnumerable<WorkitemRelation>> GetRelations(int id)
        {
            var relations = _appDBContext.WorkitemRelation.Where( rel => rel.SourceWorkitemId == id || rel.TargetWorkitemId == id );
            return relations.ToList();
        }


        [HttpPost("relation/{id}")]
        public ActionResult<string> CreateRelation([FromBody] CreateRelationDTO createRelationDto )
        {

            //🔨JHJP: validar se é uma relation valida pelo tipo
            if (createRelationDto.SourceWorkitemId == createRelationDto.TargetWorkitemId) {
                throw new Exception("Not possible to create relation between same workitems");
            }

            var sourceType = _appDBContext.Workitem.Where(wi => wi.Id == createRelationDto.SourceWorkitemId).Include(wi => wi.WorkitemType).FirstOrDefault().WorkitemType;
            var targetType = _appDBContext.Workitem.Where(wi => wi.Id == createRelationDto.TargetWorkitemId).Include(wi => wi.WorkitemType).FirstOrDefault().WorkitemType;

            var validRelation = _appDBContext.WorkitemTypeRelation.Where(rel =>
                rel.SourceWorkitemTypeId == sourceType.Id &&
                rel.TargetWorkitemTypeId == targetType.Id &&
                (int)rel.RelationMode == createRelationDto.TypeRelationId
                ).FirstOrDefault();
            if (validRelation == null) {
                throw new Exception("Not a valid relation between workitem types");
            };


            var newRelation = new WorkitemRelation()
            {
                SourceWorkitemId = createRelationDto.SourceWorkitemId,
                TargetWorkitemId = createRelationDto.TargetWorkitemId,
                WorkitemTypeRelationId = validRelation.Id,
            };
            var relations = _appDBContext.Add(newRelation);
            _appDBContext.SaveChanges();
            return "relation created";
        }



        [HttpGet("status/{id}")]
        public ActionResult<IEnumerable<TypeViewModel>> GetNextStatus(int id)
        {
            var transitions = GetValidTransitions(id);
            var optionList = transitions.Select(trs => new TypeViewModel()
            {
                Id = trs.NextStatus.Id,
                Name= trs.NextStatus.Name,
            });
            return optionList.ToList();
        }


        [HttpPost("status/{id}")]
        public ActionResult<string> SetStatus(int id, [FromQuery] int statusId)
        {
            var workItem = _appDBContext.Workitem.Include(wi => wi.WorkitemType).FirstOrDefault();
            var transitions = GetValidTransitions(id);
            if (!transitions.Any(trs => trs.NextStatusId == statusId)) {
                throw new Exception("Not a valid transition for this status");
            }
            var nextStatus = _appDBContext.Status.Where(st => st.Id == statusId).FirstOrDefault();
            workItem.Status = nextStatus;
            _appDBContext.SaveChanges();
            return "status changed";
        }


        private IEnumerable<Transition> GetValidTransitions(int workitemId) {
            var workItem = _appDBContext.Workitem
                .Include(wi => wi.WorkitemType)
                .Where(wi => wi.Id == workitemId)
                .FirstOrDefault();
            var currStatus = workItem.StatusId;
            var workitemType = workItem?.WorkitemType;
            var workflowStatus = _appDBContext.Status.Where(st => st.WorkflowId == workitemType.WorkflowId).Select(st => st.Id);
            var transitions = _appDBContext.Transition
                .Include(trs => trs.NextStatus)
                .Where(trs => workflowStatus.Contains(trs.NextStatusId));
            transitions = transitions.Where(trs => trs.InitialStatusId == currStatus);
            return transitions.AsEnumerable();
        }
    }
}
