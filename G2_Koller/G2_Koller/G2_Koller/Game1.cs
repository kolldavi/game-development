/***************************************************************************
 *David Koller
 *Assignment 2
 *9/20/11
 * 
 * 
/***************************************************************************/
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

namespace G2_Koller
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tennisBallTexture;
        Texture2D grassTexture;
        Texture2D paddleHandelTexture;
        Texture2D paddleHitTexture;
        Texture2D brickTexture;
        bool pause;
        bool start;
        Point hitPoint = new Point(100, 100);
        Point ballPoint = new Point(75, 75);
        float rotation;
        Vector2 hitPos = new Vector2(600, 200);
        Vector2 ballPos = new Vector2(600, 220);
        int screenWidth = 800;
        int screenHieght = 400;
        int top = 40;
        int bottom = 375;
        int left = 30;
        double ballX = 5;
        double ballY;
        KeyboardState oldState = Keyboard.GetState();
        //***************************************************************************
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = screenHieght;
            graphics.PreferredBackBufferWidth = screenWidth;
            oldState = Keyboard.GetState();
            graphics.ApplyChanges();
        }

        //***************************************************************************
        protected override void Initialize()
        {
            base.Initialize();
        }
        //***************************************************************************
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tennisBallTexture = Content.Load<Texture2D>(@"images\Tennis-Ball");
            grassTexture = Content.Load<Texture2D>(@"images\grass");
            paddleHandelTexture = Content.Load<Texture2D>(@"images\paddleHandle");
            paddleHitTexture = Content.Load<Texture2D>(@"images\paddleHit");
            brickTexture = Content.Load<Texture2D>(@"images\brick");
        }
        //***************************************************************************
        protected override void UnloadContent()
        {

        }
        public void onePlayerGame()
        {
        }
        //***************************************************************************
        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            KeyboardState newState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                pause = true; // Checks if P is selected to change bool pause to true
            }

            if (newState.IsKeyDown(Keys.PageUp) && oldState.IsKeyUp(Keys.PageUp))
            {
                ballX /= .9;//speed game up
            }


            if (newState.IsKeyDown(Keys.PageDown) && oldState.IsKeyUp(Keys.PageDown))
            { ballX *= .9; } //slows down up game

            oldState = newState;
            if (newState.IsKeyDown(Keys.Enter) || newState.IsKeyDown(Keys.S))//keys to stat game
            {
                start = true;
                pause = false;
            }
            if (!pause && start)//checks is can start game
            {

                ballPos.X += (float)ballX;
                ballPos.Y += (float)ballY;
                rotation += 1;

                Rectangle ballRect = new Rectangle((int)ballPos.X + 15, (int)ballPos.Y + 15,
                    ballPoint.X - 45, ballPoint.Y - 45); //Ball Rectangle

                Rectangle hitRect = new Rectangle(
                    (int)hitPos.X + 50, (int)hitPos.Y + 40, hitPoint.X - 5, hitPoint.Y); //paddle Rectangle

                if (ballRect.Intersects(hitRect))// checks if ballRec and Paddle Rectangle intersect
                {
                    ballX = -ballX;
                    ballPos.Y += (float)rand();
                }
                if (ballRect.Top < top)
                    ballY = -ballY;
                if (ballRect.X <= left + 20)
                    ballX = -ballX;
                if (ballRect.X >= screenWidth - 10) // checks if ball goes past limit then resets ball and paddle
                    reset();
                if (ballRect.Y > bottom)
                    ballY = -ballY;

                //moves paddle
                if (newState.IsKeyDown(Keys.Down))
                    hitPos.Y += 10;
                if (newState.IsKeyDown(Keys.Up))
                    hitPos.Y -= 10;
                if (newState.IsKeyDown(Keys.Left))
                    hitPos.X -= 10;
                if (newState.IsKeyDown(Keys.Right))
                    hitPos.X += 10;
                //sets limits for paddle
                if (hitRect.Top <= top)
                    hitPos.Y = top;
                if (hitPos.X <= left + 100)
                    hitPos.X = left + 100;
                if (hitPos.X >= screenWidth - 100)
                    hitPos.X = screenWidth - 100;
                if (hitPos.Y >= bottom)
                    hitPos.Y = bottom;
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    reset();


            }
        }
        //***************************************************************************
        //draw paddle and ball in original positions
        //***************************************************************************
        public void reset()
        {
            pause = true;
            hitPos = new Vector2(600, 200);
            ballPos = new Vector2(600, 220);
        }
        //***************************************************************************
        //Get random angle for ball paddle collision
        //***************************************************************************
        public double rand()
        {
            Random rnd = new Random();
            ballY = 10 * (float)rnd.Next(-45, 45) / 100;

            return ballY;
        }
        //***************************************************************************
        //Draw textures
        //***************************************************************************      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(grassTexture, new Rectangle(0, 0, screenWidth, screenHieght), Color.White);
            spriteBatch.Draw(brickTexture, new Rectangle(-180, 0, 200, screenHieght), Color.White);
            spriteBatch.Draw(brickTexture, new Rectangle(0, -180, screenWidth, 200), Color.White);
            spriteBatch.Draw(brickTexture, new Rectangle(0, 380, screenWidth, 200), Color.White);
            spriteBatch.Draw(paddleHandelTexture, new Vector2(hitPos.X + 18, hitPos.Y + 82),
         new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
         0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(paddleHitTexture, hitPos, new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
                0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            spriteBatch.Draw(tennisBallTexture, ballPos,
               null, Color.White, rotation, new Vector2(ballPoint.X, ballPoint.Y), 1 / 2f, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

       
    }


}