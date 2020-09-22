using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace NumericalAnalysis
{
    class Linalg
    {
        public void PrtEqt(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    Console.Write("{0}\t", Math.Round(A[i, j], 2));
                    if (j == n - 1) Console.Write("|");
                }
                Console.Write("\n");
            }
        }
        public void PrtMat(double[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write("{0}\t", Math.Round(A[i, j], 5));
                }
                Console.Write("\n");
            }
        }
        public double[,] Trans(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double[,] AT = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    AT[i, j] = A[j, i];
            return AT;
        }
        public double norm1(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double max = 0;
            List<double> L = new List<double>();
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < m; j++)
                {
                    sum = sum + Math.Abs(A[j, i]);
                }
                L.Add(sum);
                max = L.Max();
            }
            return max;
        }         
        public double norminf(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double max = 0;
            List<double> L = new List<double>();
            for (int i = 0; i < m; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum = sum + Math.Abs(A[i, j]);
                }
                L.Add(sum);
                max = L.Max();
            }
            return max;
        }
        public double normeuclid(double[,] A)
        {
            int m = A.GetLength(0);
            int n = A.GetLength(1);
            double sum = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum = sum + Math.Pow(A[i, j], 2);
                }
            }
            return Math.Sqrt(sum);
        }
        public double[,] MultiMat(double[,] A, double[,] B)
        {
            int r1 = A.GetLength(0);
            int c1 = A.GetLength(1);
            int r2 = B.GetLength(0);
            int c2 = B.GetLength(1);
            double sum;
            int k, i, j;
            double[,] ma_tran_tich = new double[r1, c2];
            for (i = 0; i < r1; i++)
                for (j = 0; j < c2; j++)
                    ma_tran_tich[i, j] = 0;
            for (i = 0; i < r1; i++)    //hang cua ma tran thu nhat 
            {
                for (j = 0; j < c2; j++)    //cot cua ma tran thu hai 
                {
                    sum = 0;
                    for (k = 0; k < c1; k++)
                        sum = sum + A[i, k] * B[k, j];
                    ma_tran_tich[i, j] = sum;
                }
            }
            return ma_tran_tich;
        }
        public double[] MulMatVt(double[,] A, double[] B)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] C = new double[n];
            for (int i = 0; i < n; i++)
            {
                C[i] = 0;
                for (int j = 0; j < n; j++)
                    C[i] += A[i, j] * B[j];
            }
            return C;
        }
        public double[,] SumMat(double[,] A, double[,] B)
        {
            int r = A.GetLength(0);
            int c = B.GetLength(1);
            double[,] sum = new double[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    sum[i, j] = A[i, j] + B[i, j];
            return sum;
        }
        public double[,] SubMat(double[,] A, double[,] B)
        {
            int r = A.GetLength(0);
            int c = B.GetLength(1);
            double[,] matranhieu = new double[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    matranhieu[i, j] = A[i, j] - B[i, j];
            return matranhieu;
        }
        static int SignOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        static double[,] CreateSmallerMatrix(double[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            double[,] output = new double[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }
        public double Det(double[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                double value = 0;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value = value + input[0, j] * (SignOfElement(0, j) * Det(Temp));
                }
                return value;
            }
            else if (order == 2)
            {
                return ((input[0, 0] * input[1, 1]) - (input[1, 0] * input[0, 1]));
            }
            else
            {
                return (input[0, 0]);
            }
        }
        public double[,] IMat(double[,] A)
        {
            int n = A.GetLength(0);
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
                            A1[j, k] = A1[j, k] - A1[i, k] * t;
                        }
                    }
                }
                
            }
            for (int i = 0; i < n; i++)
            {
                double t = A1[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    A1[i, j] = A1[i, j] / t;
                }
            }
            double[,] A2 = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    A2[i, j] = A1[i, j + n];
            return A2;
        }
        public Complex[,] CplxMultiMat(Complex[,] A, Complex[,] B)
        {
            int r1 = A.GetLength(0);
            int c1 = A.GetLength(1);
            int r2 = B.GetLength(0);
            int c2 = B.GetLength(1);
            Complex sum;
            int k, i, j;
            Complex[,] ma_tran_tich = new Complex[r1, c2];
            for (i = 0; i < r1; i++)
                for (j = 0; j < c2; j++)
                    ma_tran_tich[i, j] = 0;
            for (i = 0; i < r1; i++)    //hang cua ma tran thu nhat 
            {
                for (j = 0; j < c2; j++)    //cot cua ma tran thu hai 
                {
                    sum = 0;
                    for (k = 0; k < c1; k++)
                        sum = sum + A[i, k] * B[k, j];
                    ma_tran_tich[i, j] = sum;
                }
            }
            return ma_tran_tich;
        }
        public Complex[] CplxMulMatVt(Complex[,] A, Complex[] B)
        {
            int n = A.GetLength(0);
            Complex[] C = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                C[i] = 0;
                for (int j = 0; j < n; j++)
                    C[i] += A[i, j] * B[j];
            }
            return C;
        }
        public Complex[,] CplxSumMat(Complex[,] A, Complex[,] B)
        {
            int r = A.GetLength(0);
            int c = B.GetLength(1);
            Complex[,] sum = new Complex[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    sum[i, j] = A[i, j] + B[i, j];
            return sum;
        }
        public Complex[,] CplxSubMat(Complex[,] A, Complex[,] B)
        {
            int r = A.GetLength(0);
            int c = B.GetLength(1);
            Complex[,] matranhieu = new Complex[r, c];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    matranhieu[i, j] = A[i, j] - B[i, j];
            return matranhieu;
        }
        public void CplxPrtMat(Complex[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write("{0:0.00}\t", A[i, j]);
                }
                Console.Write("\n");
            }
        }
    }
    // PrtMat: In ma trận
    // PrtEqt: In hệ phương trình vừa nhập
    // Trans(A): Ma trận chuyển vị của A
    // norm1(A): Chuẩn 1 của ma trận A
    // norminf(A): Chuẩn vô cùng của ma trận A
    // normeuclid(A): Chuẩn Eucldide của ma trận A
    // MultiMat(A, B): Nhân ma trận A với B

}
