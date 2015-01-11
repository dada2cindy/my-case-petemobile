using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;

/// <summary>
/// WebUtility 的摘要描述
/// </summary>
public class WebUtility
{
    public WebUtility()
    {
    }

    public static string GetMessage(string messageKey)
    {
        return GetMessage(messageKey, "");
    }

    public static string GetMessage(string messageKey, string defaultValue)
    {
        return GetMessage(messageKey, defaultValue, false);
    }

    public static string GetMessage(string messageKey, string defaultValue, bool ignoreTag)
    {
        try
        {
            if (!string.IsNullOrEmpty(messageKey))
            {
                if (ignoreTag)
                {
                    messageKey = Regex.Replace(messageKey,
                      @"\d|\-|\s|\(|\)|\.|\,|\'|\!",
                      string.Empty,
                      RegexOptions.IgnoreCase);
                    messageKey = messageKey.ToLower();
                }
                return messageKey;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return defaultValue;
    }

    public static string SerializeToJSON(Object objectToSerialize)
    {
        return JsonConvert.SerializeObject(objectToSerialize);
    }

    public static object DeserializeAjaxResult(string jsonString, Type type)
    {
        return JsonConvert.DeserializeObject(jsonString, type);
    }

    public static string GetAppSettings(string settingName)
    {
        if (string.IsNullOrEmpty(settingName)) { return null; }
        string result = string.Empty;
        result = ConfigurationManager.AppSettings.Get(settingName);
        if (string.IsNullOrEmpty(result))
        { 
            throw new Exception("Can not get system setting!, setting name is " + settingName); 
        }
        return result;
    }

    public static string encodeQueryCode(string queryString)
    {
        string resultStr = string.Empty;
        string pattern = @"(?<match>\x7E|\x2A|\x27|\x22|\x2B|\x2D|\x21|\x28|\x29|\x5E|\x5B|\x5D|\x7B|\x7D|\x3F|\x7C|\x26|\x3A)";
        string replacement = @"\${match}";

        resultStr = Regex.Replace(queryString, pattern, replacement);

        return resultStr;
    }
    public static string GetIpAddress()
    {
        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
        }
        else
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }

    #region 驗證 Email

    public static bool ValidateEmailAddress(string address)
    {
        string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
            + @"[a-zA-Z]{2,}))$";
        System.Text.RegularExpressions.Regex reStrict = new System.Text.RegularExpressions.Regex(patternStrict);
        return reStrict.IsMatch(address);
    }

    #endregion
}