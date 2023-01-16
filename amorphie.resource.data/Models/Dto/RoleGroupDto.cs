public record GetRoleGroupResponse
    (
        Guid id,
        string name,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );

public record SaveRoleGroupRequest
    (
        string name,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );