<%@ WebHandler Language="C#" Class="download" %>

using System;
using System.Web;
using Common.Logging;
using Spring.Context;
using Spring.Context.Support;
using System.IO;

public class download : IHttpHandler {

    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //MySessionHelper mySessionHelper = new MySessionHelper();
    SessionHelper sessionHelper = new SessionHelper();
    private static readonly string LOG_FILE_PATH = "~/App_Data/Log/Log.txt";
    private static readonly string COPYED_LOG_FILE = "LogFile.txt";
    //string[] VERIFY_DOMAIN = new string[] { "http://localhost/", ConfigHelper.Host };

    public void ProcessRequest(HttpContext context)
    {
        ////判斷是否是允許來訪網域
        string urlFrom = context.Request.Headers["Referer"];
        //bool isVerifyOK = false;
        //if (!string.IsNullOrEmpty(urlFrom))
        //{
        //    foreach (string value in VERIFY_DOMAIN)
        //    {
        //        if (urlFrom.StartsWith(value))
        //        {
        //            isVerifyOK = true;
        //            break;
        //        }
        //    }
        //}

        //if (!isVerifyOK)
        //{
        //    context.Response.Write("沒有網頁權限！");
        //    return;
        //}

        string mode = context.Request.QueryString["mode"];
        string id = context.Request.QueryString["id"];
        string fileName = "";

        //檔案位置
        string filePath = "";
        if ("log".Equals(mode)) //下載log檔
        {
            filePath = context.Server.MapPath(LOG_FILE_PATH);

            //判斷檔案是否存在，若不存在則結束
            if (!File.Exists(filePath))
            {
                context.Response.Write("檔案不存在");
                return;
            }
            string copyedFile = Path.GetFullPath(filePath) + COPYED_LOG_FILE;
            File.Copy(filePath, copyedFile, true);

            //告知瀏覽器讀取之檔案格式，[application/msword]為[MICROSOFT WORD document]
            context.Response.ContentType = "application/txt";
            //編碼方式採用 UTF8
            context.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(copyedFile, System.Text.Encoding.UTF8));
            context.Response.WriteFile(copyedFile);
            context.Response.End();
        }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}