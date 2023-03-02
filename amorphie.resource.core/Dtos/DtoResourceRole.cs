public record GetResourceRoleResponse
    (
        Guid id,
        Guid resourceId,
        Guid roleId,
        string? status,  
        DateTime? createdAt, 
        DateTime? modifiedAt, 
        Guid? createdBy, 
        Guid? modifiedBy, 
        Guid? createdByBehalfOf, 
        Guid? modifiedByBehalfOf 
     );

public record SaveResourceRoleRequest
    (
        Guid id,
        Guid resourceId,
        Guid roleId,
        string? status,  
        DateTime? createdAt, 
        DateTime? modifiedAt, 
        Guid? createdBy, 
        Guid? modifiedBy, 
        Guid? createdByBehalfOf, 
        Guid? modifiedByBehalfOf
     );