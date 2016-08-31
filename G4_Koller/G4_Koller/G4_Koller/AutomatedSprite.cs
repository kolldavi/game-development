using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace G4_Koller
{

    class AutomatedSprite : Sprite
    {
    
        //----------------------------------------------------
        // Sprite is automated. 
        // Direction is same as speed in this case (speed from base class)
        //  Think of speed as Amount of Movement

        public bool playAsteroidSound = false;
        public bool bounceOffWallSound = false;
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
                speed.X = -speed.X;
            if (position.Y < -0)
                speed.Y = -speed.Y;
            if (position.X > clientBounds.Width - frameSize.X)
                speed.X = -speed.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                speed.Y = -speed.Y;

            base.Update(gameTime, clientBounds);
        }
        //*******************************************************************
        //update method for killer Sprite
        //*******************************************************************
        public  void killerSpriteUpdate(GameTime gameTime, Rectangle clientBounds)
        {
            bounceOffWallSound = false;
            playAsteroidSound = false;
            // Move sprite based on direction
            position += direction;
            Random rnd = new Random();
            float randomX, randomY, randomStartX;
            randomX = rnd.Next(-6, 6);
            randomY = rnd.Next(2, 7);
            randomStartX = rnd.Next(1, 950);

            if (isVisible == false)
            {
                position.Y = -40;
            }
            if (position.X < 0)
            {
                bounceOffWallSound = true;
                speed.X = -speed.X;             
            }
            if (position.Y <= -30)
            {
                position.X = randomStartX;
                isVisible = true;
            }
            if (position.X > clientBounds.Width - frameSize.X)
            {
                bounceOffWallSound = true;
                speed.X = -speed.X;
            }
            if (position.Y > clientBounds.Height + 40 - frameSize.Y)
            {
                playAsteroidSound = true;
                position.Y = -40;
                speed.Y = randomY;
                speed.X = randomX;                
                
            }
            base.Update(gameTime, clientBounds);
        }
        public Boolean isVisible = true;
        public Boolean addSprites = false;

        public float rand()
        {
            Random rnd = new Random();
            return 10 * (float)rnd.Next(-45, 45) / 100;
        }
        //*******************************************************************
        //update method for point sprite
        //*******************************************************************
        public void pointSpriteUpdate(GameTime gameTime, Rectangle clientBounds)
        {
           
            Random rnd = new Random();
            int randomBounceCase,randomStartPos;
          //  Boolean isVisible = true;
            float randomAngle, randomY, randomStartX;
            randomAngle = rnd.Next(-45, 45);
            randomStartX = rnd.Next(50, 950);
            randomY = rnd.Next(50, 650);
            randomBounceCase = rnd.Next(1, 100);
            randomStartPos =rnd.Next(0, 4);
            // Move sprite based on direction
            position += direction;
            if (isVisible == true)
            {
                if (randomBounceCase%2 == 1)
                {
                    if (position.X < 0)
                    {
                        speed.X = -speed.X;
                        speed.Y = rand();
                    }
                    if (position.Y < 0)
                    {
                        speed.Y = -speed.Y;
                        speed.X = rand();
                    }
                    if (position.X > clientBounds.Width - frameSize.X)
                    {
                        speed.X = -speed.X;
                        speed.Y = -rand();
                    }
                    if (position.Y > clientBounds.Height - frameSize.Y)
                    {
                        speed.Y = -speed.Y;
                        speed.X = rand();
                    }
                    if (speed.Y == 0 && speed.X == 0 && position.X<=0)
                    {
                        speed.X = 3;
                        speed.Y = -3;
                    }               
                }
                else
                {
                    if (position.X < 0)
                    {
                        speed.X = -speed.X;
                        speed.Y = 0;
                    }
                    if (position.Y < -0)
                    {
                        speed.Y = -speed.Y;
                        speed.X = 0;
                    }
                    if (position.X > clientBounds.Width - frameSize.X)
                    {
                        speed.X = -speed.X;
                        speed.Y = 0;
                    }
                    if (position.Y > clientBounds.Height - frameSize.Y)
                    {
                        speed.Y = -speed.Y;
                        speed.X = 0;
                    }
                }
            }

            if (isVisible == false)
            {
                    if (randomStartPos == 0)
                    {
                        position.Y = randomY;
                        position.X = 30;
                    }
                    if (randomStartPos == 1)
                    {
                        position.Y = randomY;
                        position.X = 970;
                    }
                    if (randomStartPos == 2)
                    {
                        position.Y = 30;
                        position.X = randomStartX;
                    }
                    if (randomStartPos == 3)
                    {
                        position.Y = 660;
                        position.X = randomStartX;
                    }
                }
            
            base.Update(gameTime, clientBounds);
        
        }
        //*******************************************************************
        //update method for super point
        //*******************************************************************
        public  void SuperPointUpdate(GameTime gameTime, Rectangle clientBounds)
        {
                        Random rnd = new Random();
            int randomStartPos;
      //  isVisible = true;
          
            float  randomY, randomStartX;
            randomStartX = rnd.Next(50, 950);
            randomY = rnd.Next(50, 650);
            randomStartPos =rnd.Next(0,4);
            position += direction;
         
         
            if (isVisible == true)
            {
                if (position.X < 0 || position.Y < 0 || position.X > clientBounds.Width 
                    || position.Y > clientBounds.Height)
                {
                    isVisible = false;
                }
            }
            else if (isVisible == false)
            {
                 if (randomStartPos == 0)
                    {
                        position.Y = randomY;
                        position.X = 20;
                        speed.X = 7;
                        speed.Y = 0;
                    }
                    if (randomStartPos == 1)
                    {
                        position.Y = randomY;
                        position.X = 970;
                        speed.X = -7;
                        speed.Y = 0;
                    }
                    if (randomStartPos == 2)
                    {
                        position.Y = 670;
                        position.X = randomStartX;
                        speed.X = 0;
                        speed.Y = -7;
                    }
                    if (randomStartPos == 3)
                    {
                        position.Y = 20;
                        position.X = randomStartX;
                        speed.X = 0;
                        speed.Y = 7;
                    }
                }
            

       

            base.Update(gameTime, clientBounds);
        }
        }
    }