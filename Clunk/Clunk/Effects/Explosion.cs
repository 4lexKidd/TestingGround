﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk;

namespace Clunk.Effects
{
    public class Explosion : Entity 
    {
        public Image img;

        // Default color is white
        public Color color = new Color("FFFFFF");

        public Sound explode = new Sound(Assets.SND_BULLET_EXPLODE);

        public Explosion(float x, float y, int width = 32, int height = 40, Color expColor = null, int radius = 20)
        {
            X = x + width / 2;
            Y = y;

            // You can pass in a custom color, if desired
            if (expColor != null)
            {
                color = expColor;
            }

            // Center ourselves and set the graphic
            img = Image.CreateCircle(radius, color);
            img.CenterOrigin();
            Graphic = img;

            Global.camShaker.ShakeCamera(40f);
            explode.Play();
        }

        public override void Update()
        {
            base.Update();

            // Gradually grow, in the fashion of an explosion
            // If you want to get fancy, perhaps you could make this
            // a decaying growth, or even Tween it so it grows faster
            // at the beginning
            img.ScaleX += 0.10f;
            img.ScaleY += 0.10f;

            img.CenterOrigin();

            // Gradually phase to invisible
            img.Alpha -= 0.04f;
            if (img.Alpha <= 0)
            {
                RemoveSelf();
            }
        }
    }
}
