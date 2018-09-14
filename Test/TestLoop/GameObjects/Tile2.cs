using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TestLoop
{
    public class Tile2 : GameObject, ICollidable, ISurface
    {
        // collision related
        public List<ScreenSector> CurrentSectors { get; } = new List<ScreenSector>();
        public Rectangle CurrentObjectRectangle { get { return PositionRectangle; } }
        public float Friction { get; set; }
        public float Bounce { get; set; }
        public SurfaceTypes SurfaceType { get; set; }

        public int TileNumber { get; set; }
        public string AssetName { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        public Tile2(GameArea currentGameArea, int x, int y, int tileNumber, string assetName, int tilewidth, int tileheight) : base(currentGameArea)
        {
            X = x;
            Y = y;

            Height = 32;
            Width = 32;

            TileNumber = tileNumber;
            AssetName = assetName;
            TileWidth = tilewidth;
            TileHeight = tileheight;
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

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            // Surface
            Friction = 0;
            Bounce = 0;
            CurrentGameArea.Collision.AddCollidableObject(this);

            if (assetName == null)
            {
                int tempTextureId = 0;

                // texture related
                assetName = AssetName;
                textureId = GraphicsUtility.LoadTexture(assetName);
                spriteSheet = new SpriteSheetHandler(textureId, 0, 0, 0, 0, TileWidth, TileHeight);
                GraphicsUtility.AddVisibleGameObjects(LayerIndex, this);
            }

            spriteSheet.CurrentFrame = TileNumber;

        }

        public override Rectangle GetDrawingRectangle()
        {
            return spriteSheet.getCurrentFrameRectangle();
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
