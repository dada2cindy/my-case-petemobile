<%@ WebHandler Language="C#" Class="swfupload" %>

using System;
using System.Web;
using Common.Logging;
using System.IO;
using WuDada.Provider.ResourceHandle;
using WuDada.Provider.ResourceHandle.Service;
using WuDada.Provider.ResourceHandle.Domain;

public class swfupload : IHttpHandler
{
    ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    
    public void ProcessRequest (HttpContext context) {

        StorageFactory storageFactory = new StorageFactory();
        IStorageFileService m_StorageFileService = storageFactory.GetStorageFileService();
        int sourceId = QueryStringHelper.GetInteger("source", 0);
        string type = QueryStringHelper.GetString("type");
        //m_Log.Debug("sourceId = " + sourceId);
        //m_Log.Debug("type = " + type);
        DirectoryInfo dir = m_StorageFileService.GetStorageDirectory(FolderType.UPLOAD_FOLDER, true);

        HttpFileCollection hfc = HttpContext.Current.Request.Files;
        for (int i = 0; i < hfc.Count; i++)
        {
            HttpPostedFile hpf = hfc[i];
            if (hpf.ContentLength > 0)
            {
                
                string sourceFileName = System.IO.Path.GetFileName(hpf.FileName);
                string sourceFilePath = Path.Combine(dir.FullName, sourceFileName);
                hpf.SaveAs(sourceFilePath);
                FileInfo sourceFileInfo = new FileInfo(sourceFilePath);
                string newFileName = Guid.NewGuid().ToString() + sourceFileInfo.Extension;
                string destFileName = Path.Combine(dir.FullName, newFileName);
                File.Copy(sourceFilePath, destFileName);

                StorageFileVO storageFileVO = new StorageFileVO();
                storageFileVO.SourceUri = sourceFileInfo.FullName;
                storageFileVO.CurrentPath = destFileName;
                storageFileVO.FileName = sourceFileInfo.Name;
                storageFileVO.DisplayName = sourceFileInfo.Name.Substring(0, sourceFileInfo.Name.LastIndexOf('.'));
                storageFileVO.FileSize = sourceFileInfo.Length;
                switch (type)
                {
                    case "post":
                        storageFileVO.SourceType = StorageFileVO.StorageSourceType.Post;
                        break;
                }
                storageFileVO.SourceId = sourceId;
                storageFileVO = m_StorageFileService.CreateStorageFile(storageFileVO);
                //m_Log.Debug("上傳檔案 Id = " + storageFileVO.StorageFileId);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}