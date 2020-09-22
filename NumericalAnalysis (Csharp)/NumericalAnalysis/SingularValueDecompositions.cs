using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class SingularValueDecompositions
    {
        static Linalg la = new Linalg();
        static double[] solvepolynomial(double[] d) //Graeffe method
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

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                c[i] = Convert.ToDouble(d[i]);
                a[i] = c[i];
                k = k - 1;
            }

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
            }
            m = m + 1;
            if (m == 7) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
            k = 1;
            for (int i = 0; i < n; i++)
            {
                x1[i] = Math.Pow(Math.Abs(b[i + 1] / b[i]), Math.Pow(2, -m));
                x2[i] = -x1[i];
                double f = 0;
                for (int j = 0; j < n; j++) f = f * x1[i] + c[j];
                if (Math.Abs(f) < 0.1) x[i] = x1[i];

                f = 0;
                for (int j = 0; j < n; j++) f = f * x2[i] + c[j];
                if (Math.Abs(f) < 0.1) x[i] = x2[i];
            }
            return x;
        }
        public static double[,] Frobenius(double[,] A, int N)
        {
            double[,] M = new double[N, N];
            double[,] m = new double[N, N];
            double[,] a = new double[N, N];
            int i, j, k, l;
            for (i = N - 2; i >= 0; i--)
            {
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        if (j == i)
                        {
                            M[j, k] = A[j + 1, k];
                        }
                        else
                        {
                            if (j == k) M[j, k] = 1; else M[j, k] = 0;
                        }
                    }
                }
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        if (j == i)
                        {
                            if (j == k) m[j, k] = 1 / M[i, i]; else m[j, k] = -(M[j, k] / M[i, i]);
                        }
                        else
                        {
                            if (j == k) m[j, k] = 1; else m[j, k] = 0;
                        }
                    }
                }
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        for (l = 0; l < N; l++)
                        { a[j, k] += M[j, l] * A[l, k]; }
                    }
                }
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        A[j, k] = 0;
                    }
                }
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        for (l = 0; l < N; l++)
                        {
                            A[j, k] += a[j, l] * m[l, k];
                        }
                    }
                }
                for (j = 0; j < N; j++)
                {
                    for (k = 0; k < N; k++)
                    {
                        a[j, k] = 0;
                    }
                }
            }
            return A;
        }
        public void Danielevski(double[,] A)
        {
            int n = A.GetLength(0);
            double[] poly = new double[n + 1];
            Console.WriteLine();
            A = Frobenius(A, n);
            for (int i = 0; i < n + 1; ++i)
            {
                if (i == 0) poly[i] = Math.Pow(-1, n) * 1;
                else poly[i] = Math.Pow(-1, n) * -1 * A[0, i - 1];
            }
            double[] lambda = solvepolynomial(poly);
        }
        
    }
}
