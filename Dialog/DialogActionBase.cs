
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
/// Base item for all of the actions that can happen in the dialog<br/>
/// This handles both creation and freeing of the game objects
/// that are necessary for the action to happen
/// </summary>
public class DialogActionBase
{
    /// <summary>
    /// Dialog object that stores all of the necessary info
    /// </summary>
    protected Dialog Dialog;

    public DialogActionBase(Dialog dialog)
    {
        Dialog = dialog;
    }
    /// <summary>
    /// Create all of the necessary buttons 
    /// </summary>
    public virtual void Init() { }
    /// <summary>
    /// Clear all of the items created by this action to allow next item to be displayed properly
    /// </summary>
    public virtual void Clear() { }
}
