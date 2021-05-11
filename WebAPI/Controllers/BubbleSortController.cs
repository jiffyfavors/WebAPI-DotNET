using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("sort/[controller]")]
    public class BubbleSortController : Controller
    {

        [HttpPost]
        public JsonResult Post(JObject payload)
        {
            int[] numbers = JTokenToArray<int>(payload.Value<JToken>("params"));
            BubbleSort sort = new BubbleSort(numbers.ToArray());
            return Json(
                new
                {
                    original = sort.getOriginal(),
                    sorted = sort.getSortedValue(),
                    largest = sort.getLargest(),
                    median = sort.getMedian()

                });

        }
        private static T[] JTokenToArray<T>(JToken jToken)
        {
            List<T> ret = new List<T>();
            foreach (JToken jItem in jToken)
                ret.Add(jItem.Value<T>());
            return ret.ToArray();
        }
    }


}
