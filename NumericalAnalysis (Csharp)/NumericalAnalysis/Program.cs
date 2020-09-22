using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalAnalysis
{
    class Program
    {
        static Linalg la = new Linalg();
        static Cholesky2 Cho2 = new Cholesky2();
        static EigenvalueandEigenvector eig = new EigenvalueandEigenvector();
        static GaussSeidel GS = new GaussSeidel();
        static InverseMatrix IM = new InverseMatrix();
        static dominatesystemequations dse = new dominatesystemequations();
        static test t = new test();
        static GaussJordan GJ = new GaussJordan();
        static Cholesky Cho1 = new Cholesky();
        static Gauss G = new Gauss();
        static polynomial_of_degreeN pn = new polynomial_of_degreeN();
        static dominateequation de = new dominateequation();
        static ComplexEig CplxEig = new ComplexEig();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ////Tìm nghiệm thực gần đúng của phương trình f(x) = 0
            //de.Chiadoi();
            //de.Daycung();
            //de.Newton();
            //pn.main();        //giải đa thức bậc n
            //de.lapdon();


            ////Giải đúng hệ phương trình đại số tuyến tính
            //G.main();
            //GJ.main();
            //Cho1.main();
            //Cho2.main();     //Cholesky cho số phức


            ////Giải gần đúng hệ phương trình đại số tuyến tính
            //dse.lapdon();
            //dse.JacobiRow();
            //dse.JacobiCol();     
            //GS.main();               //lặp Gauss-Seldel


            ////Tìm ma trận nghịch đảo
            //IM.vienquanh();
            //IM.Gauss();
            //IM.Cholesky();


            ////Tìm trị riêng, vecto riêng
            //eig.Danielevski();
            //eig.main();
            //eig.luythua();
            //eig.xuongthang();
            //CplxEig.main();


            ////Test
            //t.luythua();
            //t.xuongthang();
            //t.JacobiRow();
            Console.WriteLine("Giải tích số --v20.09");
            Console.WriteLine("Tìm nghiệm thực gần đúng của phương trình f(x) = 0");
            Console.WriteLine("--1. Phương pháp chia đôi");
            Console.WriteLine("--2. Phương pháp dây cung");
            Console.WriteLine("--3. Phương pháp Newton");
            Console.WriteLine("--4. Phương pháp lặp đơn");
            Console.WriteLine("--5. Giải đa thức bậc n");
            Console.WriteLine("Giải đúng hệ phương trình đại số tuyến tính");
            Console.WriteLine("--6. Phương pháp Gauss");
            Console.WriteLine("--7. Phương pháp Gauss - Jordan");
            Console.WriteLine("--8. Phương pháp Cholesky");
            Console.WriteLine("--9. Phương pháp Cholesky (Số phức)");
            Console.WriteLine("Giải gần đúng hệ phương trình đại số tuyến tính");
            Console.WriteLine("--10. Phương pháp lặp đơn");
            Console.WriteLine("--11. Phương pháp Jacobi (theo hàng)");
            Console.WriteLine("--12. Phương pháp Jacobi (theo cột)");
            Console.WriteLine("--13. Phương pháp lặp Gauss - Seldel");
            Console.WriteLine("Tìm ma trận nghịch đảo");
            Console.WriteLine("--14. Phương pháp viền quanh");
            Console.WriteLine("--15. Phương pháp Gauss");
            Console.WriteLine("--16. Phương pháp Cholesky");
            Console.WriteLine("Tìm trị riêng, vecto riêng");
            Console.WriteLine("--17. Phương pháp Danielevski");
            Console.WriteLine("--18. Phương pháp lũy thừa");
            Console.WriteLine("--19. Phương pháp xuống thang");
            Console.WriteLine("--20. Coming soon...");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Chọn bài toán");
            Console.Write("Bài: ");
            int mode;
            mode = Convert.ToInt32(Console.ReadLine());
            switch(mode)
            {
                case 0:
                    Console.WriteLine("Goodbye!");
                    break;
                case 1:
                    Console.WriteLine("--1. Phương pháp chia đôi");
                    de.Chiadoi();
                    break;
                case 2:
                    Console.WriteLine("--2. Phương pháp dây cung");
                    de.Daycung();
                    break;
                case 3:
                    de.Newton();
                    break;
                case 4:
                    de.lapdon();
                    break;
                case 5:
                    pn.main();
                    break;
                case 6:
                    G.main();
                    break;
                case 7:
                    GJ.main();
                    break;
                case 8:
                    Cho1.main();
                    break;
                case 9:
                    Cho2.main();
                    break;
                case 10:
                    dse.lapdon();
                    break;
                case 11:
                    dse.JacobiRow();
                    break;
                case 12:
                    dse.JacobiCol();
                    break;
                case 13:
                    GS.main();
                    break;
                case 14:
                    IM.vienquanh();
                    break;
                case 15:
                    IM.Gauss();
                    break;
                case 16:
                    IM.Cholesky();
                    break;
                case 17:
                    eig.Danielevski();
                    break;
                case 18:
                    eig.luythua();
                    break;
                case 19:
                    eig.xuongthang();
                    break;
                case 20:
                    Console.WriteLine("Coming soon...");
                    break;
            }
            Console.ReadKey();
        }
    }
}
