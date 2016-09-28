using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class PromoteVO
    {
        #region Constructor

        public PromoteVO()
        {           
        }        

        #endregion

        #region Property

        /// <summary>
        /// 圖檔
        /// </summary>
        [DataMember]
        public virtual string Title { get; set; }

        /// <summary>
        /// 單機價
        /// </summary>
        [DataMember]
        public virtual string SellPrice { get; set; }

        /// <summary>
        /// 中華折扣
        /// </summary>
        [DataMember]
        public virtual string SellPriceW1 { get; set; }

        /// <summary>
        /// 台哥大折扣
        /// </summary>
        [DataMember]
        public virtual string SellPriceW2 { get; set; }

        /// <summary>
        /// 遠傳折扣
        /// </summary>
        [DataMember]
        public virtual string SellPriceW3 { get; set; }

        /// <summary>
        /// 亞太折扣
        /// </summary>
        [DataMember]
        public virtual string SellPriceW4 { get; set; }

        /// <summary>
        /// 台灣之星折扣
        /// </summary>
        [DataMember]
        public virtual string SellPriceW5 { get; set; }

        [DataMember]
        public virtual int SortNo { get; set; }

        /// <summary>
        /// 保固商, 電信 , 品牌
        /// </summary>
        [DataMember]
        public virtual string WarrantySuppliers { get; set; }

        #endregion
    }
}
