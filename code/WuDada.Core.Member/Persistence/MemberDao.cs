using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using WuDada.Core.Member.Domain;
using System.Collections;
using WuDada.Core.Generic.Extension;

namespace WuDada.Core.Member.Persistence
{
    public class MemberDao : AdoDao, IMemberDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 會員
        /// </summary>
        /// <param name="member">被新增的會員</param>
        /// <returns>新增後的會員</returns>
        public MemberVO CreateMember(MemberVO member)
        {
            NHibernateDao.Insert(member);
            return member;
        }

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        public MemberVO UpdateMember(MemberVO member)
        {
            NHibernateDao.Update(member);

            return member;
        }

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        public void DeleteMember(MemberVO member)
        {
            NHibernateDao.Delete(member);
        }

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        public MemberVO GetMemberById(int memberId)
        {
            return NHibernateDao.GetVOById<MemberVO>(memberId);
        }

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        public IList<MemberVO> GetMemberList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select m from MemberVO m ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryMember(param, fromScript, whereScript, conditions, true).OfType<MemberVO>().ToList();
        }

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMemberCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(m.MemberId) from MemberVO m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMember(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        /// <summary>
        /// 取得門號佣金總合
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetTotalCommission(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select sum(m.Commission-m.Prepayment) from MemberVO m ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryMember(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryMember(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendMemberLoginId(conditions, whereScript, param);
            AppendMemberKeyWord(conditions, whereScript, param);
            AppendMemberStatus(conditions, whereScript, param);
            AppendMemberUserConfirm(conditions, whereScript, param);
            AppendMemberSex(conditions, whereScript, param);
            AppendMemberCreateDate(conditions, whereScript, param);
            AppendMemberDate(conditions, whereScript, param);
            AppendMemberBirthDay(conditions, whereScript, param);
            AppendMemberStore(conditions, whereScript, param);
            AppendMemberSales(conditions, whereScript, param);
            AppendMemberGetCommission(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendMemberOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendMemberGetCommission(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("GetCommission"))
            {
                whereScript.Append(" and m.GetCommission = ? ");
                param.Add(conditions["GetCommission"]);
            }
        }

        private void AppendMemberSales(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Sales"))
            {
                whereScript.Append(" and m.Sales = ? ");
                param.Add(conditions["Sales"]);
            }
        }

        private void AppendMemberStore(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Store"))
            {
                whereScript.Append(" and m.Store = ? ");
                param.Add(conditions["Store"]);
            }
        }

        private void AppendMemberBirthDay(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("BirthdayMonth"))
            {
                whereScript.Append(" and m.BirthdayMonth = ? ");
                param.Add(conditions["BirthdayMonth"]);
            }

            if (conditions.IsContainsValue("BirthdayDay"))
            {
                whereScript.Append(" and m.BirthdayDay = ? ");
                param.Add(conditions["BirthdayDay"]);
            }
        }

        private void AppendMemberDate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ApplyDateStart"))
            {
                whereScript.Append(" and m.ApplyDate >= ? ");
                param.Add(Convert.ToDateTime(conditions["ApplyDateStart"]));
            }
            if (conditions.IsContainsValue("ApplyDateEnd"))
            {
                whereScript.Append(" and m.ApplyDate <= ? ");
                param.Add(Convert.ToDateTime(conditions["ApplyDateEnd"]));
            }
            if (conditions.IsContainsValue("DueDateStart"))
            {
                whereScript.Append(" and m.DueDate >= ? ");
                param.Add(Convert.ToDateTime(conditions["DueDateStart"]));
            }
            if (conditions.IsContainsValue("DueDateEnd"))
            {
                whereScript.Append(" and m.DueDate <= ? ");
                param.Add(Convert.ToDateTime(conditions["DueDateEnd"]));
            }
            if (conditions.IsContainsValue("ApplyDate2Start"))
            {
                whereScript.Append(" and m.ApplyDate2 >= ? ");
                param.Add(Convert.ToDateTime(conditions["ApplyDate2Start"]));
            }
            if (conditions.IsContainsValue("ApplyDate2End"))
            {
                whereScript.Append(" and m.ApplyDate2 <= ? ");
                param.Add(Convert.ToDateTime(conditions["ApplyDate2End"]));
            }
        }

        private string AppendMemberOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by m.CreatedDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        private void AppendMemberCreateDate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("CreateDateFrom"))
            {
                whereScript.Append(" and m.CreatedDate >= ? ");
                param.Add(DateTime.Parse(conditions["CreateDateFrom"]));
            }

            if (conditions.IsContainsValue("CreateDateTo"))
            {
                whereScript.Append(" and m.CreatedDate <= ? ");
                param.Add(DateTime.Parse(conditions["CreateDateTo"]));
            }
        }

        private void AppendMemberLoginId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("LoginId"))
            {
                whereScript.Append(" and m.LoginId = ? ");
                param.Add(conditions["LoginId"]);
            }
        }

        private void AppendMemberKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (m.Name like ? or m.Email like ? or m.Phone like ? or m.Mobile like ? or m.Project like ? or m.Product like ? or m.PhoneSer like ? or m.PID like ? or m.OnlineWholesalers like ? or m.MobileWholesalers like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
            else
            {
                if (conditions.IsContainsValue("Name"))
                {
                    whereScript.Append(" and m.Name = ? ");
                    param.Add(conditions["Name"]);
                }

                if (conditions.IsContainsValue("Email"))
                {
                    whereScript.Append(" and m.Email = ? ");
                    param.Add(conditions["Email"]);
                }
            }
        }

        private void AppendMemberSex(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Sex"))
            {
                whereScript.Append(" and m.Sex = ? ");
                param.Add(conditions["Sex"]);
            }
        }

        private void AppendMemberUserConfirm(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("UserConfirm"))
            {
                whereScript.Append(" and m.UserConfirm = ? ");
                param.Add(conditions["UserConfirm"]);
            }
        }

        private void AppendMemberStatus(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Status"))
            {
                whereScript.Append(" and m.Status = ? ");
                param.Add(conditions["Status"]);
            }
        }
    }
}
