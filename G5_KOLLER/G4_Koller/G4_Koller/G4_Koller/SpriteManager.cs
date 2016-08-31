// Sprite Manager Class

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
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        SoundEffect sound;
        SoundEffectInstance beginInstance, scoreInstance, playerDieInstance,superScoreInstance
            ,KillerStartInstance, backgroundInstance, astroidBounceOffWallInstance, loseSoundInstance,
            exitInstance;
        UserControlledSprite playerSprite;
        AutomatedSprite asteroidSprite;
        AutomatedSprite bonusSprite;
        AutomatedSprite smallPlayer;
        int screenHieght = 700;
        SpriteFont font, font2;
        int score;
        int numberOfPointsVis = 7;
        int playerSpeed;
        Int64 secondsPerMove;//use to keep track of super sprite
        int numLives = 2;
     Boolean start = false;
        Random rnd = new Random();
        List<AutomatedSprite> asteroidSprites = new List<AutomatedSprite>();
        List<AutomatedSprite> smallPlayerSprite = new List<AutomatedSprite>();
        List<AutomatedSprite> pointSprites = new List<AutomatedSprite>();
        KeyboardState oldState = Keyboard.GetState();
        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            playerSpeed = 8;
            secondsPerMove = 5;
            font = Game.Content.Load<SpriteFont>(@"images/SpriteFont1");//load font 1
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            font2 = Game.Content.Load<SpriteFont>(@"images/SpriteFont2");//load font 2
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            playerSprite = new UserControlledSprite(//load player
                Game.Content.Load<Texture2D>(@"Images/Spaceship"),
              new Vector2(400, 500), new Point(100, 138),0, new Point(0, 0),
                new Point(1, 1), new Vector2(playerSpeed, playerSpeed), 0);

            sound = Game.Content.Load<SoundEffect>(@"Sound\space_oddity_converted");          
            beginInstance = sound.CreateInstance();
            beginInstance.IsLooped = false;

            sound = Game.Content.Load<SoundEffect>(@"Sound\womp_converted");
            astroidBounceOffWallInstance = sound.CreateInstance();
            astroidBounceOffWallInstance.IsLooped = false;
            

            sound = Game.Content.Load<SoundEffect>(@"Sound\Cool_Background_Music");
            backgroundInstance = sound.CreateInstance();
            backgroundInstance.IsLooped = false;

            sound = Game.Content.Load<SoundEffect>(@"Sound\phasers3_converted");
            KillerStartInstance = sound.CreateInstance();
            KillerStartInstance.IsLooped = false;
            
            sound = Game.Content.Load<SoundEffect>(@"Sound\coin_flip_converted");
            scoreInstance = sound.CreateInstance();
            scoreInstance.IsLooped = false;

            sound = Game.Content.Load<SoundEffect>(@"Sound\explosion_x_converted");
           playerDieInstance = sound.CreateInstance();
           playerDieInstance.IsLooped = false;

           sound = Game.Content.Load<SoundEffect>(@"Sound\bubbles_sfx_converted");
             superScoreInstance = sound.CreateInstance();
           superScoreInstance.IsLooped = false;

           sound = Game.Content.Load<SoundEffect>(@"Sound\luckiest_converted");
                  loseSoundInstance = sound.CreateInstance();
             loseSoundInstance.IsLooped = false;

             sound = Game.Content.Load<SoundEffect>(@"Sound\exit_cue_y_converted");
            exitInstance = sound.CreateInstance();
                  exitInstance.IsLooped = false;
           
            

            asteroidSprites.Add(asteroidSprite = new AutomatedSprite(
             Game.Content.Load<Texture2D>(@"Images/asteroid"),
            new Vector2(0, -40), new Point(50, 50), 40, new Point(0, 0),
             new Point(5, 6), new Vector2(2, 4), 100));//load asteroidSprite

            asteroidSprites.Add(asteroidSprite = new AutomatedSprite(
             Game.Content.Load<Texture2D>(@"Images/asteroid"),
            new Vector2(0, -40), new Point(50, 50), 40, new Point(0, 0),
             new Point(5, 6), new Vector2(3, 5), 100));

            asteroidSprites.Add(asteroidSprite = new AutomatedSprite(
             Game.Content.Load<Texture2D>(@"Images/asteroid"),
            new Vector2(0, -40), new Point(50, 50), 40, new Point(0, 0),
             new Point(5, 6), new Vector2(4, 7), 100));

            asteroidSprites.Add(asteroidSprite = new AutomatedSprite(
             Game.Content.Load<Texture2D>(@"Images/asteroid"),
            new Vector2(0, -40), new Point(50, 50), 40, new Point(0, 0),
             new Point(5, 6), new Vector2(3, 4), 100));

            asteroidSprites.Add(asteroidSprite = new AutomatedSprite(
             Game.Content.Load<Texture2D>(@"Images/asteroid"),
            new Vector2(0, -40), new Point(50, 50), 40, new Point(0, 0),
             new Point(5, 6), new Vector2(3, 2), 100));

            smallPlayerSprite.Add(smallPlayer = new AutomatedSprite(
      Game.Content.Load<Texture2D>(@"Images/smallSprite"),
     new Vector2(0, screenHieght - 50), new Point(30, 38), 40, new Point(0, 0),
      new Point(1, 1), new Vector2(0, 0), 100));//load smallPlayer

            smallPlayerSprite.Add(smallPlayer = new AutomatedSprite(
      Game.Content.Load<Texture2D>(@"Images/smallSprite"),
     new Vector2(40, screenHieght - 50), new Point(30, 38), 40, new Point(0, 0),
      new Point(1, 1), new Vector2(0, 0), 100));
            smallPlayerSprite.Add(smallPlayer = new AutomatedSprite(
  Game.Content.Load<Texture2D>(@"Images/smallSprite"),
 new Vector2(80, screenHieght - 50), new Point(30, 38), 40, new Point(0, 0),
  new Point(1, 1), new Vector2(0, 0), 100));




            bonusSprite = new AutomatedSprite(
    Game.Content.Load<Texture2D>(@"Images/bonus"),
    new Vector2(800, 650), new Point(44, 44), 5, new Point(0, 0),
    new Point(16, 4), new Vector2(0, 8), 50);//Load points

            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(0, 0), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(3, 3), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(0, 290), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(3, 3), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(900, 650), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(-4, -4), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(0, 600), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(5, 5), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(500, 0), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(4, 4), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(950, 200), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(-4, -4), 100));
            pointSprites.Add(new AutomatedSprite(
Game.Content.Load<Texture2D>(@"Images/point"),
new Vector2(0, 400), new Point(30, 30), 5, new Point(0, 0),
new Point(5, 5), new Vector2(4, 4), 100));



            base.LoadContent();
        }

        //*******************************************************************
        //update method for sprite manager
        //*******************************************************************
        public override void Update(GameTime gameTime)
        {
            if (start == false)
            {
                beginInstance.Play();                 
            }
            KeyboardState newState = Keyboard.GetState();
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                start = true;
                beginInstance.Stop();
                backgroundInstance.Play();            
               
            }
            if (newState.IsKeyDown(Keys.F6) && oldState.IsKeyUp(Keys.F6))//increase player speed
            {               
                playerSpeed += 1;
                playerSprite.Update(gameTime, Game.Window.ClientBounds);

            }
            if (newState.IsKeyDown(Keys.F7) && oldState.IsKeyUp(Keys.F7))//decrease player speed
            {
                playerSpeed -= 1;           
            }
            
            oldState = newState;
            if(start==true)
            {
                if (numLives >= 0)
                {
                    playerSprite.Update(gameTime, Game.Window.ClientBounds);
                    bonusSprite.SuperPointUpdate(gameTime, Game.Window.ClientBounds);
                    if (secondsPerMove == gameTime.TotalGameTime.Seconds)
                    {
                        bonusSprite.isVisible = true;
                        secondsPerMove = gameTime.TotalGameTime.Seconds + rnd.Next(2,4);
                    }
                    if (playerSprite.collisionRect().Intersects(bonusSprite.collisionRect()) && bonusSprite.isVisible == true)
                    {
                        superScoreInstance.Play();
                        score += 10;
                        bonusSprite.isVisible = false;
                    }
                    foreach (AutomatedSprite point in smallPlayerSprite)
                    {
                        smallPlayer.Update(gameTime, Game.Window.ClientBounds);
                    }

                    foreach (AutomatedSprite killerSprite in asteroidSprites)
                    {
                        if (killerSprite.playAsteroidSound == true)
                        {
                            KillerStartInstance.Play();
                        }
                        if (killerSprite.bounceOffWallSound == true)
                        {
                            astroidBounceOffWallInstance.Play();
                        }
                        killerSprite.killerSpriteUpdate(gameTime, Game.Window.ClientBounds);
                        if (playerSprite.collisionRect().Intersects(killerSprite.collisionRect()))
                        {
                            playerDieInstance.Play();
                            if (numLives >= 0)
                            {
                                asteroidSprites.ElementAt(0).isVisible = true;
                                asteroidSprites.ElementAt(1).isVisible = true;
                                asteroidSprites.ElementAt(2).isVisible = true;
                                asteroidSprites.ElementAt(3).isVisible = true;
                                asteroidSprites.ElementAt(4).isVisible = true;
                                killerSprite.isVisible = false;
                                smallPlayerSprite.ElementAt(numLives).isVisible = false;
                                numLives--;
                            }
                            if (numLives == 0)
                            {
                                secondsPerMove = gameTime.TotalGameTime.Seconds + 8;
                            }

                        }
                    }
                    foreach (AutomatedSprite point in pointSprites)
                    {

                        if (playerSprite.collisionRect().Intersects(point.collisionRect()) && point.isVisible == true)
                        {
                            scoreInstance.Play();
                            numberOfPointsVis--;
                            score += 1;
                            point.isVisible = false;
                        }
                        point.pointSpriteUpdate(gameTime, Game.Window.ClientBounds);
                        if (numberOfPointsVis == 2)
                        {
                            pointSprites.ElementAt(0).isVisible = true;
                            pointSprites.ElementAt(1).isVisible = true;
                            pointSprites.ElementAt(2).isVisible = true;
                            pointSprites.ElementAt(3).isVisible = true;
                            pointSprites.ElementAt(4).isVisible = true;
                            pointSprites.ElementAt(5).isVisible = true;
                            pointSprites.ElementAt(6).isVisible = true;
                            numberOfPointsVis = 7;

                        }
                    }
                }
                else
                {
                    backgroundInstance.Stop();
                    if (!loseSoundInstance.IsDisposed) 
                    {
                        loseSoundInstance.Play();
                    }
                    if (secondsPerMove  <= gameTime.TotalGameTime.Seconds)
                    {
                        loseSoundInstance.Dispose();
                        exitInstance.Play();
                        if (Keyboard.GetState().GetPressedKeys().Length > 0)
                        {
                            this.Game.Exit();
                        }
                    }

                }
               
            }
            base.Update(gameTime);
        }

        //*******************************************************************
        //draw method for sprite manager
        //*******************************************************************
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            playerSprite.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite killerSprite in asteroidSprites)
            {
                killerSprite.Draw(gameTime, spriteBatch);
            }
            foreach (AutomatedSprite point in pointSprites)
            {
                if (point.isVisible == true)
                {
                    point.Draw(gameTime, spriteBatch);
                }
            }

            if (bonusSprite.isVisible == true)
            {
                bonusSprite.Draw(gameTime, spriteBatch);
            }
            foreach (AutomatedSprite splay in smallPlayerSprite)
            {
                if (splay.isVisible == true)
                {
                    splay.Draw(gameTime, spriteBatch);
                }
            }
            //*******************************************************************
            //draw end screen
            //*******************************************************************
            spriteBatch.DrawString(font, "Score: " + score , new Vector2(0, screenHieght - 100), Color.White);
                spriteBatch.End();

                if (numLives < 0 && loseSoundInstance.IsDisposed)
                {
                    spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.DrawString(font2, "Final Score: " + score, new Vector2(100, 300), Color.White);
                    spriteBatch.End();
                }

                base.Draw(gameTime);
            }
        }
    
}

