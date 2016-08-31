//userControl Sprite Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace G4_Koller
{
    class UserControlledSprite : Sprite
    {
    
        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;
                KeyboardState keyboardState = Keyboard.GetState();

                // If player pressed arrow keys, move the sprite
                if (keyboardState.IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (keyboardState.IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (keyboardState.IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (keyboardState.IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;

                return inputDirection * speed;
            }
        }

        //----------------------------------------------------------------
        //  1st Constructor
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame)
        {
        }

        //----------------------------------------------------------------
        //  2nd Constructor
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed)
        {
        }

        //-----------------------------------------------------------------------
        //  Update Method
        //    Overrides the base class method
        //    
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move the sprite based on direction
            position += direction;

    

            // If sprite is off the screen, move it back within the game window
            //    This is not done in the parent/base class as what you do when it goes off
            //    the screen can be different for different sprite types.
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;

            //Call the base method - always remember to do this
            base.Update(gameTime, clientBounds);
        }
    }
}

