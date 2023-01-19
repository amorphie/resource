public record GetResourceRoleResponse
    (
        Guid id,
        Guid ResourceId,
        Guid RoleId,
        DateTime? createdDate,
        string createdUser
     );

public record SaveResourceRoleRequest
    (
        Guid ResourceId,
        Guid RoleId,
        DateTime? createdDate,
        string createdUser
     );