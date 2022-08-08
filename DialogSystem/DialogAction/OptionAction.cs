namespace DialogSystem;

public class OptionAction : DialogActionBase
{
    /// <summary>
    /// Where will the dialog jump to when this option is selected
    /// </summary>
    /// <value></value>
    public string JumpLocation { get; set; }

    public string Text { get; set; }

    public OptionAction(string text, string jumpLocation)
    {
        _type = DialogActionType.Option;
        JumpLocation = jumpLocation;
        Text = text;
    }
}