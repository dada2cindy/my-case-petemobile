using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WuDada.Core.Post.Domain;
using WuDada.Core.Member.Domain;

namespace WuDada.Core.Member.Dto
{
    /// <summary>
    /// 會員
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberDto
    {
        #region Constructor

        public MemberDto()
        {
            Status = "0";
            UserConfirm = "0";
            GetCommission = "否";
        }

        public MemberDto(MemberVO memberVO)
        {
            this.MemberId = memberVO.MemberId;
            this.LoginId = memberVO.LoginId;
            this.Password = memberVO.Password;
            this.Name = memberVO.Name;
            this.Company = memberVO.Company;
            this.CompanyNo = memberVO.CompanyNo;
            this.Dept = memberVO.Dept;
            this.JobTitle = memberVO.JobTitle;
            this.Phone = memberVO.Phone;
            this.Mobile = memberVO.Mobile;
            this.Fax = memberVO.Fax;
            this.Email = memberVO.Email;
            this.CreatedDate = memberVO.CreatedDate;
            this.UpdatedDate = memberVO.UpdatedDate;
            this.CreateIP = memberVO.CreateIP;
            this.Sex = memberVO.Sex;
            this.Status = memberVO.Status;
            this.UserConfirm = memberVO.UserConfirm;
            this.Token = memberVO.Token;
            this.ApplyDate = memberVO.ApplyDate;
            this.DueDate = memberVO.DueDate;
            this.Birthday = memberVO.Birthday;
            this.BirthdayYear = memberVO.BirthdayYear;
            this.BirthdayMonth = memberVO.BirthdayMonth;
            this.BirthdayDay = memberVO.BirthdayDay;
            this.Project = memberVO.Project;
            this.Product = memberVO.Product;
            this.PhoneSer = memberVO.PhoneSer;
            this.PhonePrice = memberVO.PhonePrice;
            this.PhoneSellPrice = memberVO.PhoneSellPrice;
            this.Commission = memberVO.Commission;
            this.ReturnCommission = memberVO.ReturnCommission;
            this.BreakMoney = memberVO.BreakMoney;
            this.Compensation = memberVO.Compensation;
            this.ContractMonths = memberVO.ContractMonths;
            this.Sales = memberVO.Sales;
            this.Note = memberVO.Note;
            this.WarrantySuppliers = memberVO.WarrantySuppliers;
            this.MobileWholesalers = memberVO.MobileWholesalers;
            this.ApplyDate2 = memberVO.ApplyDate2;
            this.PID = memberVO.PID;
            this.Store = memberVO.Store;
            this.OnlineWholesalers = memberVO.OnlineWholesalers;
            this.SimNo = memberVO.SimNo;
            this.Project1 = memberVO.Project1;
            this.Project2 = memberVO.Project2;
            this.Project3 = memberVO.Project3;
            this.GetCommission = memberVO.GetCommission;
            this.Prepayment = memberVO.Prepayment;
            this.SelfPrepayment = memberVO.SelfPrepayment;
            this.ServerId = memberVO.ServerId;
            this.NeedUpdate = memberVO.NeedUpdate;
            this.UpdateId = memberVO.UpdateId;
            this.CreatedBy = memberVO.CreatedBy;
            this.UpdatedBy = memberVO.UpdatedBy;
        }

        #endregion

        #region Property

        /// <summary>
        /// PK
        /// </summary>
        [DataMember]
        public virtual int MemberId { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        [DataMember]
        public virtual string LoginId { get; set; }

        /// <summary>
        /// 登入密碼
        /// </summary>
        [DataMember]
        public virtual string Password { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [DataMember]
        public virtual string Company { get; set; }

        /// <summary>
        /// 統一編號
        /// </summary>
        [DataMember]
        public virtual string CompanyNo { get; set; }

        /// <summary>
        /// 部門
        /// </summary>
        [DataMember]
        public virtual string Dept { get; set; }

        /// <summary>
        /// 職稱
        /// </summary>
        [DataMember]
        public virtual string JobTitle { get; set; }

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
        /// 傳真
        /// </summary>
        [DataMember]
        public virtual string Fax { get; set; }

        /// <summary>
        /// 電子郵件
        /// </summary>
        [DataMember]
        public virtual string Email { get; set; }

        /// <summary>
        /// 申請日期
        /// </summary>
        [DataMember]
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [DataMember]
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// 留言者IP
        /// </summary>
        [DataMember]
        public virtual string CreateIP { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 狀態. 0.刪除 1.使用中
        /// </summary>
        [DataMember]
        public virtual string Status { get; set; }

        /// <summary>
        /// 使用者確認狀態. 0.未確認 1.已確認
        /// </summary>
        [DataMember]
        public virtual string UserConfirm { get; set; }

        /// <summary>
        /// UserConfirm用的Token
        /// </summary>
        [DataMember]
        public virtual string Token { get; set; }

        /// <summary>
        /// 開通日期
        /// </summary>
        [DataMember]
        public virtual DateTime? ApplyDate { get; set; }

        /// <summary>
        /// 門號到期日
        /// </summary>
        [DataMember]
        public virtual DateTime? DueDate { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 生日-年
        /// </summary>
        [DataMember]
        public virtual string BirthdayYear { get; set; }

        /// <summary>
        /// 生日-月
        /// </summary>
        [DataMember]
        public virtual string BirthdayMonth { get; set; }

        /// <summary>
        /// 生日-日
        /// </summary>
        [DataMember]
        public virtual string BirthdayDay { get; set; }

        /// <summary>
        /// 申辦專案
        /// </summary>
        [DataMember]
        public virtual string Project { get; set; }

        /// <summary>
        /// 搭配手機
        /// </summary>
        [DataMember]
        public virtual string Product { get; set; }

        /// <summary>
        /// 手機序號
        /// </summary>
        [DataMember]
        public virtual string PhoneSer { get; set; }

        /// <summary>
        /// 手機進價
        /// </summary>
        [DataMember]
        public virtual double PhonePrice { get; set; }

        /// <summary>
        /// 銷售金額
        /// </summary>
        [DataMember]
        public virtual double PhoneSellPrice { get; set; }

        /// <summary>
        /// 門號佣金
        /// </summary>
        [DataMember]
        public virtual double Commission { get; set; }

        /// <summary>
        /// 後退佣金
        /// </summary>
        [DataMember]
        public virtual double ReturnCommission { get; set; }

        /// <summary>
        /// 違約金
        /// </summary>
        [DataMember]
        public virtual double BreakMoney { get; set; }

        /// <summary>
        /// 補償金
        /// </summary>
        [DataMember]
        public virtual double Compensation { get; set; }

        /// <summary>
        /// 綁約月數
        /// </summary>
        [DataMember]
        public virtual double ContractMonths { get; set; }

        /// <summary>
        /// 銷售員
        /// </summary>
        [DataMember]
        public virtual string Sales { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DataMember]
        public virtual string Note { get; set; }

        /// <summary>
        /// 保固商
        /// </summary>
        [DataMember]
        public virtual string WarrantySuppliers { get; set; }

        /// <summary>
        /// 手機盤商
        /// </summary>
        [DataMember]
        public virtual string MobileWholesalers { get; set; }

        /// <summary>
        /// 申辦日期
        /// </summary>
        [DataMember]
        public virtual DateTime? ApplyDate2 { get; set; }

        /// <summary>
        /// 身分證字號
        /// </summary>
        [DataMember]
        public virtual string PID { get; set; }

        /// <summary>
        /// 店家
        /// </summary>
        [DataMember]
        public virtual string Store { get; set; }

        /// <summary>
        /// 上線盤商
        /// </summary>
        [DataMember]
        public virtual string OnlineWholesalers { get; set; }

        /// <summary>
        /// Sim卡卡號
        /// </summary>
        [DataMember]
        public virtual string SimNo { get; set; }

        /// <summary>
        /// 攜碼 新辦 續約
        /// </summary>
        [DataMember]
        public virtual string Project1 { get; set; }

        /// <summary>
        /// 原電信業者
        /// </summary>
        [DataMember]
        public virtual string Project2 { get; set; }

        /// <summary>
        /// 新電信業者
        /// </summary>
        [DataMember]
        public virtual string Project3 { get; set; }

        /// <summary>
        /// 已核發佣金
        /// </summary>
        [DataMember]
        public virtual string GetCommission { get; set; }

        /// <summary>
        /// 預繳金額
        /// </summary>
        [DataMember]
        public virtual double Prepayment { get; set; }

        /// <summary>
        /// 是否幫客戶預繳 無,是,否
        /// </summary>
        [DataMember]
        public virtual string SelfPrepayment { get; set; }

        /// <summary>
        /// 同步到Server後回傳的Id
        /// </summary>
        [DataMember]
        public virtual int ServerId { get; set; }

        /// <summary>
        /// ServerId
        /// </summary>
        [DataMember]
        public virtual bool NeedUpdate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        public virtual string UpdateId { get; set; }

        /// <summary>
        /// 新增的使用者ID
        /// </summary>
        [DataMember]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// 更新的使用者ID
        /// </summary>
        [DataMember]
        public virtual string UpdatedBy { get; set; }

        private double commissionToBoss;
        /// <summary>
        /// 總部抽成
        /// </summary>
        [DataMember]
        public virtual double CommissionToBoss
        {
            get
            {
                if (Commission <= 0 || ReturnCommission < 0)
                {
                    return 0;
                }
                else
                {
                    return (Commission + ReturnCommission) * 0.03;
                }
            }
            set
            {
                commissionToBoss = value;
            }
        }

        private double franchiseesCommission;
        /// <summary>
        /// 本件毛利 (加盟者毛利)
        /// </summary>
        [DataMember]
        public virtual double FranchiseesCommission
        {
            get
            {
                if (Commission <= 0)
                {
                    return 0;
                }
                else
                {
                    return (Commission + ReturnCommission + PhoneSellPrice) - (Prepayment + BreakMoney + PhonePrice + CommissionToBoss);
                }
            }
            set
            {
                franchiseesCommission = value;
            }
        }

        #endregion
    }
}
