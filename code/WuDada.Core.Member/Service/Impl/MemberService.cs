using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Member.Domain;
using WuDada.Core.Member.Persistence;

namespace WuDada.Core.Member.Service.Impl
{
    public class MemberService : IMemberService
    {
        public IMemberDao MemberDao { get; set; }

        /// <summary>
        /// 會員
        /// </summary>
        /// <param name="member">被新增的會員</param>
        /// <returns>新增後的會員</returns>
        public MemberVO CreateMember(MemberVO member)
        {
            return MemberDao.CreateMember(member);
        }

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        public MemberVO UpdateMember(MemberVO member)
        {
            return MemberDao.UpdateMember(member);
        }

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        public void DeleteMember(MemberVO member)
        {
            MemberDao.DeleteMember(member);
        }

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        public MemberVO GetMemberById(int memberId)
        {
            return MemberDao.GetMemberById(memberId);
        }

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        public IList<MemberVO> GetMemberList(IDictionary<string, string> conditions)
        {
            return MemberDao.GetMemberList(conditions);
        }        

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetMemberCount(IDictionary<string, string> conditions)
        {
            return MemberDao.GetMemberCount(conditions);
        }

        /// <summary>
        /// 取得會員 By 登入帳號
        /// </summary>
        /// <param name="loginId">識別碼</param>
        /// <returns>會員</returns>
        public MemberVO GetMemberByLoginId(string loginId)
        {
            IDictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("LoginId", loginId);
            IList<MemberVO> memberList = MemberDao.GetMemberList(conditions);

            if (memberList != null && memberList.Count > 0)
            {
                return memberList[0];
            }
            else
            {
                return null;
            }
        }
    }
}
