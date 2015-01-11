using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using Spring.Context;
using WuDada.Core.Helper;
using WuDada.Provider.ResourceHandle.Service;

namespace WuDada.Provider.ResourceHandle
{
    public class StorageFactory
    {
        public StorageFactory()
        {
            XmlConfigurator.Configure();
        }
        public StorageFactory(IApplicationContext applicationContext)
        {
            SpringHelper.ApplicationContext = applicationContext;
            XmlConfigurator.Configure();
        }

        public IStorageFileService GetStorageFileService()
        {
            return SpringHelper.ApplicationContext["StorageFileServiceProxy"] as IStorageFileService;
        }
    }
}
