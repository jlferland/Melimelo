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

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        public Tile2(GameArea currentGameArea, int x, int y, int tileNumber) : base(currentGameArea)
        {
            X = x;
            Y = y;
            TileNumber = tileNumber;
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

            // final tile size (individual tiles are 32*32)
            Height = 32;
            Width = 32;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            // Surface
            Friction = 0;
            Bounce = 0;
            CurrentGameArea.Collision.AddCollidableObject(this);

            if (assetName == null)
            {
                int tempTextureId = 0;

                // texture related
                assetName = "Sprites\\tilemap_roche4";
                tempTextureId = GraphicsUtility.LoadTexture(assetName);
                spriteSheet = new SpriteSheetHandler(tempTextureId, 0, 0, 0, 0, 32, 32);

                // Tile helper 
                TiledTextureRenderer renderer = new TiledTextureRenderer();
                renderer.AddTextureId(spriteSheet);

                List<TiledTextureDescriptor> tiles = new List<TiledTextureDescriptor>();
                tiles.Add(new TiledTextureDescriptor(Width, Height, TileNumber));
                textureId = renderer.RenderTexture("tiletest2 " + TileNumber.ToString(), tiles, Width, Height);

                GraphicsUtility.AddVisibleGameObjects(LayerIndex, this);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
