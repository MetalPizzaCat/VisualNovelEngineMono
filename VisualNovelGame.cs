#define PERFORM_DIALOG_DUMP

/**
* A simple visual novel type of thing to try out monogame 
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

using UI;
namespace VisualNovelMono;

public class VisualNovelGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<UserInterfaceElement> _ui = new List<UserInterfaceElement>();
    Texture2D? testTexture;
    private StateManager _stateManager;

    private Dialog _dialog;
    private List<GameObject> _gameObjects = new List<GameObject>();

    public List<GameObject> GameObjects => _gameObjects;

    private bool _leftMouseButtonPressed = false;
    public void AddUiElement(UserInterfaceElement elem, bool init = false)
    {
        if (init)
        {
            elem.LoadContent(Content);
        }
        _ui.Add(elem);
    }

    public VisualNovelGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        DialogParser parser = new DialogParser("./test.diag");
        _stateManager = new StateManager();
        _dialog = parser.ParseDialog(this);
        _dialog.Game = this;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _dialog.AdvanceDialog(null);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        testTexture = Content.Load<Texture2D>("mikeisilliconl");

        Button exitButton = new Button(this, new Vector2(0, 100), new Vector2(64, 64), new Rectangle(0, 0, 32, 32));
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
        _ui.Add(exitButton);
        foreach (UserInterfaceElement elem in _ui)
        {
            elem.LoadContent(Content);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        MouseState mouse = Mouse.GetState();
        if (mouse.LeftButton == ButtonState.Pressed && !_leftMouseButtonPressed)
        {
            _leftMouseButtonPressed = true;
            foreach (UserInterfaceElement elem in _ui)
            {
                if (
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
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        _spriteBatch.Draw(testTexture, new Vector2(0, 0), Color.White);
        foreach (UserInterfaceElement elem in _ui)
        {
            elem.Draw(_spriteBatch);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
