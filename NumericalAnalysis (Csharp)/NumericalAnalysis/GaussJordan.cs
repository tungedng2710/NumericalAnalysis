using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NumericalAnalysis
{
    class GaussJordan
    {
        static Linalg la = new Linalg();
        static void GJ(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                la.PrtEqt(A);

                if (A[i, i] == 0)

                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (A[j, i] != 0)
                        {
                            for (int k = 0; k < n + 1; k++)
                            {
                                double tem = A[i, k];
                                A[i, k] = A[j, k];
                                A[j, k] = tem;
                            }
                            break;
                        }
                    }
                }

                double temp = A[i, i];

                for (int j = 0; j < n + 1; j++)
                {
                    A[i, j] = A[i, j] / temp;
                }

                for (int k = 0; k < n; k++)
                {
                    temp = A[k, i];
                    if (i != k)
                    {
                        for (int j = 0; j < n + 1; j++)
                        {
                            A[k, j] = A[k, j] - (temp * A[i, j]);
                        }
                    }
                }

            }
            Console.WriteLine("\nGAUSS JORDAN");
            la.PrtEqt(A);
            Console.WriteLine("Root:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("x{0} = {1} ", i + 1, A[i, n]);
            }
        }
        public void main()
        {
            double[,] A =
            {
                {20.5, 1.7, -3.2, 2.1, 9.23, -3.52, 21.41},
                 {2.5, 37.1, 5.2, 2.8, 7.23, -5.52, 27.11},
                 {11.3, 2.7, -38.2, 4.1, -7.58, 4.25, 14.17},
                 {8.4, -4.6, -6.5, 52.1, 1.43, 15.26, 52.49},
                 {42.7, -36.9, -42.7, 61.1, 2.43, -35.26, 56.72},
                 {9.2, -1, 35, -2, 14.73, 5.64, 18.57},
            };
            GJ(A);
        }
    }
}
    
