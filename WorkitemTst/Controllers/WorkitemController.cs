using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkitemTst.Models;

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
            var wiType = _appDBContext.WorkitemTypes.Where( witype => witype.Id == workitem.TypeId ).FirstOrDefault();
            var newWorkitem = new Workitem() { 
                Title = workitem.Title,
                WIType = wiType
            };
            _appDBContext.Workitems.Add(newWorkitem);
            _appDBContext.SaveChanges();
            return "workitem created";
        }


        [HttpGet("workitem/{id}")]
        public ActionResult<Workitem> GetWorkitem(int id)
        {
            var wi = _appDBContext.Workitems.Include(wi => wi.WIType).Where(wi => wi.Id == id).FirstOrDefault();
            if (wi == null)
            {
                return NotFound();
            }
            return wi;
        }


        [HttpGet("workitems")]
        public ActionResult<IEnumerable<Workitem>> GetWorkitem()
        {
            var wi = _appDBContext.Workitems.Include(wi => wi.WIType).ToList();
            return wi;
        }


        [HttpGet("workitemTypes")]
        public ActionResult<IEnumerable<WIType>> GetWorkitemTypes()
        {
            var witypes = _appDBContext.WorkitemTypes.ToList();
            return witypes;
        }


        [HttpGet("relation/{id}")]
        public ActionResult<IEnumerable<WIRelation>> GetRelations(int id)
        {
            var relations = _appDBContext.WIRelations.Where( rel => rel.WorkitemId == id || rel.TargetWorkitemId == id );
            return relations.ToList();
        }


        [HttpPost("relation/{id}")]
        public ActionResult<string> CreateRelation([FromBody] CreateRelationDTO createRelationDto )
        {

            //validar se é uma relation valida pelo tipo

            var newRelation = new WIRelation()
            {
                WorkitemId = (int)createRelationDto.SourceWorkitemId,
                TargetWorkitemId = (int)createRelationDto.TargetWorkitemId,
                Relation = (WIRelationTypeEnum)createRelationDto.RelationTypeEnumId
            };
            var relations = _appDBContext.Add(newRelation);
            _appDBContext.SaveChanges();
            return "relation created";
        }


    }
}
