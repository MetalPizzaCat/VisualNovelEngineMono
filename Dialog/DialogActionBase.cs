
/// <summary>
/// Defines what event will happen once dialog reaches event label
/// </summary>
public enum DialogEventType
{
    /// <summary>
    /// Dialog will jump to a new label
    /// </summary>
    Jump,
    /// <summary>
    /// The game will close
    /// TODO: Add end screen support
    /// </summary>
    Exit
}

public class DialogActionBase
{
    public string Label { get; set; } = "rick_roll";

    public DialogActionBase(string label)
    {
        Label = label;
    }

    public DialogActionBase() { }
}
