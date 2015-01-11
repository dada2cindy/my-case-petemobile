using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Provider.ResourceHandle.Domain
{
    /// <summary>
    /// 實體存放資料
    /// </summary>
    [Serializable]
    [DataContract]
    public class StorageFileVO : BaseObject
    {
        #region Constructor
        /// <summary>
        /// 建構式
        /// </summary>
        public StorageFileVO()
        {
            IsTemporary = true;
        }
        #endregion

        #region Property
        /// <summary>
        /// 檔案儲存代碼
        /// </summary>
        [DataMember]
        public virtual int StorageFileId { get; set; }

        /// <summary>
        /// 顯示名稱
        /// </summary>
        [DataMember]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [DataMember]
        public virtual string HtmlContent { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        [DataMember]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 是否為暫存檔案
        /// </summary>
        [DataMember]
        public virtual bool IsTemporary { get; set; }

        /// <summary>
        /// 來源路徑
        /// </summary>
        [DataMember]
        public virtual string SourceUri { get; set; }

        /// <summary>
        /// 目前路徑
        /// </summary>
        [DataMember]
        public virtual string CurrentPath { get; set; }

        /// <summary>
        /// 檔案大小
        /// </summary>
        [DataMember]
        public virtual long FileSize { get; set; }

        /// <summary>
        /// 檔案來源類別
        /// </summary>
        [DataMember]
        public virtual StorageSourceType SourceType { get; set; }

        /// <summary>
        /// 來源項目的Id Ex：PostId...
        /// </summary>
        [DataMember]
        public virtual int SourceId { get; set; }

        /// <summary>
        /// 是否為封面
        /// </summary>
        [DataMember]
        public virtual bool IsCover { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public virtual int SortNo { get; set; }

        #endregion

        /// <summary>
        /// 檔案來源類別
        /// </summary>
        public enum StorageSourceType
        {
            None = 0,
            Post = 1
        }
        
    }
}
