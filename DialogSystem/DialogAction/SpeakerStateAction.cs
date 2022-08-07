namespace DialogSystem;


/// <summary>
/// Action that will be performed on one of the spawned speakers
/// </summary>
public class SpeakerStateAction : DialogActionBase
{
    public string Target { get; set; } = string.Empty;

    public string TargetState;

    public SpeakerStateAction(string target, string state)
    {
        _type = DialogActionType.SpeakerStateChange;
        Target = target;
        TargetState = state;
    }
}