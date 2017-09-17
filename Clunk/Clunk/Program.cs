using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Otter;
using Clunk;
using Clunk.Scenes;

namespace Clunk
{
    public class Program
    {
        static void Main(string[] args)
        {
            //To Do Gravity einbauen (oder plattformermovement einbauen[in otter vorhanden])


            Global.CLUNK = new Game("Playground", 640, 480);
            Global.CLUNK.SetWindow(640, 480);

            Global.CLUNK.FirstScene = new TitleScene();
            Global.PlayerSession = Global.CLUNK.AddSession("Player");
            Global.PlayerSession.Controller.AddButton("Enter");
            Global.PlayerSession.Controller.Button("Enter").AddKey(Key.Return);
            Global.PlayerSession.Controller.AddButton("Up");
            Global.PlayerSession.Controller.Button("Up").AddKey(Key.Up);
            Global.PlayerSession.Controller.AddButton("Left");
            Global.PlayerSession.Controller.Button("Left").AddKey(Key.Left);
            Global.PlayerSession.Controller.AddButton("Down");
            Global.PlayerSession.Controller.Button("Down").AddKey(Key.Down);
            Global.PlayerSession.Controller.AddButton("Right");
            Global.PlayerSession.Controller.Button("Right").AddKey(Key.Right);
            Global.PlayerSession.Controller.AddButton("X");
            Global.PlayerSession.Controller.Button("X").AddKey(Key.X);

            //Code für positionen des players
            //Console.WriteLine(Global.player.ScreenX);
            //Console.WriteLine(Global.player.ScreenY);
            //To do snap to grid logik implementieren um abhängig von player position gridladen zu können 

            // Start the game \:D/
            Global.CLUNK.Start();



            /*
            float playerPosX = Game.Instance.HalfWidth;
            float playerPosY = Game.Instance.HalfHeight;

            // Create a Scene.
            var scene = new Scene();
                        
            //Funktioniert noch nicht: Components angugen und checken 
            //ob und wie beide entities player und tileMap
            //player positionen sharen können damit tileMap auf playerbewegung
            //reagieren kann!!!

            //-> Otter forum checken nach wie komponenten/variablen global bekannt sein können


            //Set and add Player
            PlayerEntity player = new PlayerEntity(playerPosX,playerPosY);
            scene.Add(player);

            //Set and add TileMap
            Tiles tileMap = new Tiles(playerPosX, playerPosY);
            scene.Add(tileMap);

            // Set the mouse visibility to true for this example.
            game.MouseVisible = true;
            */
            // Start the Game.
            //game.Start(scene);
        }
    }
}
