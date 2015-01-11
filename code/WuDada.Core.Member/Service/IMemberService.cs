using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Member.Domain;

namespace WuDada.Core.Member.Service
{
    public interface IMemberService
    {
        /// <summary>
        /// 會員
        /// </summary>
        /// <param name="member">被新增的會員</param>
        /// <returns>新增後的會員</returns>
        MemberVO CreateMember(MemberVO member);

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="member">被更新的會員</param>
        /// <returns>更新後的會員</returns>
        MemberVO UpdateMember(MemberVO member);

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="member">被刪除的會員</param>
        void DeleteMember(MemberVO member);

        /// <summary>
        /// 取得會員 By 識別碼
        /// </summary>
        /// <param name="memberId">識別碼</param>
        /// <returns>會員</returns>
        MemberVO GetMemberById(int memberId);

        /// <summary>
        /// 取得會員清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>會員清單</returns>
        IList<MemberVO> GetMemberList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得會員數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetMemberCount(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得會員 By 登入帳號
        /// </summary>
        /// <param name="loginId">識別碼</param>
        /// <returns>會員</returns>
        MemberVO GetMemberByLoginId(string loginId);
    }
}
