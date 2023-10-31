using amorphie.core.Base;
using AutoMapper;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<Resource, DtoResource>().ReverseMap();

        CreateMap<Translation, MultilanguageText>().ReverseMap();

        CreateMap<Privilege, DtoPrivilege>().ReverseMap();

        CreateMap<ResourceRateLimit, DtoResourceRateLimit>().ReverseMap();

        CreateMap<ResourceRole, DtoResourceRole>().ReverseMap();

        CreateMap<Role, DtoRole>().ReverseMap();

        CreateMap<RoleGroup, DtoRoleGroup>().ReverseMap();

        CreateMap<RoleGroupRole, DtoRoleGroupRole>().ReverseMap();

        CreateMap<Scope, DtoScope>().ReverseMap();

        CreateMap<ResourcePrivilege, DtoResourcePrivilege>().ReverseMap();
    }
}