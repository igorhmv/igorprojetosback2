
using AutoMapper;

namespace arquiteturaBase.application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(i =>
            {
                i.AddProfile<DomainToDto>();
            });
        }
    }
}
