public record GetRoleGroupRoleResponse
    (
        Guid id,
        Guid RoleGroupId,
        Guid RoleId,
        DateTime? createdDate,
        string createdUser
     );

public record SaveRoleGroupRoleRequest
    (
        Guid RoleGroupId,
        Guid RoleId,
        DateTime? createdDate,
        string createdUser
     );