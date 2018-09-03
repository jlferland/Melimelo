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

        // sprite helper
        private SpriteSheetHandler spriteSheet;

        public Tile(GameArea currentGameArea, int x, int y) : base(currentGameArea)
        {
            X = x;
            Y = y;
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
            Height = 32;
            Width = 48;

            PositionRectangle = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Width, Height);

            // Surface
            Friction = 0;
            Bounce = 0;

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
                tiles.Add(new TiledTextureDescriptor(0, 0, 2));
                tiles.Add(new TiledTextureDescriptor(16, 0, 3));
                tiles.Add(new TiledTextureDescriptor(32, 0, 2));
                tiles.Add(new TiledTextureDescriptor(0, 16, 0));
                tiles.Add(new TiledTextureDescriptor(16, 16, 1));
                tiles.Add(new TiledTextureDescriptor(32, 16, 0));

                textureId = renderer.RenderTexture(tiles, Width, Height);

                GraphicsUtility.AddVisibleGameObjects(LayerIndex, this);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
