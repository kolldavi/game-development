using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace G4_Koller
{
    // Each layer of the background has an object of this type
    class BackgroundLayer
    {
        public Texture2D picture; //Picture being used
        public Vector2 position = Vector2.Zero; //Position for drawing
        public Vector2 offset = Vector2.Zero; //Start offset (if wanted to start elsewhere)
        public float depth = 0.0f; //Determines order of layers while drawing (0 top, 1 bottom)
        public float moveRate = 0.0f; //How fast it should move in pixels per second
        public Vector2 pictureSize = Vector2.Zero; //Size of the picture being used
        public Color color = Color.White; //Color Channel - White is no tinting
    }

    //--------------------------------------------------------
    //  MultiBackground Class
    //    Implements multiple backgrounds that can scroll
    //
    class MultiBackground
    {
        private bool moving = false; //Is it currently moving or not
        private bool moveLeftRight = true; //If false, moving up/down

        private Vector2 windowSize; //Size of Window in which background is drawn

        // List of BackgroundLayers
        private List<BackgroundLayer> layerList = new List<BackgroundLayer>();

        private SpriteBatch batch; //for drawing backgrounds

        //---------------------------------------------------
        // Constructor
        public MultiBackground(GraphicsDeviceManager graphics)
        {
            //Set the window size and create a new SpriteBatch to draw the backgrounds
            windowSize.X = graphics.PreferredBackBufferWidth;
            windowSize.Y = graphics.PreferredBackBufferHeight;
            batch = new SpriteBatch(graphics.GraphicsDevice);
        }

        //---------------------------------------------------
        // AddLayer method
        //   Adds in another layer (picture) to the background list
        //     Parameters are the picture being added, depth (order in
        //     which it is displayed and speed with which it moves.
        public void AddLayer(Texture2D picture, float depth, float moveRate)
        {
            BackgroundLayer layer = new BackgroundLayer();
            layer.picture = picture;
            layer.depth = depth;
            layer.moveRate = moveRate;
            layer.pictureSize.X = picture.Width;
            layer.pictureSize.Y = picture.Height;

            layerList.Add(layer);
        }

        //------------------------------------------------------------
        // CompareDepth method
        //   Given two background layers, it compares their depths
        //     Used for sorting
        public int CompareDepth(BackgroundLayer layer1, BackgroundLayer layer2)
        {
            if (layer1.depth < layer2.depth)
            {
                return 1;
            }
            else if (layer1.depth > layer2.depth)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        //-----------------------------------------------------------------
        //  Move method
        //    Moves the background layers. This is used to move the 
        //    backgrounds when scrolling is not on (one time move)
        //    rate parameter(If a layer has a moveRate of 80 pixels/sec
        //      to move it 40, we would send in a rate of 0.5)
        //    Movement direction is dependent on the moveLeftRight field
        //    Does not sort/order layers before moving
        //
        public void Move(float rate)
        {
            float moveRate = rate / 60.0f; //Movement in a frame
            float moveDistance;
            if (!moving) //Only used if not already scrolling
            {
                foreach (BackgroundLayer layer in layerList)
                {
                    //Distance moved is dependent on moveRate for the
                    //  layer and rate passed in.
                    moveDistance = layer.moveRate * moveRate;


                    if (moveLeftRight) //Horizontal move
                    {
                        layer.position.X += moveDistance;
                        // If moving past end, go back to beginning
                        layer.position.X = layer.position.X % layer.pictureSize.X;
                    }
                    else //Vertical move
                    {
                        layer.position.Y += moveDistance;
                        layer.position.Y = layer.position.Y % layer.pictureSize.Y;
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        // Draw method
        //  This method actually draws out the background layers
        //  Called from the Draw method of the game
        //
        public void Draw()
        {
            //Sort the layers (on depth) so that they are drawn in order
            layerList.Sort(CompareDepth);

            batch.Begin(); //Begin drawing

            for (int i = 0; i < layerList.Count; i++) //go through each layer
            {
                if (!moveLeftRight) //Vertical scroll/move
                {
                    //Draw the picture if it is before the end of the screen
                    //  Will almost always be true if layers are correctly created
                    if (layerList[i].position.Y < windowSize.Y)
                    {
                        batch.Draw(layerList[i].picture, new Vector2(0.0f, layerList[i].position.Y), layerList[i].color);
                    }

                    //Drawing the second part of the picture (above/below the first)
                    if (layerList[i].position.Y > 0.0f) //+ve movement
                        //Drawing top of picture off the screen, so that the exact amount
                        // is on the screen, upto what was drawn above (ending at position.y)
                        batch.Draw(layerList[i].picture, new Vector2(0.0f, layerList[i].position.Y - layerList[i].pictureSize.Y), layerList[i].color);
                    else //-ve movement, therefore -ve position.y
                        batch.Draw(layerList[i].picture, new Vector2(0.0f, layerList[i].position.Y + layerList[i].pictureSize.Y), layerList[i].color);
                }
                else //Horizontal scroll/move
                {
                    if (layerList[i].position.X < windowSize.X)
                    {
                        batch.Draw(layerList[i].picture, new Vector2(layerList[i].position.X,
                            0.0f), layerList[i].color);
                    }
                    if (layerList[i].position.X > 0.0f) //Moving left to right
                        batch.Draw(layerList[i].picture, new Vector2
                            (layerList[i].position.X - layerList[i].pictureSize.X, 0.0f),
                            layerList[i].color);
                    else //Moving right to left (-ve moveRate, x position)
                        batch.Draw(layerList[i].picture, new Vector2
                            (layerList[i].position.X + layerList[i].pictureSize.X, 0.0f),
                            layerList[i].color);
                }
            }
            batch.End();
        }

        //---------------------------------------------------------------
        // SetMoveUpDown method
        //   changes moveLeftRight to false, indicating a vertical move/scroll
        public void SetMoveUpDown()
        {
            moveLeftRight = false;
        }

        //---------------------------------------------------------------
        // SetMoveLeftRight method
        //   changes moveLeftRight to true, indicating a horizontal move/scroll
        public void SetMoveLeftRight()
        {
            moveLeftRight = true;
        }


        //---------------------------------------------------------------
        //  Stop method
        //      Stops the movement
        public void Stop()
        {
            moving = false;
        }

        //---------------------------------------------------------------
        //  StartMoving method
        //      Starts the movement
        public void StartMoving()
        {
            moving = true;
        }

        //---------------------------------------------------------------
        //  SetLayerPosition method
        //     Can change the starting position of a layer
        public void SetLayerPosition(int layerNumber, Vector2 startPosition)
        {
            if (layerNumber < 0 || layerNumber >= layerList.Count) return;

            layerList[layerNumber].position = startPosition;
        }

        //---------------------------------------------------------------
        //  SetLayerAlpha method
        //     Can change the tint of a layer
        public void SetLayerAlpha(int layerNumber, float percent)
        {
            if (layerNumber < 0 || layerNumber >= layerList.Count) return;

            float alpha = (percent / 100.0f);

            layerList[layerNumber].color = new Color(new Vector4(0.0f, 0.0f, 0.0f, alpha));
        }

        //---------------------------------------------------------------
        //  Update method
        //     If the backgrounds are scrolling, changes the position of the
        //     background for each layer. 
        //     Called from the update method of the game
        //     Parameter is not really needed - kept for uniformity
        public void Update(GameTime gameTime)
        {

            if (moving) //Only change position if they are moving
            {
                foreach (BackgroundLayer layer in layerList)
                {
                    //Convert the layer's moveRate to a frame rate
                    float moveDistance = layer.moveRate / 60.0f;

                    if (moveLeftRight) //Horizontal scroll - change x
                    {
                        layer.position.X += moveDistance;
                        // Mod (%) to scroll back to the beginning of the picture
                        layer.position.X = layer.position.X % layer.pictureSize.X;
                    }
                    else //Vertical scroll - change y
                    {
                        layer.position.Y += moveDistance;
                        layer.position.Y = layer.position.Y % layer.pictureSize.Y;
                    }
                }
            }
        }
    }
}