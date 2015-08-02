using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Accounting.Domain
{
    /// <summary>
    /// 業績目標
    /// </summary>
    [Serializable]
    [DataContract]
    public class TargetVO
    {
        #region Constructor

        public TargetVO()
        {
        }

        #endregion

        #region Property

        /// <summary>
        /// 使用yyyyMM+Name
        /// </summary>
        [DataMember]
        public virtual string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 本月目標
        /// </summary>
        [DataMember]
        public virtual double Amount { get; set; }        
        

        #endregion
    }
}
