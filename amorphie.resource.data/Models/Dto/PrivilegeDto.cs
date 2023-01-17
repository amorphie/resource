public record GetPrivilegeResponse
    (
        Guid id,
        string name,
        int enabled,
        DateTime? createdDate,
        DateTime? updatedDate,
        string createdUser,
        string updatedUser
     );

public record SavePrivilegeRequest
    (
        string name,
        int enabled,
        DateTime? createdDate,
        DateTime? updatedDate,
        string createdUser,
        string updatedUser
     );