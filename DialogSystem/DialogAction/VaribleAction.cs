namespace DialogSystem;

public enum VariableOperation
{
    Assignment,
    Addition
}
public class VariableAction : DialogActionBase
{
    public VariableOperation Operation = VariableOperation.Assignment;
    public string VariableName;
    public DialogVariable Value;

    public VariableAction(string name, DialogVariable value, VariableOperation operation)
    {
        _type = DialogActionType.Variable;
        VariableName = name;
        Value = value;
        Operation = operation;
    }
}