using amorphie.core.Base;
using amorphie.core.Enums;

public record GetPrivilegeResponse
    (
        Guid Id,
        Guid ResourceId,
        int? Ttl,
        string? Status,
        DateTime? CreatedAt,
        DateTime? ModifiedAt,
        Guid? CreatedBy,
        Guid? ModifiedBy,
        Guid? CreatedByBehalfOf,
        Guid? ModifiedByBehalfOf
     );

public record SavePrivilegeRequest
    (
        Guid Id,
        Guid ResourceId,
        int? Ttl,
        string? Status,
        Guid? CreatedBy,
        Guid? ModifiedBy,
        Guid? CreatedByBehalfOf,
        Guid? ModifiedByBehalfOf
     );

public record GetResourceResponse
    (
    Guid Id,
    Translation[] DisplayNames,
    HttpMethodType Type,
    string? Url,
    Translation[] Descriptions,
    string[]? Tags,
    string? Status,
    DateTime? CreatedAt,
    DateTime? ModifiedAt,
    Guid? CreatedBy,
    Guid? ModifiedBy,
    Guid? CreatedByBehalfOf,
    Guid? ModifiedByBehalfOf
    );

public record SaveResourceRequest
    (
        Guid Id,
        Translation[] DisplayNames,
        HttpMethodType Type,
        string? Url,
        Translation[] Descriptions,
        string[]? Tags,
        string? Status,
        Guid? CreatedBy,
        Guid? ModifiedBy,
        Guid? CreatedByBehalfOf,
        Guid? ModifiedByBehalfOf
     );