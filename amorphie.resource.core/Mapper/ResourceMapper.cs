using amorphie.core.Base;
using AutoMapper;

public class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<Resource, DtoResource>().ReverseMap();
        CreateMap<DtoSaveResourceRequest, Resource>();

        CreateMap<Translation, MultilanguageText>().ReverseMap();

        CreateMap<Privilege, DtoPrivilege>().ReverseMap();
        CreateMap<DtoSavePrivilegeRequest, Privilege>();

        CreateMap<ResourceRateLimit, DtoResourceRateLimit>().ReverseMap();
        CreateMap<DtoSaveResourceRateLimitRequest, ResourceRateLimit>();

        CreateMap<ResourceRole, DtoResourceRole>().ReverseMap();
        CreateMap<DtoSaveResourceRoleRequest, ResourceRole>();

        CreateMap<Role, DtoRole>().ReverseMap();
        CreateMap<DtoSaveRoleRequest, Role>().ReverseMap();

        CreateMap<RoleGroup, DtoRoleGroup>().ReverseMap();
        CreateMap<DtoSaveRoleGroupRequest, RoleGroup>();

        CreateMap<RoleGroupRole, DtoRoleGroupRole>().ReverseMap();
        CreateMap<DtoSaveRoleGroupRoleRequest, RoleGroupRole>();

        CreateMap<Scope, DtoScope>().ReverseMap();
        CreateMap<DtoSaveScopeRequest, Scope>();
    }
}