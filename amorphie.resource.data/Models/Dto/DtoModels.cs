public record GetResourceResponse
    (
        Guid id, 
        string name,
        string displayName,
        string url,
        string description,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );

public record SaveResourceRequest
    (
        string name,
        string displayName,
        string url,
        string description,
        DateTime? createdDate,
        DateTime? updatedDate,
        int enabled,
        string createdUser,
        string updatedUser
     );