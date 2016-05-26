using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Accounting.Domain;

namespace WuDada.Core.Accounting.Service
{
    public interface IAccountingService
    {
        /// <summary>
        /// 取得當月業績 帳號角度
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <returns></returns>
        IList<SalesStatisticsVO> GetSalesStatisticsByLoginUser(string ym);

        /// <summary>
        /// 取得當月業績
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <param name="store"></param>
        /// <returns></returns>
        IList<SalesStatisticsVO> GetSalesStatistics(string ym, string store);

        /// <summary>
        /// 更新業績
        /// </summary>
        /// <param name="targetList"></param>
        void UpdateTargetList(IList<TargetVO> targetList);

        /// <summary>
        /// 取得業績目標 by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TargetVO GetTargetById(string id);

        /// <summary>
        /// 建立或更新Target
        /// </summary>
        /// <param name="targetVO"></param>
        /// <returns></returns>
        TargetVO SaveOrUpdateTarget(TargetVO targetVO);

        /// <summary>
        /// 取得當月業績 從店點角度
        /// </summary>
        /// <param name="ym">yyyyMM</param>
        /// <returns></returns>
        IList<SalesStatisticsVO> GetSalesStatisticsByStore(string ym);

        /// <summary>
        /// 更新現金帳
        /// </summary>
        void UpdateCash();

        /// <summary>
        /// 取得結帳資訊 By 日期
        /// </summary>
        /// <param name="day"></param>
        /// <returns>如果沒有前一天的結帳金額,則回傳null</returns>
        CashStatisticsVO GetCashStatisticsVO(DateTime day);

        void UpdateCashByPeriod(DateTime dateFrom, DateTime dateTo);
    }
}
