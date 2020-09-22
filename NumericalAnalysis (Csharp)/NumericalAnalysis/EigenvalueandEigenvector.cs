using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace NumericalAnalysis
{
    class EigenvalueandEigenvector
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
        //static double[,] MatrixCreate(int rows, int cols)
        //{
        //    double[,] result = new double[rows, cols];
        //    return result;
        //}
        //static double[,] Slide(double[,] a, int n)
        //{
        //    double[,] result = MatrixCreate(n - 1, n - 1);
        //    for (int i = 0; i < n - 1; ++i)
        //        for (int j = 0; j < n - 1; ++j)
        //            result[i, j] = a[i, j];
        //    return result;
        //}
        //static void getCofactor(double[,] mat, double[,] temp, int p, int q, int n)
        //{
        //    int i = 0, j = 0;
        //    for (int row = 0; row < n; row++)
        //    {
        //        for (int col = 0; col < n; col++)
        //        {
        //            if (row != p && col != q)
        //            {
        //                temp[i, j++] = mat[row, col];
        //                if (j == n - 1)
        //                {
        //                    j = 0;
        //                    i++;
        //                }
        //            }
        //        }
        //    }
        //}
        //static double determinantOfMatrix(double[,] mat, int n)
        //{
        //    double D = 0;
        //    if (n == 1)
        //        return mat[0, 0];
        //    double[,] temp = new double[N, N];
        //    int sign = 1;
        //    for (int f = 0; f < n; f++)
        //    {
        //        getCofactor(mat, temp, 0, f, n);
        //        D = D + sign * mat[0, f] * determinantOfMatrix(temp, n - 1);
        //        sign = -sign;
        //    }
        //    return D;
        //}
        //static bool Check(double[,] A)
        //{
        //    int t = 0;
        //    for (int i = 1; i < N; i++)
        //    {
        //        double[,] SubA = Slide(A, N);
        //        double d1 = determinantOfMatrix(SubA, i);
        //        if (d1 < 0) t = 0;
        //        else t++; 
        //    }
        //    double d = determinantOfMatrix(A, N);
        //    if (d < 0) t = 0;
        //    else t++;
        //    if (t == 0) return false;
        //    else return true;
        //}
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
            //double[] Y = { -1, 1, 0, 0 };
            //double[,] A = { {1, 2, 3, 4},
            //                {2, 1, 2, 3},
            //                {3, 2, 1, 2},
            //                {4, 3, 2, 1} };
            double[] Y = { 1, 1, 1, 1, 1, 1, 1, 1};
            int n = Y.Length,
                m = 1;
            double[] C = new double[n];
            double[] Lambda = new double[n];
            double[] X = new double[n];

            Console.Write("Y:   ");
            for (int i = 0; i < n; i++)
                Console.Write("{0}      ", Y[i]);
            Console.WriteLine("\n");

        ITERATION:
            Console.Write("A^{0}*Y:", m);
            for (int i = 0; i < n; i++)
            {
                C[i] = 0;

                for (int j = 0; j < n; j++)
                    C[i] += A[i, j] * Y[j];

                Console.Write("\t" + C[i]);
            }
            Console.WriteLine("\n");

            m = m + 1;
            if (m == 9) goto FIND;
            for (int i = 0; i < n; i++) Y[i] = C[i];
            goto ITERATION;
        FIND:
            Console.Write("λ1=  ");
            for (int i = 0; i < n; i++)
            {
                Lambda[i] = C[i] / Y[i];
                Console.Write("{0}  ", Math.Round(Lambda[i], 5));
            }
            Console.WriteLine();
            Console.Write("X1=  ");
            for (int i = 0; i < n; i++)
            {
                X[i] = C[i] / C[n - 1];
                Console.Write("{0}  ", Math.Round(X[i], 5));
            }
        }
        //Danielevsky Method
        static double[] Graeffe(double[] d)
        {
            int n = d.Length;
            int k = n - 1;
            int m = 0;

            double[] x1 = new double[n];
            double[] x2 = new double[n];
            double[] x = new double[n];
            double[] c = new double[n];
            double[] a = new double[n + 1];
            double[] b = new double[n + 1];
            double[] b1 = new double[n + 1];
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                c[i] = Convert.ToDouble(d[i]);
                Console.WriteLine("Hệ số của λ^{0}= {1}", k, c[i]);
                a[i] = c[i];
                k = k - 1;
            }
            //Console.WriteLine("\nNghiệm của phương trình là: ");

            //lobasepski
        SQUARE:
            b[0] = Math.Pow(a[0], 2);
            for (int i = 1; i <= n; i++)
            {
                int j = 1;
                b[i] = Math.Pow(a[i], 2);
                while ((i + j <= n) && (j <= i))
                {
                    b[i] = b[i] + Math.Pow(-1, j) * 2 * a[i - j] * a[i + j];
                    j = j + 1;
                }
                for (int z = 1; z <= n; z++)
                    b1[z] = b[z];
            }


            m = m + 1;
            if (m == 6) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
            for (int i = 0; i < n; i++)
            {
                x1[i] = Math.Pow(Math.Abs(b[i + 1] / b[i]), Math.Pow(2, -m));
                x2[i] = -x1[i];

                double f = 0;
                for (int j = 0; j < n; j++) f = f * x1[i] + c[j];
                if (Math.Abs(f) < 0.001) x[i] = x1[i];

                f = 0;
                for (int j = 0; j < n; j++) f = f * x2[i] + c[j];
                if (Math.Abs(f) < 0.001) x[i] = x2[i];
            }

            return x;
        } 
        static void DanilevskyMethod(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            //double[] poly = new double[n + 1];
            double[,] M = new double[n, n];
            double[,] iM = new double[n, n];
            double[,] B = new double[n, n];
            double[,] B1 = new double[n, n];
            double[,] V = new double[n, n];
            int c = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) B1[i, j] = 1;
                    else B1[i, j] = 0;
                }
            }

            for (int k = n - 2; k >= 0; k--)
            {
                Console.WriteLine("Step {0}", c);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i != k)
                        {
                            if (i == j) M[i, j] = iM[i, j] = 1;
                            else M[i, j] = iM[i, j] = 0;
                        }
                        else
                        {
                            M[i, j] = A[k + 1, j];
                            if (j == k) iM[i, j] = 1 / A[k + 1, k];
                            else iM[i, j] = -A[k + 1, j] / A[k + 1, k];
                        }
                    }
                }
                Console.WriteLine("M{0}: ", c);
                la.PrtMat(M);
                Console.WriteLine("\niM{0}: ", c);
                la.PrtMat(iM);
                B = la.MultiMat(A, iM);
                A = la.MultiMat(M, B);
                B = la.MultiMat(B1, iM);
                B1 = B;

                Console.WriteLine("\nA{0}: ", c + 1);
                la.PrtMat(A);
                if (k == 0)
                {
                    Console.WriteLine("\nB: ");
                    la.PrtMat(B);
                }
                Console.WriteLine("----------------------------------");
                c++;
            }
            Console.WriteLine("Frobenius form:");
            la.PrtMat(A);

            //for (int i = 0; i < n + 1; ++i)
            //{
            //    if (i == 0) poly[i] = Math.Pow(-1, n) * 1;
            //    else poly[i] = Math.Pow(-1, n) * -1 * A[0, i - 1];
            //}
            double[] poly = { 1, -53, -27, 10, 99, 1999 };
            double[] lambda = Graeffe(poly);
            //double[] lambda = { -2, 2, 3 };
            for (int i = 0; i < n; i++)
            {
                double[] X = new double[n];
                Console.WriteLine("\nλ{0} = {1}", i + 1, lambda[i]);
                Console.Write("Y{0} = ", i + 1);
                for (int j = 0; j < n; j++)
                {
                    V[i, j] = Math.Pow(lambda[i], n - j - 1);
                    Console.Write("{0}    ", V[i, j]);
                    X[j] = V[i, j];
                }
                Console.WriteLine();

                //Tìm vector riêng bằng ma trận B cuối nhân với ma trận cột Y
                poly = la.MulMatVt(B, X);
                Console.Write("X{0} =  ", i + 1);
                for (int j = 0; j < n; j++)
                    Console.Write("{0}   ", poly[j]);

                Console.WriteLine();
            }
        }
        public void Danielevski()
        {
            double[,] A = { {1, 2, 3, 4},
                            {2, 1, 2, 3},
                            {3, 2, 1, 2},
                            {4, 3, 2, 1} };
            //double[,] A = { {1, 3, 1, 4, -3, 5},
            //                {4, 12, 1, 2, 5, 6},
            //                {-1, 2, 5, 1, 4, 7},
            //                {4, 12, 1, 2, 6, 1},
            //                {2, 3, 4, 5, 8, 6 },
            //                {-1, 8, -6, -3, -7, 21},
            //};
            DanilevskyMethod(A);
        }
        //Phuong phap xuong thang
        public void xuongthang()
        {
            /*double[,] A =
            {
                {4, 2, 2},
                {2, 5, 1},
                {2, 1, 6},
            };*/

            double[,] A = { {1, 3, 1, 4, -3},
                            {4, 12, 1, 2, 5},
                            {-1, 2, 5, 1, 4},
                            {4, 12, 1, 2, 1},
                            {2, 3, 4, 5, 8 },
            };
            //double[,] A =
            //    {
            //                {2, 3, 2},
            //                {4, 3, 5},
            //                {3, 2, 9},
            //            };
            /*double[,] A =
            {
                {1, 2, 3, 4 },
                {2, 1, 2, 3 },
                {3, 2, 1, 2 },
                {4, 3, 2, 1 },
            };*/

            /*double[,] A = {
                  {4.9, 1.0, 0.1, 1.1},
                  { 1.0, 6.4, 1.2, 0.2},
                  { 0.1, 1.2, 3.6, 1.1},
                  { 1.1, 0.2, 1.1, 6.4},
                  };*/

            double[] X = { 1, 1, 1, 1, 1, 1};

            int n = X.Length;
            double eps = 0.000001, λ0 = 0, λ = 0;
            double[] Y = new double[n];
            double[] C = new double[n];
            double[] Lambda = new double[n]; // Mảng chứa giá trị riêng
            double[,] vv = new double[n, n]; // Mảng chứa vector riêng

            for (int l = 0; l < n; l++)
            {
                la.PrtMat(A);
                
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
                Console.WriteLine("λ{0}= {1:0.0000}\t", l + 1, λ);
                Console.WriteLine("Vec to rieng: ");
                for (int i = 0; i < n; i++)
                    Console.Write("{0:0.000}\t", X[i]);
                Console.WriteLine("\n");

                /*--------------Xuống thang tìm ma trận A ---------------*/

                double ps = 0;
                for (int i = 0; i < n; i++)
                {
                    ps += X[i] * X[i];
                    vv[l, i] = X[i]; //Lưu vector riêng
                }
                //ps: binh phuong do dai vecto x
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        A[i, j] -= (λ / ps) * X[i] * X[j];
            }
        }
    }
}
