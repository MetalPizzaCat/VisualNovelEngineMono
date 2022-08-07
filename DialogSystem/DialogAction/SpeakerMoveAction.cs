namespace DialogSystem;


/// <summary>
/// Action that will be performed on one of the spawned speakers
/// </summary>
public class SpeakerMoveAction : DialogActionBase
{
    public string Target { get; set; } = string.Empty;

    public SpeakerPosition TargetPosition = SpeakerPosition.Offscreen;

    public SpeakerMoveAction(string target, SpeakerPosition position)
    {
        Target = target;
        TargetPosition = position;
        _type = DialogActionType.SpeakerMove;
    }
}