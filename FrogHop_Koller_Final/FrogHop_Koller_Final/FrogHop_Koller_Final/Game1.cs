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

namespace FrogHop_Koller_Final
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font1;
        Vector2 scorePos;
        Vector2 timePos;
        Rectangle timeBar;
        Rectangle runOutTimeBar;
        Rectangle sandRect;
        int score;
        Texture2D timerTexture;
        Texture2D runOutTimerTexture;
        Texture2D sandTexture;
        int screenWidth = 700;
            int screenHeight=700;
        
       // Texture2D frogSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

         // frogSprite =  Content.Load<Texture2D>(@"Images/frogPlayer");
            font1 = Content.Load<SpriteFont>(@"Images/scoreFont");
            timerTexture = Content.Load<Texture2D>(@"Images/Box");
            sandTexture = Content.Load<Texture2D>(@"Images/sand");
            runOutTimerTexture = Content.Load<Texture2D>(@"Images/Box");
            scorePos.X=10;
            scorePos.Y = 10;
            score = 0;
            timeBar.X = screenWidth - screenWidth/2;
            timeBar.Y = screenHeight-20;
            timeBar.Width = 200;
            timeBar.Height= 20;
            runOutTimeBar = timeBar;
            runOutTimeBar.Width = 0;
            sandRect.X = 0;
            sandRect.Y = 0;
            sandRect.Width = screenWidth;
            sandRect.Height = screenHeight;
            timePos.X = timeBar.X+200;
            timePos.Y = timeBar.Y-5;

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
           
            if (runOutTimeBar.Width != timeBar.Width)
            {
                runOutTimeBar.Width = runOutTimeBar.Width + 1;
            }

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
           // spriteBatch.Draw(frogSprite, new Rectangle(300,100,300,300), Color.White);    
          //  spriteBatch.Draw(sandTexture, sandRect, Color.White);     
            spriteBatch.Draw(timerTexture, timeBar, Color.Green); 
            spriteBatch.Draw(timerTexture, runOutTimeBar, Color.CornflowerBlue);     
            spriteBatch.DrawString(font1, "Score: " + score, scorePos, Color.White);
            spriteBatch.DrawString(font1, "Time Left ", timePos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
