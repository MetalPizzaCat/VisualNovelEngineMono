namespace DialogSystem;

public enum DialogVariableType
{
    String,
    Number,
}

public class DialogVariable
{
    public DialogVariableType VariableType;
    public float? NumberValue;
    public string? StringValue;

    public DialogVariable(string value)
    {
        VariableType = DialogVariableType.String;
        StringValue = value;
    }

    public DialogVariable( float value)
    {
        VariableType = DialogVariableType.Number;
        NumberValue = value;
    }

    public void Set(object value)
    {
        if (value is float number)
        {
            NumberValue = number;
        }
        if (value is string text)
        {
            StringValue = text;
        }
    }

    /// <summary>
    /// Returns value that is stored inside
    /// </summary>
    public object? Get()
    {
        switch (VariableType)
        {
            case DialogVariableType.Number:
                return NumberValue;
            case DialogVariableType.String:
                return StringValue;
        }
        return null;
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