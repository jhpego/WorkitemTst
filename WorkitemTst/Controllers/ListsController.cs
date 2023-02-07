using Microsoft.AspNetCore.Mvc;
using WorkitemTst.Models;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    public class ListsController : Controller
    {


        readonly AppDBContext _appDBContext;
        public ListsController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }


        [HttpGet("relationtypes")]
        public ActionResult<IEnumerable<TypeViewModel>> GetRelationTypes()
        {
            var typeList = Enum.GetValues(typeof(WorkitemRelationKind))
                           .Cast<WorkitemRelationKind>()
                           .Select(t => new TypeViewModel
                           {
                               Id = (int)t,
                               Name = t.ToString()
                           });
            return typeList.ToList();
        }


        [HttpGet("interactions")]
        public ActionResult<IEnumerable<TypeViewModel>> GetInteractions()
        {
            var interactions = _appDBContext.Iteration.Select( iteraction => new TypeViewModel() { 
                Id = (int)iteraction.Id,
                Name = iteraction.Name,
            });
            return interactions.ToList();
        }

    }
}
