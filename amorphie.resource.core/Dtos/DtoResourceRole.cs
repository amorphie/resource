using amorphie.core.Base;

public class DtoResourceRole : DtoBase
{
    public Guid ResourceId { get; set; }
    public Guid RoleId { get; set; }    
    public string? Status { get; set; }   
}