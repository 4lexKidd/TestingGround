﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk.Entities;

namespace Clunk.Scenes
{
    

    public class GameScene : Scene
    {
        public Music gameMusic = new Music(Assets.MUSIC_GAME);
        // Scene object that will hold the next scene that we transition to
        public Scene nextScene;

        // Use a J,I coordinate system for our map's screens to avoid 
        // confusion with our already existing X,Y coordinate systems
        public int screenJ;
        public int screenI;

        // Our Tilemap's calculated width and height
        public const int WIDTH = Global.GAME_WIDTH * 3;
        public const int HEIGHT = Global.GAME_HEIGHT * 2;

        public Tilemap tilemap = null;
        public GridCollider grid = null;

        //Players TileGridPositions
        public int GridXPosOld = 0;
        public int GridYPosOld = 0;
        public int GridXPos = 0;
        public int GridYPos = 0;


        // Our new constructor takes in the new J,I coordinates, and a Player object
        public GameScene(int nextJ = 0, int nextI = 0, Player player = null, HaguruClock clock = null) : base()
        {
            screenJ = nextJ;
            screenI = nextI;

            // If a Player object isn't passed in, start at the default x,y position of 100,100
            if (player == null)
            {
                Global.player = new Player(100, 100);
            }
            else
            {
                Global.player = player;
            }

            //if no Clock is passed in start a new clock at the default pos
            if (clock == null)
            {
                Global.clock = new HaguruClock();
            }
            else
            {
                Global.clock = clock;
            }

            // Create and load our Tilemap and GridCollider
            tilemap = new Tilemap(Assets.TILESET, WIDTH, HEIGHT, Global.GRID_WIDTH, Global.GRID_HEIGHT);
            grid = new GridCollider(WIDTH, HEIGHT, Global.GRID_WIDTH, Global.GRID_HEIGHT);
            string mapToLoad = Assets.MAP_WORLD;
            string solidsToLoad = Assets.MAP_SOLID;
            LoadWorld(mapToLoad, solidsToLoad);

            // Since we are constantly switching Scenes we need to do some checking,
            // ensuring that the music doesn't get restarted.
            // We should probably add an isPlaying boolean to the Music class. I will do this soon.
            if (Global.gameMusic == null)
            {
                Global.gameMusic = new Music(Assets.MUSIC_GAME);
                Global.gameMusic.Play();
                Global.gameMusic.Volume = 0.40f;
            }
        
    }

        // We now add our Entities and Graphics once the Scene has been switched to
        public override void Begin()
        {
            Entity gridEntity = new Entity(0, 0, null, grid);
            Add(gridEntity);
            AddGraphic(tilemap);
            Add(Global.clock);

            // Ensure that the player is not null
            if (Global.player != null)
            {
                Add(Global.player);

                // Never should be paused once transitioning is complete
                Global.paused = false;
            }
            
            //Set PlayerGridposition (plus sprite margin)
            GridXPosOld = (int)(Global.player.X + 16f) / tilemap.TileWidth;
            GridXPos = GridXPosOld;
            GridYPosOld = (int)(Global.player.Y + 20f)/ tilemap.TileHeight;
            GridYPos = GridYPosOld;

            Add(Global.camShaker);

            // This is rather crude, as we re-add the Enemy every time we switch screens
            // A good task beyond these tutorials would be ensuring that non-player
            // Entities retain their state upon switching screens
            Add(new Enemy(500, 400));
        }

