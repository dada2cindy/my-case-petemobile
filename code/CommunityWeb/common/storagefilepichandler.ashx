<%@ WebHandler Language="C#" Class="storagefilepichandler" %>

using System;
using System.Web;
using System.IO;
using WuDada.Provider.ResourceHandle;
using WuDada.Provider.ResourceHandle.Service;
using WuDada.Provider.ResourceHandle.Domain;

public class storagefilepichandler : IHttpHandler
{
    public void ProcessRequest (HttpContext context) {

        StorageFactory storageFactory = new StorageFactory();
        IStorageFileService storageFileService = storageFactory.GetStorageFileService();

        int fileId = QueryStringHelper.GetInteger("fileid", 0);

        StorageFileVO storageFileVO = storageFileService.GetStorageFileById(fileId);
        if (storageFileVO != null)
        {
            FileInfo fileInfo = new FileInfo(storageFileVO.CurrentPath);

            context.Response.Clear();
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                int buffersize = int.Parse(fs.Length.ToString());
                byte[] buffer = new byte[buffersize];
                int count = fs.Read(buffer, 0, buffersize);

                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileInfo.Name, System.Text.Encoding.UTF8));
                context.Response.ContentType = "image/jpeg";

                context.Response.OutputStream.Write(buffer, 0, buffersize);
                context.Response.Flush();
                fs.Dispose();
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}