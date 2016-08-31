//David Koller
//G6_Koller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace G6_Koller1
{
    public class Board
    {
        //Diffent movments of pieces
        public enum PieceMove { None, Left, Right, Up, Down, Rotate,FarDown,Pause,Resume }
        //Size of each block
        public  int BlockSize = 30;
        //boardSizeX
        public  int SizeX = 10;
        //BoardSizeY
        public  int SizeY = 20;
        
         int Margin = 4;
         int PieceStartX = 8;

        bool[,] squares;
        int ScoreX = 300;
        int ScoreY = 200;
        Random r = new Random();
        Color c;
        int Pause=1;
        Piece CurrentPiece;
        Piece NextPiece;
        int CurrentPieceX;
        int CurrentPieceY;
        int NextPieceX;
        int NextPieceY;
        public int Score;
 
        public int LinesCompleted;

        public Board(GraphicsDevice device)
        {
            squares = new bool[Margin + SizeX + Margin, Margin + SizeY + Margin];
            //set boarder bottom
            for (int i = 0; i < Margin + SizeX + Margin; i++)
            {
                for (int j = 0; j < Margin; j++)
                {
                    squares[i, j] = true;
                    squares[i, j + SizeY + Margin] = true;
                }
            }
            //set boarderleft and right
            for (int i = 0; i < Margin; i++)
            {
                for (int j = 0; j < Margin + SizeY + Margin; j++)
                {
                    squares[i, j] = true;
                    squares[i + SizeX + Margin, j] = true;
                }
            }
            //gets random piece for first piece
            NextPiece = Piece.GetRandomPiece();
            //Sets first piece to be next piece
            CurrentPiece = NextPiece;
            CurrentPieceX = PieceStartX;
            CurrentPieceY = Margin;
            //Lets draws next piece at start
            NextPieceUpdate();
           
        }
        //*************************
        //Draw board and pieces
        //*************************
        public void DrawOntoSpriteBatch(SpriteBatch batch,SpriteFont font, Texture2D box)
        {
            //Draw board
            for (int i = 0; i < SizeX ; i++)
                for (int j = 0; j < SizeY; j++)
                {
                    batch.Draw(box, new Rectangle(i * BlockSize, j * BlockSize, BlockSize, BlockSize), Color.White);
                    }
            //Draw set pieces
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    if (squares[Margin + i, Margin + j])
                    {
                        batch.Draw(box, new Rectangle(i * BlockSize, j * BlockSize, BlockSize, BlockSize), Color.Blue);
                    }
            //Draw moving pieces
            if (CurrentPiece != null)
            {
                for (int i = 0; i < Piece.SIZE; i++)
                    for (int j = 0; j < Piece.SIZE; j++)
                        if (CurrentPiece.BlockData[i, j])
                        {
                            batch.Draw(box, new Rectangle((CurrentPieceX - Margin + i) * BlockSize, (CurrentPieceY - Margin + j) * BlockSize, BlockSize, BlockSize), c);
                        }
            }
            //draw next Pieces
            if (CurrentPiece != null)
            {
                for (int i = 0; i < Piece.SIZE; i++)
                    for (int j = 0; j < Piece.SIZE; j++)
                        if (NextPiece.BlockData[i, j])
                        {
                            batch.Draw(box, new Rectangle((NextPieceX - Margin + i) * BlockSize, (NextPieceY - Margin + j) * BlockSize, BlockSize, BlockSize), Color.Green);
                        }
            }
      //draw score
            batch.DrawString(font, string.Format("Score: "+ this.Score), new Vector2(ScoreX, ScoreY), Color.Black);
        }
        //gets random for nextPiect
        public void NextPieceUpdate()
        {
            NextPieceX = 15;
            NextPieceY = 5;
            NextPiece = Piece.GetRandomPiece();
            c = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 200), (byte)r.Next(0, 200));
        }
        //updates board
        public bool Update()
        {
            //check is lines need to be removed
            RemoveFullLines();
          //moves piece down
            if (CurrentPiece != null)
            {
                int newX = CurrentPieceX;
                int newY = CurrentPieceY + Pause;

                if (IsLegalPosition(CurrentPiece, newX, newY))
                {
                    CurrentPieceY = newY;
                }
                else
                {
                    //if currentPiece cannot move gets new one and sets position
                    SetPiecePos();                   
                    CurrentPiece = NextPiece;                    
                    CurrentPieceX = PieceStartX;
                    CurrentPieceY = Margin;
                    NextPieceUpdate();                    
                    Score += Pause;
                    if (!IsLegalPosition(CurrentPiece, CurrentPieceX, CurrentPieceY))
                        return false;
                }
            }
      
            return true;
        }

       
        private void RemoveFullLines()
        {
            int FullLineNumber = -1;
            do
            {
                FullLineNumber = GetPossibleFullLine();
                if (FullLineNumber >= 0)
                    RemoveLine(FullLineNumber);
            } while (FullLineNumber >= 0);
        }

        private int GetPossibleFullLine()
        {
            for (int j = 0; j < SizeY; j++)
            {
                bool isFull = true;
                for (int i = 0; i < SizeX; i++)
                    isFull &= this.squares[Margin + i, Margin + j];

                if (isFull)
                    return j + Margin;
            }

            return -1;
        }

        //removed filled rows
        private void RemoveLine(int LineNumber)
        {
            for (int j = LineNumber; j > Margin; j--)
                for (int i = Margin; i < SizeX + Margin; i++)
                    this.squares[i, j] = this.squares[i, j - 1];
            LinesCompleted += 1;
            Score += 5;
 
        }
        //set Piece
        private void SetPiecePos()
        {
            if (CurrentPiece != null)
                for (int i = 0; i < Piece.SIZE; i++)
                    for (int j = 0; j < Piece.SIZE; j++)
                        if (CurrentPiece.BlockData[i, j])
                            this.squares[i + CurrentPieceX, j + CurrentPieceY] = true;
        }
        //handles differnt movements for shape
        public void MoveCurrentPiece(PieceMove Move)
        {
            Piece newPiece;
            int newX;
            int newY;

            newPiece = CurrentPiece;
            newX = CurrentPieceX;
            newY = CurrentPieceY;
            if (CurrentPiece != null)
            {
                switch (Move)
                {
                 
                    case PieceMove.Rotate:
                        if (Pause == 1)
                        newPiece = Piece.Rotate(CurrentPiece); break;                    
                    case PieceMove.Down:
                        if(Pause==1)
                        newY = CurrentPieceY + 5; break;
                    case PieceMove.FarDown:
                        newY = CurrentPieceY + Pause;
                        break;
                    case PieceMove.Left:
                        newX = CurrentPieceX - Pause; break;
                    case PieceMove.Right:
                        newX = CurrentPieceX + Pause; break;
                    case PieceMove.Pause:
                        Pause = 0; break;
                    case PieceMove.Resume:
                        Pause = 1; break;
                    default:
                        break;
                }

                // move only if legal position
                if (this.IsLegalPosition(newPiece, newX, newY))
                {
                    CurrentPiece = newPiece;
                    CurrentPieceX = newX;
                    CurrentPieceY = newY;
                }
            }
        }

     //check if movement is legal
        public bool IsLegalPosition(Piece piece, int pieceX, int pieceY)
        {
            for (int i = 0; i < Piece.SIZE; i++)
                for (int j = 0; j < Piece.SIZE; j++)
                    if (piece.BlockData[i, j] &&
                         this.squares[i + pieceX, j + pieceY]
                        )
                        return false;

            return true;
        }

    }
}
