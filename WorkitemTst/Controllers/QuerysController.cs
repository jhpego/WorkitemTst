using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WorkitemTst.Entitys;
using WorkitemTst.Models;

namespace WorkitemTst.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class QuerysController : Controller
    {

        readonly AppDBContext _appDBContext;
        public QuerysController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        /// <summary>
        /// Uses Database JSON Functions to query Dynamic Data
        /// </summary>
        [HttpGet("json")]
        public ActionResult<IEnumerable<dynamic>> QueryJson([FromQuery] string field, [FromQuery] string content, [FromQuery] bool desc = false)
        {
            var jsonValues = _appDBContext.WorkitemValue
                .Where(v => AppDBContext.IsJson(v.Value) == 1) //campo é JSON válido
                .Where(v => !string.IsNullOrEmpty(AppDBContext.JsonValue(v.Value, $"$.{field}")) ); //campo tem propriedade

            var filtered = jsonValues
                .Where(v => AppDBContext.JsonValue(v.Value, $"$.{field}").Contains($"{content}"))
                ;

            var ordered = desc 
                ? filtered.OrderByDescending(wv => AppDBContext.JsonValue(wv.Value, $"$.{field}"))
                : filtered.OrderBy(wv => AppDBContext.JsonValue(wv.Value, $"$.{field}"));

            return ordered
                .Select(v =>
                    //new
                    //{
                    //    Task = AppDBContext.JsonValue(v.Value, $"$.{field}")
                    //}
                    new Dictionary<string, object>() {
                        { $"{field}", AppDBContext.JsonValue(v.Value, $"$.{field}") }
                    }
                )
                .ToList();
        }




        //public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
        //                  bool desc)
        //{
        //    string command = desc ? "OrderByDescending" : "OrderBy";
        //    var type = typeof(TEntity);
        //    var property = type.GetProperty(orderByProperty);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        //    var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
        //                                  source.Expression, Expression.Quote(orderByExpression));
        //    return source.Provider.CreateQuery<TEntity>(resultExpression);
        //}


    }

    public class ValueDTO {
        public int Id { get; set; }
        public JObject Value { get; set; }
    } 
}
