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
    class Block
    {
          Texture2D boxTexture;
            public Vector2 Position;
           public Block(ContentManager content, Vector2 position)
            {
                Position = position;
                boxTexture = content.Load<Texture2D>("Images/Box");
            }

            public void Update(GameTime gameTime)
            {
            }

            public void Draw (SpriteBatch spritebatch)
            {
                spritebatch.Draw(boxTexture, Position, Color.White);
            }
        }
    }

