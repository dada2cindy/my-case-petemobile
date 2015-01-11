using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    /// <summary>
    /// 訊息
    /// </summary>
    [Serializable]
    [DataContract]
    public class MessageVO
    {
        #region Constructor

        public MessageVO()
        {
            this.QType = "1";
        }

        #endregion

        #region Property

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int MessageId { get; set; }

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
        /// LineID
        /// </summary>
        [DataMember]
        public virtual string LineID { get; set; }

        /// <summary>
        /// 問題類別 1.線上預約
        /// </summary>
        [DataMember]
        public virtual string QType { get; set; }

        /// <summary>
        /// 問題/留言內容
        /// </summary>
        [DataMember]
        public virtual string Content { get; set; }

        /// <summary>
        /// 預約時間
        /// </summary>
        [DataMember]
        public virtual DateTime? ReservationDate { get; set; }

        /// <summary>
        /// 預約時段 上午/下午
        /// </summary>
        [DataMember]
        public virtual string ReservationPeriod { get; set; }

        /// <summary>
        /// 預約時間  
        /// </summary>
        [DataMember]
        public virtual string ReservationTime { get; set; }

        /// <summary>
        /// 取得問題類別文字
        /// </summary>
        public virtual string Str_QType
        {
            get
            {
                string result = string.Empty;

                switch (this.QType)
                {
                    case "1":
                        result = "線上預約";
                        //result = "問題諮詢";
                        break;
                    //case "2":
                    //    result = "商品詢價";
                    //    break;
                    //case "3":
                    //    result = "預約丈量";
                    //    break;
                    //case "4":
                    //    result = "其他問題";
                    //    break;
                }

                return result;
            }
        }

        #endregion
    }
}
