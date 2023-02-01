using Microsoft.AspNetCore.Mvc;
using WorkitemTst.Models;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    public class ListsController : Controller
    {


        [HttpGet("relationtypes")]
        public ActionResult<IEnumerable<TypeViewModel>> GetRelationTypes()
        {
            var typeList = Enum.GetValues(typeof(WIRelationTypeEnum))
                           .Cast<WIRelationTypeEnum>()
                           .Select(t => new TypeViewModel
                           {
                               Id = (int)t,
                               Name = t.ToString()
                           });
            return typeList.ToList();
        }

    }
}
