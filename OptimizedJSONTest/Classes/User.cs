using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OptimizedJSONTest
{
    public class User : JsonObject
    {
        public string Name = "Alex";
        public int Age = 15;
        public DateTime DateOfBirth = DateTime.Now;
    }

    public class JsonObject
    {

        public Dictionary<string, object> ConvertToDictionary()
        {
            return this.GetType()
            .GetProperties()
            .Select(pi => new { Name = pi.Name, Value = pi.GetValue(this, null) })
            .Union(
                this.GetType()
                .GetFields()
                .Select(fi => new { Name = fi.Name, Value = fi.GetValue(this) })
             )
            .ToDictionary(ks => ks.Name, vs => vs.Value);
        }

        public List<string> GetAllPropertyValues()
        {
            var rc = new List<string>();
            foreach (var key in ConvertToDictionary().Keys)
            {
                rc.Add(key);
            }
            return rc;
        }

        public List<object> GetValuesForKeys(List<string> keys)
        {
            List<object> rc = new List<object>();
            foreach (var key in keys)
            {
                rc.Add(GetPropValue(key));
            }
            return rc;
        }

        public object GetPropValue(string propertyName)
        {
            var item = ConvertToDictionary()[propertyName];

            if (item.GetType() == typeof(DateTime))
            {
                var date = (DateTime)item;
                item = date.ToShortDateString();
            }

            return item;
        }
    }
}