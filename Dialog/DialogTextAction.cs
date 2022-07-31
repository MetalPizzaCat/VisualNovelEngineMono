using System.Collections.Generic;

using Microsoft.Xna.Framework;

using UI;

public class DialogTextAction : DialogActionBase
{
    /// <summary>
    /// Defines what happens once dialog is finished
    /// </summary>
    public readonly DialogEventType EndEventType = DialogEventType.Exit;
    public List<DialogText> Text { get; set; } = new List<DialogText>();
    private Label _textLabel;

    private int _currentTextId = 0;

    public DialogTextAction(Dialog dialog) { }
}