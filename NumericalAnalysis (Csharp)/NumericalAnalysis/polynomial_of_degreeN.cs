using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class polynomial_of_degreeN
    {
        static Linalg la = new Linalg();
        static double[] a = new double[1000];
        static int n, KL, dem = 0;
        static double eps = 0.001;
        static double f(double x)
        {
            double p = 0;
            for (int i = 0; i <= n; i++)
            {
                p = p * x + a[i];
            }
            return p;
        }
        static double fdh(double x)
        {
            double p = 0;
            int m;
            m = n;
            for (int i = 0; i <= n - 1; i++)
            {
                p = p * x + m * a[i];
                m = m - 1;
            }
            return p;
        }
        static double GiaiNghiem(double x)
        {
            int start, finish;
            //int dem=0;
            start = 0;
            finish = 300;
            if (n == 1)
            {
                if (a[0] != 0) return -a[1] / a[0];
                else return KL = 1;
            }
            while (fdh(x) == 0)
            {
                x = x + 0.6969; //Để đạo hàm khác 0
            }
            while (start < finish)
            {
                if (start == (finish - 1))
                {
                    if (Math.Abs(f(x)) > 0) KL = 1;
                }
                x = x - (f(x) / fdh(x));
                //Console.WriteLine("x = "+ x);
                if (Math.Abs(f(x)) < eps) return x;
                start++;
                //dem++;
            }
            return x;
        }
        static void chiadathuc(double x)
        {
            for (int i = 1; i < n; i++)
            {
                a[i] = a[i] + a[i - 1] * x;
            }
            n = n - 1;
        }
        public void main()
        {
            double x;
            int T = 0;
            do
            {
                Console.Write("Giải đa thức bậc n, n = ");
                n = Convert.ToInt32(Console.ReadLine());
            }
            while (n <= 0);
            Console.WriteLine("\nNhập các hệ số");
            do
            {
                Console.Write("x^{0} = ", n);
                a[0] = Convert.ToDouble(Console.ReadLine());
            } while (a[0] == 0);
            for (int i = 1; i <= n; i++)
            {
                Console.Write("x^{0} = ", n - i);
                a[i] = Convert.ToDouble(Console.ReadLine());

            }
            while (n > 0)
            {
                x = GiaiNghiem(0);
                if (KL == 1)
                {
                    if (T == 0) Console.WriteLine("Phương trình vô nghiệm");
                    break;
                }
                if (T == 0) Console.WriteLine("\nNghiệm của phương trình là:");
                Console.WriteLine("X= {0}", x);
                chiadathuc(x);
                T = 1;
                //Console.WriteLine("{0}", dem);
            }
            Console.ReadKey();
        }
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
            }


            m = m + 1;
            if (m == 4) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
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
        public void main2()
        {
            double[] poly = new double[n + 1];
            double[,] M = new double[n, n];
            double[,] iM = new double[n, n];
            double[,] B = new double[n, n];
            double[,] B1 = new double[n, n];
            double[,] V = new double[n, n];
            double[] y = { 1, -53, -27, 10, 99, 1999};
            double[] lambda = Graeffe(y);
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
                y = la.MulMatVt(B, X);
                Console.Write("X{0} =  ", i + 1);
                for (int j = 0; j < n; j++)
                    Console.Write("{0}   ", y[j]);

                Console.WriteLine();
            }
        }
    }
}
