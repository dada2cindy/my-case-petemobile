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
        /// <returns></returns>
        IList<SalesStatisticsVO> GetSalesStatistics(string ym);
    }
}
