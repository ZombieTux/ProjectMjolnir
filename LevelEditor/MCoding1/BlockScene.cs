using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct2D1;
using System.Xml;
using SharpDX.Toolkit.Input;

namespace MCoding1
{
    public class BlockScene
    {
        //public List<BlockLayer> Layer;
        public BlockLayer Layer;
        public int currentLayer;

        //public MovingBlock Player;

        internal Test1 game;
        internal int blocks_x = 15;
        internal int blocks_y = 15;
        internal bool suspendUpdate = false;
        internal Vector2 gravity = new Vector2(0, 0.35f);
        internal float movementSpeed = 2f;

        internal int block_size
        {
            get
            {
                //if ((this.game.Window.ClientBounds.Size.Height / this.blocks_y) > (this.game.Window.ClientBounds.Size.Width / this.blocks_x))
                //{
                //    //return (this.game.Window.ClientBounds.Size.Width / (this.blocks_x + 0));
                //    return (this.game.Window.ClientBounds.Size.Width / this.blocks_y);
                //}
                //else
                //{
                    return (this.game.Window.ClientBounds.Size.Height / (this.blocks_y + 1));
                //}
            }
        }

        public BlockScene(Test1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            //TODO: Moar dynamic
            //for(int i = 0; i < this.Layer.Count; i++)
            //{
            //    BlockLayer layer = this.Layer[i];
            //    if(layer.IsMain)
            //    {
            //        this.currentLayer = i;

            //        break;
            //    }
            //}
        }

        public void Draw(GameTime gameTime)
        {
            this.Layer.Draw(gameTime);

            drawDebugLines();
            //drawDebugBlockInfo();
        }

        public void Update(GameTime gameTime)
        {
            processKeyboard();
            processMouse();
            if (suspendUpdate)
                return;
            //for (int i = 0; i < this.Layer.Count; i++)
            //{
            //    BlockLayer layer = this.Layer[i];

            //    if(!suspendUpdate)
            //        layer.Update(gameTime);
            //}
            //moveLayer();
            //this.Layer.Update(gameTime);
        }

        public void Restart()
        {
            //this.game.points = 0;
            //this.game.timeElapsed = 0;

            this.Layer.Position = new Vector2(2, 0);
            //((Block)this.Player).Position = new Point(0, (this.blocks_y / 2));
        }

        /*internal void moveLayer()
        {
            this.Layer.Position.X -= this.movementSpeed;
        }*/

        internal void processKeyboard()
        {
            KeyboardManager keyboardManager = this.game.keyboardManager;
            KeyboardState keyboardState = keyboardManager.GetState();
            //this.suspendUpdate = false;
        }

        internal void processMouse()
        {
            MouseManager mouseManager = this.game.mouseManager;
            MouseState mouseState = mouseManager.GetState();
            if (mouseState.LeftButton.Down&&this.Layer.Blocks[(int)(mouseState.X * game.Window.ClientBounds.Width / this.block_size)][(int)(mouseState.Y * game.Window.ClientBounds.Height/this.block_size)]!=new Block(this.Layer, new Point((int)(mouseState.X * game.Window.ClientBounds.Width / this.block_size), (int)(mouseState.Y * game.Window.ClientBounds.Height/this.block_size)), Layer.scene.game.Content.Load<Texture2D>("pipe")))
            {
                this.Layer.Blocks[(int)(mouseState.X * game.Window.ClientBounds.Width / this.block_size)][(int)(mouseState.Y * game.Window.ClientBounds.Height/this.block_size)]=new Block(this.Layer, new Point((int)(mouseState.X * game.Window.ClientBounds.Width / this.block_size), (int)(mouseState.Y * game.Window.ClientBounds.Height/this.block_size)), Layer.scene.game.Content.Load<Texture2D>("pipe"));
            }

            if (mouseState.RightButton.Down)
            {
                this.Layer.Blocks[(int)(mouseState.X * game.Window.ClientBounds.Width / this.block_size)][(int)(mouseState.Y * game.Window.ClientBounds.Height / this.block_size)] = null;
            }

        }

