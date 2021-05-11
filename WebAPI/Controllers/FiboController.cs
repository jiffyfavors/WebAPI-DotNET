using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("func/[controller]")]
    public class FiboController : Controller
    {

        public IActionResult Get(int range)
        {


            if (range > 20 || range < 1)
            {
                return BadRequest("Range Error: Please set range between 1 and 20");
                // return new ArgumentOutOfRangeException("Range Error: Please set range between 1 and 20");
            }
            else
            {
                return Ok(Fibo(range));
            }



        }

        private String Fibo(int range)
        {
            int n1 = 0, n2 = 1, next;
            List<string> seq = new List<string>();
            for (int i = 0; i < range; i++)
            {
                seq.Add(n1.ToString());
                next = n1 + n2;
                n1 = n2;
                n2 = next;
            }
            return string.Join<string>(",", seq);
        }
    }
}
