using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NumericalAnalysis
{
    class InverseMatrix
    {
        static Linalg la = new Linalg();
        static double[,] Cholesky_Decomposition(double[,] matrix, int n)
        {
            double eps = 0.00000000000000001;
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
        static double[,] MatrixCreate(int rows, int cols)
        {
            double[,] result = new double[rows, cols];
            return result;
        }
        //static double[,] Trans(double[,] a, int size)
        //{
        //    double[,] result = MatrixCreate(size, size);
        //    for (int i = 0; i < size; ++i)
        //        for (int j = 0; j < size; ++j)
        //            result[i, j] = a[j, i];

        //    return result;
        //}
        static double[,] Slide(double[,] a, int n)
        {
            double[,] result = MatrixCreate(n - 1, n - 1);
            for (int i = 0; i < n - 1; ++i)
                for (int j = 0; j < n - 1; ++j)
                    result[i,j] = a[i,j];
            return result;
        }
        static double[,] MatrixInverse(double[,] a, int n)// n = size
        {
            double[,] result = MatrixCreate(n, n);
            if (n == 1)
            {
                result[0,0] = 1 / a[0,0];
                return result;
            }
            else if (n == 2)
            {
                double m = a[0,0] * a[1,1] - a[0,1] * a[1,0];
                result[0,0] = a[1,1] / m;
                result[0,1] = -a[0,1] / m;
                result[1,0] = -a[1,0] / m;
                result[1,1] = a[0,0] / m;
                return result;
            }
            else
            {
                var b11 = MatrixCreate(n - 1, n - 1);
                var b12 = MatrixCreate(n - 1, 1);
                var b21 = MatrixCreate(1, n - 1);
                var b22 = MatrixCreate(1, 1);

                var a11 = MatrixCreate(n - 1, n - 1);
                var a12 = MatrixCreate(n - 1, 1);
                var a21 = MatrixCreate(1, n - 1);
                var a22 = MatrixCreate(1, 1);

                a11 = Slide(a, n);
                for (int i = 0; i < n - 1; ++i)
                {
                    a12[i,0] = a[i,n - 1];
                    a21[0,i] = a[n - 1,i];
                }
                a22[0,0] = a[n - 1,n - 1];
                double[,] X = MatrixCreate(n - 1, 1);
                double[,] Y = MatrixCreate(1, n - 1);
                var theta = MatrixCreate(1, 1);
                var ia11 = MatrixCreate(n - 1, n - 1);
                ia11 = MatrixInverse(a11, n - 1);
                X = la.MultiMat(ia11, a12);
                Y = la.MultiMat(a21, ia11);
                theta = la.SubMat(a22, la.MultiMat(Y, a12));
                var mX = MatrixCreate(n - 1, 1);
                var mY = MatrixCreate(1, n - 1);
                for (int i = 0; i < n - 1; ++i)
                {
                    mX[i,0] = -X[i,0];
                    mY[0,i] = -Y[0,i];
                }
                var itheta = MatrixCreate(1, 1);
                itheta = MatrixInverse(theta, 1);
                b11 = la.SumMat(ia11, la.MultiMat(la.MultiMat(X, itheta), Y));
                b12 = la.MultiMat(mX, itheta);
                b21 = la.MultiMat(itheta, mY);
                b22 = itheta;

                for (int i = 0; i < n - 1; ++i)
                {
                    for (int j = 0; j < n - 1; ++j)
                    {
                        result[i,j] = b11[i,j];
                    }
                    result[n - 1,i] = b21[0,i];
                    result[i,n - 1] = b12[i,0];
                    result[n - 1,n - 1] = b22[0,0];
                }
                return result;
            }
        }
        public void vienquanh()
        {
            int n = 8;
            double[,] iA = new double[n, n];
            //double[,] A = { {1, 2, 3, 4},
            //                {2, 1, 2, 3},
            //                {3, 2, 1, 2},
            //                {4, 3, 2, 1} };
            //double[,] A =
            //{
            //    {50, 107, 36 },
            //    {25, 54, 20 },
            //    {31, 66, 21 },
            //};
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
            Console.WriteLine("Ma trận vừa nhập: ");
            la.PrtMat(A);
            Console.WriteLine("\nMa trận nghịch đảo: ");
            iA = MatrixInverse(A, n);
            la.PrtMat(iA);
        }
        public void Gauss()
        {
            int n = 3;
            //double[,] A =
            //{
            //     {25, 15, -5},
            //     {15, 18, 0},
            //     {-5, 0, 11},
            //};
            double[,] A =
            {
                {50, 107, 36 },
                {25, 54, 20 },
                {31, 66, 21 },
            };
            double[,] A1 = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A1[i, j] = A[i, j];
                    if (i + n == j + n) A1[i, j + n] = 1;
                    else A1[i, j + n] = 0;
                }
            }
            Console.WriteLine("Expand Matrix:");
            la.PrtMat(A1);
            //Gauss
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double t = (A1[j, i] / A1[i, i]);
                        for (int k = 0; k < 2 * n; k++)
                        {
                            A1[j, k] = A1[j, k] -  A1[i, k] * t;
                        }
                    }
                }
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Step {0}", i + 1) ;
                la.PrtMat(A1);
            }
            for (int i = 0; i < n; i++)
            {
                double t = A1[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    A1[i, j] = A1[i, j] / t;
                }
            }
            Console.WriteLine("\nInverse Matrix:");
            for (int i = 0; i < n; i++)
            {
                for (int j = n; j < 2 * n; j++)
                {
                    Console.Write("{0}\t", Math.Round(A1[i, j], 3));
                }
                Console.Write("\n");
            }
        }
        public void Cholesky()
        {
            double[,] A =
            {
                 {25, 15, -5},
                 {15, 18, 0},
                 {-5, 0, 11},
            };
            int n = (int)Math.Sqrt(A.Length);
            double[,] iS = new double[n, n];
            double[,] iA = new double[n, n];
            Console.WriteLine("Matrix A: ");
            la.PrtMat(A);

            //Tìm ma trận tam giác dưới
            double[,] S = Cholesky_Decomposition(A, n);
            Console.WriteLine("\nMatrix S: ");
            la.PrtMat(S);
            //Tìm ma trận nghịch đảo của ma trận tam giác dưới
            iS = MatrixInverse(S, n);
            Console.WriteLine("\nS^(-1): ");
            la.PrtMat(iS);
            //Tính tích hai ma trận S^(-1) và iS^(-1)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    iA[i, j] = 0;

                    for (int k = 0; k < n; k++)
                        iA[i, j] += iS[k, i] * iS[k, j];
                }
            }

            Console.WriteLine("\nIversing Matrix of A: ");
            la.PrtMat(iA);
        }

    }
}
