using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Represents the whole dialog tree
/// </summary>
public class Dialog
{
    public VisualNovelMono.VisualNovelGame Game;

    /// <summary>
    /// Key value pair where key is the dialog label and value 
    /// is the action that will happen when dialog jumps to that label
    /// </summary>
    public Dictionary<string, DialogActionBase> DialogItems { get; set; } = new Dictionary<string, DialogActionBase>();

    /// <summary>
    /// Everyone who speaks.
    /// </summary>
    public List<Speaker> Speakers { get; set; } = new List<Speaker>();

    private string? _currentLabel;
    public string? CurrentLabel => _currentLabel;

    public void AdvanceDialog(string? newLabel)
    {
        _currentLabel = newLabel ?? DialogItems.Keys.First();
        DialogItems[_currentLabel].Init();
    }

    public Dialog(VisualNovelMono.VisualNovelGame game)
    {
        Game = game;
    }
}