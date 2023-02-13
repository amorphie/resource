public record GetResourceResponse
    (
        Guid id, 
        string? displayName,
        string? type,
        string? url,
        string? description,
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
        string? displayName,
        string? type,
        string? url,
        string? description,
        string[]? tags, 
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,        
        Guid? createdBy, 
        Guid? modifiedBy,
        Guid? createdByBehalfOf, 
        Guid? modifiedByBehalfOf
     );