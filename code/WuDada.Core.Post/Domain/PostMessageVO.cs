﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    /// <summary>
    /// Post的留言
    /// </summary>
    [Serializable]
    [DataContract]
    public class PostMessageVO
    {
        #region Constructor

        public PostMessageVO()
        {

        }

        #endregion

        #region Property

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int PostMessageId { get; set; }

        /// <summary>
        /// 所屬的Post
        /// </summary>
        [DataMember]
        public virtual PostVO Post { get; set; }

        /// <summary>
        /// 上層的PostMessage
        /// </summary>
        [DataMember]
        public virtual PostMessageVO ParentPostMessage { get; set; }

        /// <summary>
        /// 留言者姓名
        /// </summary>
        [DataMember]
        public virtual string CreateName { get; set; }

        /// <summary>
        /// 留言者IP
        /// </summary>
        [DataMember]
        public virtual string CreateIP { get; set; }

        /// <summary>
        /// 留言時間
        /// </summary>
        [DataMember]
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [DataMember]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        [DataMember]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 傳真IP
        /// </summary>
        [DataMember]
        public virtual string Fax { get; set; }

        /// <summary>
        /// 電子信箱
        /// </summary>
        [DataMember]
        public virtual string EMail { get; set; }

        /// <summary>
        /// 意見內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }        

        #endregion
    }
}
