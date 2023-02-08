public record GetResourceLanguageResponse
    (
        Guid id,
        string? tableName,
        Guid rowId,
        string? fieldName,
        string? text,
        string? languageCode,
        int order,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );

public record SaveResourceLanguageRequest
    (
        Guid id,
        string? tableName,
        Guid rowId,
        string? fieldName,
        string? text,
        string? languageCode,
        int order,
        string? status,
        DateTime? createdAt,
        DateTime? modifiedAt,
        Guid? createdBy,
        Guid? modifiedBy,
        Guid? createdByBehalfOf,
        Guid? modifiedByBehalfOf
     );