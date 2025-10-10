using System.Diagnostics.Tracing;
using System.Text;

namespace HW5
{
    internal class Program
    {
        static List<Motor> motors = new List<Motor>();
        static void Input()
        {
            int sl;
            while (true)
            {
                try
                {
                    Console.Write("Nhập số lượng xe: ");
                    string soluong = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(soluong))
                        throw new Exception("Lỗi: Số lượng xe không được để trống!");
                    if (!int.TryParse(soluong, out sl))
                        throw new Exception("Lỗi: Số lượng xe không được chứa chữ cái hoặc kí tự đặc biệt!");
                    if (sl <= 0)
                        throw new Exception("Lỗi: Số lượng xe phải > 0!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            for (int i = 0; i < sl; i++)
            {
                int chon;
                while (true)
                {
                    try
                    {
                        Console.Write("Nhập lựa chọn (1. Jupyter || 2. Serius): ");
                        string luachon = Console.ReadLine().Trim();
                        if (string.IsNullOrEmpty(luachon))
                            throw new Exception("Lỗi: Lựa chọn model xe không được để trống!");
                        if (!int.TryParse(luachon, out chon))
                            throw new Exception("Lỗi: Lựa chọn model không được chứa chữ cái hoặc kí tự đặc biệt!");
                        if (chon <= 0 || chon > 2)
                            throw new Exception("Lỗi: Lựa chọn model xe phải > 0!");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Motor motor;
                if (chon == 1)
                {
                    motor = new Jupiter();
                    motor.inputInfor();
                    motors.Add(motor);
                }
                else
                {
                    motor = new Serius();
                    motor.inputInfor();
                    motors.Add(motor);
                }
            }
        }

        static void display()
        {
            if(motors.Count == 0)
            {
                Console.WriteLine("Lỗi: Không có thông tin về xe!");
                return;
            }
            Console.WriteLine("Danh sách thông tin các xe của Công ty Yamaha Việt Nam: ");
            foreach(var mortor in motors)
            {
                mortor.displayInfor();
            }
        }

        static int GetWarranty(Motor m)
        {
            var prop = m.GetType().GetProperty("Warranty");
            if (prop != null)
                return Convert.ToInt32(prop.GetValue(m));
            return 0;
        }

        static void Sort()
        {
            Console.WriteLine("\nSắp xếp danh sách theo:");
            Console.WriteLine("1. Tên xe (A–Z)");
            Console.WriteLine("2. Dung tích (nhỏ -> lớn)");
            Console.WriteLine("3. Kiểu truyền lực (ít -> nhiều)");
            Console.WriteLine("4. Thời gian bảo hành");
            Console.WriteLine("5. Quay lại trang chính");
            Console.Write("Nhập lựa chọn: ");

            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    motors = motors.OrderBy(m => m.Name).ToList();
                    break;
                case "2":
                    motors = motors.OrderBy(m => m.Capacity).ToList();
                    break;
                case "3":
                    motors = motors.OrderBy(m => m.Num).ToList();
                    break;
                case "4":
                    motors = motors.OrderByDescending(GetWarranty).ToList();
                    //foreach (var m in motors)
                    //{
                    //    var prop = m.GetType().GetProperty("Warranty");
                    //    var w = prop != null ? prop.GetValue(m) : "Không có";
                    //    Console.WriteLine($"{m.Name,-20} | Bảo hành: {w}");
                    //}
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    break;
            }

            Console.WriteLine("\nDanh sách sau khi sắp xếp:");
            foreach (var m in motors)
            {
                m.displayInfor();
            }
        }

        static void Search()
        {
            
            string keyword;
            while (true)
            {
                try
                {
                    Console.Write("Nhập tiêu chí cần tìm (Tên hoặc mã của xe): ");
                    keyword = Console.ReadLine().Trim().ToLower();
                    if (string.IsNullOrEmpty(keyword))
                        throw new Exception("Lỗi: Tiêu chí không được để trống!");
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            

            var result = motors.Where(m => m.Name.ToLower().Contains(keyword) || m.Code.ToLower().Contains(keyword)).ToList();
            if(result.Count == 0)
            {
                Console.WriteLine("Không tìm thấy xe phù hợp!");
                return;
            }
            Console.WriteLine($"\nKết quả tìm kếm theo {keyword}: ");
            foreach(var motor in result)
            {
                motor.displayInfor();
            }
            return;
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            string keyword;
            do
            {
                Console.WriteLine("------ Hệ thống xe công ty Yamaha Việt Nam -----");
                Console.WriteLine("1. Nhập thông tin xe");
                Console.WriteLine("2. Hiển thị danh sách xe");
                Console.WriteLine("3. Sắp xếp");
                Console.WriteLine("4. Tìm kiếm theo tên");
                Console.WriteLine("5. Thoát");
                Console.Write("Nhập lựa chọn: ");
                while (true)
                {
                    try
                    {
                        keyword = Console.ReadLine().Trim().ToLower();
                        if (string.IsNullOrEmpty(keyword))
                            throw new Exception("Lỗi: Tiêu chí không được để trống!");
                        if (!keyword.All(char.IsDigit))
                            throw new Exception("Lỗi: Lựa chọn phải là kí tự số!");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                //Console.Clear();
                //Console.WriteLine("Nhấn phím bất kì để quay lại trang chính....");
                //Console.ReadKey();
                //Console.Clear();

                switch (keyword)
                {
                    case "1":
                        Input();
                        break;
                    case "2":
                        display();
                        break;
                    case "3":
                        Sort();
                        break;
                    case "4":
                        Search();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
                
            } while (keyword != "4");
            

        }
    }
}
