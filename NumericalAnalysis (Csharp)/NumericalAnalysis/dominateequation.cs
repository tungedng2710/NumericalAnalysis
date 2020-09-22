using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class dominateequation
    {
        static double x;
        static double pi(double x)
        {
            return Math.Sin(x);
        }
        static double f1(double x)
        {
            return Math.Pow(x, 3) - 4 * x * x + 3 * x + 1;
        }
        static double m1 = daoham2(-1);
        static double f(double x)
        {
            return Math.Pow(1.65, 0.5) * x * x * x + 1.12 * x * x + 14.35 * x - 12.75;
        }
        static double fe(double x)
        {
            return Math.Sin(x);
        }
        static double g(double x)
        {
            return Math.Pow(x + 1, 0.2);
        }
        static void e()
        {
            double a = 3, b = 2;
            double c;
            Console.Write("ep = "); double ep = Convert.ToDouble(Console.ReadLine());
            do
            {
                c = (a + b) / 2;
                if (fe(c) < 0)
                {
                    b = c;
                }
                else { a = c; }
            } while (Math.Abs(fe(c)) > ep);
            Console.Write("e = " + c);
        }
        static double daoham1(double x1)
        {
            double ep = 0.00001;
            double a = (g(x1) - g(x1 - ep)) / (ep);
            return a;
        }
        public static double daoham2(double x1)
        {
            double ep = 0.0000000001;
            double a = (f1(x1 + ep) - f1(x1 - ep)) / (2 * ep);
            return a;
        }
        static double daohamcap2(double x)
        {
            const double ep = 0.00001;
            return (daoham2(x + ep) - daoham2(x - ep)) / (2 * ep);
        }
        public void Chiadoi()
        {
            double c, ep, a, b;
            //Console.WriteLine("Phương trình X^10 - 5 = 0");
            Console.Write("epsilon = "); ep = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Khoang phan ly nghiem (a; b)");
            Console.Write("Nhap a sao cho f(a) < 0 = "); a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Nhap b sao cho f(b) > 0 = "); b = Convert.ToDouble(Console.ReadLine());
            do
            {
                c = (a + b) / 2;
                if (pi(c) < 0)
                {
                    a = c;
                }
                else { b = c; }
                Console.WriteLine("x = " + c);
            } while (Math.Abs(pi(c)) > ep);

            Console.WriteLine("Nghiệm của phương trình theo pp chia đôi là x = " + c);
        }
        public void Daycung()
        {
            double dx, x = 0, ep, a, b;
            Console.Write("epsilon = "); ep = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhập khoảng phân li nghiệm (a;b) ");
            Console.Write("Nhập a = "); a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Nhập b = "); b = Convert.ToDouble(Console.ReadLine());
            if (f1(a) * daohamcap2(a) < 0 && f1(b) * f1(a) < 0) x = a;
            else if (f1(b) * daohamcap2(b) < 0 && f1(b) * f1(a) < 0) x = b;
            if (f1(a) * f1(b) > 0) Console.Write("Không hội tụ");
            else
            {
                Console.WriteLine("x0 = " + x);
                dx = -((b - a) * f1(a)) / (f1(b) - f1(a));
                double fa = f1(a), fb = f1(b);
                while (Math.Abs(dx) > ep)
                {
                    x = a + dx;
                    fa = f1(x);
                    if ((fa * fb) < 0) a = x;
                    else b = x;
                    fa = f1(a);
                    fb = f1(b);
                    dx = fa * (b - a) / (fa - fb);
                    Console.WriteLine("Nghiệm của phương trình theo pp dây cung là x = " + x);
                }
                Console.WriteLine("Nghiệm của phương trình theo pp dây cung là x = " + x);
            }
        }
        public void Newton()
        {
            double i = 0, y = 0;
            double ep = 0.5 * Math.Pow(10, -8);
            double dfx, a, b, fa, fb;
            //Console.Write("Nhập x0 = "); double x = Convert.ToDouble(Console.ReadLine());
            //double fx = f1(x);
            Console.WriteLine("(a;b)");
            Console.Write("a = "); a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b = "); b = Convert.ToDouble(Console.ReadLine());
            fa = f1(a); fb = f1(b);
            if (fa == 0) Console.WriteLine("x = " + a);
            else if (fb == 0) Console.WriteLine("x = " + b);
            else if (fa * fb > 0) Console.WriteLine("Không hội tụ");
            //Console.WriteLine("x0 = {0}", x); 
            //if (f1(x) * daohamcap2(x) <= 0) Console.WriteLine("Không hội tụ");
            else if (fa * fb < 0)
            {
                if (f1(a) * daohamcap2(a) > 0) x = a;
                if (f1(b) * daohamcap2(b) > 0) x = b;
                Console.WriteLine("x0 = {0}", x);
                double ep1 = m1 * ep;
                while (Math.Abs(f1(x)) > ep1)
                {
                    i++;
                    dfx = daoham2(x);
                    y = x;
                    x = x - f1(x) / dfx;
                    Console.WriteLine("x{0} = {1} ", i, x);
                }
                Console.WriteLine("Nghiệm của phương trình là " + x);
            }
            Console.ReadKey();
        }
        public void lapdon()
        {
            double y, a, b, x0;
            Console.Write("Nhập epsilon = "); double eps = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("(a; b)");
            Console.Write("a = "); a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b = "); b = Convert.ToDouble(Console.ReadLine());
            x0 = a;
            double fa = a - g(a);
            double fb = b - g(b);
            if (Math.Abs(daoham1(x0)) >= 1 || fa * fb > 0) Console.WriteLine("Math Error");
            else
            {
                do
                {
                    y = x0;
                    x0 = g(x0);
                    Console.WriteLine("x =  " + x0);
                } while (Math.Abs(x0 - y) > eps);
                Console.WriteLine("Nghiệm của phương trình là " + x0);
            }
        }
    }
}
