using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Direct2D1;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;

namespace MCoding1
{
    public class Test1 : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;
        internal KeyboardManager keyboardManager;
        internal MouseManager mouseManager;
        //Helpers.FPSMonitor fpsMonitor;

        internal BlockScene scene;
        //internal GameState State;

        //Texture2D pointsTexture;
        //internal int points;
        //internal int timeElapsed = 0;

        public Test1()
        {
            this.graphicsDeviceManager = new GraphicsDeviceManager(this);
            this.graphicsDeviceManager.PreferredBackBufferWidth = 480;
            this.graphicsDeviceManager.PreferredBackBufferHeight = 800;

            

            this.Content.RootDirectory = "Content";
            base.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            //this.fpsMonitor = new Helpers.FPSMonitor();

            this.keyboardManager = new KeyboardManager(this);
            this.mouseManager = new MouseManager(this);

            //this.pointsTexture = this.Content.Load<Texture2D>("points");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            scene = BlockScene.Read("Content/testLevel", this);
            scene.Initialize();

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.BeginDraw();
            this.spriteBatch.Begin();

            //this.spriteBatch.Draw(this.Content.Load<Texture2D>("background"), new RectangleF(0, 0, this.GraphicsDevice.BackBuffer.Width, this.GraphicsDevice.BackBuffer.Height), Color.White);

            this.scene.Draw(gameTime);
            //this.spriteBatch.DrawString(this.Content.Load<SpriteFont>("Arial7"), ("FPS: " + this.fpsMonitor.FPS.ToString()), new Vector2(0, 0), Color.Black);

            /*if (!this.scene.suspendUpdate)
            {
                this.timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (this.timeElapsed >= 1000)
                {
                    this.points++;
                    this.timeElapsed = 0;
                }
            }*/
            
            //this.spriteBatch.Draw(this.pointsTexture, new RectangleF(this.Window.ClientBounds.Width - this.pointsTexture.Width, 0, this.pointsTexture.Width, this.pointsTexture.Height), Color.White);
            //this.spriteBatch.DrawString(this.Content.Load<SpriteFont>("Arial13"), string.Format("{0}", this.points), new Vector2((this.Window.ClientBounds.Width - this.pointsTexture.Width) + 80, 30), Color.White);

            this.spriteBatch.End();
            base.EndDraw();
            base.Draw(gameTime);

            System.Windows.Forms.Application.DoEvents();
        }

        protected override void Update(GameTime gameTime)
        {
            //this.fpsMonitor.Update();
            this.scene.Update(gameTime);
            base.Update(gameTime);
        }

        public enum GameState
        {
            Menu,
            Game_Start,
            Game_Pause
        }
    }
}
