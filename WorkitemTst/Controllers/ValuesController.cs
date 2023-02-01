//using System.Data.Entity;
//using Microsoft.AspNetCore.Mvc;
//using WorkitemTst.Models;





//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WorkitemTst.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {

//        readonly AppDBContext _appDBContext;
//        public ValuesController(AppDBContext appDBContext)
//        {
//           _appDBContext= appDBContext;
//        }

//        // GET: api/<ValuesController>
//        [HttpGet]
//        public IEnumerable<Sample> Get()
//        {


//            return _appDBContext.SampleSet.ToList();
//            //return _appDBContext.SampleSet.;
//            //return new string[] { "value1", "value2" };
//        }

//        // GET api/<ValuesController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<ValuesController>
//        [HttpPost]
//        public void Post([FromBody] Sample value)
//        {
//            _appDBContext.SampleSet.Add(value);
//            _appDBContext.SaveChanges();
//        }

//        // PUT api/<ValuesController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<ValuesController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }




//    }
//}
