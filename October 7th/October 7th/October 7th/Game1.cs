/*------------------------------------------------------------------------------
 * 
 *  October 7th 2011
 *  
 * Simple Sound
 * 
 * 
 * 
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

namespace October_7th
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SoundEffect soundEffect;
        SoundEffectInstance soundInstance;
        MediaLibrary samplMediaLibrary;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            /*
            soundEffect = Content.Load<SoundEffect>(@"Audio\911");
            soundEffect.Play();
             * */

            // ------------------------------------------------------
            soundEffect = Content.Load<SoundEffect>(@"Audio\track");
            soundInstance = soundEffect.CreateInstance();
            soundInstance.IsLooped = true;

            //Pitch goes from -1 to 1, 0 being normal
            soundInstance.Pitch = 0.3f;
            soundInstance.Volume = 0.9f; // goes from 0 to 1
            soundInstance.Play();
             


        }

       
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.P))
            {
                soundInstance.Pause();
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                soundInstance.Resume();
            }
            if (keyboardState.IsKeyDown(Keys.F2))
            {
                MediaPlayer.Stop();
                Song myWmaSong = Content.Load<Song>(@"Audio\Help");
                MediaPlayer.Play(myWmaSong);
            }

            if (keyboardState.IsKeyDown(Keys.F3))
            {
                MediaPlayer.Stop();
                Uri uriStreaming = new Uri("http://www.archive.org/download/gd1977-05-08.shure57.stevenson.29303.flac16/gd1977-05-08d02t06_vbr.mp3");
                Song song = Song.FromUri("StreamingUri", uriStreaming);
                MediaPlayer.Play(song);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
