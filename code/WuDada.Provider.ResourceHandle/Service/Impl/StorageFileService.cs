using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Provider.ResourceHandle.Persistence;
using WuDada.Provider.ResourceHandle.Helper;
using System.IO;
using WuDada.Provider.ResourceHandle.Domain;
using Common.Logging;

namespace WuDada.Provider.ResourceHandle.Service.Impl
{
    public class StorageFileService : IStorageFileService
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IStorageFileDao StorageFileDao { get; set; }
        public StorageHelper StorageHelper { get; set; }

        #region IStorageFileService 成員

        /// <summary>
        /// 以 FolderType 查詢實體存放資料夾
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <param name="isCreate">是否建立資料夾</param>
        /// <returns>實體存放資料夾(絕對路徑)</returns>
        public DirectoryInfo GetStorageDirectory(FolderType folderType, bool isCreate)
        {
            return StorageHelper.GetStorageDirectory(folderType, isCreate);
        }

        /// <summary>
        /// 新增實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被新增的實體存放資料</param>
        /// <returns>新增後的實體存放資料</returns>
        public StorageFileVO CreateStorageFile(StorageFileVO storageFileVO)
        {
            //檔案搬移到public資料夾
            storageFileVO = StorageHelper.RemoveStorageFile(FolderType.PUBLIC_FOLDER, storageFileVO);
            storageFileVO.IsTemporary = false;

            storageFileVO = StorageFileDao.CreateStorageFile(storageFileVO);
            if (storageFileVO.SortNo == 0)
            {
                storageFileVO.SortNo = storageFileVO.StorageFileId;
                storageFileVO = StorageFileDao.UpdateStorageFile(storageFileVO);
            }
            return storageFileVO;
        }

        /// <summary>
        /// 更新實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被更新的實體存放資料</param>
        /// <returns>更新後的實體存放資料</returns>
        public StorageFileVO UpdateStorageFile(StorageFileVO storageFileVO)
        {
            //檔案搬移到public資料夾
            storageFileVO = StorageHelper.RemoveStorageFile(FolderType.PUBLIC_FOLDER, storageFileVO);
            storageFileVO.IsTemporary = false;

            return StorageFileDao.UpdateStorageFile(storageFileVO);
        }

        /// <summary>
        /// 刪除實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被刪除的實體存放資料</param>
        public void DeleteStorageFile(StorageFileVO storageFileVO)
        {
            StorageHelper.DeleteFile(storageFileVO.CurrentPath);
            StorageFileDao.DeleteStorageFile(storageFileVO);
        }

        /// <summary>
        /// 取得實體存放資料 By 識別碼
        /// </summary>
        /// <param name="storageFileId">識別碼</param>
        /// <returns>實體存放資料</returns>
        public StorageFileVO GetStorageFileById(int storageFileId)
        {
            return StorageFileDao.GetStorageFileById(storageFileId);
        }

        /// <summary>
        /// 取得取得實體存放資料清單
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單</returns>
        public IList<StorageFileVO> GetStorageFileList(StorageFileVO.StorageSourceType storageSourceType, int sourceId)
        {
            return StorageFileDao.GetStorageFileList(storageSourceType, sourceId);
        }

        /// <summary>
        /// 刪除實體存放資料 By SourceId
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        public void DeleteStorageFileBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId)
        {
            IList<StorageFileVO> storageFileList = GetStorageFileList(storageSourceType, sourceId);
            if (storageFileList != null && storageFileList.Count > 0)
            {
                for (int i = 0; i < storageFileList.Count; i++)
                {
                    DeleteStorageFile(storageFileList[i]);
                }
            }
        }

        /// <summary>
        /// 取得取得實體存放資料清單
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>實體存放資料清單</returns>
        public IList<StorageFileVO> GetStorageFileListBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId, int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return StorageFileDao.GetStorageFileListBySourceId(storageSourceType, sourceId, pageIndex, pageSize, sortField, sortDesc);
        }

        /// <summary>
        /// 取得實體存放資料筆數 By SourceId
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單筆數</returns>
        public int CountStorageFileListBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId)
        {
            return StorageFileDao.CountStorageFileListBySourceId(storageSourceType, sourceId);
        }

        #endregion
    }
}
