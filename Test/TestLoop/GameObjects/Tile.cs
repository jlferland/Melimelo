using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class Tile : GameObject, ICollidable, ISurface
    {
        // collision related
        public List<ScreenSector> CurrentSectors { get; } = new List<ScreenSector>();
        public Rectangle CurrentObjectRectangle { get { return PositionRectangle; } }
        public float Friction { get; set; }
        public float Bounce { get; set; }
        public SurfaceTypes SurfaceType { get; set; }

        public int BlockHeight { get; set; }
        public int BlockWidth { get; set; }

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        public Tile(GameArea currentGameArea, int x, int y, int blockWidth, int blockHeight) : base(currentGameArea)
        {
            X = x;
            Y = y;
            BlockHeight = blockHeight;
            BlockWidth = blockWidth;
        }

        // public methods 
        public override void Initialize()
        {
            // set object state
            SetInitialState();
        }

        private void SetInitialState()
        {
            Active = true;
            Visible = true;
            LayerIndex = 0;

            // final tile size (individual tiles are 16*16)
            Height = BlockHeight * 16;
            Width = BlockWidth * 16;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            // Surface
            Friction = 0;
            Bounce = 0;
            CurrentGameArea.Collision.AddCollidableObject(this);

            if (assetName == null)
            {
                int tempTextureId = 0;
                
                // texture related
                assetName = "Sprites\\Game Boy GBC - Mega Man I Dr Wilys Revenge - Ice Mans Stage";
                tempTextureId = GraphicsUtility.LoadTexture(assetName);
                spriteSheet = new SpriteSheetHandler(tempTextureId, 0, 0, 0, 0, 16, 16);
                
                // Tile helper 
                TiledTextureRenderer renderer = new TiledTextureRenderer();
                renderer.AddTextureId(spriteSheet);

                List<TiledTextureDescriptor> tiles = new List<TiledTextureDescriptor>();
                int spriteIdOffset = 2;
                for (int i = 0; i < BlockHeight; i++)
                {
                    if (i > 0)
                        spriteIdOffset = 0;

                    for (int j = 0; j < BlockWidth; j++)
                    {
                        tiles.Add(new TiledTextureDescriptor(j * 16, i * 16, spriteIdOffset + j % 2));
                    }
                }
                textureId = renderer.RenderTexture("tiletest " + Height.ToString() + " " + Width.ToString(), tiles, Width, Height);

                GraphicsUtility.AddVisibleGameObjects(LayerIndex, this);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
