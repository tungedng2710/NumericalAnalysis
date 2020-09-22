using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
namespace NumericalAnalysis
{
    class ComplexEig
    {
        static Linalg la = new Linalg();
        static Complex MAX(Complex[] A)
        {
            int n = A.Length;
            Complex max = A[0];
            for (int i = 1; i < n; i++)
            {
                if (Complex.Abs(max) < Complex.Abs(A[i]))
                    max = A[i];
            }
            return max;
        }
        public void luythua()
        {
            double[,] A =
            {
                {2, 3, 2},
                {4, 3, 5},
                {3, 2, 9},
            };
            //double[] Y = { -1, 1, 0, 0 };
            //double[,] A = { {1, 2, 3, 4},
            //                {2, 1, 2, 3},
            //                {3, 2, 1, 2},
            //                {4, 3, 2, 1} };
            double[] Y = { 1, 1, 1 };
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
        static Complex[] Graeffe(Complex[] d)
        {
            int n = d.Length;
            int k = n - 1;
            int m = 0;

            Complex[] x1 = new Complex[n];
            Complex[] x2 = new Complex[n];
            Complex[] x = new Complex[n];
            Complex[] c = new Complex[n];
            Complex[] a = new Complex[n + 1];
            Complex[] b = new Complex[n + 1];

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                c[i] = d[i];
                Console.WriteLine("Hệ số của λ^{0}= {1}", k, c[i]);
                a[i] = c[i];
                k = k - 1;
            }
        //Console.WriteLine("\nNghiệm của phương trình là: ");

        //lobasepski
        SQUARE:
            b[0] = Complex.Pow(a[0], 2);
            for (int i = 1; i <= n; i++)
            {
                int j = 1;
                b[i] = Complex.Pow(a[i], 2);
                while ((i + j <= n) && (j <= i))
                {
                    b[i] = b[i] + Math.Pow(-1, j) * 2 * a[i - j] * a[i + j];
                    j = j + 1;
                }
            }


            m = m + 1;
            if (m == 5) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
            for (int i = 0; i < n; i++)
            {
                x1[i] = Complex.Pow(Complex.Abs(b[i + 1] / b[i]), Complex.Pow(2, -m));
                x2[i] = -x1[i];

                Complex f = 0;
                for (int j = 0; j < n; j++) f = f * x1[i] + c[j];
                if (Complex.Abs(f) < 0.1) x[i] = x1[i];

                f = 0;
                for (int j = 0; j < n; j++) f = f * x2[i] + c[j];
                if (Complex.Abs(f) < 0.1) x[i] = x2[i];
            }

            return x;
        }
        static void DanilevskyMethod(Complex[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            Complex[] poly = new Complex[n + 1];
            Complex[,] M = new Complex[n, n];
            Complex[,] iM = new Complex[n, n];
            Complex[,] B = new Complex[n, n];
            Complex[,] B1 = new Complex[n, n];
            Complex[,] V = new Complex[n, n];
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
                la.CplxPrtMat(M);
                Console.WriteLine("\niM{0}: ", c);
                la.CplxPrtMat(iM);
                B = la.CplxMultiMat(A, iM);
                A = la.CplxMultiMat(M, B);
                B = la.CplxMultiMat(B1, iM);
                B1 = B;

                Console.WriteLine("\nA{0}: ", c + 1);
                la.CplxPrtMat(A);
                if (k == 0)
                {
                    Console.WriteLine("\nB: ");
                    la.CplxPrtMat(B);
                }
                Console.WriteLine("----------------------------------");
                c++;
            }
            Console.WriteLine("Frobenius form:");
            la.CplxPrtMat(A);

            for (int i = 0; i < n + 1; ++i)
            {
                if (i == 0) poly[i] = Math.Pow(-1, n) * 1;
                else poly[i] = Math.Pow(-1, n) * -1 * A[0, i - 1];
            }

            Complex[] lambda = Graeffe(poly);
            //double[] lambda = { -2, 2, 3 };
            for (int i = 0; i < n; i++)
            {
                Complex[] X = new Complex[n];
                Console.WriteLine("\nλ{0} = {1}", i + 1, lambda[i]);
                Console.Write("Y{0} = ", i + 1);
                for (int j = 0; j < n; j++)
                {
                    V[i, j] = Complex.Pow(lambda[i], n - j - 1);
                    Console.Write("{0}    ", V[i, j]);
                    X[j] = V[i, j];
                }
                Console.WriteLine();

                //Tìm vector riêng bằng ma trận B cuối nhân với ma trận cột Y
                poly = la.CplxMulMatVt(B, X);
                Console.Write("X{0} =  ", i + 1);
                for (int j = 0; j < n; j++)
                    Console.Write("{0}   ", poly[j]);

                Console.WriteLine();
            }
        }
        public void Danielevski()
        {
            //double[,] A = { {1, 2, 3, 4},
            //                {2, 1, 2, 3},
            //                {3, 2, 1, 2},
            //                {4, 3, 2, 1} };
            Complex[,] A = { {1, 3, 1, 4, -3, 5},
                            {4, 12, 1, 2, 5, 6},
                            {-1, 2, 5, 1, 4, 7},
                            {4, 12, 1, 2, 6, 1},
                            {2, 3, 4, 5, 8, 6 },
                            {-1, 8, -6, -3, -7, 21},
            };
            DanilevskyMethod(A);
        }
        static Complex[][] MatrixCreate(int rows, int cols)
        {
            Complex[][] result = new Complex[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new Complex[cols];
            return result;
        }

        static Complex[][] Trans(Complex[][] a, int size)
        {
            var result = MatrixCreate(size, size);
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    result[i][j] = a[j][i];

            return result;
        }

        static Complex[][] MatrixProduct(Complex[][] matrixA, Complex[][] matrixB)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bRows)
                throw new Exception("Ma tran khong hop le");
            Complex[][] result = MatrixCreate(aRows, bCols);
            for (int i = 0; i < aRows; ++i)
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
            return result;
        }

