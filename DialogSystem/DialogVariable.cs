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

    public DialogVariable(float value)
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

    /// <summary>
    /// Compares two values depending on which type they are
    /// </summary>
    /// <param name="variable"></param>
    /// <returns>
    /// 0 if equal, 1 if greater then, -1 if less then for int<br/>
    /// and 0 if equal and -1 if not equal for string
    /// </returns>
    public int Compare(DialogVariable variable)
    {
        if (variable.VariableType != VariableType)
        {
            throw new System.Exception("Dialog variable types need to have matching data type. String can not be compared to number");
        }
        switch (VariableType)
        {
            case DialogVariableType.String:
                return variable.StringValue == StringValue ? 0 : -1;
            case DialogVariableType.Number:
                return NumberValue == variable.NumberValue ? 0 :
                NumberValue > variable.NumberValue ? 1 : -1;
            default:
                throw new System.Exception("Invalid operation reached");
        }
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