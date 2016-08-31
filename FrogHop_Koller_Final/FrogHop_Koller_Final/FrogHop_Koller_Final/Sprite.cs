using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FrogHop_Koller_Final
{
    abstract class Sprite
    {
        Texture2D textureImage;
        protected Point frameSize;
        Point sheetSize;
        Point currentFrame;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        int collisionOffset;
        protected Vector2 speed;
        protected Vector2 position;

        const int defaultMillisecondsPerFrame = 16;

        //----------------------------------------------------------------
        // direction property
        public abstract Vector2 direction
        {
            get;
        }


        //----------------------------------------------------------------
        //  1st Constructor
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        //----------------------------------------------------------------
        //  2nd Constructor
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = defaultMillisecondsPerFrame;
        }

        //----------------------------------------------------------------
        //  Update method
        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {

            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                // Advance to the next frame
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }

        }

        //----------------------------------------------------------------
        //  Draw method
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(textureImage, position,
                new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero,
                    1, SpriteEffects.None, 0);
        }

        //----------------------------------------------------------------
        //  collisionRect method

        public Rectangle collisionRect()
        {
            return new Rectangle((int)position.X + collisionOffset, (int)position.Y + collisionOffset,
                frameSize.X - 2 * collisionOffset, frameSize.Y - 2 * collisionOffset);
        }
    }
}
