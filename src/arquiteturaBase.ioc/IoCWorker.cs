using StructureMap;

namespace arquiteturaBase.ioc
{
    public static class IoCWorker
    {
        public static void ConfigureWEB()
        {
            HMV.Core.IoC.IoCWorker.ConfigureWIN();
            ObjectFactory.Configure(i =>
            {
                i.AddRegistry<ServiceRegistry>();
                i.AddRegistry<ConsultRegistry>();
            });
        }
    }
}
