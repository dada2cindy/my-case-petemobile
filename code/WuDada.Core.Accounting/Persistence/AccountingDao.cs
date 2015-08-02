using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using WuDada.Core.Member.Domain;
using System.Collections;
using WuDada.Core.Generic.Extension;
using WuDada.Core.Accounting.Domain;

namespace WuDada.Core.Accounting.Persistence
{
    public class AccountingDao : AdoDao, IAccountingDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        #region IAccountingDao 成員

        /// <summary>
        /// 取得業績目標 by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TargetVO GetTargetById(string id)
        {
            return NHibernateDao.GetVOById<TargetVO>(id);
        }

        /// <summary>
        /// 建立或更新Target
        /// </summary>
        /// <param name="targetVO"></param>
        /// <returns></returns>
        public TargetVO SaveOrUpdateTarget(TargetVO targetVO)
        {
            NHibernateDao.SaveOrUpdate(targetVO);

            return targetVO;
        }

        #endregion
    }
}
