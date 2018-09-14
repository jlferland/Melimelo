using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TiledSharp;

namespace TestLoop
{
    public class TestGameArea2 : GameArea
    {
        private Tile tile;
        private Tile2 tile2;

        public TestGameArea2(int screenPositionX, int screenPositionY, int viewportWidth, int viewportHeight) :
            base(screenPositionX, screenPositionY, viewportWidth, viewportHeight)
        {
        }

        public override void Initialize()
        {
            // change parameters before calling base.initialize
            PixelsPerMeters = 1f;
            MaxFramePerSecond = 60;

            // initialize parent obj
            base.Initialize();

            Background.Color = Color.Bisque;

            tile = new Tile(this, 10, 100, 6, 6);

            // register new objects to appropriate handlers
            AddGameObject(tile);

            Map mapTest = new Map("Content\\XML\\tile_test.xml");

            // initialize custom items
            foreach (TmxLayer layer in mapTest.map.Layers)
            {
                foreach (TmxLayerTile tile in layer.Tiles)                    
                {
                    if (tile.Gid > 0)
                    {
                        tile2 = new Tile2(this, 
                                          tile.X * mapTest.map.Tilesets[0].TileWidth, 
                                          tile.Y * mapTest.map.Tilesets[0].TileHeight, 
                                          tile.Gid - 1, 
                                          "Sprites\\tilemap_roche4",
                                          mapTest.map.Tilesets[0].TileWidth, 
                                          mapTest.map.Tilesets[0].TileHeight);
                        this.AddGameObject(tile2);
                    }
                }
            }

            // register new objects to appropriate handlers

            // Scale test
            Scale = 0.15f;
        }
    }
}
