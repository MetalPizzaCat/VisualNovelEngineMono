using System.Collections.Generic;
public class DialogOptionAction : DialogActionBase
{
    public List<DialogOption> Options { get; set; } = new List<DialogOption>();

    public DialogOptionAction(Dialog dialog) { }
}