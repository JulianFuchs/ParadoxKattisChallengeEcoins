using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCoins
{
    class Program
    {
        static int nrOfCoins;
        static int S;

        static Ecoin[] coins;
        static int[][] reached;

        static void Main(string[] args)
        {
            int cases = Convert.ToInt32(Console.ReadLine());

            for (int iter = 0; iter < cases; iter++)
            {
                string[] rowCols = Console.ReadLine().Split(new char[0]);

                nrOfCoins = Convert.ToInt32(rowCols[0]);
                S = Convert.ToInt32(rowCols[1]);

                reached = new int[S + 1][];
                coins = new Ecoin[nrOfCoins];

                for (int i = 0; i <= S; i++)
                {
                    reached[i] = new int [S + 1];
                }

                for (int i = 0; i <= S; i++)
                {
                    for (int k = 0; k <= S; k++)
                    {
                        reached[i][k] = Int32.MaxValue-1;
                    }
                }

                reached[0][0] = 0;

                // read the input
                for (int i = 0; i < nrOfCoins; i++)
                {
                    string[] coinStr = Console.ReadLine().Split(new char[0]);

                    int conventional = Convert.ToInt32(coinStr[0]);
                    int infoTech = Convert.ToInt32(coinStr[1]);

                    Ecoin newCoin = new Ecoin(conventional, infoTech);

                    coins[i] = newCoin;
                }

                findReachable();

                int min = Int32.MaxValue;

                for (int i = 0; i <= S; i++)
                {
                    for (int j = 0; j <= S; j++)
                    {
                        if (reached[i][j] != Int32.MaxValue)
                        {
                            int res = calculateEModulus(i, j);

                            if (res == S && reached[i][j] < min)
                            {
                                min = reached[i][j];
                            }
                        }
                    }
                }

                if (min != Int32.MaxValue -1)
                {
                    Console.WriteLine(min);
                }
                else
                {
                    Console.WriteLine("not possible");
                }

                Console.ReadLine();
            }

            //debug
            //Console.ReadLine();

        }


        private static void findReachable()
        {
            for (int i = 0; i < nrOfCoins; i++)
            {
                for (int j = coins[i].conventional; j <= S; j++)
                {
                    for (int k = coins[i].infoTech; k <= S; k++)
                    {
                        reached[j][k] = Math.Min(reached[j][k], reached[j - coins[i].conventional][k - coins[i].infoTech] + 1);
                    }
                }
            }
        }

        private static int calculateEModulus(int conservative, int infoTech)
        {
            double squareSum = conservative * conservative + infoTech * infoTech;

            double root = Math.Sqrt(squareSum);

            if ((root % 1) == 0)
            {
                return (int)root;
            }
            else
            {
                return 0;
            }
        
        }


    }

    public class Ecoin
    {
        public int conventional;
        public int infoTech;

        public Ecoin(int conventional, int infoTech)
        {
            this.conventional = conventional;
            this.infoTech = infoTech;
        }

    }
}
