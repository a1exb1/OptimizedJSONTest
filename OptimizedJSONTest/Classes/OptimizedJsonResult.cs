using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptimizedJSONTest
{
    public class OJson
    {

        public static string JsonResult(object data)
        {
            return new JavaScriptSerializer().Serialize(data);
        }

    }
}