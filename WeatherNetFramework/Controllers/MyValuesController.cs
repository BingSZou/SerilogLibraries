using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeatherNetFramework.Controllers
{
    public class MyValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("api/MyValues")]
        public IEnumerable<string> GetMyValues()
        {
            throw new Exception("The id isn't found");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        
        public void Delete(int id)
        {
            throw new Exception("The id isn't found");
        }
    }
}
