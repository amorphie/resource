using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using amorphie.core.Enums;

public class Privilege : EntityBase
{
    public string? Url { get; set; }
    public HttpMethodType Type { get; set; }
}