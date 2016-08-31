//David Koller
//G6_Koller

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

namespace G6_Koller1
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch           spriteBatch;
        SpriteFont            defaultSpriteFont;
        SpriteFont            gameOverSpriteFont; 
        Texture2D boardkTexture;        
        Board                 board;

        bool gameOver = false;       
        double FrameDelayConst = 300;    
        double FrameDelayDelta = 20;    
        double FrameDelay = 0;
        int level=1;
        int Frames = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 550;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }      


        protected override void Initialize()
        {    
            base.Initialize();
            oldState = Keyboard.GetState();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );
            defaultSpriteFont = this.Content.Load<SpriteFont>("font/SpriteFont1");
            gameOverSpriteFont = this.Content.Load<SpriteFont>("font/SpriteFont2");
            boardkTexture = this.Content.Load<Texture2D>("images/Box");      
            board = new Board(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        KeyboardState oldState;
        protected override void Update( GameTime gameTime )
        {
            KeyboardState state = Keyboard.GetState();

            if ( state.IsKeyDown( Keys.Escape ) )
                this.Exit();

            Frames++;
            //*************************
            //check keyboard input
            //*************************
            Board.PieceMove move = Board.PieceMove.None;
            if (state.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left)||
                (state.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A)))
                move = Board.PieceMove.Left;
            else if (state.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right) ||
                (state.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D)))
                move = Board.PieceMove.Right;
            else if (state.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up) ||
                (state.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W)))
                move = Board.PieceMove.Rotate;
            else if (state.IsKeyDown(Keys.Down) && !oldState.IsKeyDown(Keys.Down) ||
                (state.IsKeyDown(Keys.X) && !oldState.IsKeyDown(Keys.X)))
                move = Board.PieceMove.Down;
            else if (state.IsKeyDown(Keys.Space))
                move = Board.PieceMove.FarDown;
            else if (state.IsKeyDown(Keys.P) && !oldState.IsKeyDown(Keys.P))
                move = Board.PieceMove.Pause;
            else if (state.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
                move = Board.PieceMove.Resume;
            //move shapes 
            FrameDelay += gameTime.ElapsedGameTime.TotalMilliseconds;
            if ( FrameDelay > FrameDelayConst )
            {
                FrameDelay = 0;
                if (!board.Update())
                {
                    move = Board.PieceMove.Pause;
                    gameOver = true;           
                }
            }

            board.MoveCurrentPiece(move);
            //*************************
            //speeds up falling speed
            //*************************
            if(FrameDelayConst>=100)
            if (board.LinesCompleted>=10 )
            {
                FrameDelayConst -= FrameDelayDelta;
                board.LinesCompleted = 0;
                level += 1;
            }

            oldState = state;
            base.Update(gameTime);
        }
        //*************************
        //Draw game
        //*************************
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            board.DrawOntoSpriteBatch(spriteBatch, defaultSpriteFont, boardkTexture);
            spriteBatch.DrawString(this.defaultSpriteFont, "Level: " + level, new Vector2(
                300, 250), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "_______Controls_______ ", new Vector2(
                300, 300), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "left,right,a,d: move Piece", new Vector2(
              300, 330), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "down, x: move Piece down 5", new Vector2(
            300, 360), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "up, w: rotate Piece", new Vector2(
            300, 390), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "Spacebar moves Piece to bottom", new Vector2(
            300, 420), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(this.defaultSpriteFont, "p: pause Enter: Resume", new Vector2(
         300, 450), Color.Turquoise, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            if (gameOver)
                spriteBatch.DrawString(this.gameOverSpriteFont, "Game Over", new Vector2(
          this.graphics.GraphicsDevice.Viewport.Width / 2, this.graphics.GraphicsDevice.Viewport.Height / 2),
          Color.Red, 0.0f, new Vector2(100, 100), 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
