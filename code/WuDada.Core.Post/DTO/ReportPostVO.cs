using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.DTO
{
    /// <summary>
    /// 報表用的Post (一個物件必須包入多個的時候用)
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReportPostVO
    {
        /// <summary>
        /// 第1個Post
        /// </summary>
        [DataMember]
        public virtual PostVO Post1 { get; set; }

        /// <summary>
        /// 第2個Post
        /// </summary>
        [DataMember]
        public virtual PostVO Post2 { get; set; }

        ///// <summary>
        ///// 第3個Post
        ///// </summary>
        //[DataMember]
        //public virtual PostVO Post3 { get; set; }

        ///// <summary>
        ///// 第4個Post
        ///// </summary>
        //[DataMember]
        //public virtual PostVO Post4 { get; set; }

        ///// <summary>
        ///// 第5個Post
        ///// </summary>
        //[DataMember]
        //public virtual PostVO Post5 { get; set; }

        ///// <summary>
        ///// 第6個Post
        ///// </summary>
        //[DataMember]
        //public virtual PostVO Post6 { get; set; }
    }
}
