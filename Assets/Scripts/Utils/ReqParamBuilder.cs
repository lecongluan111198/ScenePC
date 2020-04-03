using Newtonsoft.Json;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ReqParamBuilder
{
    //private bool containParam = false;
    private string uri;
    private Dictionary<string, object> param;

    public ReqParamBuilder(string uri)
    {
        this.uri = uri;
        param = new Dictionary<string, object>();
    }

    public ReqParamBuilder AddParam(string name, object value)
    {
        param.Add(name, value);
        return this;
    }

    public string build()
    {
        StringBuilder sb = new StringBuilder(uri);
        if (param.Count != 0)
        {
            sb.Append("?");
            foreach (KeyValuePair<string, object> entry in param)
            {
                sb.Append(entry.Key).Append("=").Append(entry.Value).Append("&");
            }
            int length = sb.Length;
            sb.Remove(length - 1, 1);
        }
        return sb.ToString();
    }

    public string toBodyJson()
    {
        return JsonConvert.SerializeObject(param);
    }
}