        static void Display(Complex[][] a, int rows, int cols)
        {
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    if (j == cols - 1) Console.WriteLine("{0}", a[i][j]);
                    else Console.Write("{0} ", a[i][j]);
        }

        static Complex[][] MatrixPow(Complex[][] a, int n)
        {
            int l = a.Length;
            var result = MatrixCreate(l, l);
            result = a;
            int c = 1;
            while (c < n)
            {
                result = MatrixProduct(result, a);

                ++c;
            }
            return result;
        }

        static Complex[][] AkY(Complex[][] A, Complex[][] Y, int k)
        {
            int n = A.Length;
            var result = MatrixCreate(n, n);
            result = MatrixProduct(MatrixPow(A, k), Y);
            return result;
        }

        static Complex[][] MatrixProduct2(Complex[][] A, double k)
        {
            int r = A.Length;
            int c = A[0].Length;
            var result = MatrixCreate(r, c);
            for (int i = 0; i < r; ++i)
                for (int j = 0; j < c; ++j)
                    result[i][j] = A[i][j] * k;
            return result;
        }

        static Complex[][] MatrixSum(Complex[][] a, Complex[][] b)
        {
            int r = a.Length;
            int c = a[0].Length;
            var result = MatrixCreate(r, c);
            for (int i = 0; i < r; ++i)
                for (int j = 0; j < c; ++j)
                    result[i][j] = a[i][j] + b[i][j];
            return result;
        }

        static Complex[] Solve(Complex a, Complex b, Complex c)
        {
            Complex[] result = new Complex[2];
            Complex delta = b * b - 4 * a * c;
            result[0] = (-b + Complex.Sqrt(delta)) / (2 * a);
            result[1] = (-b - Complex.Sqrt(delta)) / (2 * a);
            return result;
        }

        static double Norm(Complex[][] X)
        {
            int n = X.Length;
            double temp = Math.Abs(X[0][0].Real);
            for (int i = 1; i < n; ++i)
                if (Math.Abs(X[i][0].Real) > temp)
                    temp = Math.Abs(X[i][0].Real);
            return temp;
        }

        static Complex[][] Ai(Complex[][] A, Complex[][] X)
        {
            int la = A.Length;
            double norm;
            norm = Norm(X);
            var result = MatrixCreate(la, la);
            for (int i = 0; i < la; ++i)
                X[i][0] = X[i][0] / norm;
            var theta = MatrixCreate(la, la);

            for (int i = 0; i < la; ++i)
                for (int j = 0; j < la; ++j)
                {
                    if (j == 0) theta[i][j] = -X[i][0]; //chon cot 1
                    else if (i == j) theta[i][j] = 1;
                    else theta[i][j] = 0;
                }
            return MatrixProduct(theta, A);
        }

