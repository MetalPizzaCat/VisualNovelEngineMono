using System.Collections.Generic;
public class DialogOptionAction : DialogActionBase
{
    public List<DialogOption> Options { get; set; } = new List<DialogOption>();

    public DialogOptionAction() { }
}

public class DialogOption
{
    public string Text { get; set; } = "PLACEHOLDER_OPTION_123456789";
    public string LabelToJumpTo { get; set; } = "default";
    public readonly DialogEventType EventType = DialogEventType.Exit;

    public DialogOption(DialogEventType eventType, string text, string jumpToLabel)
    {
        Text = text;
        EventType = eventType;
        LabelToJumpTo = jumpToLabel;
    }
}