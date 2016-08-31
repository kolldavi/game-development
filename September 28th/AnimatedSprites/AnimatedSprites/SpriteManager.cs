/*
 *  SpriteManager Class
 *  
 * Derived from DrawableGameComponent
 * 
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
        AutomatedSprite aSprite;
        AutomatedSprite bSprite;

        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
    
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6));

            aSprite = new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(4, 7));

            bSprite = new AutomatedSprite(
    Game.Content.Load<Texture2D>(@"Images/skullball"),
    new Vector2(100, 150), new Point(75, 75), 10, new Point(0, 0),
    new Point(6, 8), new Vector2(6, 2));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, Game.Window.ClientBounds);
            aSprite.Update(gameTime, Game.Window.ClientBounds);
            bSprite.Update(gameTime, Game.Window.ClientBounds);

            if (player.collisionRect().Intersects(aSprite.collisionRect()) ||
                player.collisionRect().Intersects(bSprite.collisionRect()))
            {
                Game.Exit();
            }
    
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            player.Draw(gameTime, spriteBatch);
            aSprite.Draw(gameTime, spriteBatch);
            bSprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
