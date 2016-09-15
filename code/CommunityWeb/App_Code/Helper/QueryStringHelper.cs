using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// QueryStringHelper 的摘要描述
/// </summary>
public class QueryStringHelper
{
    public QueryStringHelper()
    {
    }

    public static string GetString(string paraName, string defaultValue)
    {
        //HttpContext.Current.Request.QueryString[paraName] 有時會取到字串的 "null"，所以多做一個判斷
        if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[paraName]) && !("null").Equals(HttpContext.Current.Request.QueryString[paraName]))
        {
            defaultValue = HttpContext.Current.Request.QueryString[paraName].Trim();
        }
        else
        {
            if (HttpContext.Current.Request.Form[paraName] != null)
            {
                defaultValue = HttpContext.Current.Request.Form[paraName].Trim();
            }
        }
        return defaultValue;
    }

    public static string GetString(string paraName)
    {
        return GetString(paraName, string.Empty);
    }

    public static bool GetBoolean(string paraName)
    {
        bool result = false;
        if (!Boolean.TryParse(GetString(paraName), out result))
        {
            result = false;
        }
        return result;
    }

    public static int GetInteger(string paraName, int defaultValue)
    {
        int paraValue = defaultValue;
        string s = GetString(paraName);
        if (!int.TryParse(s, out paraValue))
        {
            paraValue = defaultValue;
        }
        return paraValue;
    }

    public static string GetAjaxCommand()
    {
        return GetString("cmd").ToLower();
    }
}