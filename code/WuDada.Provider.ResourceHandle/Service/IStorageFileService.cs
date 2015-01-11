using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WuDada.Provider.ResourceHandle.Domain;

namespace WuDada.Provider.ResourceHandle.Service
{
    public interface IStorageFileService
    {
        /// <summary>
        /// 以 FolderType 查詢實體存放資料夾
        /// </summary>
        /// <param name="folderType">資料夾類型</param>
        /// <param name="isCreate">是否建立資料夾</param>
        /// <returns>實體存放資料夾(絕對路徑)</returns>
        DirectoryInfo GetStorageDirectory(FolderType folderType, bool isCreate);

        /// <summary>
        /// 新增實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被新增的實體存放資料</param>
        /// <returns>新增後的實體存放資料</returns>
        StorageFileVO CreateStorageFile(StorageFileVO storageFileVO);

        /// <summary>
        /// 更新實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被更新的實體存放資料</param>
        /// <returns>更新後的實體存放資料</returns>
        StorageFileVO UpdateStorageFile(StorageFileVO storageFileVO);

        /// <summary>
        /// 刪除實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被刪除的實體存放資料</param>
        void DeleteStorageFile(StorageFileVO storageFileVO);

        /// <summary>
        /// 取得實體存放資料 By 識別碼
        /// </summary>
        /// <param name="storageFileId">識別碼</param>
        /// <returns>實體存放資料</returns>
        StorageFileVO GetStorageFileById(int storageFileId);

        /// <summary>
        /// 取得取得實體存放資料清單
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單</returns>
        IList<StorageFileVO> GetStorageFileList(StorageFileVO.StorageSourceType storageSourceType, int sourceId);

        /// <summary>
        /// 刪除實體存放資料 By SourceId
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        void DeleteStorageFileBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId);

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
        IList<StorageFileVO> GetStorageFileListBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得實體存放資料筆數 By SourceId
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單筆數</returns>
        int CountStorageFileListBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId);
    }
}
