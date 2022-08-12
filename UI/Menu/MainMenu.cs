using Microsoft.Xna.Framework;

using UI;

using VisualNovelMono;

public class MainMenu : UserInterfaceElement
{
    private Button _exitButton;
    private Button _continueButton;
    private Button _saveButton;
    private Button _loadButton;

    public MainMenu(Vector2 position, Vector2 size, VisualNovelGame game) : base(position, size, game)
    {
    
    }
}