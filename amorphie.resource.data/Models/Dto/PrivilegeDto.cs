public record GetPrivilegeResponse
    (
        Guid id,
        Guid resourceId,
        string? url,
        int ttl,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );

public record SavePrivilegeRequest
    (
        Guid id,
        Guid resourceId,
        string? url,
        int ttl,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );