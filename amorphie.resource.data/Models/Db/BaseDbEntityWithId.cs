public class BaseDbEntityWithId : BaseDbEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}