public record GetResourceResponse
    (
        Guid id, 
        string? displayName,
        HttpMethodType? type,
        string? url,
        string? description,
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
        HttpMethodType? type,
        string? url,
        string? description,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,        
        Guid? createdBy, 
        Guid? modifiedBy,
        Guid? createdByBehalfOf, 
        Guid? modifiedByBehalfOf
     );