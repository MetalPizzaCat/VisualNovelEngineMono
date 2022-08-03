namespace DialogSystem;

public class OptionAction : DialogActionBase
{
    /// <summary>
    /// Where, in the block action list, will block jump to
    /// </summary>
    public int JumpLocation = 0;

    public string Text { get; set; }

    public OptionAction(string text, int jumpLocation = 0)
    {
        _type = DialogActionType.Option;
        JumpLocation = jumpLocation;
        Text = text;
    }
}