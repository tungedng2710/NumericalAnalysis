using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class dominatesystemequations
    {
        static Linalg la = new Linalg();
        static double chuan1(double[,] x, int n)
        {
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum = sum + Math.Abs(x[i, 0]);
            }
            return sum;
        }
        public void lapdon()
        {
            int count = 0;
            double eps = 0.01;
            double[] sum = new double[100];
            double[] sum1 = new double[100];
            int flag = 0, temp = 0;
            for (int i = 0; i < 100; i++) sum[i] = 0;
            for (int i = 0; i < 100; i++) sum1[i] = 0;
            double[,] A = { {4, 0.24, -0.08},
                            {0.09, 3, -0.15},
                            {0.04, -0.08, 4}};
            double[,] B = { {8},
                            {9},
                            {20}};
            int r = A.GetLength(0);
            int c = A.GetLength(1);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (i == j) continue;
                    else sum1[i] = sum1[i] + Math.Abs(A[i, j]);
                }
            }
            for (int i = 0; i < r; i++)
            {
                if (A[i, i] < sum1[i]) { temp = 0; break; }
                else temp = 1;
            }
            if (temp == 0) Console.WriteLine("Không phải ma trận đường chéo trội");
            else
            {
                double[,] a1 = new double[3, 3];
                double[,] b1 = new double[3, 1];
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (i == j) a1[i, j] = 0;
                        else a1[i, j] = -A[i, j] / A[i, i];
                    }
                    b1[i, 0] = B[i, 0] / A[i, i];
                }
                for (int i = 0; i < r; i++) //Kiểm tra điều kiện hội tụ 
                {
                    for (int j = 0; j < c; j++)
                    {
                        sum[i] = sum[i] + Math.Abs(a1[i, j]);
                    }
                }
                if (sum.Max() < 1) flag = 1;
                else flag = 0;
                if (flag == 1)
                {
                    double[,] x = { { 0 }, { 0 }, { 0 } };
                    double[,] y = new double[3, 1];
                    do
                    {
                        for (int k = 0; k < 3; k++)
                            y[k, 0] = x[k, 0];
                        x = la.SumMat(la.MultiMat(a1, x), b1); // X(k) = aX(k-1) + b
                        count++;
                    } while (la.norm1(la.SubMat(x, y)) > eps);
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write("\n");
                        for (int j = 0; j < 3; j++)
                            Console.Write(A[i, j] + "    ");
                        Console.Write("|  " + B[i, 0]);
                    }
                    Console.Write("\n\n");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write("\n");
                        for (int j = 0; j < 3; j++)
                            Console.Write(a1[i, j] + "    ");
                        Console.Write("|  " + b1[i, 0]);
                    }
                    Console.Write("\n\n");
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write("\n");
                        for (int j = 0; j < 1; j++)
                            Console.Write("x{0} = {1}\t", i + 1, x[i, j]);
                    }
                    Console.Write("\n\n");
                }
                else Console.WriteLine("Không đủ điều kiện lặp");
            }
        }
        static bool CheckJacobiRow(double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                double Sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        Sum += Math.Abs(A[i, j]);
                }
                if (Math.Abs(A[i, i]) <= Sum)
                    return false;
            }
            return true;
        }
        static bool CheckJacobiCol(double[,] A, int n)
        {
            for (int i = 0; i < n; i++)
            {
                double Sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        Sum += Math.Abs(A[j, i]);
                }
                if (Math.Abs(A[i, i]) <= Sum)
                    return false;
            }
            return true;
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
                {1 },
                {0 },
                {0 }
            };
            int n = b.GetLength(0);
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
                {0 },
                {0 },
                {0 }
            };

            //danh gia sai so
            //List<double> AbsT = new List<double>();
            //for (int i = 0; i < n; i++)
            //    AbsT.Add(Math.Abs(A[i, i]));
            //double minaii = AbsT.Min();
            //double MAXaii = AbsT.Max();
            //double lambda = MAXaii / minaii;
            double[,] y = new double[n, 1]; //bien trung gian

            int c = 0;
            do
            {
                Console.Write("\nNo.{0}", c + 1);
                for (int i = 0; i < n; i++)
                    y[i, 0] = x[i, 0];
                x = la.SumMat(la.MultiMat(B, x), d);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\n");
                    Console.Write("x{0} = {1}\t", j, x[j, 0]);
                }
                Console.WriteLine();
                c++;
            } while (la.norminf(B)/(1 - la.norminf(B)) * la.norminf(la.SubMat(x, y)) > eps);
        }
        public void JacobiCol()
        {
            double eps = 0.0001;
            int n = 3;
            double[,] A =
            {
                {4, 0.24, -0.08},
                {0.09, 3, -0.15},
                {0.04, -0.08, 4}
            };
            double[,] b =
            {
                {8 },
                {9 },
                {20 }
            };
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

            double[,] B = la.SubMat(I, la.MultiMat(A, T));
            double[,] x =
            {
                {0 },
                {0 },
                {0 }
            };

            //danh gia sai so
            List<double> AbsT = new List<double>();
            for (int i = 0; i < n; i++)
                AbsT.Add(Math.Abs(A[i, i]));
            double minaii = AbsT.Min();
            double MAXaii = AbsT.Max();
            double lambda = MAXaii / minaii;

            double[,] y = new double[n, 1]; //bien trung gian
            int c = 0;
            do
            {
                Console.Write("\nNo.{0}", c + 1);
                for (int i = 0; i < n; i++)
                    y[i, 0] = x[i, 0];
                x = la.SumMat(la.MultiMat(B, x), b);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\n");
                    Console.Write("x{0} = {1}\t", j, x[j, 0]);
                }
                Console.WriteLine();
                c++;
            } while (lambda * (la.norm1(B) / (1 - la.norm1(B))) * la.norm1(la.SubMat(x, y)) > eps);
        }
    }
}
