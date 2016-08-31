//*******************************************************************
//David Koller
//A4 Koller
//*******************************************************************
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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth = 1000;
        int screenHieght = 700;
        SpriteManager spriteManager;
     //   Texture2D background;
        MultiBackground background;
        bool start = false;
   
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = screenHieght;
            graphics.PreferredBackBufferWidth = screenWidth;

        }

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
         
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new MultiBackground(graphics);
            Texture2D space = Content.Load<Texture2D>(@"Images/backgroundTexture");
            background.AddLayer(space, 0.0f, 100.0f);
                background.SetMoveUpDown();
            background.StartMoving();
        }

        protected override void UnloadContent()
        {
            
        }
        //*******************************************************************
        //update method for main
        //*******************************************************************
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                start = true;
            }
            if (start == true)
            {
                background.Update(gameTime);
            }
           
            base.Update(gameTime);
        }
        //*******************************************************************
        //draw method for main
        //*******************************************************************
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);      
            background.Draw();
  
            base.Draw(gameTime);
        }
    }
}
