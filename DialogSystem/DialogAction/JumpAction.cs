namespace DialogSystem;

public class JumpAction : DialogSystem.DialogActionBase
{
    public string Destination { get; set; }
    public JumpAction(string destination)
    {
        Destination = destination;
        _type = DialogActionType.Jump;
    }
}