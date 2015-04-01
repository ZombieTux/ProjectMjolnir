using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct2D1;
using System.Xml;
using SharpDX.XAudio2;
using SharpDX.Multimedia;

namespace MCoding1
{
    public class BlockLayer
    {
        public Vector2 Position = new Vector2(0, 0);
        Block[,] blocks;
        //public List<MovingBlock> MovingBlocks;

        public bool IsVisible
        {
            get
            {
                //return (scene.Layer.IndexOf(this) == scene.currentLayer);
                return true;
            }
        }
        public bool IsMain = false;

        internal BlockScene scene;
        //internal XAudio2 xaudio = new XAudio2();

        public BlockLayer(BlockScene scene)
        {
            this.scene = scene;
            blocks = new Block[0,0];
            //MovingBlocks = new List<MovingBlock>();
        }

        public void Draw(GameTime gameTime)
        {
            for(int i = 0; i <= blocks.Length; i++)
            {
/*-------------------------------------------------------------- TODO: ÄNDERN FÜR BESSERE PERFORMANCE ----------------------------------------------------*/
                Block block = this.blocks[i,i];

                if (block.Position.X < ((this.scene.block_size * this.Position.X) + block.Position.X))
                    continue;
                else
                {
                    this.scene.drawBlock(block, this);
                    block.Tint = Color.White;
                }
            }
        }

        /*public void Update(GameTime gameTime)
        {
            for (int i = 0; i < MovingBlocks.Count; i++)
            {
                MovingBlock block = this.MovingBlocks[i];
                updateCollision(block);
                block.Update(gameTime);
            }
        }*/

        private Block getBlockByCoordinate(int x, int y)
        {
            foreach (List<Block> blockList in this.Blocks)
            {
                foreach (Block block in blockList)
                {
                    if (block.Position.X == x && block.Position.Y == y)
                        return block;
                }
            }

            return null;
        }

        public static BlockLayer Read(XmlNode node, BlockScene scene)
        {
            BlockLayer layer = new BlockLayer(scene);
            Block[,] blocks;
            int x = 0;
            int y = 0;
            if (node.Name == "Info")
            {
                
                x = int.Parse(node.Attributes["blocks_x"].Value);
                y = int.Parse(node.Attributes["blocks_y"].Value);

                blocks = new Block[x,y];
                
            }
            if(node.Name == "Layer")
            {
                for (int i = 0; i >= x; i++)
                {
                    for (int j = 0; j >= y; j++)
                    {
                        layer.Blocks.AddRange(;

                    }
                }
                for(int blocksCount = 0; blocksCount < node.ChildNodes.Count; blocksCount++)
                {
                    XmlNode blockNode = node.ChildNodes[blocksCount];

                    if(Statics.Functions.nodeHasAttribute(node, "main"))
                    {
                        layer.IsMain = bool.Parse(node.Attributes["main"].Value);
                    }

                    if (blockNode.Name.StartsWith("Block"))
                    {
                        Block block = Block.Read(blockNode, layer);
                        layer.Blocks[block.Position.X][block.Position.Y]=block;
                    }
                }

                return layer;
            }
            else
            {
                return null;
                throw new Exception("No!");
            }
        }
    }
}
