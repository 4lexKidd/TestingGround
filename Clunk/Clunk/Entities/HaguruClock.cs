using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk;
using Clunk.Effects;

namespace Clunk.Entities
{
    public class HaguruClock : Entity
    {
        private Image mainWheel;
        private Image sideWheel;

        private GraphicList graphics = new GraphicList();
        
        //Timer for showing time again
        float timer;

        // Our Text object that displays on screen to show time
        private Text text;
        
        private bool isFreezed;
       
        private int currentHour;
        private int currentMinute;
        

        // referenced gameTime
        private int summedHours;
        private int summedMinutes;
        


        //Initial Konstrukter - Is only called ones the game runs the first time
        public HaguruClock()
        {
            //Clock Position (Entity Position)
            X = 200;
            Y = 200;

            //Mainwheel Graphics
            mainWheel = new Image(Assets.HAGURUMAINCOCKWEEL);
            mainWheel.CenterOrigin();

            //Darf nicht relativ zur Uhr gerendered werden weil drehen der Uhr sonst mainwheel mitdreht!!!
            mainWheel.Relative = false;

            mainWheel.ScaleX = 0.25f;
            mainWheel.ScaleY = 0.25f;

            mainWheel.X = 0;
            mainWheel.Y = 0;
           
            //add it to the rendered Graphiclist            
            graphics.Add(mainWheel);

            //SideWheel Graphics
            sideWheel = new Image(Assets.HAGURUSIDECOCKWEEL);
            sideWheel.CenterOrigin();

            //Muss relativ zur Uhr gerendered werden weil drehen der Uhr sonst sidewheels position nicht mitdreht!!!
            sideWheel.Relative = true;
            sideWheel.ScaleX = 0.25f;
            sideWheel.ScaleY = 0.25f;

            sideWheel.X = 0;
            //Relative Position zur Uhr
            sideWheel.Y = 0 - ((mainWheel.ScaledHeight/2) + (sideWheel.ScaledHeight/2)-20);
                        
            //add it to the rendered Graphiclist
            graphics.Add(sideWheel);

            //Set overall Graphic to the list so all graphics are rendered properly
            Graphic = graphics;


        }

        public override void Update()
        {
            base.Update();

            currentHour = 1;
            currentMinute = 1;
           
            //Rotation of the main weel to match 1 hour ingame time by rotation when both hour and minute pointer meet
            //therefor 1minute ingame = aprox. 1,09 minutes in real time
            
            mainWheel.Transform.Rotation += 32.7272f/3600f;
            sideWheel.Transform.Rotation -= 32.7272f /50f;

            timer += Game.Instance.DeltaTime; ;
            
            if (timer < 0f)
            {
                
                summedHours += 1;
                summedMinutes += 1;

                text = new Text(howLongPlayed(), 16);
                text.Color = Color.Red;
                X = 20f;
                Y = 20f;
                timer = 0f;
                Graphic = mainWheel;
                
            }

        }

        public string howLongPlayed()
        {
            string time = "Time played: " + summedHours + " hours and " + summedMinutes + " minutes!";
            return time;
        }

        public void whatTimeIsIt(out int hours, out int minutes)
        {
            hours = currentHour;
            minutes = currentMinute;
        }
    }
}
