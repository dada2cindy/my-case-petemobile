using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class FileVO : BaseObject
    {
        #region Constructor

        public FileVO()
        {
            this.Flag = 1;
        }

        #endregion

        #region Property

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int FileId { get; set; }

        /// <summary>
        /// 檔案編號
        /// 1.門號: 欄位: 身分證字號,  門號, 日期(自動產生編號 C+YYYYMMDD+門號+身分證)
        /// 2.進貨: 欄位: 日期(自動產生編號 I+YYYYMMDD+001... 002)
        /// 3.切結書: 日期, 身分證(自動產生編號 B+YYYYMMDD+身分證)
        /// </summary>
        [DataMember]
        public virtual string FileNo { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        [DataMember]
        public virtual string Content1 { get; set; }

        /// <summary>
        /// 門號
        /// </summary>
        [DataMember]
        public virtual string Content2 { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public virtual DateTime? ShowDate { get; set; }

        /// <summary>
        /// 檔案1
        /// </summary>
        [DataMember]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 檔案2
        /// </summary>
        [DataMember]
        public virtual string FileName2 { get; set; }

        /// <summary>
        /// 檔案2
        /// </summary>
        [DataMember]
        public virtual string FileName3 { get; set; }

        [DataMember]
        public virtual int Flag { get; set; }

        /// <summary>
        /// 1門號 2.進貨 3.切結書
        /// </summary>
        [DataMember]
        public virtual string Type { get; set; }

        #endregion
    }
}
