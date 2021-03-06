﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Otter;
using Clunk.Effects;
using Clunk.Scenes;

namespace Clunk.Entities
{
    public class Player : Entity
    {
        // Our entity's graphic will be a Spritemap
        private Spritemap<string> sprite;

        public float moveSpeed = 4.0f;

        public int direction = 0;

        public const int WIDTH = 32;
        public const int HEIGHT = 40;
        public const float DIAGONAL_SPEED = 1.4f;

        public Player(float x = 0, float y = 0)
        {
            // When creating a new player, the desired X,Y coordinates are passed in. If excluded, we start at 0,0
            X = x;
            Y = y;
            // Create a new spritemap, with the player.png image as our source, 32 pixels wide, and 40 pixels tall
            sprite = new Spritemap<string>(Assets.PLAYER, 32, 40);

            // We must define each animation for our spritemap.
            // An animation is made up of a group of frames, ranging from 1 frame to many frames.
            // Each 32x40 box is a single frame in our particular sprite map. 
            // The frames start counting from 0, and count from left-to-right, top-to-bottom
            sprite.Add("standLeft", new int[] { 0, 1 }, new float[] { 10f, 10f });
            sprite.Add("standRight", new int[] { 0, 1 }, new float[] { 10f, 10f });
            sprite.Add("standDown", new int[] { 3, 4 }, new float[] { 10f, 10f });
            sprite.Add("standUp", new int[] { 6, 7 }, new float[] { 10f, 10f });
            sprite.Add("walkLeft", new int[] { 0, 1 }, new float[] { 10f, 10f });
            sprite.Add("walkRight", new int[] { 0, 1 }, new float[] { 10f, 10f });
            sprite.Add("walkDown", new int[] { 3, 4 }, new float[] { 10f, 10f });
            sprite.Add("walkUp", new int[] { 6, 7 }, new float[] { 10f, 10f });

            // Tell the spritemap which animation to play when the scene starts
            sprite.Play("standDown");

            // Lastly, we must set our Entity's graphic, otherwise it will not display
            Graphic = sprite;
        }
             
            public override void Update()
        {
            base.Update();

            // Used to determine which directions we are moving in
            bool horizontalMovement = true;
            bool verticalMovement = true;

            float xSpeed = 0;
            float ySpeed = 0;
            float newX;
            float newY;
            GameScene checkScene = (GameScene)Scene;
                        
            // Check horizontal movement
            if (Global.PlayerSession.Controller.Button("Left").Down)
            {
                newX = X - moveSpeed;

                // Check if we are colliding with a solid rectangle or not.
                // Ensure the GridCollider snaps our values to a grid, by passing
                // in a false boolean for the usingGrid parameter
                if (!checkScene.grid.GetRect(newX, Y, newX + WIDTH, Y + HEIGHT, false))
                {
                    xSpeed = -moveSpeed;
                }

                direction = Global.DIR_LEFT;
                sprite.FlippedX = true;
            }
            else if (Global.PlayerSession.Controller.Button("Right").Down)
            {
                newX = X + moveSpeed;
                if (!checkScene.grid.GetRect(newX, Y, newX + WIDTH, Y + HEIGHT, false))
                {
                    xSpeed = moveSpeed;
                }

                direction = Global.DIR_RIGHT;
                sprite.FlippedX = false;
            }
            else
            {
                horizontalMovement = false;
            }

            // Check vertical movement
            if (Global.PlayerSession.Controller.Button("Up").Down)
            {
                newY = Y - moveSpeed;
                if (!checkScene.grid.GetRect(X, newY, X + WIDTH, newY + HEIGHT, false))
                {
                    ySpeed = -moveSpeed;
                }

                direction = Global.DIR_UP;
                sprite.FlippedX = false;
            }
            else if (Global.PlayerSession.Controller.Button("Down").Down)
            {
                newY = Y + moveSpeed;
                if (!checkScene.grid.GetRect(X, newY, X + WIDTH, newY + HEIGHT, false))
                {
                    ySpeed = moveSpeed;
                }

                direction = Global.DIR_DOWN;
                sprite.FlippedX = false;
            }
            else
            {
                verticalMovement = false;
            }

            if (Global.PlayerSession.Controller.Button("X").Pressed)
            {
                Global.CLUNK.Scene.Add(new Bullet(X, Y, direction));
            }

            // If we are not moving play our idle animations
            // Currently our spritesheet lacks true idle
            // animations, but this helps get the idea across
            if (!horizontalMovement && !verticalMovement)
            {
                if (sprite.CurrentAnim.Equals("walkLeft"))
                {
                    sprite.Play("standLeft");
                }
                else if (sprite.CurrentAnim.Equals("walkRight"))
                {
                    sprite.Play("standRight");
                }
                else if (sprite.CurrentAnim.Equals("walkDown"))
                {
                    sprite.Play("standDown");
                }
                else if (sprite.CurrentAnim.Equals("walkUp"))
                {
                    sprite.Play("standUp");
                }
            }

            // Add particles if the player is moving in any direction
            if (verticalMovement || horizontalMovement)
            {
                // Add walking particles
                float particleXBuffer = 0;
                float particleYBuffer = 0;
                switch (direction)
                {
                    case Global.DIR_UP:
                        {
                            particleXBuffer = Otter.Rand.Float(8, 24);
                            particleYBuffer = Otter.Rand.Float(0, 5);
                            Global.CLUNK.Scene.Add(new WalkParticle(X + particleXBuffer, Y + 40));
                            break;
                        }
                    case Global.DIR_DOWN:
                        {
                            particleXBuffer = Otter.Rand.Float(8, 24);
                            Global.CLUNK.Scene.Add(new WalkParticle(X + particleXBuffer, Y));
                            break;
                        }
                    case Global.DIR_LEFT:
                        {
                            particleYBuffer = Otter.Rand.Float(-2, 2);
                            Global.CLUNK.Scene.Add(new WalkParticle(X + 32 - 3, Y + 40 + particleYBuffer));
                            break;
                        }
                    case Global.DIR_RIGHT:
                        {
                            particleYBuffer = Otter.Rand.Float(-2, 2);
                            Global.CLUNK.Scene.Add(new WalkParticle(X + 3, Y + 40 + particleYBuffer));
                            break;
                        }
                }
            }

            if (verticalMovement && horizontalMovement)
            {
                X += xSpeed / DIAGONAL_SPEED;
                Y += ySpeed / DIAGONAL_SPEED;
            }
            else
            {
                X += xSpeed;
                Y += ySpeed;
            }
        }
    }
    
}
