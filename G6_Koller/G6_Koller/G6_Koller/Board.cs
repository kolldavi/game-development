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
    class Board
    {
        Texture2D BoardTexture;


        public Vector2 BoardPosition;
        public int numPiecesWidth = 10;
        public int numPiecesheight = 20;
        public int width = 30;
        public int height = 30;
      //  private int[,] blockbucketArray = new int[numPiecesWidth, numPiecesheight];

        public Board(ContentManager content, Vector2 bPosition, int width, int height)
        {

            int Width = width;
            int Height = height;            
            BoardPosition = bPosition;
            BoardTexture = content.Load<Texture2D>("Images/Box");

            //blockbucketArray=new int [width,height];
        }

        public void Draw(SpriteBatch spritebatch)
        {
            //spritebatch.Draw(BoardTexture, BoardPosition, Color.White);
            for (int x = 0; x < numPiecesWidth; x++)
                for (int y = 0; y < numPiecesheight; y++)
                {
               
                        spritebatch.Draw(BoardTexture, new Rectangle((int)BoardPosition.X + x * width, (int)BoardPosition.Y + y
                          * height, width - 1, height - 1), new Color(60, 60, 60, 128));
                }
        }
    }

    }

