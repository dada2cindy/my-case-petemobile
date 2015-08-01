using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Accounting.Domain;
using NHibernate;
using WuDada.Core.Post.Service;
using WuDada.Core.Member.Service;
using WuDada.Core.Accounting.Service;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;

namespace WuDada.Core.Accounting.Service.Impl
{
    public class AccountingService : IAccountingService
    {
        public IPostService PostService { get; set; }
        public IMemberService MemberService { get; set; }
        public IAuthService AuthService { get; set; }               

        #region IAccountingService 成員

        /// <summary>
        /// 取得當月業績
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <returns></returns>
        IList<SalesStatisticsVO> IAccountingService.GetSalesStatistics(string ym)
        {
            //test
            IList<LoginUserVO> userList = AuthService.GetAllLoginUserList();
            if (userList != null && userList.Count > 0)
            {
                return new List<SalesStatisticsVO>();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
