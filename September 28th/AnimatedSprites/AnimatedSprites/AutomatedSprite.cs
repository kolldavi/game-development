/*
 * Automated Sprite Class
 * 
 * Derived from Sprite class
 * 
 * September 26th 2011
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class AutomatedSprite : Sprite
    {
        //----------------------------------------------------
        // Sprite is automated. 
        // Direction is same as speed in this case (speed from base class)
        //  Think of speed as Amount of Movement
        public override Vector2 direction
        {
            get { return speed; }
        }

        //----------------------------------------------------
        // First Constructor
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {
        }

        //----------------------------------------------------
        // Second Constructor
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
        }

        //----------------------------------------------------
        // Update Method
        //   Update the position and call the base class update
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move sprite based on direction
            position += direction;
            // If sprite tries to go off the screen, move it back within the game window reversing 
            //    direction. Bouncing off the edge
            if (position.X < 0)
                speed.X = - speed.X;
            if (position.Y < 0)
                speed.Y = -speed.Y;
            if (position.X > clientBounds.Width - frameSize.X)
                speed.X = -speed.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                speed.Y = -speed.Y;

            base.Update(gameTime, clientBounds);
        }
    }
}
