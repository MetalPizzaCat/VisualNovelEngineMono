/**
* Dialog action is the data type that only holds data that will be processed by dialog blocks
*/
namespace DialogSystem;

/// <summary>
/// Defines every action that can be performed by the dialog action system
/// </summary>
public enum DialogActionType
{
    /// <summary>
    /// Action to display text on screen
    /// </summary>
    Text,
    /// <summary>
    /// Change speaker position
    /// </summary>
    SpeakerMove,
    /// <summary>
    /// Change speaker's texture
    /// </summary>
    SpeakerStateChange,
    /// <summary>
    /// Perform option
    /// </summary>
    Option,
    /// <summary>
    /// Jump to other block in the dialog
    /// </summary>
    Jump,
    /// <summary>
    /// Any action that involves variable modification
    /// </summary>
    Variable,
    /// <summary>
    /// Exit game
    /// </summary>
    Exit
}

/// <summary>
/// Base class for all actions
/// </summary>
public abstract class DialogActionBase
{
    protected DialogActionType _type;
    public DialogActionType Action => _type;
}