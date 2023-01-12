public static class ModuleHelper
{
    public static void PreUpdate(string data, string? existingRecord, ref bool hasChanges)
    {
        if (data != null && data != existingRecord)
        {
            existingRecord = data; hasChanges = true;
        }
    }
}