        public void main()
        {
            int n;
            int No = 1;
            Console.Write("Nhap vao kich thuoc ma tran: "); n = Convert.ToInt32(Console.ReadLine());
            double[,] a = new double[n, n];
            var A = MatrixCreate(n, n);
            String input = File.ReadAllText(@"E:\matrix.txt");
            var X1 = MatrixCreate(n, 1);
            var X2 = MatrixCreate(n, 1);
            var X3 = MatrixCreate(n, 1);

            int r = 0, c = 0;
            foreach (var row in input.Split('\n'))
            {
                c = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    A[r][c] = double.Parse(col.Trim());
                    c++;
                }
                r++;
            }

            double eps;
            Console.Write("Epsilon: "); eps = Convert.ToDouble(Console.ReadLine());

        label:
            Console.WriteLine("\nNo.{0}", No);
            Display(A, n, n);
            var Y = MatrixCreate(n, 1);
            for (int i = 0; i < n; ++i)
                Y[i][0] = 1;

            Console.WriteLine("\nGia thuyet roi vao TH1: \n");
            Complex[] p1 = new Complex[n];
            bool b = false;
            int count = 1;
            int m = 2;
            while (b == false && count < 10)
            {
                for (int i = 0; i < n; ++i)
                {
                    p1[i] = AkY(A, Y, m)[i][0] / AkY(A, Y, m - 1)[i][0];
                }

                for (int i = 1; i < n; ++i)
                {
                    if (Math.Abs(p1[i].Real - p1[0].Real) > eps)
                    {
                        b = false;
                        break;
                    }
                    else
                    {
                        b = true;
                    }
                }//kiem tra xem cac ti so co "bang nhau" khong

                Console.WriteLine("m = " + m);
                Console.WriteLine("p = " + p1[n - 1]);//tri rieng lambda
                Console.WriteLine("X: ");//vec to rieng
                X1 = AkY(A, Y, m);
                Display(X1, n, 1);
                Console.WriteLine("\n**************************\n");

                ++m;
                ++count;
            }

            Console.WriteLine("\nGia thuyet roi vao TH2: \n");
            Complex[] p2 = new Complex[n];
            b = false;
            count = 1;
            m = 1;
            while (b == false && count < 10)
            {
                for (int i = 0; i < n; ++i)
                {
                    p2[i] = AkY(A, Y, 2 * m + 2)[i][0] / AkY(A, Y, 2 * m)[i][0];
                }

                for (int i = 1; i < n; ++i)
                {
                    if (Math.Abs(p2[i].Real - p2[0].Real) > eps)
                    {
                        b = false;
                        break;
                    }
                    else
                    {
                        b = true;
                    }
                }//kiem tra xem cac ti so co "bang nhau" khong

                Console.WriteLine("m = " + m);
                Complex p = Complex.Sqrt(p2[n - 1]);
                Console.WriteLine("p = " + p);
                X2 = MatrixSum(AkY(A, Y, 2 * m), MatrixProduct2(AkY(A, Y, 2 * m - 1), p.Real));
                Display(X2, n, 1);
                Console.WriteLine("\n**************************\n");

                ++m;
                ++count;
            }

            Console.WriteLine("\nGia thuyet roi vao TH3: \n");
            Complex[] p3 = new Complex[2];
            m = 2 * n; //chon m = 2n
            Complex a2, a1, a0;
            var A2 = MatrixCreate(n, 1);
            var A1 = MatrixCreate(n, 1);
            var A0 = MatrixCreate(n, 1);

            A0 = AkY(A, Y, m);
            A1 = AkY(A, Y, m + 1);
            A2 = AkY(A, Y, m + 2);

            a2 = A0[0][0] * A1[1][0] - A1[0][0] * A0[1][0];
            a1 = A0[0][0] * A2[1][0] - A2[0][0] * A0[1][0];
            a0 = A1[0][0] * A2[1][0] - A2[0][0] * A1[1][0];

            p3 = Solve(a2, -a1, a0);

            Console.WriteLine("\nm = " + m);
            Console.WriteLine("\np = " + p3[0]);
            X3 = MatrixSum(AkY(A, Y, m + 1), MatrixProduct2(AkY(A, Y, m), -p3[1].Real));
            Display(X3, n, 1);

            int k = 0;
            Console.WriteLine("Chon tri rieng va vec to rieng: ");
            while (k != 1 && k != 2 && k != 3)
            {
                Console.Write("1, 2 hay 3?: ");
                k = Convert.ToInt32(Console.ReadLine());
            }

            var X = MatrixCreate(n, 1);
            if (k == 1) X = X1;
            else if (k == 2) X = X2;
            else X = X3;

            A = Ai(A, X3);
            while (No != n)
            {
                ++No;
                goto label;
            }

            Console.ReadKey();
        }
    }
}
