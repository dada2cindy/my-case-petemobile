﻿using System;
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

        public FileVO(FileDto fileDto)
        {
            this.FileId = fileDto.FileId;
            this.FileNo = fileDto.FileNo;
            this.Content1 = fileDto.Content1;
            this.Content2 = fileDto.Content2;
            this.ShowDate = fileDto.ShowDate;
            this.FileName = fileDto.FileName;
            this.FileName2 = fileDto.FileName2;
            this.FileName3 = fileDto.FileName3;
            this.Flag = fileDto.Flag;
            this.Type = fileDto.Type;
            this.UpdateId = fileDto.UpdateId;
            this.ServerId = fileDto.ServerId;
            this.NeedUpdate = fileDto.NeedUpdate;
            this.CreatedBy = fileDto.CreatedBy;
            this.UpdatedBy = fileDto.UpdatedBy;
            this.CreatedDate = fileDto.CreatedDate;
            this.UpdatedDate = fileDto.UpdatedDate;
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

        /// <summary>
        /// 是否同步到Server中
        /// </summary>
        [DataMember]
        public virtual bool IsUpdatingToServer { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        public virtual string UpdateId { get; set; }

        /// <summary>
        /// 同步到Server後回傳的Id
        /// </summary>
        [DataMember]
        public virtual int ServerId { get; set; }

        /// <summary>
        /// 需要同步
        /// </summary>
        [DataMember]
        public virtual bool NeedUpdate { get; set; }

        public virtual string GetStr_Type
        {
            get
            {
                string result = string.Empty;
                switch (this.Type)
                {
                    case "1":
                        result = "門號";
                        break;
                    case "2":
                        result = "進貨";
                        break;
                    case "3":
                        result = "切結書";
                        break;
                }

                return result;
            }
        }

        #endregion
    }
}
