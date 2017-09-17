using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk;

namespace Clunk.Scenes
{
    public class TitleScene : Scene
    {
        // Create a new Image object, referencing the Otter image in our Assets folder
        public Image titleImage = new Image(Assets.TITLE_IMG);
        public Text titleText = new Text("Clunk", Assets.FONT_PANIC, 84);
        public Text enterText = new Text("Press Enter", Assets.FONT_PANIC, 40);
        public const float TIMER_BLINK = 25f;
        public float blinkTimer = 0;
        // Create a new, looping sound object, with our MUSIC_TITLE as its source
        public Music titleSong = new Music(Assets.MUSIC_TITLE, true);

        public TitleScene()
        {
            // Center the title picture 
            titleImage.CenterOrigin();
            titleImage.X = Global.CLUNK.HalfWidth;
            titleImage.Y = 1000; // When tweening something in, make sure it is actually off the screen first
            this.AddGraphic(titleImage);

            // Otter utilizes the C# Tweening library called Glide
            // More info can be found here: http://www.reddit.com/r/gamedev/comments/1fabdh/
            Tweener.Tween(titleImage, new { Y = 250 }, 30f, 0f).Ease(Ease.BackOut);

            // Set the text's outline color to the 
            // hex color #7FA8D2 (Otter2d.com Blue)
            titleText.OutlineColor = new Otter.Color("7FA8D2");
            titleText.OutlineThickness = 3; // Set the outline thickness to 3 pixels
            titleText.CenterOrigin();
            titleText.X = Global.CLUNK.HalfWidth;
            titleText.Y = 25;
            this.AddGraphic(titleText);

            enterText.OutlineColor = new Otter.Color("7FA8D2");
            enterText.OutlineThickness = 2;
            enterText.CenterOrigin();
            enterText.X = Global.CLUNK.HalfWidth;
            enterText.Y = 450;
            this.AddGraphic(enterText);

            titleSong.Play();
        }

        public override void Update()
        {
            base.Update();

            blinkTimer++;
            if (blinkTimer >= TIMER_BLINK)
            {
                enterText.Visible = !enterText.Visible;
                blinkTimer = 0;
            }

            if (Global.PlayerSession.Controller.Button("Enter").Pressed)
            {
                titleSong.Stop();

                Global.CLUNK.RemoveScene();
                Global.CLUNK.AddScene(new GameScene());
            }
        }
    }
}
