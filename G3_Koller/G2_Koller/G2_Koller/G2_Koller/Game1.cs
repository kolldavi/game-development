/***************************************************************************
 *David Koller
 *Assignment 3
 *10/02/11
 * extra 1 play sound when hit ball
 * extra 2 menu screen
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
        Texture2D startTexture;
        Texture2D grayBack;
        Texture2D meTexture;
        SoundEffect hitSound;
        SpriteFont font;
        bool pause;
        bool start;
        Point hitPoint = new Point(100, 100);
        Point ballPoint = new Point(75, 75);
        float rotation;
        Vector2 hitPos = new Vector2(600, 200);
        Vector2 hitPos2 = new Vector2(50, 200);
        Vector2 ballPos = new Vector2(600, 220);
        Point moveGray = new Point(0, 0);
        int screenWidth = 800;
        int screenHieght = 400;
        int top = 40;
        int bottom = 375;
        int left = 30;
        double ballX = 5;
        double ballY;
        int score1=0;
        int score2 = 0;
        string winner;
       
        KeyboardState oldState = Keyboard.GetState();
        
        public enum State { Start, InGame1, Welcome, Help,InGame2,ExitScreen };

        State gameState = State.Welcome;
     
        State pastState = State.Start;
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
            startTexture = Content.Load<Texture2D>(@"images\StartPage");
            font = Content.Load<SpriteFont>(@"images\SpriteFont1");
           grayBack= Content.Load<Texture2D>(@"images\grayBack");
           meTexture = Content.Load<Texture2D>(@"images\me");
           hitSound = Content.Load<SoundEffect>(@"Sound\hit");
        }
      
        //***************************************************************************
        protected override void UnloadContent()
        {

        }
    
        //***************************************************************************
        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            KeyboardState newState = Keyboard.GetState();
                switch (gameState)
                {
                    case State.Welcome:
                        {

                            if (gameTime.TotalGameTime.Seconds > 5)
                            {

                                gameState = State.Start;
                            }
                        }
                        break;
                    case State.Start:
                        if (newState.IsKeyDown(Keys.Enter) && counter == 0)
                        {
                            gameState = State.InGame1;
                            pastState = State.InGame1;
                            resetOnePlayer();
                        }
                        else if (newState.IsKeyDown(Keys.Enter) && counter == 1)
                        {
                            gameState = State.InGame2;
                            pastState = State.InGame2;

                        }
                        else if (newState.IsKeyDown(Keys.Enter)  && counter == 2)
                        {
                            gameState = State.Help;
                            pastState = State.Start;
                            oldState = newState;
                        }                
                        else if (gameState == State.Start)
                        {
                            MainMenu();
                        }
                        break;
                    case State.InGame1:
                        {
                            if (gameState == State.InGame1)
                            {
                                HelpScreen();
                                onePlayerGame();                                
                                pastState = State.InGame1;
                            }
                            if (newState.IsKeyDown(Keys.R))
                            {
                                gameState = State.Start;                              
                            }

                        }
                        break;
                    case State.InGame2:
                        if (gameState == State.InGame2)
                        {
                            HelpScreen(); 
                            TwoPlayerGame();                                                                           
                        }
                        if (score1 == 3 || score2 == 3)
                        {
                            winScreen();
                            gameState = State.ExitScreen;
                        }
                        if (newState.IsKeyDown(Keys.R))
                        {
                            gameState = State.Start;
                            score1 = 0;
                            score2 = 0;
                        }
                        break;
                    case State.ExitScreen:
                        {
                            if (Keyboard.GetState().GetPressedKeys().Length > 0 )
                            {
                                this.Exit();
                            }
                        }
                        break;
                    case State.Help:
                        {                            
                                if (Keyboard.GetState().GetPressedKeys().Length > 0 && oldState!=newState)
                                {
                                    if (pastState == State.InGame1)
                                    {
                                        gameState = State.InGame1;
                                    }
                                    if (pastState == State.Start)
                                    {
                                        gameState = State.Start;
                                    }
                                    if (pastState == State.InGame2)
                                    {
                                        gameState = State.InGame2;
                                    }
                                    oldState = newState;
                                }
                            break;                       
                       

                        }        
            }
        }
        //*********************************************************************
        //Display Exit Screen
        //*********************************************************************
        public void winScreen()
        {
            if (score1 == 3)
            {
                winner = "Player One";
            }
            if (score2 == 3)
            {
                winner = "Player Two";
            }
        }
        //*********************************************************************
        //Display help Screen
        //*********************************************************************
        public void HelpScreen()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
            {
                gameState = State.Help;             
                pause = true;
            }
     
    
        }
        //*********************************************************************
        //two player game Screen
        //*********************************************************************
        public void TwoPlayerGame()
        {
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
            if (newState.IsKeyDown(Keys.Enter))//keys to stat game
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
                    (int)hitPos.X + 50, (int)hitPos.Y + 40, hitPoint.X - 25, hitPoint.Y - 25); //paddle Rectangle
                Rectangle hitRect2 = new Rectangle(
                                 (int)hitPos2.X + 50, (int)hitPos2.Y + 40, hitPoint.X - 25, hitPoint.Y - 25); //paddle Rectangle

                if (hitRect2.Contains(ballRect))
                {
                    ballPos.X += 60;
                    hitPos2.X -= 10;
                }
                if (hitRect2.Intersects(ballRect))
                {
                    ballX = -ballX;
                    hitSound.Play();
                    ballPos.Y += (float)rand();
                }

                if (hitRect.Contains(ballRect))//check if ball gets stuck inside paddle
                {
                    ballPos.X -= 60;
                    hitPos.X += 10;
                }
                if (hitRect.Intersects(ballRect))
                {
                    ballX = -ballX;
                    hitSound.Play();
                   ballPos.Y += (float)rand();
                }
                if (ballRect.Top < top)
                    ballY = -ballY;
                if (ballRect.X <= left + 20)
                {
                    ballX = -ballX;
                    score2 += 1;
                    hitPos = new Vector2(600, 200);
                    hitPos2 = new Vector2(50, 200);
                    ballPos = new Vector2(160, 220);
                }
                if (ballRect.X >= screenWidth - 10) // checks if ball goes past limit then resets ball and paddle
                {
                    score1 += 1;
                    hitPos = new Vector2(600, 200);
                    hitPos2 = new Vector2(50, 200);
                    ballPos = new Vector2(600, 220);
                }
                if (ballRect.Y > bottom)
                    ballY = -ballY;
                //moves paddle 2
                if (newState.IsKeyDown(Keys.S))
                    hitPos2.Y += 7;
                if (newState.IsKeyDown(Keys.W))
                    hitPos2.Y -= 7;
                if (newState.IsKeyDown(Keys.A))
                    hitPos2.X -= 7;
                if (newState.IsKeyDown(Keys.D))
                    hitPos2.X += 7;
                //moves paddle
                if (newState.IsKeyDown(Keys.Down))
                    hitPos.Y += 7;
                if (newState.IsKeyDown(Keys.Up))
                    hitPos.Y -= 7;
                if (newState.IsKeyDown(Keys.Left))
                    hitPos.X -= 7;
                if (newState.IsKeyDown(Keys.Right))
                    hitPos.X += 7;
                //sets limits for paddle
                if (hitRect2.Top < top)
                    hitPos2.Y = top - 30;
                if (hitPos2.X <= left-20)
                    hitPos2.X = left-20;
                if (hitPos2.X >= screenWidth/2-100)
                    hitPos2.X = screenWidth/2-100;
                if (hitPos2.Y >= bottom)
                    hitPos2.Y = bottom;
                //sets limits for paddle
                if (hitRect.Top < top)
                    hitPos.Y = top-30;
                if (hitPos.X <= screenWidth/2)
                    hitPos.X = screenWidth/2;
                if (hitPos.X >= screenWidth - 100)
                    hitPos.X = screenWidth - 100;
                if (hitPos.Y >= bottom)
                    hitPos.Y = bottom;

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    resetOnePlayer();
            }
        }
        //*********************************************************************
        //Display Help if h is slected Screen
        //************************************************************************
        public void Help()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
            {
                gameState = State.Help;
            }
        }

        int counter = 0;//keeps track of menu
        public void MainMenu()
        {
            Help();
             KeyboardState newState = Keyboard.GetState();
             if (newState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) && counter < 2)
             {
                 moveGray.Y += 50;
                 counter++;
             }
             if (newState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) && counter > 0)
             {
                 moveGray.Y -= 50;          
                 counter--;
             }
             oldState = newState;
         
        }
        //*********************************************************************
        //One player game Screen
        //*********************************************************************
           public void onePlayerGame()     
        {
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
                    (int)hitPos.X + 50, (int)hitPos.Y+40, hitPoint.X-25, hitPoint.Y-25); //paddle Rectangle

                if(hitRect.Contains(ballRect))
                {
                    ballPos.X -= 60;
                    hitPos.X += 10;
                }
                if (hitRect.Intersects(ballRect) )
                    {
                    ballX = -ballX;
                    ballPos.Y += (float)rand();
                    hitSound.Play();
                    }
             
                if (ballRect.Top < top)
                    ballY = -ballY;
                if (ballRect.X <= left + 20)
                    ballX = -ballX;
                if (ballRect.X >= screenWidth - 10) // checks if ball goes past limit then resets ball and paddle
                    resetOnePlayer();
                if (ballRect.Y > bottom)
                    ballY = -ballY;

                //moves paddle
                if (newState.IsKeyDown(Keys.Down))
                    hitPos.Y += 7;
                if (newState.IsKeyDown(Keys.Up))
                    hitPos.Y -= 7;
                if (newState.IsKeyDown(Keys.Left))
                    hitPos.X -= 7;
                if (newState.IsKeyDown(Keys.Right))
                    hitPos.X += 7;
                //sets limits for paddle
                if (hitRect.Top < top-30)
                    hitPos.Y = top-30;
                if (hitPos.X <= left + 100)
                    hitPos.X = left + 100;
                if (hitPos.X >= screenWidth - 100)
                    hitPos.X = screenWidth - 100;
                if (hitPos.Y >= bottom)
                    hitPos.Y = bottom;
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    resetOnePlayer();
            }
        }
        //***************************************************************************
        //draw paddle and ball in original positions
        //***************************************************************************
           public void resetOnePlayer()
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
            if (gameState == State.InGame1)//draw one player game
            {               
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
                   null, Color.White, rotation, new Vector2(ballPoint.X, ballPoint.Y), 1 / 3f, SpriteEffects.None, 0);
             
            }
            if (gameState == State.Start)//draw start menu
            {
            
                spriteBatch.Draw(startTexture, new Rectangle(0,0, screenWidth, screenHieght), Color.White);
                spriteBatch.Draw(grayBack, new Rectangle(moveGray.X+50, moveGray.Y+50, 125, 30), Color.White);
                spriteBatch.DrawString(font, "One Player", new Vector2(50, 50), Color.Red);
                spriteBatch.DrawString(font, "Two Players", new Vector2(50,100), Color.Red);
                spriteBatch.DrawString(font, "Help", new Vector2(50, 150), Color.Red);
            }
            if (gameState == State.Welcome)//draw welcome screen
            {
                spriteBatch.Draw(grayBack, new Rectangle(0, 0, screenWidth, screenHieght), Color.White);
                spriteBatch.Draw(meTexture, new Rectangle(500, 100, 150, 150), Color.White);
                spriteBatch.DrawString(font, "Created By David Koller", new Vector2(50, 100), Color.Red);
                spriteBatch.DrawString(font, "Select H for help", new Vector2(50, 150), Color.Red);
                spriteBatch.DrawString(font, "Extra feature 1: Play Sound when hit Ball", new Vector2(50, 200), Color.Red);
                spriteBatch.DrawString(font, "Extra feature 2: Menu Screen", new Vector2(50, 250), Color.Red);

            }
            if (gameState == State.Help)//draw help screen
            {
                spriteBatch.Draw(grayBack, new Rectangle(0, 0, screenWidth, screenHieght), Color.White);               
                spriteBatch.DrawString(font, "Select R to Reset", new Vector2(50, 100), Color.Red);
                spriteBatch.DrawString(font, "Select H for help", new Vector2(50, 150), Color.Red);
                spriteBatch.DrawString(font, "Select page up to speed ball up", new Vector2(50, 200), Color.Red);
                spriteBatch.DrawString(font, "Select page down to slow ball down", new Vector2(50, 250), Color.Red);
                spriteBatch.DrawString(font, "Select p to pause game", new Vector2(50, 300), Color.Red);
                spriteBatch.DrawString(font, "Select player one use a,s,d,w to move", new Vector2(50, 300), Color.Red);
                spriteBatch.DrawString(font, "Select player two use arrows to move", new Vector2(50, 350), Color.Red);
            }
            if (gameState == State.InGame2)//draw two player game
            {
                   spriteBatch.Draw(grassTexture, new Rectangle(0, 0, screenWidth, screenHieght), Color.White);
                   spriteBatch.Draw(grayBack, new Rectangle(screenWidth / 2 - 10, 0, 10, screenHieght), Color.White);  
                spriteBatch.Draw(brickTexture, new Rectangle(-180, 0, 200, screenHieght), Color.White);
                spriteBatch.Draw(brickTexture, new Rectangle(780, 0, 200, screenHieght), Color.White);
                spriteBatch.Draw(brickTexture, new Rectangle(0, -180, screenWidth, 200), Color.White);
                spriteBatch.Draw(brickTexture, new Rectangle(0, 380, screenWidth, 200), Color.White);             
                spriteBatch.Draw(paddleHandelTexture, new Vector2(hitPos.X + 18, hitPos.Y + 82),
             new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
             0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.Draw(paddleHitTexture, hitPos, new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
                    0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.Draw(paddleHandelTexture, new Vector2(hitPos.X + 18, hitPos.Y + 82),
       new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
       0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.Draw(paddleHitTexture, hitPos2, new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
                    0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.Draw(tennisBallTexture, ballPos,
                   null, Color.White, rotation, new Vector2(ballPoint.X, ballPoint.Y), 1 / 3f, SpriteEffects.None, 0);
                spriteBatch.Draw(paddleHandelTexture, new Vector2(hitPos2.X + 18, hitPos2.Y + 82),
           new Rectangle(0, 0, hitPoint.X, hitPoint.Y), Color.White,
           0, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Player One:"+score1, new Vector2(50, 350), Color.Snow);
                spriteBatch.DrawString(font, "Player Two:" + score2, new Vector2(600, 350), Color.Snow);
            }
            if (gameState == State.ExitScreen)//draw exit screen
            {
                spriteBatch.Draw(grayBack, new Rectangle(0, 0, screenWidth, screenHieght), Color.White);
                spriteBatch.DrawString(font, "Player One: "+ score1, new Vector2(50, 100), Color.Red);
                spriteBatch.DrawString(font, "Player Two: "+score2, new Vector2(250, 100), Color.Red);
                spriteBatch.DrawString(font, "Congratulations " + winner, new Vector2(50, 200), Color.Red);
            }
            spriteBatch.End();

            base.Draw(gameTime);


        }
       
    }


}