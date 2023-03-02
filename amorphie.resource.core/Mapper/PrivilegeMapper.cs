using AutoMapper;

class PrivilegeMapper : Profile
{
    public PrivilegeMapper()
    {
        CreateMap<Privilege, DtoPrivilege>().ReverseMap();
        CreateMap<DtoSavePrivilegeRequest, Privilege>();
    }
}