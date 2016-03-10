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
        /// <param name="store"></param>
        /// <returns></returns>
        public IList<SalesStatisticsVO> GetSalesStatistics(string ym, string store)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Debug("GetSalesStatistics");

            //test
            IList<LoginUserVO> userList = AuthService.GetAllLoginUserList().Where(u => u.ShowInSalesStatistics == 1).OrderBy(u => u.SortNo).ToList();
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
                        
                        //預繳金, 幫客戶預繳的用減的, 沒有幫客戶預繳不用算
                        //salesStatisticsVO.ApplyProfit += memberList.Where(m => m.Prepayment > 0 && "否".Equals(m.SelfPrepayment)).Sum(m => m.Prepayment);
                        salesStatisticsVO.ApplyProfit -= memberList.Where(m => m.Prepayment > 0 && "是".Equals(m.SelfPrepayment)).Sum(m => m.Prepayment);    

                        salesStatisticsVO.ApplyTelCom1Count = memberList.Count(m => "太電".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom2Count = memberList.Count(m => "遠傳".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom3Count = memberList.Count(m => "中華".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom4Count = memberList.Count(m => "亞太".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom5Count = memberList.Count(m => "星星".Equals(m.Project3));
                    }
                    else
                    {
                        salesStatisticsVO.ApplyCount = 0;
                        salesStatisticsVO.ApplyRevenue = 0;
                        salesStatisticsVO.ApplyProfit = 0;
                        salesStatisticsVO.ApplyTelCom1Count = 0;
                        salesStatisticsVO.ApplyTelCom2Count = 0;
                        salesStatisticsVO.ApplyTelCom3Count = 0;
                        salesStatisticsVO.ApplyTelCom4Count = 0;
                        salesStatisticsVO.ApplyTelCom5Count = 0;
                    }

                    //配件
                    Dictionary<string, string> conditionsPost = new Dictionary<string, string>();
                    conditionsPost.Add("Flag", "1");
                    conditionsPost.Add("WithOutMemberId", "1");
                    conditionsPost.Add("Type", "1");
                    conditionsPost.Add("NodeId", "2");
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
                            //salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TotalProfit / (salesStatisticsVO.Target * (DateTime.Today.Day / DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))));
                            salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                            double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);

                            salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TargetAchievementRates * DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)) / DateTime.Today.Day;

                            rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                        }
                        else
                        {
                            //進度達成率=總毛利/ 本月目標
                            salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                            double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                        }                        
                    }      
                    else
                    {
                        salesStatisticsVO.TargetAchievementRates = 0;
                    }

                    result.Add(salesStatisticsVO);
                }

                result.Add(GetTotal(ym, result));

                return result;
            }
            else
            {
                return null;
            }
        }

        private SalesStatisticsVO GetTotal(string ym, IList<SalesStatisticsVO> result)
        {
            int year = int.Parse(ym.Substring(0, 4));
            int month = int.Parse(ym.Substring(4, 2));
            DateTime dateStart = new DateTime(year, month, 1);

            SalesStatisticsVO salesStatisticsVO = new SalesStatisticsVO();
            salesStatisticsVO.Name = "總合";
            salesStatisticsVO.Target = result.Sum(m => m.Target);
            salesStatisticsVO.ApplyCount = result.Sum(m => m.ApplyCount);
            salesStatisticsVO.ApplyRevenue = result.Sum(m => m.ApplyRevenue);
            salesStatisticsVO.ApplyProfit = result.Sum(m => m.ApplyProfit);
            salesStatisticsVO.ApplyTelCom1Count = result.Sum(m => m.ApplyTelCom1Count);
            salesStatisticsVO.ApplyTelCom2Count = result.Sum(m => m.ApplyTelCom2Count);
            salesStatisticsVO.ApplyTelCom3Count = result.Sum(m => m.ApplyTelCom3Count);
            salesStatisticsVO.ApplyTelCom4Count = result.Sum(m => m.ApplyTelCom4Count);
            salesStatisticsVO.ApplyTelCom5Count = result.Sum(m => m.ApplyTelCom5Count);
            salesStatisticsVO.FittingCount = result.Sum(m => m.FittingCount);
            salesStatisticsVO.FittingRevenue = result.Sum(m => m.FittingRevenue);
            salesStatisticsVO.FittingProfit = result.Sum(m => m.FittingProfit);
            salesStatisticsVO.TotalProfit = result.Sum(m => m.TotalProfit);
            salesStatisticsVO.NotGetTotalCommission = result.Sum(m => m.NotGetTotalCommission);

            if (salesStatisticsVO.TotalProfit != 0 && salesStatisticsVO.Target != 0)
            {
                if (DateTime.Today.ToString("yyyyMM").Equals(dateStart.ToString("yyyyMM")))
                {
                    //為本月要算出目前日子已過了本月幾分之幾, 進度達成率=總毛利/ [本月目標* (當月已過天數/當月天數)]
                    //salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TotalProfit / (salesStatisticsVO.Target * (DateTime.Today.Day / DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))));
                    salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                    double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                    salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);

                    salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TargetAchievementRates * DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)) / DateTime.Today.Day;

                    rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates);
                    salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                }
                else
                {
                    //進度達成率=總毛利/ 本月目標
                    salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                    double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                    salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                }
            }
            else
            {
                salesStatisticsVO.TargetAchievementRates = 0;
            }

            return salesStatisticsVO;
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

        /// <summary>
        /// 取得當月業績 從店點角度
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <returns></returns>
        public IList<SalesStatisticsVO> GetSalesStatisticsByStore(string ym)
        {
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Debug("GetSalesStatisticsByStore");

            //test
            IList<NodeVO> storeList = PostService.GetNodeListByParentName("店家");
            if (storeList != null && storeList.Count > 0)
            {
                IList<SalesStatisticsVO> result = new List<SalesStatisticsVO>();

                int year = int.Parse(ym.Substring(0, 4));
                int month = int.Parse(ym.Substring(4, 2));
                DateTime dateStart = new DateTime(year, month, 1);
                DateTime dateEnd = dateStart.AddMonths(1).AddDays(-1);

                foreach (NodeVO store in storeList)
                {
                    SalesStatisticsVO salesStatisticsVO = new SalesStatisticsVO();

                    string targetId = string.Format("{0}{1}", ym, store.Name);
                    TargetVO targetVO = GetTargetById(targetId);

                    salesStatisticsVO.Name = store.Name;

                    ////本月目標
                    salesStatisticsVO.Target = targetVO == null ? 0 : targetVO.Amount;

                    //未核發佣金
                    Dictionary<string, string> conditionsNotGetTotalCommission = new Dictionary<string, string>();
                    conditionsNotGetTotalCommission.Add("Status", "1");
                    conditionsNotGetTotalCommission.Add("GetCommission", "否");
                    conditionsNotGetTotalCommission.Add("Store", store.Name);
                    salesStatisticsVO.NotGetTotalCommission = MemberService.GetNotGetTotalCommission(conditionsNotGetTotalCommission);

                    //門號
                    Dictionary<string, string> conditionsMember = new Dictionary<string, string>();
                    conditionsMember.Add("Status", "1");
                    conditionsMember.Add("ApplyDate2Start", dateStart.ToString("yyyy/MM/dd"));
                    conditionsMember.Add("ApplyDate2End", dateEnd.ToString("yyyy/MM/dd"));
                    conditionsMember.Add("Store", store.Name);

                    IList<MemberVO> memberList = MemberService.GetMemberList(conditionsMember);
                    if (memberList != null)
                    {
                        salesStatisticsVO.ApplyCount = memberList.Count;
                        salesStatisticsVO.ApplyRevenue = memberList.Sum(m => m.Commission);
                        salesStatisticsVO.ApplyProfit = memberList.Sum(m => m.Commission + m.PhoneSellPrice - m.PhonePrice - m.BreakMoney);

                        //預繳金, 幫客戶預繳的用減的, 沒有幫客戶預繳不用算
                        //salesStatisticsVO.ApplyProfit += memberList.Where(m => m.Prepayment > 0 && "否".Equals(m.SelfPrepayment)).Sum(m => m.Prepayment);
                        salesStatisticsVO.ApplyProfit -= memberList.Where(m => m.Prepayment > 0 && "是".Equals(m.SelfPrepayment)).Sum(m => m.Prepayment);    

                        salesStatisticsVO.ApplyTelCom1Count = memberList.Count(m => "太電".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom2Count = memberList.Count(m => "遠傳".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom3Count = memberList.Count(m => "中華".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom4Count = memberList.Count(m => "亞太".Equals(m.Project3));
                        salesStatisticsVO.ApplyTelCom5Count = memberList.Count(m => "星星".Equals(m.Project3));
                    }
                    else
                    {
                        salesStatisticsVO.ApplyCount = 0;
                        salesStatisticsVO.ApplyRevenue = 0;
                        salesStatisticsVO.ApplyProfit = 0;
                        salesStatisticsVO.ApplyTelCom1Count = 0;
                        salesStatisticsVO.ApplyTelCom2Count = 0;
                        salesStatisticsVO.ApplyTelCom3Count = 0;
                        salesStatisticsVO.ApplyTelCom4Count = 0;
                        salesStatisticsVO.ApplyTelCom5Count = 0;
                    }

                    //配件
                    Dictionary<string, string> conditionsPost = new Dictionary<string, string>();
                    conditionsPost.Add("Flag", "1");
                    conditionsPost.Add("WithOutMemberId", "1");
                    conditionsPost.Add("Type", "1");
                    conditionsPost.Add("NodeId", "2");
                    conditionsPost.Add("CloseDateStart", dateStart.ToString("yyyy/MM/dd"));
                    conditionsPost.Add("CloseDateEnd", dateEnd.ToString("yyyy/MM/dd"));
                    //conditionsPost.Add("Store", store.Name);
                    IList<PostVO> postList = PostService.GetPostList(conditionsPost);
                    //因為配件沒有分店家，所以這邊只有第一個本店要算
                    if (postList != null && store.Name.Equals(storeList[0].Name))
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

                    log.Debug("store: " + store.Name);
                    //進度達成率  總毛利/ [本月目標* (當月已過天數/當月天數)]
                    if (salesStatisticsVO.TotalProfit != 0 && salesStatisticsVO.Target != 0)
                    {
                        if (DateTime.Today.ToString("yyyyMM").Equals(dateStart.ToString("yyyyMM")))
                        {
                            //為本月要算出目前日子已過了本月幾分之幾, 進度達成率=總毛利/ [本月目標* (當月已過天數/當月天數)]
                            //salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TotalProfit / (salesStatisticsVO.Target * (DateTime.Today.Day / DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))));
                            salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                            double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);

                            salesStatisticsVO.TargetAchievementRates = (salesStatisticsVO.TargetAchievementRates * DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)) / DateTime.Today.Day;

                            rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                        }
                        else
                        {
                            //進度達成率=總毛利/ 本月目標
                            salesStatisticsVO.TargetAchievementRates = salesStatisticsVO.TotalProfit / salesStatisticsVO.Target;

                            double rates = Convert.ToDouble(salesStatisticsVO.TargetAchievementRates * 100);
                            salesStatisticsVO.TargetAchievementRates = Math.Round(rates, 2);
                        }
                    }
                    else
                    {
                        salesStatisticsVO.TargetAchievementRates = 0;
                    }

                    result.Add(salesStatisticsVO);
                }

                result.Add(GetTotal(ym, result));

                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新現金帳
        /// </summary>
        public void UpdateCash()
        {
            NodeVO node = PostService.GetNodeByName("#每日結帳");            
            
            ////得最後一日的結帳金額   
            Dictionary<string, string> conditionsPost = new Dictionary<string, string>();
            conditionsPost.Add("Flag", "1");
            conditionsPost.Add("NodeId", node.NodeId.ToString());
            conditionsPost.Add("Order", "order by p.CloseDate desc");
            conditionsPost.Add("PageIndex", "0");
            conditionsPost.Add("PageSize", "1");
            IList<PostVO> postList = PostService.GetPostList(conditionsPost);

            if (postList != null && postList.Count > 0)
            {
                DateTime lastCashDate = postList[0].CloseDate.Value;

                DateTime dateFrom = lastCashDate.AddDays(1);
                DateTime dateTo = DateTime.Today.AddDays(-1);

                UpdateCashByPeriod(dateFrom, dateTo);
            }            
        }

        public void UpdateCashByPeriod(DateTime dateFrom, DateTime dateTo)
        {
            NodeVO node = PostService.GetNodeByName("#每日結帳");   

            for (DateTime day = dateFrom; day.Date <= dateTo; day = day.AddDays(1))
            {
                CashStatisticsVO cashStatisticsVO = GetCashStatisticsVO(day);

                if (cashStatisticsVO != null)
                {
                    //找出有沒有這天的舊的結帳資料,有的話刪除
                    Dictionary<string, string> conditionsOldCash = new Dictionary<string, string>();
                    conditionsOldCash.Add("Flag", "1");
                    conditionsOldCash.Add("NodeId", node.NodeId.ToString());
                    conditionsOldCash.Add("CloseDate", day.ToString("yyyy/MM/dd"));
                    conditionsOldCash.Add("PageIndex", "0");
                    conditionsOldCash.Add("PageSize", "1");
                    
                    IList<PostVO> cashList = PostService.GetPostList(conditionsOldCash);
                    if (cashList != null && cashList.Count > 0)
                    {
                        PostService.DeletePost(cashList[0]);
                    }

                    PostVO post = new PostVO();
                    post.Node = node;
                    post.Title = "每日結帳";
                    post.CreatedBy = "admin";
                    post.UpdatedBy = "admin";
                    post.CloseDate = cashStatisticsVO.CloseDate;
                    post.Price = cashStatisticsVO.CashToday;

                    PostService.CreatePost(post);
                }
            }
        }

        /// <summary>
        /// 取得結帳資訊 By 日期
        /// </summary>
        /// <param name="day"></param>
        /// <returns>如果沒有前一天的結帳金額,則回傳null</returns>
        public CashStatisticsVO GetCashStatisticsVO(DateTime day)
        {
            ////抓前一日的結帳金額，有的話才計算該天的結帳
            NodeVO node = PostService.GetNodeByName("#每日結帳");
            Dictionary<string, string> conditionsYesterdayCash = new Dictionary<string, string>();
            conditionsYesterdayCash.Add("Flag", "1");
            conditionsYesterdayCash.Add("NodeId", node.NodeId.ToString());
            conditionsYesterdayCash.Add("CloseDate", day.AddDays(-1).ToString("yyyy/MM/dd"));
            conditionsYesterdayCash.Add("PageIndex", "0");
            conditionsYesterdayCash.Add("PageSize", "1");
            IList<PostVO> postYesterdayCashList = PostService.GetPostList(conditionsYesterdayCash);
            if (postYesterdayCashList != null && postYesterdayCashList.Count > 0)
            {
                CashStatisticsVO cashStatisticsVO = new CashStatisticsVO();
                cashStatisticsVO.CashYesterday = postYesterdayCashList[0].Price;
                cashStatisticsVO.CloseDate = day;
                
                //今日庫存進貨
                Dictionary<string, string> conditionsBuyToday = new Dictionary<string, string>();
                conditionsBuyToday.Add("Flag", "1");
                conditionsBuyToday.Add("NodeId", "2");
                conditionsBuyToday.Add("ShowDate", day.ToString("yyyy/MM/dd"));
                IList<PostVO> postBuyTodayList = PostService.GetPostList(conditionsBuyToday);

                if (postBuyTodayList != null && postBuyTodayList.Count > 0)
                {
                    cashStatisticsVO.BuyToday -= postBuyTodayList.Sum(p => p.Price * p.Quantity);
                }

                //今日庫存銷貨
                Dictionary<string, string> conditionsSellToday = new Dictionary<string, string>();
                conditionsSellToday.Add("Flag", "1");
                conditionsSellToday.Add("NodeId", "2");
                //conditionsSellToday.Add("WithOutMemberId", "1");
                conditionsSellToday.Add("CloseDate", day.ToString("yyyy/MM/dd"));
                IList<PostVO> postSellTodayList = PostService.GetPostList(conditionsSellToday);

                if (postSellTodayList != null && postSellTodayList.Count > 0)
                {
                    cashStatisticsVO.SellToday = postSellTodayList.Sum(p => p.SellPrice * p.Quantity);
                }

                //門號
                IList<NodeVO> storeList = PostService.GetNodeListByParentName("店家");
                Dictionary<string, string> conditionsMember = new Dictionary<string, string>();
                conditionsMember.Add("Status", "1");
                conditionsMember.Add("ApplyDate2", day.ToString("yyyy/MM/dd"));
                conditionsMember.Add("Store", storeList[0].Name);
                IList<MemberVO> memberList = MemberService.GetMemberList(conditionsMember);

                if (memberList != null && memberList.Count > 0)
                {                    
                    cashStatisticsVO.MobileToday = memberList.Sum(m => m.PhoneSellPrice - m.PhonePrice - m.BreakMoney);
                    
                    //如果沒有幫客戶預繳, 則現金要加上預繳金額
                    cashStatisticsVO.MobileToday += memberList.Where(m => m.Prepayment > 0 && "否".Equals(m.SelfPrepayment)).Sum(m => m.Prepayment);

                    foreach (MemberVO member in memberList)
                    {
                        Dictionary<string, string> conditionsPostWithMember = new Dictionary<string, string>();
                        conditionsSellToday.Add("Flag", "1");
                        conditionsSellToday.Add("NodeId", "2");
                        conditionsSellToday.Add("MemberId", member.MemberId.ToString());
                        int postWithMemberCount = PostService.GetPostCount(conditionsPostWithMember);

                        if (postWithMemberCount > 0)
                        {
                            //如果這個客戶的手機是來自庫存的, 那麼手機進價要移除支出的部分
                            cashStatisticsVO.MobileToday += member.PhonePrice;
                        }
                    }
                }

                //特別現金收支
                NodeVO nodeSpecial = PostService.GetNodeByName("#特別現金收支");

                Dictionary<string, string> conditionsSpecial = new Dictionary<string, string>();
                conditionsSpecial.Add("Flag", "1");
                conditionsSpecial.Add("NodeId", nodeSpecial.NodeId.ToString());
                conditionsSpecial.Add("CloseDate", day.ToString("yyyy/MM/dd"));
                IList<PostVO> postSpecialList = PostService.GetPostList(conditionsSpecial);

                if (postSpecialList != null && postSpecialList.Count > 0)
                {
                    cashStatisticsVO.SpecialToday = postSellTodayList.Sum(p => p.Price);
                }

                //最後總計
                cashStatisticsVO.TotalToday = cashStatisticsVO.BuyToday + cashStatisticsVO.SellToday + cashStatisticsVO.MobileToday + cashStatisticsVO.SpecialToday;
                cashStatisticsVO.CashToday = cashStatisticsVO.CashYesterday + cashStatisticsVO.TotalToday;


                return cashStatisticsVO;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