        internal bool drawBlock(Block block, BlockLayer layer)
        {
            return drawBlock(block, layer, new Vector2(0, 0));
        }

        internal bool drawBlock(Block block, BlockLayer layer, Vector2 tposition)
        {
                RectangleF position = new RectangleF();
                position.X = ((block.Position.X * this.block_size) + tposition.X) + layer.Position.X;
                position.Y = (block.Position.Y * this.block_size) + tposition.Y;
                position.Height = this.block_size;
                position.Width = this.block_size;

                game.Services.GetService<SpriteBatch>().Draw(block.Texture, position, block.Tint);
                return true;
        }

        internal void drawDebugBlockInfo()
        {
            SpriteBatch spriteBatch = game.Services.GetService<SpriteBatch>();
            SpriteFont spriteFont = game.Content.Load<SpriteFont>("Arial13");
        }

        internal void drawDebugLines()
        {
            SpriteBatch spriteBatch = game.Services.GetService<SpriteBatch>();
            Texture2D debugLine = game.Content.Load<Texture2D>("debugLine");
            Color c = new ColorBGRA(50, 50, 50, 50);

            for (int x = 0; x < blocks_x; x++)
            {
                for (int y = 0; y < blocks_y; y++)
                {
                    // oben
                    spriteBatch.Draw(debugLine, new RectangleF(
                        (x * this.block_size) + this.Layer.Position.X, y * this.block_size,
                        this.block_size, 1), c);
                    // links
                    spriteBatch.Draw(debugLine, new RectangleF(
                        (x * this.block_size) + this.Layer.Position.X, y * this.block_size,
                        1, this.block_size), c);

                    if(y == (blocks_y - 1))
                    {
                        spriteBatch.Draw(debugLine, new RectangleF(
                            (x * this.block_size) + this.Layer.Position.X, (y + 1) * this.block_size,
                            this.block_size, 1), c);
                    }
                    if(x == (blocks_x - 1))
                    {
                        spriteBatch.Draw(debugLine, new RectangleF(
                            ((x + 1)) + this.Layer.Position.X * this.block_size, y * this.block_size,
                            1, this.block_size), c);
                    }
                }
            }
        }

        public static BlockScene Read(string Filepath, Test1 test)
        {
            if(!Filepath.EndsWith(".xml"))
            {
                string[] split = Filepath.Split('/');
                string folderName = split[split.Length - 1];

                if (!Filepath.EndsWith("/"))
                    Filepath += "/";
                Filepath += (folderName + ".xml");
            }

            XmlDocument document = new System.Xml.XmlDocument();
            document.Load(Filepath);

            XmlNode levelElement = null;
            try
            {
                //levelElement = document.GetElementById("Level");
                levelElement = document.GetElementsByTagName("Level").Item(0);
            }
            catch
            {
                Console.WriteLine("[BlockScene] Read(string): Couldn´t load \"" + Filepath + "\"");
            }

            if(levelElement != null)
            {
                BlockScene returnScene = new BlockScene(test);

                foreach(XmlNode child in levelElement.ChildNodes)
                {
                    if(child.Name == "Info")
                    {
                        returnScene.blocks_x = int.Parse(child.Attributes["blocks_x"].Value);
                        returnScene.blocks_y = int.Parse(child.Attributes["blocks_y"].Value);

                        try
                        {
                            float gravity = float.Parse(child.Attributes["gravity"].Value);
                            returnScene.gravity = new Vector2(0, gravity);
                        }
                        catch { }
                    }
                    else if(child.Name == "Layers")
                    {
                        //returnScene.Layer = new List<BlockLayer>();
                        for (int i = 0; i < child.ChildNodes.Count; i++)
                        {
                            XmlNode xmllayer = child.ChildNodes[i];

                            BlockLayer layer = BlockLayer.Read(xmllayer, returnScene);
                            //returnScene.Layer.Add(layer);
                            returnScene.Layer = layer;
                        }
                    }
                }

                return returnScene;
            }

            return null;
        }

    }
}
