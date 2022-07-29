using System.Collections.Generic;

/// <summary>
/// Represents the whole dialog tree
/// </summary>
public class Dialog
{
    /// <summary>
    /// All of the dialog text that will be present in the dialog
    /// </summary>
    public List<DialogText> DialogTexts { get; set; } = new List<DialogText>();
    /// <summary>
    /// Everyone who speaks.
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();//Speaker because speaking. Get it? Shut up i'm funny

    private int _currentDialogIndex = 0;
    public int CurrentDialogIndex => _currentDialogIndex;
}