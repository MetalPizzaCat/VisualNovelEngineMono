/**
* Dialog block handles logic behind various parts of interaction with player
*/
using UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DialogSystem;

/// <summary>
/// Dialog block represents all pieces of dialog that have logic.
/// Such text and option choosing block
/// </summary>
public abstract class DialogBlockBase : UserInterfaceElement
{
    public delegate void ActionEventHandler(DialogSystem.DialogActionBase action);

    /// <summary>
    /// Raised when dialog block requests any sort of action to be executed
    /// </summary>
    public event ActionEventHandler? OnActionEvent;
    /// <summary>
    /// All actions that can be processed by the block
    /// </summary>
    protected List<DialogSystem.DialogActionBase> Actions = new List<DialogActionBase>();
    
    protected void RaiseActionEvent(DialogSystem.DialogActionBase action)
    {
        OnActionEvent?.Invoke(action);
    }
    public DialogBlockBase(Vector2 position, Vector2 size, VisualNovelMono.VisualNovelGame game) : base(position, size, game) { }
}
