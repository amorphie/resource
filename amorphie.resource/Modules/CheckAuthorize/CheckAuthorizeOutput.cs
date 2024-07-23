namespace amorphie.resource.Modules.CheckAuthorize;

public class CheckAuthorizeOutput(string reason)
{
    public string Reason { get; set; } = reason;
}
