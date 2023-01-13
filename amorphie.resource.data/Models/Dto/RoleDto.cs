public record GetRoleResponse
    (
        Guid id,
        string name,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );

public record SaveRoleRequest
    (
        string name,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );