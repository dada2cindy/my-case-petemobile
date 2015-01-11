using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using log4net.Config;
using WuDada.Core.Helper;
using WuDada.Core.Member.Service;

namespace WuDada.Core.Member
{
    public class MemberFactory
    {
        public MemberFactory()
        {
            XmlConfigurator.Configure();
        }

        public MemberFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IMemberService GetMemberService()
        {
            return SpringHelper.ApplicationContext["MemberServiceProxy"] as IMemberService;
        }
    }
}
