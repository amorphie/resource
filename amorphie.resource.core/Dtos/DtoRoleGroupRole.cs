public record GetRoleGroupRoleResponse
    (
        Guid id,
        Guid RoleGroupId,
        Guid RoleId,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );

public record SaveRoleGroupRoleRequest
    (
        Guid id,
        Guid RoleGroupId,
        Guid RoleId,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );