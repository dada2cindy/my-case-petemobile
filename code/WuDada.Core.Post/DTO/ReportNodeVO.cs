using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.DTO
{
    /// <summary>
    /// 報表用的Node (一個物件必須包入多個的時候用)
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReportNodeVO
    {
        /// <summary>
        /// 第1個Node
        /// </summary>
        [DataMember]
        public virtual NodeVO Node1 { get; set; }

        /// <summary>
        /// 第2個Node
        /// </summary>
        [DataMember]
        public virtual NodeVO Node2 { get; set; }

        ///// <summary>
        ///// 第3個Node
        ///// </summary>
        //[DataMember]
        //public virtual NodeVO Node3 { get; set; }

        ///// <summary>
        ///// 第4個Node
        ///// </summary>
        //[DataMember]
        //public virtual NodeVO Node4 { get; set; }

        ///// <summary>
        ///// 第5個Node
        ///// </summary>
        //[DataMember]
        //public virtual NodeVO Node5 { get; set; }

        ///// <summary>
        ///// 第6個Node
        ///// </summary>
        //[DataMember]
        //public virtual NodeVO Node6 { get; set; }
    }
}
