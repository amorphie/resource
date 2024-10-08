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

        CreateMap<ResourceGroupRole, DtoResourceGroupRole>().ReverseMap();

        CreateMap<Role, DtoRole>().ReverseMap();
        CreateMap<RoleDefinition, DtoRoleDefinition>().ReverseMap();

        CreateMap<RoleGroup, DtoRoleGroup>().ReverseMap();

        CreateMap<RoleGroupRole, DtoRoleGroupRole>().ReverseMap();

        CreateMap<Scope, DtoScope>().ReverseMap();

        CreateMap<ResourcePrivilege, DtoResourcePrivilege>().ReverseMap();

        CreateMap<ResponseTransformation, DtoResponseTransformation>().ReverseMap();
        CreateMap<ResponseTransformation, DtoGetResponseTransformation>();

        CreateMap<ResponseTransformationMessage, DtoResponseTransformationMessage>().ReverseMap();
        CreateMap<ResponseTransformationMessage, DtoGetResponseTransformationMessage>();

        CreateMap<ResourceGroup, DtoResourceGroup>().ReverseMap();

        CreateMap<Rule, DtoRule>().ReverseMap();

        CreateMap<ResourceRule, DtoResourceRule>().ReverseMap();
    }
}