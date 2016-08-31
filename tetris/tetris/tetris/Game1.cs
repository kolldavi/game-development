using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Tetris
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        string tetrisstring;

        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;

        int screenWidth;
        int screenHeight;

        SpriteFont font;

        Texture2D background;
        Texture2D block;

        Random random = new Random();

        Rectangle tetrisBounds = new Rectangle(12, 12, 228, 480);
        Rectangle innerBoundsLeft = new Rectangle(12, 30, 36, 462);
        Rectangle innerBoundsRight = new Rectangle(210, 30, 228, 462);
        const int tetrisWidth = 12;
        const int tetrisHeight = 26;
        int[,] tetrisBlockData = new int[tetrisHeight, tetrisWidth];
        Color[,] tetrisBlockColors = new Color[tetrisHeight, tetrisWidth];
        Vector2[,] tetrisBlockPositions = new Vector2[tetrisHeight, tetrisWidth];

        const int numBlocks = 4;
        Shape currentShape;
        Shape nextShape;
       

        int addBlockTime = 1500;
        int lastTickCount;

        int level = 1;
        int score = 0;

        KeyboardState previousKeyboardState = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 434;
            graphics.PreferredBackBufferHeight = 497;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tetris";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Gives direct access to the graphics hardware inside the computer
            device = graphics.GraphicsDevice;

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            font = Content.Load<SpriteFont>("Fonts//gameFont");

           // background = Content.Load<Texture2D>("Sprites//background");
            block = Content.Load<Texture2D>("images/Box");

            currentShape = new Shape(block, SelectShape(), SelectColor(), numBlocks);
            nextShape = new Shape(block, SelectShape(), SelectColor(), numBlocks);

            // Initialize a counter
            lastTickCount = System.Environment.TickCount;

            InitializeTetrisData();
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

        public int SelectShape()
        {
            return random.Next(7);
        }

        public int SelectColor()
        {
            return random.Next(7);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            CheckBlockCollision();

            UpdateTetrisBlockDataArrayPositions();

            MoveShapeDown();

            ShapeControls();

            CheckTetrisBoundsCollision();

            base.Update(gameTime);
        }

        private void ShapeControls()
        {
            //... input from keyboard
        }

        private void ResetTetrisBlockDataArrayPositions()
        {
            for (int i = 0; i < 4; i++)
            {
                tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                    currentShape.tetrisBlockData_ArrayPosition[i, 1]] = 0;
            }
        }

        private void UpdateTetrisBlockDataArrayPositions()
        {
            for (int i = 0; i < 4; i++)
            {
                tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                    currentShape.tetrisBlockData_ArrayPosition[i, 1]] = 1;
            }
        }

        private void StoreTetrisBlockDataArrayPositions()
        {
            for (int i = 0; i < 4; i++)
            {
                tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                    currentShape.tetrisBlockData_ArrayPosition[i, 1]] = 2;

                tetrisBlockColors[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                    currentShape.tetrisBlockData_ArrayPosition[i, 1]] =
                        currentShape.DetermineShapeColor(currentShape.ColorNumber);

                tetrisBlockPositions[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                    currentShape.tetrisBlockData_ArrayPosition[i, 1]] = currentShape.block[i].position;
            }

            currentShape = nextShape;

            nextShape = new Shape(Content.Load<Texture2D>("Sprites//block"), SelectShape(), SelectColor(), numBlocks);
        }

        private void MoveShapeDown()
        {
            if (((System.Environment.TickCount - lastTickCount) > addBlockTime) && (!currentShape.touchBoundsBottom)
                && (!currentShape.touchAnotherBlockBottom))
            {
                lastTickCount = System.Environment.TickCount;

                for (int i = 0; i < numBlocks; i++)
                {
                    currentShape.block[i].position.Y += 18;
                }

                ResetTetrisBlockDataArrayPositions();

                for (int i = 0; i < 4; i++)
                {
                    currentShape.tetrisBlockData_ArrayPosition[i, 0]++;
                }
            }
        }

        private void CheckTetrisBoundsCollision()
        {
            currentShape.touchBoundsLeft = false;
            currentShape.touchBoundsRight = false;

            for (int i = 0; i < numBlocks; i++)
            {
                // Left Collision with Tetris Grid
                if (currentShape.block[i].position.X == tetrisBounds.X)
                {
                    currentShape.touchBoundsLeft = true;
                }

                // Right Collision with Tetris Grid
                if (currentShape.block[i].position.X + currentShape.block[i].blockSize.X > tetrisBounds.Width)
                {
                    currentShape.touchBoundsRight = true;
                }

                // Bottom Collision with Tetris Grid
                if (currentShape.block[i].position.Y + currentShape.block[i].blockSize.Y > tetrisBounds.Height)
                {
                    currentShape.touchBoundsBottom = true;

                    if ((System.Environment.TickCount - lastTickCount) > 200)
                    {
                        StoreTetrisBlockDataArrayPositions();

                        for (int x = 0; x < tetrisHeight; x++)
                        {
                            CheckWinningLines();
                        }

                        break;
                    }
                }
            }
        }

        private void CheckBlockCollision()
        {
            currentShape.touchAnotherBlockSideLeft = false;
            currentShape.touchAnotherBlockSideRight = false;

            for (int i = 0; i < numBlocks; i++)
            {
                // Left Side Collision with other Blocks
                if (!currentShape.touchBoundsLeft)
                {
                    if (tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                            currentShape.tetrisBlockData_ArrayPosition[i, 1] - 1] == 2)
                    {
                        currentShape.touchAnotherBlockSideLeft = true;
                    }
                }

                // Right Side Collision with other Blocks
                if (!currentShape.touchBoundsRight)
                {
                    if (tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0],
                            currentShape.tetrisBlockData_ArrayPosition[i, 1] + 1] == 2)
                    {
                        currentShape.touchAnotherBlockSideRight = true;
                    }
                }

                // Bottom Collision with other Blocks
                if (!currentShape.touchBoundsBottom)
                {
                    if (tetrisBlockData[currentShape.tetrisBlockData_ArrayPosition[i, 0] + 1,
                            currentShape.tetrisBlockData_ArrayPosition[i, 1]] == 2)
                    {
                        currentShape.touchAnotherBlockBottom = true;

                        if ((System.Environment.TickCount - lastTickCount) > 200)
                        {
                            StoreTetrisBlockDataArrayPositions();

                            for (int x = 0; x < tetrisHeight; x++)
                            {
                                CheckWinningLines();
                            }

                            break;
                        }
                    }
                }
            }
        }

        private void CheckWinningLines()
        {
            for (int i = tetrisHeight - 1; i > 0; i--)
            {
                int count = 0;

                for (int j = 0; j < tetrisWidth; j++)
                {
                    if (!(tetrisBlockData[i, j] == 2))
                    {
                        break;
                    }
                    else
                    {
                        count++;

                        if (count == tetrisWidth)
                        {
                            for (int y = 0; y < tetrisWidth; y++)
                            {
                                tetrisBlockData[i, y] = 0;
                                tetrisBlockColors[i, y] = Color.White;
                                tetrisBlockPositions[i, y] = Vector2.Zero;
                            }

                            for (int x = i; x > 0; x--)
                            {
                                for (int y = 0; y < tetrisWidth; y++)
                                {
                                    tetrisBlockData[x, y] = tetrisBlockData[x - 1, y];
                                    tetrisBlockColors[x, y] = tetrisBlockColors[x - 1, y];
                                    tetrisBlockPositions[x, y] = tetrisBlockPositions[x - 1, y];
                                }
                            }

                            for (int y = 0; y < tetrisWidth; y++)
                            {
                                tetrisBlockData[0, y] = 0;
                                tetrisBlockColors[0, y] = Color.White;
                                tetrisBlockPositions[0, y] = Vector2.Zero;
                            }
                        }
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawBackground();
            DrawPreviousShapes();
            DrawCurrentShape();
            DrawNextShape();
            DrawFont();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            //..
        }

        private void DrawPreviousShapes()
        {
            for (int i = 0; i < tetrisHeight; i++)
            {
                for (int j = 0; j < tetrisWidth; j++)
                {
                    if (tetrisBlockData[i, j] == 2)
                    {
                        spriteBatch.Draw(block, tetrisBlockPositions[i, j], tetrisBlockColors[i, j]);
                    }
                }
            }
        }

        private void DrawCurrentShape()
        {
            for (int i = 0; i < numBlocks; i++)
            {
                spriteBatch.Draw(currentShape.block[i].texture,
                    currentShape.block[i].position,
                    currentShape.block[i].color);
            }
        }

        private void DrawNextShape()
        {
            //..
        }

        private void DrawFont()
        {
            //..
        }
    }
}