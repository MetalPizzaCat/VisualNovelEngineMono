
/// <summary>
/// Defines what event will happen once dialog reaches event label
/// </summary>
public enum DialogEventType
{
    /// <summary>
    /// Dialog will jump to a new label
    /// </summary>
    Jump,
    /// <summary>
    /// The game will close
    /// TODO: Add end screen support
    /// </summary>
    Exit
}

/// <summary>
/// Enum that describes all of the possible dialog action types<br/>
/// Exists purely to simplify displaying new action in the dialog
/// </summary>
public enum DialogActionType
{
    None,
    Text,
    Option,
    SceneChange,
    SpeakerChange
}

/// <summary>
/// Base data class for storing dialog actions 
/// </summary>
public abstract class DialogActionBase
{
}
