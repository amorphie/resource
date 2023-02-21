public class Translation : BaseDbEntityWithId 
{
    public string Language { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}