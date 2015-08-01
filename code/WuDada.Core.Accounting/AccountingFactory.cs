using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using Spring.Context;
using WuDada.Core.Helper;
using WuDada.Core.Accounting.Service;

namespace WuDada.Core.Auth
{
    public class AccountingFactory
    {
        public AccountingFactory()
        {
            XmlConfigurator.Configure();
        }

        public AccountingFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IAccountingService GetAccountingService()
        {
            return SpringHelper.ApplicationContext["AccountingServiceProxy"] as IAccountingService;
        }
    }
}
