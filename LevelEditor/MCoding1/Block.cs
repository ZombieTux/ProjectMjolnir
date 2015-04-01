using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace MCoding1
{
    public class Block
    {
        /// <summary>
        /// Relative Position (blocks)
        /// </summary>
        public Point Position;

        public Texture2D Texture;
        public Color Tint = Color.White;
        public bool hasCollision;
        public bool causeDeath = true;
        public string Name;
        public BlockLayer layer;

        public Block(BlockLayer layer, Point position, Texture2D texture, bool hasCollision = false, string name = null)
        {
            this.layer = layer;
            this.Position = position;
            this.Texture = texture;

            this.hasCollision = hasCollision;
            this.Name = name;
        }

        public static Block Read(System.Xml.XmlNode node, BlockLayer layer)
        {
            if(node.Name.StartsWith("Block"))
            {
                int x = int.Parse(node.Attributes["x"].Value);
                int y = int.Parse(node.Attributes["y"].Value);
                Texture2D texture = null;

                if(Statics.Functions.nodeHasAttribute(node, "texture"))
                {
                    if (layer.scene.game.Content.Exists(node.Attributes["texture"].Value))
                    {
                        texture = (Texture2D)layer.scene.game.Content.Load(typeof(Texture2D), node.Attributes["texture"].Value);
                    }
                    else
                    {
                        texture = Texture2D.New(layer.scene.game.GraphicsDevice, Image.Load(node.Attributes["texture"].Value));
                    }
                }

                bool hascollision = false;
                string name = null;

                try
                {
                    hascollision = bool.Parse(node.Attributes["hasCollision"].Value);
                }
                catch { }
                try
                {
                    name = node.Attributes["name"].Value;
                }
                catch { }

                if (node.Name.Length > ("Block").Length)
                {
                    int seperator = node.Name.IndexOf(".");
                    string subclass = node.Name.Substring(seperator + 1, (node.Name.Length - (seperator + 1)));

                    //TODO: MOAR dynamic
                    //Type subclassType = typeof(Block).GetNestedType(subclass);
                    //Block subclassBlock = (Block)Helpers.DynamicInitializer.NewInstance(subclassType);
                    if(subclass == "Ground")
                    {
                        Ground groundBlock = new Ground(layer, new Point(x, y), texture, name);
                        return groundBlock;
                    }
                    else if(subclass == "Pipe")
                    {
                        Pipe pipeBlock = new Pipe(layer, new Point(x, y), texture, name);
                        return pipeBlock;
                    }
                }
                else
                {
                    Block returnBlock = new Block(layer, new Point(x, y), texture, hascollision, name);
                    return returnBlock;
                }
            }

            return null;
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", this.Position.X, this.Position.Y);
        }

        public class Ground : Block
        {
            public Ground(BlockLayer layer, Point position, Texture2D texture, string name = null)
                :base(layer, position, texture, true, name)
            {
                if (texture == null)
                    base.Texture = layer.scene.game.Content.Load<Texture2D>("ground");
            }
        }

        public class Pipe : Block
        {
            public Pipe(BlockLayer layer, Point position, Texture2D texture, string name = null)
                :base(layer, position, texture, true, name)
            {
                if (texture == null)
                    base.Texture = layer.scene.game.Content.Load<Texture2D>("pipe");
            }
        }

    }
}
