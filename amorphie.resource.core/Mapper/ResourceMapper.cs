using AutoMapper;

class ResourceMapper : Profile
{
    public ResourceMapper()
    {
        CreateMap<Resource, DtoResource>().ReverseMap();
        CreateMap<DtoSaveResourceRequest, Resource>();
    }
}