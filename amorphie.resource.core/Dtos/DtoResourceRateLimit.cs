public record GetResourceRateLimitResponse
    (
        Guid id,
        Guid resourceId,
        string? scope,
        string? condition,
        string? cron,
        int limit,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );

public record SaveResourceRateLimitRequest
    (
        Guid id,
        Guid resourceId,
        string? scope,
        string? condition,
        string? cron,
        int limit,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );