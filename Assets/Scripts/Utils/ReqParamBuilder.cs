using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ReqParamBuilder
{
    private StringBuilder sb;
    private bool containParam = false;

    public ReqParamBuilder(string uri)
    {
        sb = new StringBuilder(uri);
    }

    public ReqParamBuilder AddParam(string name, object value)
    {
        if (!containParam)
        {
            sb.Append("?");
            containParam = true;
        }
        else
        {
            sb.Append("&");
        }
        sb.Append(name).Append("=").Append(value);
        return this;
    }

    public string build()
    {
        return sb.ToString();
    }
}
