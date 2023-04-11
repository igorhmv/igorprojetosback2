using StructureMap.Attributes;
using StructureMap.Configuration.DSL;

namespace arquiteturaBase.ioc
{
    public class ConsultRegistry : Registry
    {
        public ConsultRegistry()
        {
            //ForRequestedType<IConsult>()
            //    .CacheBy(InstanceScope.PerRequest)
            //    .TheDefault.Is.OfConcreteType<Consult>();
        }
    }
}
