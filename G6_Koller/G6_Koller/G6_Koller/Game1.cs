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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace G6_Koller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public float TimeToInput = 5;
        public float FrameCounter = 0;
        Texture2D background;
        Texture2D block;

        Random random = new Random();

        Rectangle tetrisBounds = new Rectangle(12, 12, 228, 480);
        Rectangle innerBoundsLeft = new Rectangle(12, 30, 36, 462);
        Rectangle innerBoundsRight = new Rectangle(210, 30, 228, 462);
        const int tetrisWidth = 10;
        const int tetrisHeight = 20;
        int[,] tetrisBlockData = new int[tetrisHeight, tetrisWidth];
        Color[,] tetrisBlockColors = new Color[tetrisHeight, tetrisWidth];
        Vector2[,] tetrisBlockPositions = new Vector2[tetrisHeight, tetrisWidth];

        const int numBlocks = 4;
        Block newBlock;
        Board newBoard;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 650;
            graphics.PreferredBackBufferHeight = 700;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
             newBlock = new Block(this.Content, new Vector2(110, 100));
          //  newBoard = new Board(this.Content, new Vector2( 50, 50), 0, 0);
            block = Content.Load<Texture2D>("images/Box");
        }
            // TODO: use this.Content to load your game content here
        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void InitializeTetrisData()
        {
            for (int i = 0; i < tetrisHeight; i++)
            {
                for (int j = 0; j < tetrisWidth; j++)
                {
                    tetrisBlockData[i, j] = 0;
                    tetrisBlockColors[i, j] = Color.White;
                    tetrisBlockPositions[i, j] = Vector2.Zero;
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
         
            // TODO: Add your update logic here
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Left))
                newBlock.Position.X -= 5;

            if (keystate.IsKeyDown(Keys.Right))
                newBlock.Position.X += 5;

            if (keystate.IsKeyDown(Keys.Down))
                newBlock.Position.Y += 5;

            if (keystate.IsKeyDown(Keys.Up))
                newBlock.Position.Y -= 5;

            FrameCounter = 0;
            if (FrameCounter == 0)
                newBlock.Position.Y += 1;
            base.Update(gameTime);
        }

  
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
           // newBoard.Draw(spriteBatch);
            newBlock.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
