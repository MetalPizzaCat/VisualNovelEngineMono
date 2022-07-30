using System.Collections.Generic;

public class DialogTextAction : DialogActionBase
{
    /// <summary>
    /// Defines what happens once dialog is finished
    /// </summary>
    public readonly DialogEventType EndEventType = DialogEventType.Exit;
    public List<DialogText> Text { get; set; } = new List<DialogText>();

    public DialogTextAction() { }
}

public class SceneChangeAction : DialogActionBase
{
    public string NextSceneName { get; set; } = "default";

    public SceneChangeAction() { }
}

public class DialogText
{
    /// <summary>
    /// This id is used to reference proper speaker from the memory
    /// Leave this as null to speak as "narrator"(no speaker will be explicitly used)
    /// </summary>
    /// <value></value>
    public int? SpeakerId { get; set; }

    public string Text { get; set; } = "PLACEHOLDER_TEXT_123456789";
    public DialogText(string text, int? speakerId)
    {
        SpeakerId = speakerId;
        Text = text;
    }
}