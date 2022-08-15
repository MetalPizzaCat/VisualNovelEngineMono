namespace DialogSystem;

public enum ConditionType
{
    Equal,
    LessThen,
    GreaterThen,
    NotEqual
}
/// <summary>
/// Conditional jump action works in the same way as jump action but only executes if given condition is true
/// </summary>
public class ConditionalJumpAction : DialogSystem.DialogActionBase
{
    public ConditionalJumpAction(string destination, string variableName, ConditionType conditionType, DialogVariable? value, string? variableValue)
    {
        Destination = destination;
        VariableName = variableName;
        Value = value;
        VariableValue = variableValue;
        _type = DialogActionType.ConditionalJump;
        ConditionType = conditionType;
    }
    public ConditionType ConditionType { get; set; }
    public string Destination { get; set; }
    public string VariableName { get; set; }
    /// <summary>
    /// Constant value to compare against
    /// </summary>
    public DialogVariable? Value { get; set; }
    /// <summary>
    /// Name of the variable to compare against
    /// </summary>
    public string? VariableValue { get; set; }
}