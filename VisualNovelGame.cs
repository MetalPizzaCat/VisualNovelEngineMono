/**
* A simple visual novel type of thing to try out monogame 
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace VisualNovelMono;


public class VisualNovelGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    private List<UserInterfaceElement> _ui = new List<UserInterfaceElement>();

    private Texture2D _buttonTexture;
    Texture2D testTexture;

    public VisualNovelGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        testTexture = Content.Load<Texture2D>("mikeisilliconl");
        _buttonTexture = Content.Load<Texture2D>("buttons_small");

        Button exitButton = new Button(new Vector2(0, 100), new Vector2(64, 64), _buttonTexture, new Rectangle(0, 0, 32, 32));
        exitButton.OnClicked += () =>
        {
            System.Console.WriteLine("You clicked me!");
        };

        _ui.Add(exitButton);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

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
                else
                {
                    System.Console.WriteLine($"elem.Position.X >= mouse.X = {elem.Position.X >= mouse.X}");
                    System.Console.WriteLine($"elem.Position.Y >= mouse.Y = {elem.Position.Y >= mouse.Y}");
                    System.Console.WriteLine($"(elem.BoundingBoxSize.X + elem.Position.X) >= mouse.X = {(elem.BoundingBoxSize.X + elem.Position.X) >= mouse.X}");
                    System.Console.WriteLine($"e(elem.BoundingBoxSize.Y + elem.Position.Y) >= mouse.Y = {(elem.BoundingBoxSize.Y + elem.Position.Y) >= mouse.Y}");
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
