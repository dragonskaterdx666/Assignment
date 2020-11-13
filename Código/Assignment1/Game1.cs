using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Assignment1
{
    public class Game1 : Game
    {
        #region variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Prism prism;
        private Axis3D axis;
        private int _height = 1;
        private int _sides = 3;
        #endregion

        #region Methods
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            prism = new Prism(GraphicsDevice, _height, _sides);
            axis = new Axis3D(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_sides > 3)
                {
                    _sides--;
                    prism = new Prism(GraphicsDevice, _height, _sides);
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_sides < 10)
                {
                    _sides++;
                    prism = new Prism(GraphicsDevice, _height, _sides);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //axis.Draw();
            prism.Draw();

            base.Draw(gameTime);
        }
        #endregion

    }
}
