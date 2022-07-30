public class DialogSceneChangeAction : DialogActionBase
{
    public string NextSceneName { get; set; } = "default";

    public DialogSceneChangeAction(Dialog dialog) : base(dialog) { }
}