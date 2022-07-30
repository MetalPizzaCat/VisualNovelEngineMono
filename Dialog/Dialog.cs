using System.Collections.Generic;

/// <summary>
/// Represents the whole dialog tree
/// </summary>
public class Dialog
{
    /// <summary>
    /// All of the dialog text that will be present in the dialog
    /// </summary>
    public List<DialogActionBase> DialogItems { get; set; } = new List<DialogActionBase>();
    
    /// <summary>
    /// Everyone who speaks.
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();

    private int _currentDialogIndex = 0;
    public int CurrentDialogIndex => _currentDialogIndex;

    public void AdvanceDialog(int nextDialogPosition)
    {
        _currentDialogIndex = 0;
    }
}