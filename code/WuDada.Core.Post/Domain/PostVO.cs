using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WuDada.Core.Post.Domain
{
    [Serializable]
    [DataContract]
    public class PostVO : BasePost, IPageTagObject
    {
        #region Constructor

        public PostVO()
        {
            Flag = 1;
            SortNo = 0;
            CloseDate = DateTime.MaxValue;
        }

        #endregion

        #region Property

        /// <summary>
        /// Pkey
        /// </summary>
        [DataMember]
        public virtual int PostId { get; set; }

        /// <summary>
        /// 屬於的Node
        /// </summary>
        [DataMember]
        public virtual NodeVO Node { get; set; }

        /// <summary>
        /// 上層的Post
        /// </summary>
        [DataMember]
        public virtual PostVO ParentPost { get; set; }

        /// <summary>
        /// 圖檔
        /// </summary>
        [DataMember]
        public virtual string PicFileName { get; set; }

        /// <summary>
        /// 圖檔2
        /// </summary>
        [DataMember]
        public virtual string PicFileName2 { get; set; }

        ///// <summary>
        ///// 圖檔英文
        ///// </summary>
        //[DataMember]
        //public virtual string PicFileNameENG { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [DataMember]
        public virtual string DocFileName { get; set; }

        //[DataMember]
        //public virtual string StoreName { get; set; }

        //[DataMember]
        //public virtual string Phone { get; set; }

        //[DataMember]
        //public virtual string Fax { get; set; }

        //[DataMember]
        //public virtual string Address { get; set; }

        //[DataMember]
        //public virtual string GoogleMap { get; set; }

        [DataMember]
        public virtual int SortNo { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [DataMember]
        public virtual int Flag { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [DataMember]
        public virtual int Quantity { get; set; }

        /// <summary>
        /// 上架日/進貨日
        /// </summary>
        [DataMember]
        public virtual DateTime? ShowDate { get; set; }

        /// <summary>
        /// 下架日/銷貨日
        /// </summary>
        [DataMember]
        public virtual DateTime? CloseDate { get; set; }

        /// <summary>
        /// 0.站內/庫存 1.連結/售出
        /// </summary>
        [DataMember]
        public virtual int Type { get; set; }

        /// <summary>
        /// 連結網址
        /// </summary>
        [DataMember]
        public virtual string LinkUrl { get; set; }

        ///// <summary>
        ///// 內容2(右邊)
        ///// </summary>
        //[DataMember]
        //public virtual string HtmlContent2 { get; set; }

        ///// <summary>
        ///// 內容2英文(右邊)
        ///// </summary>
        //[DataMember]
        //public virtual string HtmlContent2ENG { get; set; }

        /// <summary>
        /// PageTitle
        /// </summary>
        [DataMember]
        public virtual string PageTitle { get; set; }

        /// <summary>
        /// KeyWord
        /// </summary>
        [DataMember]
        public virtual string PageKeyWord { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DataMember]
        public virtual string PageDescription { get; set; }

        /// <summary>
        /// 是否為推薦的
        /// </summary>
        [DataMember]
        public virtual bool IsRecommend { get; set; }

        /// <summary>
        /// 價格/進貨價
        /// </summary>
        [DataMember]
        public virtual double? Price { get; set; }

        /// <summary>
        /// 價格/售價
        /// </summary>
        [DataMember]
        public virtual double? SellPrice { get; set; }

        /// <summary>
        /// 是否為暫存
        /// </summary>
        [DataMember]
        public virtual bool IsTemp { get; set; }

        /// <summary>
        /// 自定欄位1
        /// </summary>
        [DataMember]
        public virtual string CustomField1 { get; set; }

        /// <summary>
        /// 自定欄位2
        /// </summary>
        [DataMember]
        public virtual string CustomField2 { get; set; }

        /// <summary>
        /// 客戶姓名
        /// </summary>
        [DataMember]
        public virtual string MemberName { get; set; }

        /// <summary>
        /// 客戶電話
        /// </summary>
        [DataMember]
        public virtual string MemberPhone { get; set; }

        /// <summary>
        /// 商品序號
        /// </summary>
        [DataMember]
        public virtual string ProductSer { get; set; }

        /// <summary>
        /// 保固商
        /// </summary>
        [DataMember]
        public virtual string WarrantySuppliers { get; set; }

        /// <summary>
        /// 進貨盤商
        /// </summary>
        [DataMember]
        public virtual string Wholesalers { get; set; }

        /// <summary>
        /// 會員Id
        /// </summary>
        [DataMember]
        public virtual string MemberId { get; set; }

        #endregion

        /// <summary>
        /// 取文字_上下架
        /// </summary>
        [DataMember]
        public virtual string GetStr_Flag
        {
            get
            {
                string result = "";

                if (Flag == 0)
                {
                    result = "否";
                }
                else if (Flag == 1)
                {
                    result = "是";
                }

                return result;
            }
        }

        /// <summary>
        /// 取文字_庫存出售
        /// </summary>
        [DataMember]
        public virtual string GetStr_Type
        {
            get
            {
                string result = "";

                if (Type == 0)
                {
                    result = "庫存";
                }
                else if (Type == 1)
                {
                    result = "售出";
                }

                return result;
            }
        }

        /// <summary>
        /// 取文字_名稱+上下架
        /// </summary>
        [DataMember]
        public virtual string GetStr_Name_And_Flag
        {
            get
            {
                string result = Title;

                if (Flag == 0 || !(DateTime.Today >= ShowDate && DateTime.Today <= CloseDate))
                {
                    result += "(下架)";
                }
                //else if (Flag == 1)
                //{
                //    result = "是";
                //}

                return result;
            }
        }        
    }
}
