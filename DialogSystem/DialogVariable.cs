namespace DialogSystem;

public enum DialogVariableType
{
    String,
    Number,
}

public class DialogVariable
{
    public DialogVariableType VariableType;
    public string Name { get; set; }

    public float? NumberValue;
    public string? StringValue;

    public DialogVariable(string name, string value)
    {
        VariableType = DialogVariableType.String;
        StringValue = value;
        Name = name;
    }

    public DialogVariable(string name, float value)
    {
        VariableType = DialogVariableType.Number;
        NumberValue = value;
        Name = name;
    }

    public override string ToString()
    {
        switch (VariableType)
        {
            case DialogVariableType.String:
                return StringValue;
            case DialogVariableType.Number:
                return NumberValue.ToString();
        }
        return "%MISSING VALUE%";
    }
}