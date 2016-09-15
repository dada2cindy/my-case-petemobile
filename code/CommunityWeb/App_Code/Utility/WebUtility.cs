using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Net;

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

    public void UploadFileToFTP(string filename)
    {
        string ftpServerIP = "salal.arvixe.com/";
        string ftpUserName = "plike";
        //string ftpUserName = "pliketest";
        string ftpPassword = "plike7788";

        FileInfo objFile = new FileInfo(filename);
        FtpWebRequest objFTPRequest;

        // Create FtpWebRequest object 
        objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));

        // Set Credintials
        objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

        // By default KeepAlive is true, where the control connection is 
        // not closed after a command is executed.
        objFTPRequest.KeepAlive = false;

        // Set the data transfer type.
        objFTPRequest.UseBinary = true;

        // Set content length
        objFTPRequest.ContentLength = objFile.Length;

        // Set request method
        objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

        // Set buffer size
        int intBufferLength = 16 * 1024;
        byte[] objBuffer = new byte[intBufferLength];

        // Opens a file to read
        FileStream objFileStream = objFile.OpenRead();

        try
        {
            // Get Stream of the file
            Stream objStream = objFTPRequest.GetRequestStream();

            int len = 0;

            while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
            {
                // Write file Content 
                objStream.Write(objBuffer, 0, len);

            }

            objStream.Close();
            objFileStream.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}