        public override void Update()
        {
            if (Global.paused)
            {
                return;
            }

            /* Zeichnen der Map je nach Player Position
            GridXPos = (int)(Global.player.X + 16f) / tilemap.TileWidth ;
            GridYPos = (int)(Global.player.Y + 20f)  / tilemap.TileHeight ;
                          
            if (GridXPos != GridXPosOld || GridYPos != GridYPosOld)
            {
                //clear old markings
                tilemap.SetTile(GridXPosOld + 2, GridYPosOld, 0);
                tilemap.SetTile(GridXPosOld + 1, GridYPosOld - 1, 0);
                tilemap.SetTile(GridXPosOld + 1, GridYPosOld + 1, 0);
                tilemap.SetTile(GridXPosOld, GridYPosOld + 2, 0);
                tilemap.SetTile(GridXPosOld, GridYPosOld - 2, 0);
                tilemap.SetTile(GridXPosOld - 1, GridYPosOld - 1, 0);
                tilemap.SetTile(GridXPosOld - 1, GridYPosOld + 1, 0);
                tilemap.SetTile(GridXPosOld - 2, GridYPosOld, 0);

                //write new markings
                tilemap.SetTile(GridXPos + 2, GridYPos, 2);
                tilemap.SetTile(GridXPos + 1, GridYPos - 1, 2);
                tilemap.SetTile(GridXPos + 1, GridYPos + 1, 2);
                tilemap.SetTile(GridXPos , GridYPos + 2, 2);
                tilemap.SetTile(GridXPos, GridYPos - 2, 2);
                tilemap.SetTile(GridXPos - 1, GridYPos - 1, 2);
                tilemap.SetTile(GridXPos - 1, GridYPos + 1, 2);
                tilemap.SetTile(GridXPos - 2, GridYPos, 2);
                GridXPosOld = GridXPos;
                GridYPosOld = GridYPos;
            } */

            // Check the Player's X,Y position, and determine if we need to move a
            // Scene up, down, left, or right. We also check the current J and I values, 
            // ensuring that we don't move past our actual tileset, into a plain grey screen
            const float HALF_TILE = Global.GRID_WIDTH / 2;
            if (Global.player.X - CameraX < HALF_TILE)
            {
                if (screenJ > 0)
                {
                    if (Global.player.X > 50)
                    {
                        screenJ--;
                        this.Scroll(-1, 0);
                    }
                }
            }

            if (Global.player.Y - CameraY < HALF_TILE)
            {
                if (screenI > 0)
                {
                    if (Global.player.Y > 32)
                    {
                        screenI--;
                        this.Scroll(0, -1);
                    }
                }
            }

            if (Global.player.X - CameraX - Global.GAME_WIDTH > -HALF_TILE)
            {
                if (screenJ < 2)
                {
                    screenJ++;
                    this.Scroll(1, 0);
                }
            }

            if (Global.player.Y - CameraY - Global.GAME_HEIGHT > -HALF_TILE)
            {
                if (screenI < 1)
                {
                    screenI++;
                    this.Scroll(0, 1);
                }
            }

            //Write Tiles around the player
            
        }

        // Scroll method that moves the CameraX, CameraY
        // coordinates by the multiple dx, dy values
        public void Scroll(int dx, int dy)
        {
            // Pause the game when we start scrolling
            Global.paused = true;

            // Set the nextScene and call UpdateLists to 
            // ensure all Entities are cleaned up properly
            nextScene = new GameScene(screenJ, screenI, Global.player);
            nextScene.UpdateLists();

            // Push the player over with the screen via a Tween
            float pushPlayerX = dx * 30;
            float pushPlayerY = dy * 30;

            Tweener.Tween(Global.player, new
            {
                X = Global.player.X + pushPlayerX,
                Y = Global.player.Y + pushPlayerY
            }, 30f, 0);

            // Finally, push the Camera over by a multiple of the 
            // Game's width and height, and set the call back method
            // to our new ScrollDone method
            Tweener.Tween(this, new
            {
                CameraX = CameraX + Global.GAME_WIDTH * dx,
                CameraY = CameraY + Global.GAME_HEIGHT * dy
            }, 30f, 0).OnComplete(ScrollDone);
        }

        // Method called once the screen scrolling is all done
        public void ScrollDone()
        {
            // Once the scroll is done remove all added graphics
            // and call UpdateLists to clean everything up and 
            // then switch to the nextScene
            RemoveAll();
            UpdateLists();

            // Set the nextScene's Camera values to the current Scene's 
            // freshly tweened camera values, otherwise we snap back to screen 0,0
            nextScene.CameraX = CameraX;
            nextScene.CameraY = CameraY;
            Global.CLUNK.SwitchScene(nextScene);
        }

        private void LoadWorld(string map, string solids)
        {
            // Get our CSV map in string format and load it via our tilemap
            string newMap = CSVToString(map);
            tilemap.LoadCSV(newMap);

            // Get our csv solid map and load it into our GridCollider
            string newSolids = CSVToString(solids);
            grid.LoadCSV(newSolids);
        }

        // Add this method to your GameScene.cs class
        private static string CSVToString(string csvMap)
        {
            string ourMap = "";

            using (var reader = new StreamReader(csvMap))
            {
                // Read each line, adding a line-break to the end of each
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ourMap += line;
                    ourMap += "\n";
                }
            }

            return ourMap;
        }

    }
}
