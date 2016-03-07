using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Accounting.Domain
{
    /// <summary>
    /// 每日結帳統計
    /// </summary>
    [Serializable]
    [DataContract]
    public class CashStatisticsVO
    {
        #region Constructor

        public CashStatisticsVO()
        {
            CashYesterday = 0;
            CashToday = 0;
            TotalToday = 0;
            BuyToday = 0;
            SellToday = 0;
            MobileToday = 0;
            SpecialToday = 0;
        }

        #endregion

        #region Property

        /// <summary>
        /// 結帳日
        /// </summary>
        [DataMember]
        public virtual DateTime CloseDate { get; set; }

        /// <summary>
        /// 昨日餘額
        /// </summary>
        [DataMember]
        public virtual double? CashYesterday { get; set; }

        /// <summary>
        /// 今日結餘
        /// </summary>
        [DataMember]
        public virtual double? CashToday { get; set; }

        /// <summary>
        /// 今日總收支
        /// </summary>
        [DataMember]
        public virtual double? TotalToday { get; set; }

        /// <summary>
        /// 今日進貨收支
        /// </summary>
        [DataMember]
        public virtual double? BuyToday { get; set; }

        /// <summary>
        /// 今日銷貨收支
        /// </summary>
        [DataMember]
        public virtual double? SellToday { get; set; }

        /// <summary>
        /// 今日門號收支
        /// </summary>
        [DataMember]
        public virtual double? MobileToday { get; set; }

        /// <summary>
        /// 今日特別收支
        /// </summary>
        [DataMember]
        public virtual double? SpecialToday { get; set; }
        
        #endregion
    }
}
