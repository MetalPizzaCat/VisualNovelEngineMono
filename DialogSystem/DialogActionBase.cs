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
    /// <summary>
    /// Defines type of the action<br/>
    /// This must be same as the class as this is used for casting types
    /// </summary>
    protected DialogActionType _type;
    /// <summary>
    /// Defines if this action can be auto skipped, instead of waiting for player input
    /// </summary>
    protected bool _skippable = true;
    public bool Skippable => _skippable;
    public DialogActionType Action => _type;
}