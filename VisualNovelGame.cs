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

    public VisualNovelGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _stateManager = new StateManager();
        _dialog = Newtonsoft.Json.JsonConvert.DeserializeObject<Dialog>(System.IO.File.ReadAllText("./dialog.json"))
            ?? throw new System.NullReferenceException("Invalid dialog file");

        int i = 0;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        testTexture = Content.Load<Texture2D>("mikeisilliconl");

        Button exitButton = new Button(this, new Vector2(0, 100), new Vector2(64, 64), new Rectangle(0, 0, 32, 32));
        exitButton.OnClicked += () =>
        {
            System.Console.WriteLine("You clicked me!");
            System.IO.File.WriteAllText("./dialog.json", Newtonsoft.Json.JsonConvert.SerializeObject(_dialog));
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
        if (mouse.LeftButton == ButtonState.Pressed)
        {
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
