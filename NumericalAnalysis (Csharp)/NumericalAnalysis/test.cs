using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class test
    {
        static Linalg la = new Linalg();
        static double MAX(double[] A)
        {
            int n = A.Length;
            double max = A[0];
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(max) < Math.Abs(A[i]))
                    max = A[i];
            }
            return max;
        }
        public void luythua()
        {

            double[,] A =
            {
                { 211, 22, -13, 24, 15, -26, 17, 28 },
                { 22, 433, 24, 35, 26, 37, 28, -39 },
                { 33, -24, 235, -26, 37, 28, -39, 20 },
                { 14, 45, 26, 247, 38, 49, 40, -41 },
                { -55, 16, 57, 28, 259, 30, -51, 42 },
                { 46, 27, -48, 39, 40, 261, 42, 73 },
                { 27, -58, 29, 70, -21, 42, 223, 34 },
                { 38, 59, 60, -71, 82, -93, 24, 215 }
            };
            double[,] Y =
            {
                {1 },
                {1 },
                {1 },
                {1 },
                {1 },
                {1 },
                {1 },
                {1 },
            };
            double[,] B = new double[A.GetLength(0), 1];
            B = la.MultiMat(A, Y);
            for (int i = 0; i < 6; i++)
            {
                A = la.MultiMat(A, B);
                Console.WriteLine();
                la.PrtMat(A);
            }
        }
        public void xuongthang()
        {
            /*double[,] A =
            {
                {4, 2, 2},
                {2, 5, 1},
                {2, 1, 6},
            };*/

            //double[,] A =
            //{
            //    {7, 4, 1},
            //    {4, 4, 4},
            //    {1, 4, 7},
            //};

            double[,] A =
            {
                {1, 2, 3, 4 },
                {2, 1, 2, 3 },
                {3, 2, 1, 2 },
                {4, 3, 2, 1 },
            };

            /*double[,] A = {
                  {4.9, 1.0, 0.1, 1.1},
                  { 1.0, 6.4, 1.2, 0.2},
                  { 0.1, 1.2, 3.6, 1.1},
                  { 1.1, 0.2, 1.1, 6.4},
                  };*/

            double[] X = { 1, 1, 1, 1 };

            int n = X.Length;
            double eps = 0.000001, λ0 = 0, λ = 0;
            double[] Y = new double[n];
            double[] C = new double[n];
            double[] Lambda = new double[n]; // Mảng chứa giá trị riêng
            double[,] vv = new double[n, n]; // Mảng chứa vector riêng
            Console.WriteLine("Matrix A:");
            for (int l = 0; l < n; l++)
            {
                la.PrtMat(A);
                for (int i = 0; i < n; i++)
                    X[i] = i + 1;

                for (int k = 1; k <= 1000; k++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Y[i] = 0;

                        for (int j = 0; j < n; j++)
                            Y[i] += A[i, j] * X[j];
                    }

                    for (int i = 0; i < n; i++)
                    {
                        C[i] = Y[i] / X[i];
                        X[i] = Y[i] / Y[n - 1];  //Tinh vector riêng
                    }

                    λ = MAX(C); // Tính giá trị riêng

                    if (Math.Abs(λ0 - λ) < eps) break;

                    λ0 = λ;
                }

                Lambda[l] = λ; // Lưu giá trị riêng
                Console.WriteLine("lambda{0} = {1}\t", l + 1, λ);
                Console.WriteLine("Eigenvector for lambda{0}: ", l + 1);
                for (int i = 0; i < n; i++)
                    Console.Write("{0}\t", X[i]);
                Console.WriteLine("\n");

                /*--------------Xuống thang tìm ma trận A---------------*/

                double ps = 0;
                for (int i = 0; i < n; i++)
                {
                    ps += X[i] * X[i];
                    vv[l, i] = X[i]; //Lưu vector riêng
                }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        A[i, j] -= (λ / ps) * X[i] * X[j];
            }
        }
        public void JacobiRow()
        {
            double eps = 0.0001;
            //double[,] A = 
            //{ 
            //    {4, 0.24, -0.08},
            //    {0.09, 3, -0.15},
            //    {0.04, -0.08, 4}
            //};
            //double[,] b =
            //{
            //    {8 },
            //    {9 },
            //    {20 }
            //};
            double[,] A =
            {
                {50, 107, 36 },
                {25, 54, 20 },
                {31, 66, 21 },
            };
            double[,] b =
            {
                {1, 0, 0 },
                {0, 1, 0 },
                {0, 0, 1 }
            };
            int n = b.GetLength(0);
            int m = b.GetLength(1);
            double[,] I = new double[n, n];
            double[,] T = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) I[i, j] = 1;
                    else I[i, j] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) T[i, j] = 1 / A[i, j];
                    else T[i, j] = 0;
                }
            }
            double[,] B = la.SubMat(I, la.MultiMat(T, A));
            double[,] d = la.MultiMat(T, b);
            double[,] x =
            {
                {0, 0, 0 },
                {0, 0, 0 },
                {0, 0, 0 }
            };

            //danh gia sai so
            //List<double> AbsT = new List<double>();
            //for (int i = 0; i < n; i++)
            //    AbsT.Add(Math.Abs(A[i, i]));
            //double minaii = AbsT.Min();
            //double MAXaii = AbsT.Max();
            //double lambda = MAXaii / minaii;
            double[,] y = new double[n, m]; //bien trung gian

            int c = 0;
            do
            {
                Console.Write("\nNo.{0}", c + 1);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        y[i, j] = x[i, j];
                x = la.SumMat(la.MultiMat(B, x), d);
                Console.WriteLine("x = ");
                la.PrtMat(x);
                Console.WriteLine();
                c++;
            } while (la.norminf(B) / (1 - la.norminf(B)) * la.norminf(la.SubMat(x, y)) > eps);
        }
    }
}
