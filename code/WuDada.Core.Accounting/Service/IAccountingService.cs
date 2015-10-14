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
    }
}
