using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using WuDada.Provider.ResourceHandle.Domain;
using NHibernate.Criterion;

namespace WuDada.Provider.ResourceHandle.Persistence
{
    public class StorageFileDao : AdoDao, IStorageFileDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        #region IStorageFileDao 成員

        /// <summary>
        /// 新增實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被新增的實體存放資料</param>
        /// <returns>新增後的實體存放資料</returns>
        public StorageFileVO CreateStorageFile(StorageFileVO storageFileVO)
        {
            NHibernateDao.Insert(storageFileVO);

            return storageFileVO;
        }

        /// <summary>
        /// 更新實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被更新的實體存放資料</param>
        /// <returns>更新後的實體存放資料</returns>
        public StorageFileVO UpdateStorageFile(StorageFileVO storageFileVO)
        {
            NHibernateDao.Update(storageFileVO);

            return storageFileVO;
        }

        /// <summary>
        /// 刪除實體存放資料
        /// </summary>
        /// <param name="storageFileVO">被刪除的實體存放資料</param>
        public void DeleteStorageFile(StorageFileVO storageFileVO)
        {
            NHibernateDao.Delete(storageFileVO);
        }

        /// <summary>
        /// 取得實體存放資料 By 識別碼
        /// </summary>
        /// <param name="storageFileId">識別碼</param>
        /// <returns>實體存放資料</returns>
        public StorageFileVO GetStorageFileById(int storageFileId)
        {
            return NHibernateDao.GetVOById<StorageFileVO>(storageFileId);
        }

        /// <summary>
        /// 取得取得實體存放資料清單
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單</returns>
        public IList<StorageFileVO> GetStorageFileList(StorageFileVO.StorageSourceType storageSourceType, int sourceId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<StorageFileVO>();
            dCriteria.Add(Expression.Eq("SourceType", (int)storageSourceType));
            dCriteria.Add(Expression.Eq("SourceId", sourceId));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<StorageFileVO>(dCriteria);
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
            DetachedCriteria dCriteria = DetachedCriteria.For<StorageFileVO>();
            dCriteria.Add(Expression.Eq("SourceType", (int)storageSourceType));
            dCriteria.Add(Expression.Eq("SourceId", sourceId));

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortDesc)
                {
                    dCriteria.AddOrder(Order.Desc(sortField));
                }
                else
                {
                    dCriteria.AddOrder(Order.Asc(sortField));
                }
            }

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<StorageFileVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得實體存放資料筆數 By SourceId
        /// </summary>
        /// <param name="storageSourceType">檔案來源類別</param>
        /// <param name="sourceId">來源項目的Id</param>
        /// <returns>實體存放資料清單筆數</returns>
        public int CountStorageFileListBySourceId(StorageFileVO.StorageSourceType storageSourceType, int sourceId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<StorageFileVO>();
            dCriteria.Add(Expression.Eq("SourceType", (int)storageSourceType));
            dCriteria.Add(Expression.Eq("SourceId", sourceId));

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        #endregion
    }
}
