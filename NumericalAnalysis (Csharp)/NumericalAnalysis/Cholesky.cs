using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NumericalAnalysis
{
    class Cholesky
    {
        static Linalg la = new Linalg();
        static double eps = 0.0000000001;
        static double[,] Cholesky_Decomposition(double[,] matrix, int n)
        {
            double[,] lower = new double[n, n]; //Biến về ma trận tam giác dưới
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double sum = 0;
                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                            sum = sum + Math.Pow(lower[j, k], 2);
                        lower[j, j] = Math.Sqrt(matrix[j, j] - sum);
                        if (lower[i, j] < eps)
                        {
                            Console.Write("Ma trận suy biến"); break;
                        }
                    }
                    else
                    {
                        for (int k = 0; k < j; k++)
                            sum = sum + (lower[i, k] * lower[j, k]);
                        lower[i, j] = (matrix[i, j] - sum) / lower[j, j];
                    }
                }
            }
            Console.WriteLine("\nPhương trình vừa nhập là: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0}\t", matrix[i, j]);
                }
                Console.Write("\n");
            }
            return lower;
        }
        static void Chol(double[,] A, double[] B)
        {
            int n = B.Length;
            double[,] S = new double[n, n];
            double[] Y = new double[n];
            double[] X = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double sum = 0;

                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                            sum += Math.Pow(S[j, k], 2);
                        S[j, j] = Math.Sqrt(A[j, j] - sum);
                    }

                    else
                    {
                        for (int k = 0; k < j; k++)
                            sum += S[i, k] * S[j, k];
                        S[i, j] = (A[i, j] - sum) / S[j, j];
                    }
                }
            }

            Console.WriteLine("[CHOLESKY]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("{0}\t", Math.Round(S[i, j], 4));
                Console.Write("\n");
            }

            //Y
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                    sum += S[i, j] * Y[j];
                Y[i] = (B[i] - sum) / S[i, i];
            }
            Console.WriteLine("\n[Y]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(Y[i], 4));

            //X
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = n - 1; j > i; j--)
                    sum += S[j, i] * X[j];
                X[i] = (Y[i] - sum) / S[i, i];
            }
            Console.WriteLine("\n\n[X]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(X[i], 4));
        }
        public void main()
        {
            int flag = 1;
            //double[,] A =
            //{
            //    {1, 3, -2, 0, -2},
            //    {3, 4, -5, 1, -3},
            //    {-2, -5, 3, -2, 2},
            //    {0, 1, -2, 5, 3},
            //    {-2, -3, 2, 3, 4}
            //};
            //double[] B = { 0.5, 5.4, 5.0, 7.5, 3.3 };
            //double[,] A =
            //{
            //    {5, 3, 2, 1},
            //    {3, 6, 1, 2},
            //    {2, 1, 5, 1},
            //    {1, 2, 1, 6},
            //};
            //double[] B = { 1, 4, 7, 2 };
            double[,] A =
            {
                {25, 15, -5},
                {15, 18, 0},
                {-5, 0, 11},
            };
            double[] B = { 1, 1, 1 };
            int n = B.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (A[i, j] != A[j, i])
                    {
                        flag = 0; break;
                    }
                }
            }
            double[] X = new double[n];
            if (flag == 1)
                Chol(A, B);
            else Console.Write("Ma trận không đối xứng");
        }
    }
}