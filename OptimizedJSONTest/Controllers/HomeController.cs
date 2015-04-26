using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptimizedJSONTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var user = new User();

            var array = new List<object>();

            array.Add(user.ConvertToDictionary());
            array.Add(user.GetAllPropertyValues());
            array.Add(user.GetValuesForKeys(user.GetAllPropertyValues()));

            var str = OJson.JsonResult(array);
            str = OJson.Compress(str);

            return Content(str);
        }
    }
}