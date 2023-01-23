public record GetResourceRateLimitResponse
    (
        Guid id,
        Guid ResourceId,
        Guid RoleId,
        int Period,
        int Limit,
        int Enabled,
        DateTime? createdDate,
        DateTime? updatedDate,
        string createdUser,
        string updatedUser
     );

public record SaveResourceRateLimitRequest
    (
        Guid ResourceId,
        Guid RoleId,
        int Period,
        int Limit,
        int Enabled,
        DateTime? createdDate,
        DateTime? updatedDate,
        string createdUser,
        string updatedUser
     );