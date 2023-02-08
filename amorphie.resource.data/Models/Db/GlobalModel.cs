using System.ComponentModel.DataAnnotations;

// public class MultiLanguageText
// {
//     public MultiLanguageText(string language, string label)
//     {
//         Id = Guid.NewGuid();
//         Language = language;
//         Label = label;
//     }

//     [Key]
//     public Guid Id { get; set; }
//     public string? Language { get; set; }
//     public string? Label { get; set; }
// }


public enum HttpMethodType
{
    CONNECT,
    DELETE,
    GET,
    HEAD,
    OPTIONS,
    POST,
    PUT,
    TRACE
}