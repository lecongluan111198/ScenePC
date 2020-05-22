using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class JSONUtils
{
    private static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

    public static string toJSONString(object obj)
    {
        try
        {
            string json = JsonConvert.SerializeObject(obj, settings);
            return json;
        }
        catch (Exception e)
        {
            return "{}";
        }
    }

    public static T toObject<T>(string json)
    {
        try
        {
            T ret = JsonConvert.DeserializeObject<T>(json, settings);
            return ret;
        }
        catch (Exception e)
        {
            return default(T);
        }

    }
}
