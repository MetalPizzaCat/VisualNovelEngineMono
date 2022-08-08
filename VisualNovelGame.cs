#define PERFORM_DIALOG_DUMP

/**
* A simple visual novel type of thing to try out monogame 
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

using UI;
using DialogSystem;
namespace VisualNovelMono;

public class VisualNovelGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    /// <summary>
    /// All of the ui elements in the game
    /// </summary>
    private List<UserInterfaceElement> _ui = new List<UserInterfaceElement>();
    /// <summary>
    /// Array for storing newly added elements, that will be appended to main array at the end of the frame<br/>
    /// Due to design of this engine, this array acts more like actual main array for the game
    /// </summary>
    private List<UserInterfaceElement> _uiStaging = new List<UserInterfaceElement>();
    private GameState _currentState = GameState.Normal;

    public GameState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
        }
    }

    /// <summary>
    /// State of keyboard in the previous state
    /// </summary>
    private KeyboardState _previousKeyState;

    /// <summary>
    /// Current dialog
    /// </summary>
    private Dialog _dialog;
    /// <summary>
    /// All of game objects present
    /// </summary>
    private List<GameObject> _gameObjects = new List<GameObject>();

    public List<GameObject> GameObjects => _gameObjects;

    private bool _leftMouseButtonPressed = false;

    /// <summary>
    /// Scale of the window<br/>
    /// Used for window resizing
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;
    public void AddUiElement(UserInterfaceElement elem, bool init = true)
    {
        if (init)
        {
            elem.LoadContent(Content);
            elem.Init();
        }
        _uiStaging.Add(elem);
    }

    public VisualNovelGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Window.AllowAltF4 = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;

    }

    protected override void Initialize()
    {
        base.Initialize();
        _dialog.AdvanceDialog(null);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Button exitButton = new Button(this, new Vector2(0, 300), new Vector2(64, 64), new Rectangle(0, 0, 32, 32));
        exitButton.OnClicked += (UserInterfaceElement sender) =>
        {
            System.Console.WriteLine("You clicked me!");
#if PERFORM_DIALOG_DUMP
            //dump the dialog 
            //for debug purposes
            System.IO.File.WriteAllText("./.temp/dialog.json", Newtonsoft.Json.JsonConvert.SerializeObject(_dialog));
#endif
            Exit();
        };
        AddUiElement(exitButton, true);

        DialogParser parser = new DialogParser("./test.diag");
        _dialog = parser.ParseDialog(this);
        _dialog.Game = this;
        _dialog.SceneSize = new Vector2(1280, 720);
        _dialog.Init();
    }

    public void ChangeWindowSize(Vector2 scale)
    {
        _graphics.PreferredBackBufferWidth = (int)(1280 * scale.X);
        _graphics.PreferredBackBufferHeight = (int)(720 * scale.Y);
        Scale = scale;
        _graphics.ApplyChanges();
    }

    private void _inputKeyboard(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();

        if (keyboard.IsKeyDown(Keys.Add) && _previousKeyState.IsKeyUp(Keys.Add))
        {
            ChangeWindowSize(Scale + new Vector2(0.25f, 0.25f));
        }
        if (keyboard.IsKeyDown(Keys.Subtract) && _previousKeyState.IsKeyUp(Keys.Subtract))
        {
            ChangeWindowSize(Scale - new Vector2(0.25f, 0.25f));
        }
        _previousKeyState = keyboard;
    }

    private void _updateObjects(GameTime gameTime)
    {
        foreach (UserInterfaceElement elem in _ui)
        {
            elem.Update(gameTime);
        }
    }

    private void _inputMouse(GameTime gameTime)
    {
        MouseState mouse = Mouse.GetState();
        if (mouse.LeftButton == ButtonState.Pressed && !_leftMouseButtonPressed)
        {
            _leftMouseButtonPressed = true;
            foreach (UserInterfaceElement elem in _ui)
            {
                if
                (
                    elem.Position.X <= mouse.X &&
                    elem.Position.Y <= mouse.Y &&
                    (elem.BoundingBoxSize.X + elem.Position.X) >= mouse.X &&
                    (elem.BoundingBoxSize.Y + elem.Position.Y) >= mouse.Y
                )
                {
                    elem.Click();
                }
            }
        }
        else if (mouse.LeftButton == ButtonState.Released && _leftMouseButtonPressed)
        {
            _leftMouseButtonPressed = false;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        _updateObjects(gameTime);
        _inputKeyboard(gameTime);
        _inputMouse(gameTime);

        if (_uiStaging.Count > 0)
        {
            _ui.AddRange(_uiStaging);
            _uiStaging.Clear();
        }

        //clear dead ones
        for (int i = _ui.Count - 1; i >= 0; i--)
        {
            if (!_ui[i].Valid)
            {
                _ui.RemoveAt(i);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

        foreach (UserInterfaceElement elem in _ui)
        {
            elem.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
