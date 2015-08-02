﻿using System;
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
using WuDada.Core.Accounting.Persistence;
using WuDada.Core.Member.Domain;
using WuDada.Core.Post.Domain;
using Common.Logging;

namespace WuDada.Core.Accounting.Service.Impl
{
    public class AccountingService : IAccountingService
    {
        public IPostService PostService { get; set; }
        public IMemberService MemberService { get; set; }
        public IAuthService AuthService { get; set; }
        public IAccountingDao AccountingDao { get; set; }  

        #region IAccountingService 成員

        /// <summary>
        /// 取得當月業績
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <returns></returns>
        public IList<SalesStatisticsVO> GetSalesStatistics(string ym, string store)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Debug("GetSalesStatistics");

            //test
            IList<LoginUserVO> userList = AuthService.GetAllLoginUserList();
            if (userList != null && userList.Count > 0)
            {
                IList<SalesStatisticsVO> result = new List<SalesStatisticsVO>();

                int year = int.Parse(ym.Substring(0, 4));
                int month = int.Parse(ym.Substring(4, 2));
                DateTime dateStart = new DateTime(year, month, 1);
                DateTime dateEnd = dateStart.AddMonths(1).AddDays(-1);

                foreach (LoginUserVO user in userList)
                {
                    SalesStatisticsVO salesStatisticsVO = new SalesStatisticsVO();                    

                    string targetId = string.Format("{0}{1}", ym, user.FullNameInChinese);
                    TargetVO targetVO = GetTargetById(targetId);                                       

                    salesStatisticsVO.Name = user.FullNameInChinese;

                    ////本月目標
                    salesStatisticsVO.Target = targetVO == null ? 0 : targetVO.Amount;

                    //門號
                    Dictionary<string, string> conditionsMember = new Dictionary<string, string>();
                    conditionsMember.Add("Status", "1");
                    conditionsMember.Add("ApplyDate2Start", dateStart.ToString("yyyy/MM/dd"));
                    conditionsMember.Add("ApplyDate2End", dateEnd.ToString("yyyy/MM/dd"));
                    conditionsMember.Add("Store", store);
                    conditionsMember.Add("Sales", user.FullNameInChinese);

                    IList<MemberVO> memberList = MemberService.GetMemberList(conditionsMember);
                    if (memberList != null)
                    {
                        salesStatisticsVO.ApplyCount = memberList.Count;
                        salesStatisticsVO.ApplyRevenue = memberList.Sum(m => m.Commission);
                        salesStatisticsVO.ApplyProfit = memberList.Sum(m => m.Commission + m.PhoneSellPrice - m.PhonePrice - m.BreakMoney);
                    }
                    else
                    {
                        salesStatisticsVO.ApplyCount = 0;
                        salesStatisticsVO.ApplyRevenue = 0;
                        salesStatisticsVO.ApplyProfit = 0;
                    }

                    //配件
                    Dictionary<string, string> conditionsPost = new Dictionary<string, string>();
                    conditionsPost.Add("Flag", "1");
                    conditionsPost.Add("Type", "1");
                    conditionsPost.Add("CloseDateStart", dateStart.ToString("yyyy/MM/dd"));
                    conditionsPost.Add("CloseDateEnd", dateEnd.ToString("yyyy/MM/dd"));
                    conditionsPost.Add("Store", store);
                    conditionsPost.Add("CustomField2", user.FullNameInChinese);
                    IList<PostVO> postList = PostService.GetPostList(conditionsPost);
                    if (postList != null)
                    {
                        salesStatisticsVO.FittingCount = postList.Count;
                        salesStatisticsVO.FittingRevenue = postList.Sum(p => p.SellPrice);
                        salesStatisticsVO.FittingProfit = postList.Sum(p => p.SellPrice - p.Price);
                    }
                    else
                    {
                        salesStatisticsVO.FittingCount = 0;
                        salesStatisticsVO.FittingRevenue = 0;
                        salesStatisticsVO.FittingProfit = 0;
                    }

                    //總毛利
                    salesStatisticsVO.TotalProfit = salesStatisticsVO.ApplyProfit + salesStatisticsVO.FittingProfit;

                    log.Debug("user: " + user.FullNameInChinese);
                    //進度達成率  總毛利/ [本月目標* (當月已過天數/當月天數)]
                    if (salesStatisticsVO.TotalProfit != 0 && salesStatisticsVO.Target != 0)
                    {
                        if (DateTime.Today.ToString("yyyyMM").Equals(dateStart.ToString("yyyyMM")))
                        {
                            //為本月要算出目前日子已過了本月幾分之幾, 進度達成率=總毛利/ [本月目標* (當月已過天數/當月天數)]
                            salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TotalProfit / (salesStatisticsVO.Target * (DateTime.Today.Day / DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)))) * 100;
                        }
                        else
                        {
                            //進度達成率=總毛利/ 本月目標
                            salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;
                        }
                    }      
                    else
                    {
                        salesStatisticsVO.TargetAchievementRates = 0;
                    }

                    result.Add(salesStatisticsVO);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新業績
        /// </summary>
        /// <param name="targetList"></param>
        public void UpdateTargetList(IList<TargetVO> targetList)
        {
            if (targetList != null && targetList.Count > 0)
            {
                foreach (TargetVO targetVO in targetList)
                {
                    TargetVO t = GetTargetById(targetVO.Id);
                    if (t == null)
                    {
                        t = new TargetVO();
                        t.Id = targetVO.Id;
                    }

                    t.Name = targetVO.Name;
                    t.Amount = targetVO.Amount;

                    SaveOrUpdateTarget(t);
                }
            }
        }

        /// <summary>
        /// 取得業績目標 by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TargetVO GetTargetById(string id)
        {
            return AccountingDao.GetTargetById(id);
        }

        /// <summary>
        /// 建立或更新Target
        /// </summary>
        /// <param name="targetVO"></param>
        /// <returns></returns>
        public TargetVO SaveOrUpdateTarget(TargetVO targetVO)
        {
            return AccountingDao.SaveOrUpdateTarget(targetVO);
        }

        #endregion
    }
}