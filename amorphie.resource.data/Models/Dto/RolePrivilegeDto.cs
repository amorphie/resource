public record GetRolePrivilegeResponse
    (
        Guid id,
        Guid RoleId,
        Guid PrivilegeId,
        DateTime? createdDate,
        string createdUser
     );

public record SaveRolePrivilegeRequest
    (
        Guid RoleId,
        Guid PrivilegeId,
        DateTime? createdDate,
        string createdUser
     );