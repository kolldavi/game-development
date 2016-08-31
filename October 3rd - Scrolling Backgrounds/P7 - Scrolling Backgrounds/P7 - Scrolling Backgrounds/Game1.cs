
/*---------------------------------------------------------------------
 * P7 Scrolling Backgrounds
 * 
 * Game to demonstrate the MultiBackground class
 *  This class includes scrolling backgrounds and background layers
 * -------------------------------------------------------------------
 */

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

namespace P7___Scrolling_Backgrounds
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        MultiBackground background; //Define a MultiBackground object

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Create the new background object
            background = new MultiBackground(graphics);
            //Create a texture and add it as a background layer with -ve movement
            Texture2D hill = Content.Load<Texture2D>(@"Images\HillLayer");
            background.AddLayer(hill, 0.0f, -100.0f); //Hills on top of sky
            //Create a texture and add it as a background layer with +ve movement (L to R)
            Texture2D sky = Content.Load<Texture2D>(@"Images\SkyLayer");
            background.AddLayer(sky, 0.5f, 200.0f);
            background.StartMoving(); //Start the backgrounds moving
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            background.Update(gameTime); //Call the backgrounds Update method

            UpdateInput(); //Update the user input


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            background.Draw(); //Draw the background

            base.Draw(gameTime);
        }

        //-----------------------------------------------------------------------
        public void UpdateInput()
        {
            KeyboardState keyState = Keyboard.GetState();
            //Exit if X pressed
            if (keyState.IsKeyDown(Keys.X))
                this.Exit();

            // The following code is to demo movement, without automatic scrolling
            //   If the left key is pressed, move to the left, right to the right
            //   Holding down either shift key while doing this causes a larger move
            //  Warning : If you have vertical scrolling, the left & right keys will 
            //   move up and down (the Move() method does both).
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (keyState.IsKeyDown(Keys.LeftShift) ||
                    keyState.IsKeyDown(Keys.RightShift))
                    background.Move(-20.0f);
                else
                    background.Move(-2.0f);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                if (keyState.IsKeyDown(Keys.LeftShift) ||
                    keyState.IsKeyDown(Keys.RightShift))
                    background.Move(20.0f);
                else
                    background.Move(2.0f);
            }

            //Changing the movement
            //  M - start the scrolling
            //  S - stop the scrolling
            //  U - up/down (vertical movement/scrolling)
            //  R - right/left (horizontal movement/scrolling)
            if (keyState.IsKeyDown(Keys.M))
                background.StartMoving();
            if (keyState.IsKeyDown(Keys.S))
                background.Stop();
            if (keyState.IsKeyDown(Keys.U))
                background.SetMoveUpDown();
            if (keyState.IsKeyDown(Keys.R))
                background.SetMoveLeftRight();
        }


    }
}
