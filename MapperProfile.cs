using Automapper.EntityFramework.Example.Model;

namespace AutoMapper.EntityFramework.Example
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NameEntity, NameModel>();

            CreateMap<NameModel, NameEntity>()
                .ForMember(entity => entity.Id, expression => expression.Ignore());
        }
    }
}