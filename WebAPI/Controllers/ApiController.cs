using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        [HttpPost]

        public IActionResult Post(JObject payload)
        {
            try
            {
                string fullname = payload.SelectToken(@"fullname").Value<String>();
                string email = payload.SelectToken(@"email").Value<String>();
                string mobile = payload.SelectToken(@"mobile").Value<String>();
                string birthdate = payload.SelectToken(@"birthdate").Value<String>();
                string gender = payload.SelectToken(@"gender").Value<String>();
                RESTAPI rest = new RESTAPI(fullname, email, mobile, birthdate, gender);
                if (rest.GetErrors().Count() > 0)
                    return BadRequest(new { message = "Errors Found. Please see error list.", errors = rest.GetErrors().ToArray(), status = 400 });
                else
                    return Ok(new { message = "Data saved to MySQL Database", status = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Missing one or more parameters", status = 400, exception = ex.Message });
            }


        }
    }
}
