using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace NumericalAnalysis
{
    class Cholesky2
    {
        public void main()
        {
            Complex[,] test2 = new Complex[,]
            {
                {25, 15, -5},
                {15, 18, 0},
                {-5, 0, 11},
            };

            Complex[,] test1 = new Complex[,]
            {
                {1, 3, -2, 0, -2},
                {3, 4, -5, 1, -3},
                {-2, -5, 3, -2, 2},
                {0, 1, -2, 5, 3},
                {-2, -3, 2, 3, 4}
            };

            Complex[,] chol1 = Cholesky(test1);
            Complex[,] chol2 = Cholesky(test2);

            Console.WriteLine("Test 1: ");
            Print(test1);
            Console.WriteLine("");
            Console.WriteLine("Lower Cholesky 1: ");
            Print(chol1);
            Console.WriteLine("");
            //Console.WriteLine("Test 2: ");
            //Print(test2);
            //Console.WriteLine("");
            //Console.WriteLine("Lower Cholesky 2: ");
            //Print(chol2);

        }
        public static void Print(Complex[,] a)
        {
            int n = (int)Math.Sqrt(a.Length);
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < n; r++)
            {
                string s = "";
                for (int c = 0; c < n; c++)
                {
                    s = s + a[r, c].ToString("f2").PadLeft(9) + ",";
                }
                sb.AppendLine(s);
            }

            Console.WriteLine(sb.ToString());
        }
        public static Complex[,] Cholesky(Complex[,] a)
        {
            int n = (int)Math.Sqrt(a.Length);
            Console.WriteLine(a.Length);
            Complex[,] ret = new Complex[n, n];
            for (int r = 0; r < n; r++)
                for (int c = 0; c <= r; c++)
                {
                    if (c == r)
                    {
                        Complex sum = 0;
                        for (int j = 0; j < c; j++)
                        {
                            sum += ret[c, j] * ret[c, j];
                        }
                        ret[c, c] = Complex.Sqrt(a[c, c] - sum);
                    }
                    else
                    {
                        Complex sum = 0;
                        for (int j = 0; j < c; j++)
                            sum += ret[r, j] * ret[c, j];
                        ret[r, c] = 1.0 / ret[c, c] * (a[r, c] - sum);
                    }
                }

            return ret;
        }
    }
}
