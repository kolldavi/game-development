//David Koller
//G6_Koller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G6_Koller1
{
    public class Piece
    {
        public const int SIZE = 4;
        public bool[,] BlockData;

        private Piece()
        {
            BlockData = new bool[SIZE, SIZE];
        }

        public Piece(int[] r0, int[] r1, int[] r2, int[] r3)
            : this()
        {
            for (int i = 0; i < r0.Length; i++)
                BlockData[i, 0] = r0[i] == 1;
            for (int i = 0; i < r1.Length; i++)
                BlockData[i, 1] = r1[i] == 1;
            for (int i = 0; i < r2.Length; i++)
                BlockData[i, 2] = r2[i] == 1;
            for (int i = 0; i < r3.Length; i++)
                BlockData[i, 3] = r3[i] == 1;
         
        }
        //Gets differnt rotations of shapes
        public static Piece Rotate(Piece Source)
        {            
            if (Source == I1)
                return I2;
            if (Source == I2)
                return I1;

      
            if (Source == S1)
                return S2;
            if (Source == S2)
                return S1;

            if (Source == Z1)
                return Z2;
            if (Source == Z2)
                return Z1;

  
            if (Source == L1)
                return L2;
            if (Source == L2)
                return L3;
            if (Source == L3)
                return L4;
            if (Source == L4)
                return L1;

            if (Source == J1)
                return J2;
            if (Source == J2)
                return J3;
            if (Source == J3)
                return J4;
            if (Source == J4)
                return J1;

            if (Source == T1)
                return T2;
            if (Source == T2)
                return T3;
            if (Source == T3)
                return T4;
            if (Source == T4)
                return T1;
            return O;
        }

        private static Random _r = new Random();
        public static Piece GetRandomPiece()
        {
            switch (_r.Next(7))
            {
                case 1:
                    return Piece.S1;
                case 2:
                    return Piece.Z1;
                case 3:
                    return Piece.L1;
                case 4:
                    return Piece.J1;
                case 5:
                    return Piece.I1;
                case 6:
                    return Piece.T1;

                default:
                    return Piece.O;
            }
        }

        //gets O Shape
        private static Piece o;
        public static Piece O
        {
            get
            {
                if (o == null)
                {
                    o = new Piece(
                        new int[] {  0, 0, 0, 0 },
                        new int[] {  0, 1, 1, 0 },
                        new int[] {  0, 1, 1, 0 },
                        new int[] {  0, 0, 0, 0 });
                }

                return o;
            }
        }


//gets differnt I Shapes
        private static Piece i1;
        public static Piece I1
        {
            get
            {
                if (i1 == null)
                {
                    i1 = new Piece(
                            new int[] { 0, 0, 0, 0 },
                        new int[] { 1, 1, 1, 1 },
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0 });
              
                }

                return i1;
            }
        }

        private static Piece i2;
        public static Piece I2
        {
            get
            {
                if (i2 == null)
                {
                    i2 = new Piece(
                            new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 0},
                        new int[] { 0, 0, 1, 0},
                        new int[] { 0, 0, 1, 0 });                        
                }

                return i2;
            }
        }

        //gets differnt Z Shapes
        private static Piece z1;
        public static Piece Z1
        {
            get
            {
                if (z1 == null)
                {
                    z1 = new Piece(
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 1, 1, 0 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 0, 0 });
         
                }

                return z1;
            }
        }

        private static Piece z2;
        public static Piece Z2
        {
            get
            {
                if (z2 == null)
                {
                    z2 = new Piece(
                        new int[] { 0, 0, 0, 1 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });
                
                }

                return z2;
            }
        }


        //gets differnt S Shapes
        private static Piece s1;
        public static Piece S1
        {
            get
            {
                if (s1 == null)
                {
                    s1 = new Piece(
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 1, 1, 0 },
                        new int[] { 0, 0, 0, 0 });
                   
                }

                return s1;
            }
        }

        private static Piece s2;
        public static Piece S2
        {
            get
            {
                if (s2 == null)
                {
                    s2 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 0, 1 },
                        new int[] { 0, 0, 0, 0 });
                  
                }

                return s2;
            }
        }

        //gets differnt L Shapes
        private static Piece l1;
        public static Piece L1
        {
            get
            {
                if (l1 == null)
                {
                    l1 = new Piece(
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 1, 0, 0 },
                        new int[] { 0, 0, 0, 0 });
                   
                }

                return l1;
            }
        }

        private static Piece l2;
        public static Piece L2
        {
            get
            {
                if (l2 == null)
                {
                    l2 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 0, 0 });
                     
                }

                return l2;
            }
        }

        private static Piece l3;
        public static Piece L3
        {
            get
            {
                if (l3 == null)
                {
                    l3 = new Piece(
                        new int[] { 0, 0, 0, 1 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0 });
                     
                }

                return l3;
            }
        }

        private static Piece l4;
        public static Piece L4
        {
            get
            {
                if (l4 == null)
                {
                    l4 = new Piece(
                        new int[] { 0, 1, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });
                        
                }

                return l4;
            }
        }

        //gets differnt J Shapes
        private static Piece j1;
        public static Piece J1
        {
            get
            {
                if (j1 == null)
                {
                    j1 = new Piece(
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 0, 0, 1 },
                        new int[] { 0, 0, 0, 0 });
                      
                }

                return j1;
            }
        }

        private static Piece j2;
        public static Piece J2
        {
            get
            {
                if (j2 == null)
                {
                    j2 = new Piece(
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });
                    
                }

                return j2;
            }
        }

        private static Piece j3;
        public static Piece J3
        {
            get
            {
                if (j3 == null)
                {
                    j3 = new Piece(
                        new int[] { 0, 1, 0, 0 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 0, 0, 0 },
                       new int[] { 0, 0, 0, 0 });
                }

                return j3;
            }
        }

        private static Piece j4;
        public static Piece J4
        {
            get
            {
                if (j4 == null)
                {
                    j4 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 1, 1, 0 },
                          new int[] { 0, 0, 0, 0 });
                }

                return j4;
            }
        }
        //gets differnt T Shapes
        private static Piece t1;
        public static Piece T1
        {
            get
            {
                if (t1 == null)
                {
                    t1 = new Piece(
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });

                }

                return t1;
            }
        }
        private static Piece t2;
        public static Piece T2
        {
            get
            {
                if (t2 == null)
                {
                    t2 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 1, 1 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });

                }

                return t2;
            }
        }
        private static Piece t3;
        public static Piece T3
        {
            get
            {
                if (t3 == null)
                {
                    t3 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 1, 1, 1 },
                        new int[] { 0, 0, 0, 0 },
                        new int[] { 0, 0, 0, 0 });

                }

                return t3;
            }
        }
        private static Piece t4;
        public static Piece T4
        {
            get
            {
                if (t4 == null)
                {
                    t4 = new Piece(
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 1, 1, 0 },
                        new int[] { 0, 0, 1, 0 },
                        new int[] { 0, 0, 0, 0 });

                }

                return t4;
            }
        }
    }
}
