//David Koller
//G1_Koller
//9/8
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

namespace G1_Koller
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bananasTexture;
        Texture2D chessBoardTexture;
        Texture2D monkeyTexture;
        Vector2 pos;
        int screenSize = 400;
        int ScaleScreen;
        Rectangle backgroundRectangle;
        public struct BananaFill
        {
            public Vector2 Position;
            public bool IsVisible;
        }
        BananaFill[,] bananas;
        int timeSinceLastMove = 0;
        int milliSecondsPerMove = 500;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = screenSize;
            ScaleScreen = screenSize / 8;
            graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            chessBoardTexture = Content.Load<Texture2D>(@"images\chessboard");
            monkeyTexture = Content.Load<Texture2D>(@"images\monkey");
            bananasTexture = Content.Load<Texture2D>(@"images\bananas");
            backgroundRectangle = new Rectangle(0, 0,
            graphics.GraphicsDevice.Viewport.Width,
            graphics.GraphicsDevice.Viewport.Height);

            int width = 0;
            bananas = new BananaFill[8, 9];
            for (int i = 0; i < 8; i++)
            {
                int hieght = 0;

                for (int j = 0; j < 9; j++)
                {
                   bananas[i, j].IsVisible = true;
                    bananas[i, j].Position = new Vector2(width, hieght);
                    hieght += ScaleScreen;
                }
                width += ScaleScreen;
            }
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {          
            timeSinceLastMove += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastMove > milliSecondsPerMove)
            {
                timeSinceLastMove -= milliSecondsPerMove;
                isVisible();
                if (pos.X == 0 && pos.Y != bananas[0, 7].Position.Y)
                {
                    pos.Y += ScaleScreen;
                }
                else if (pos.Y == bananas[0, 7].Position.Y && pos != bananas[7, 7].Position)
                {
                    pos.X += ScaleScreen;
                }
                else if (pos.X == bananas[7, 7].Position.X && pos != bananas[7, 0].Position)
                {
                    pos.Y -= ScaleScreen;
                }
                else if (pos.Y == bananas[7, 0].Position.Y && pos != bananas[1, 0].Position)
                {
                    pos.X -= ScaleScreen;
                }
                else if (pos.X == bananas[1, 6].Position.X && pos != bananas[1, 6].Position)
                {
                    pos.Y += ScaleScreen;
                }
                else if (pos.Y == bananas[1, 6].Position.Y && pos != bananas[6, 6].Position)
                {
                    pos.X += ScaleScreen;
                }
                else if (pos.X == bananas[6, 1].Position.X && pos != bananas[6, 1].Position)
                {
                    pos.Y -= ScaleScreen;
                }
                else if (pos.Y == bananas[2, 1].Position.Y && pos != bananas[2, 1].Position)
                {
                    pos.X -= ScaleScreen;
                }
                else if (pos.X == bananas[2, 5].Position.X && pos != bananas[2, 5].Position)
                {
                    pos.Y += ScaleScreen;
                }
                else if (pos.Y == bananas[5, 5].Position.Y && pos != bananas[5, 5].Position)
                {
                    pos.X += ScaleScreen;
                }
                else if (pos.X == bananas[5, 2].Position.X && pos != bananas[5, 2].Position)
                {
                    pos.Y -= ScaleScreen;
                }
                else if (pos.Y == bananas[3, 2].Position.Y && pos != bananas[3, 2].Position)
                {
                    pos.X -= ScaleScreen;
                }
                else if (pos.X == bananas[3, 4].Position.X && pos != bananas[3, 4].Position)
                {
                    pos.Y += ScaleScreen;
                }
                else if (pos.Y == bananas[4, 4].Position.Y && pos != bananas[4, 4].Position)
                {
                    pos.X += ScaleScreen;
                }
                else if (pos.X == bananas[4, 3].Position.X && pos != bananas[4, 3].Position)
                {
                    pos.Y -= ScaleScreen;

                }
                else
                {
                    this.Exit();   //exit program
                }
            }
            base.Update(gameTime);
        }
        private void isVisible() //check whether to make array of bananas visible or not
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (bananas[i, j].Position == pos)
                    {
                        bananas[i, j].IsVisible = true;
                    }
                }
            }
        }
        //draw Banna backGround only visible if was past already
        private void DrawBannaTexture()
        {
            foreach (BananaFill banana in bananas)
            {
                if (banana.IsVisible)
                {
                    spriteBatch.Draw(bananasTexture, banana.Position, backgroundRectangle, Color.White, 0,
                        new Vector2(0, 0), 1 / 8f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(bananasTexture, banana.Position, backgroundRectangle, Color.Transparent, 0,
                        new Vector2(0, 0), 1 / 8f, SpriteEffects.None, 0);
                }
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            //draws texture2d
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(chessBoardTexture, backgroundRectangle, Color.White);
            DrawBannaTexture();
            spriteBatch.Draw(monkeyTexture, pos, backgroundRectangle, Color.White, 0,
          new Vector2(0, 0), 1 / 8f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
