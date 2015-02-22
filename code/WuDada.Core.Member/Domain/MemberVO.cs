﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
        /// 申請日期
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
