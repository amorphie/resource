public record GetResourceResponse
    (
        Guid id,
        Translation[] displayNames,
        HttpMethodType type,
        string? url,
        Translation[] descriptions,
        string[]? tags,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );

public record SaveResourceRequest
    (
        Guid id,
        Translation[] displayNames,
        HttpMethodType type,
        string? url,
        Translation[] descriptions,
        string[]? tags,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );