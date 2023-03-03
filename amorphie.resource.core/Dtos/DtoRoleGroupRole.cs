using amorphie.core.Base;

public class DtoRoleGroupRole : DtoBase
{    
    public Guid RoleGroupId { get; set; }
    
    public Guid RoleId { get; set; }

    public string? Status { get; set; }
}