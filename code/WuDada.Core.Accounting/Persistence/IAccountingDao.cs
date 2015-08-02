using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Member.Domain;
using WuDada.Core.Accounting.Domain;

namespace WuDada.Core.Accounting.Persistence
{
    public interface IAccountingDao
    {
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
    }
}
