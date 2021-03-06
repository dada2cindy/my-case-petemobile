﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WuDada.Core.Post.Domain;
using WuDada.Core.Member.Dto;

namespace WuDada.Core.Member.Domain
{
    /// <summary>
    /// 會員
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemberVO
    {
        #region Constructor

        public MemberVO()
        {
            Status = "0";
            UserConfirm = "0";
            GetCommission = "否";
            NeedUpdate = true;
        }

        public MemberVO(MemberDto memberDto)
        {
            this.MemberId = memberDto.MemberId;
            this.LoginId = memberDto.LoginId;
            this.Password = memberDto.Password;
            this.Name = memberDto.Name;
            this.Company = memberDto.Company;
            this.CompanyNo = memberDto.CompanyNo;
            this.Dept = memberDto.Dept;
            this.JobTitle = memberDto.JobTitle;
            this.Phone = memberDto.Phone;
            this.Mobile = memberDto.Mobile;
            this.Fax = memberDto.Fax;
            this.Email = memberDto.Email;
            this.CreatedDate = memberDto.CreatedDate;
            this.UpdatedDate = memberDto.UpdatedDate;
            this.CreateIP = memberDto.CreateIP;
            this.Sex = memberDto.Sex;
            this.Status = memberDto.Status;
            this.UserConfirm = memberDto.UserConfirm;
            this.Token = memberDto.Token;
            this.ApplyDate = memberDto.ApplyDate;
            this.DueDate = memberDto.DueDate;
            this.Birthday = memberDto.Birthday;
            this.BirthdayYear = memberDto.BirthdayYear;
            this.BirthdayMonth = memberDto.BirthdayMonth;
            this.BirthdayDay = memberDto.BirthdayDay;
            this.Project = memberDto.Project;
            this.Product = memberDto.Product;
            this.PhoneSer = memberDto.PhoneSer;
            this.PhonePrice = memberDto.PhonePrice;
            this.PhoneSellPrice = memberDto.PhoneSellPrice;
            this.Commission = memberDto.Commission;
            this.ReturnCommission = memberDto.ReturnCommission;
            this.BreakMoney = memberDto.BreakMoney;
            this.Compensation = memberDto.Compensation;
            this.ContractMonths = memberDto.ContractMonths;
            this.Sales = memberDto.Sales;
            this.Note = memberDto.Note;
            this.WarrantySuppliers = memberDto.WarrantySuppliers;
            this.MobileWholesalers = memberDto.MobileWholesalers;
            this.ApplyDate2 = memberDto.ApplyDate2;
            this.PID = memberDto.PID;
            this.Store = memberDto.Store;
            this.OnlineWholesalers = memberDto.OnlineWholesalers;
            this.SimNo = memberDto.SimNo;
            this.Project1 = memberDto.Project1;
            this.Project2 = memberDto.Project2;
            this.Project3 = memberDto.Project3;
            this.GetCommission = memberDto.GetCommission;
            this.Prepayment = memberDto.Prepayment;
            this.SelfPrepayment = memberDto.SelfPrepayment;
            this.ServerId = memberDto.ServerId;
            this.NeedUpdate = memberDto.NeedUpdate;
            this.UpdateId = memberDto.UpdateId;
            this.CreatedBy = memberDto.CreatedBy;
            this.UpdatedBy = memberDto.UpdatedBy;
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
        /// 關聯的庫存
        /// </summary>
        [DataMember]
        public virtual IList<PostVO> Posts { get; set; }

        /// <summary>
        /// 同步到Server後回傳的Id
        /// </summary>
        [DataMember]
        public virtual int ServerId { get; set; }

        /// <summary>
        /// 需要同步
        /// </summary>
        [DataMember]
        public virtual bool NeedUpdate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        public virtual string UpdateId { get; set; }

        /// <summary>
        /// 是否同步到Server中
        /// </summary>
        [DataMember]
        public virtual bool IsUpdatingToServer { get; set; }

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
        /// 品讚抽成
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

        /// <summary>
        /// 是否有關聯的庫存
        /// </summary>
        [DataMember]
        public virtual string GetStr_HasPost
        {
            get
            {
                string result = "";

                if (Posts != null && Posts.Count > 0)
                {
                    result = "是";
                }
                else
                {
                    result = "否";
                }

                return result;
            }
        }

        public virtual string GetStr_Status
        {
            get
            {
                string result = string.Empty;
                switch (this.Status)
                {
                    case "0":
                        result = "刪除";
                        break;
                    case "1":
                        result = "使用中";
                        break;
                }

                return result;
            }
        }

        public virtual string GetStr_Project
        {
            get
            {
                string result = Project;
                switch (this.Project1)
                {
                    case "續約":
                        result = string.Format("{0}, {1}, {2}", Project1, Project2, Project);
                        break;
                    case "新辦":
                        result = "使用中";
                        result = string.Format("{0}, {1}, {2}", Project1, Project3, Project);
                        break;
                    case "攜碼":
                        result = string.Format("{0}, {1}->{2}, {3}", Project1, Project2, Project3, Project);
                        break;
                }

                return result;
            }
        }

        public virtual string GetStr_UserConfirm
        {
            get
            {
                string result = string.Empty;
                switch (this.UserConfirm)
                {
                    case "0":
                        result = "未確認";
                        break;
                    case "1":
                        result = "已確認";
                        break;
                }

                return result;
            }
        }

        #endregion
    }
}
