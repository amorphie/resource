public class WorkflowRuleDefinition 
{
    public string WorkflowName { get; set; } = default!;
    public RuleDefinition[] Rules { get; set; } = default!;  
}

public class RuleDefinition 
{
    public string RuleName { get; set; } = default!;
     public string Expression { get; set; } = default!;  
}