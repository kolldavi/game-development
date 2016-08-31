/*
 *  SpriteManager Class
 *  
 * Derived from DrawableGameComponent
 * 
 * 30th September 2011
 *   Continuing work done on
 *  28th September 2011
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


namespace AnimatedSprites
{
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        UserControlledSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        //----------------------------------------------
        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        //----------------------------------------------
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6));

            spriteList.Add( new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(4, 7)));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(100, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 2)));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(120, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(2, 1)));

            spriteList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(200, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(0, 0)));

            base.LoadContent();
        }

        //----------------------------------------------
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Game.Window.ClientBounds);

            foreach (Sprite spr in spriteList)
            {
                spr.Update(gameTime, Game.Window.ClientBounds);

                if (player.collisionRect().Intersects(spr.collisionRect()))
                {
                    Game.Exit();
                }
            }
    
            base.Update(gameTime);
        }

        //----------------------------------------------
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            player.Draw(gameTime, spriteBatch);
            foreach (Sprite spr in spriteList)
            {
                spr.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
