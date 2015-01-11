using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Spring.Collections;
using WuDada.Provider.ResourceHandle.Domain;
using System.IO;
using Common.Logging;

namespace WuDada.Provider.ResourceHandle.Helper
{
    public class StorageHelper
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDictionary storageSettings = new SynchronizedHashtable();

        #region Property

        /// <summary>
        /// Sets the StorageSettings.
        /// </summary>
        /// <value>StorageSettings.</value>
        public IDictionary StorageSettings
        {
            set { storageSettings = value; }
        }
        #endregion

        #region Constructor
        private StorageHelper()
        { }
        #endregion

        #region Storage Path
        /// <summary>
        /// 以 FolderType 查詢實體存放資料夾
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <returns>實體存放資料夾(絕對路徑)</returns>
        public DirectoryInfo GetStorageDirectory(FolderType folderType)
        {
            return GetStorageDirectory(folderType, false);
        }

        /// <summary>
        /// 以 FolderType 查詢實體存放資料夾
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <param name="isCreate">是否建立資料夾</param>
        /// <returns>實體存放資料夾(絕對路徑)</returns>
        public DirectoryInfo GetStorageDirectory(FolderType folderType, bool isCreate)
        {
            string rootPath = GetStoragePath(FolderType.ROOT);
            string storagePath = GetStoragePath(folderType);
            m_Log.Debug("rootPath = " + rootPath);
            m_Log.Debug("storagePath = " + storagePath);
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(rootPath, storagePath));

            if (isCreate && !dir.Exists)
            {
                dir.Create();
            }

            return dir;
        }

        /// <summary>
        /// 以 FolderType 取得實體存放路徑
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <returns>實體存放路徑 (相對路徑)</returns>
        public string GetStoragePath(FolderType folderType)
        {
            IDictionary<FolderType, string> storagePaths = GetStoragePaths();
            string storagePath = storagePaths[folderType];
            return storagePath;
        }

        /// <summary>
        /// 從 config 檔中取得所有 Storage path 並以 FolderType 為 Key 製成 Dictionary，並依據使用者組織傳回對應路徑清單
        /// </summary>
        /// <param name="actor">使用者</param>
        /// <returns>路徑清單</returns>
        /// <remarks>
        /// RootPath 若 config 中設定相對路徑，會轉換成絕對路徑。
        /// </remarks>
        public IDictionary<FolderType, string> GetStoragePaths()
        {
            IDictionary<FolderType, string> storagePath = new Dictionary<FolderType, string>();

            string rootPath = (string)storageSettings["Root"];
            string publicFolder = (string)storageSettings["Public"];
            string temporaryFolder = (string)storageSettings["Temporary"];
            string uploadFolder = (string)storageSettings["Upload"];

            if (string.IsNullOrEmpty(rootPath))
            {
                rootPath = AppDomain.CurrentDomain.BaseDirectory;
                rootPath = Path.Combine(rootPath, "upload");
            }
            storagePath.Add(FolderType.ROOT, (new DirectoryInfo(Path.Combine(rootPath, rootPath))).FullName);

            if (string.IsNullOrEmpty(publicFolder))
            {
                publicFolder = @"storage\public";
            }
            storagePath.Add(FolderType.PUBLIC_FOLDER, publicFolder);

            if (string.IsNullOrEmpty(temporaryFolder))
            {
                temporaryFolder = @"temp";
            }
            storagePath.Add(FolderType.TEMPORARY_FOLDER, temporaryFolder);

            if (string.IsNullOrEmpty(uploadFolder))
            {
                uploadFolder = @"temp\upload";
            }
            storagePath.Add(FolderType.UPLOAD_FOLDER, uploadFolder);

            return storagePath;
        }

        #endregion

        #region File
        /// <summary>
        /// 搬移實體檔案
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <param name="storageFileVO">實體存放資料</param>
        /// <returns>搬移後的實體存放資料</returns>
        public StorageFileVO RemoveStorageFile(FolderType folderType, StorageFileVO storageFileVO)
        {
            DirectoryInfo dir = GetStorageDirectory(folderType, true);

            if (File.Exists(storageFileVO.CurrentPath))
            {
                string newFileName = Guid.NewGuid().ToString() + new FileInfo(storageFileVO.CurrentPath).Extension;
                string destFileName = Path.Combine(dir.FullName, newFileName);
                m_Log.Debug("============搬移實體檔案============");
                m_Log.Debug("CurrentPath = " + storageFileVO.CurrentPath);
                m_Log.Debug("destFileName = " + destFileName);
                File.Copy(storageFileVO.CurrentPath, destFileName);
                DeleteFile(storageFileVO.CurrentPath);
                storageFileVO.CurrentPath = destFileName;
            }

            return storageFileVO;
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="path">檔案實體路徑</param>
        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                m_Log.Debug("============刪除實體檔案============");
                m_Log.Debug("path = " + path);
                File.Delete(path);
            }
        }
        #endregion
    }
}
