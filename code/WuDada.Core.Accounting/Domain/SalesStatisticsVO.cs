using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Accounting.Domain
{
    /// <summary>
    /// 後台角色
    /// </summary>
    [Serializable]
    [DataContract]
    public class SalesStatisticsVO
    {
        #region Constructor

        public SalesStatisticsVO()
        {
        }

        #endregion

        #region Property

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 本月目標
        /// </summary>
        [DataMember]
        public virtual double Target { get; set; }

        /// <summary>
        /// 門號件數
        /// </summary>
        [DataMember]
        public virtual int ApplyCount { get; set; }

        /// <summary>
        /// 門號營收
        /// </summary>
        [DataMember]
        public virtual double ApplyRevenue { get; set; }

        /// <summary>
        /// 門號毛利
        /// </summary>
        [DataMember]
        public virtual double ApplyProfit { get; set; }

        /// <summary>
        /// 配件件數
        /// </summary>
        [DataMember]
        public virtual int FittingCount { get; set; }

        /// <summary>
        /// 配件營收
        /// </summary>
        [DataMember]
        public virtual double FittingRevenue { get; set; }

        /// <summary>
        /// 配件毛利
        /// </summary>
        [DataMember]
        public virtual double FittingProfit { get; set; }
        

        #endregion
    }
}
