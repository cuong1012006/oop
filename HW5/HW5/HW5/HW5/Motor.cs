using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5
{
    public class Motor : IMotor
    {
        private string code;// mã xe
        private string name; // tên xe
        private double capacity; // dung tích xi lanh
        private int num; // kiểu truyền lực mấy số

        public Motor()
        {

        }
        public Motor(string code, string name, double capacity, int num)
        {
            this.code = code;
            this.name = name;
            this.capacity = capacity;
            this.num = num;
        }

        public string Code {
            get { return code; }
            set
            {
                code = value;
            }
        }
        public string Name {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public double Capacity
        {
            get { return capacity; }
            set
            {
                capacity = value;
            }
        }
        public int Num
        {
            get { return num; }
            set
            {
                num = value;
            }
        }

        public virtual void inputInfor()
        {
            while (true)
            {
                try
                {
                    Console.Write("Nhập mã xe: ");
                    code = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(code))
                        throw new Exception("Lỗi: Mã xe không được để trống!");
                    break;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Nhập tên xe: ");
                    name = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(name))
                        throw new Exception("Lỗi: Tên xe không được để trống!");
                    break;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Nhập dung tích xi lanh: ");
                    string dungTich = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(dungTich))
                        throw new Exception("Lỗi: Dung tích xi lanh không được để trống!");
                    if (!double.TryParse(dungTich, out capacity))
                        throw new Exception("Lỗi: Dung tích xi lanh không được chứa kí tự đặc biệt hoặc chữ cái!");
                    if (capacity <= 0)
                        throw new Exception("Lỗi: Dung tích xi lanh > 0!");
                    break;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Nhập kiểu truyền lực: ");
                    string kieuTruyen = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(kieuTruyen))
                        throw new Exception("Lỗi: Kiểu truyền lực không được để trống!");
                    if (!int.TryParse(kieuTruyen, out num))
                        throw new Exception("Lỗi: Kiểu truyền lực không được chứa kí tự đặc biệt hoặc chữ cái!");
                    if (num <= 0)
                        throw new Exception("Lỗi: Kiểu truyền lực > 0!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public virtual void displayInfor()
        {
            Console.WriteLine($"\nThông tin xe {name}");
            Console.WriteLine($"Mã xe: {code}");
            Console.WriteLine($"Dung tích xi lanh: {capacity}");
            Console.WriteLine($"Kiểu truyền lực: {num}");
        }

        public void changeInfor()
        {
            string maXe;
            while (true)
            {
                try
                {
                    Console.Write("Nhập mã xe cần đổi thông tin: ");
                    maXe = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(maXe))
                        throw new Exception("Lỗi: Mã xe không được để trống!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Kiểm tra mã xe có trùng mã hiện tại hay không
            if (!maXe.Equals(code, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Không tìm thấy mã xe trong danh sách!");
                return;
            }

            Console.WriteLine("\nDanh sách các thông tin có thể thay đổi: ");
            Console.WriteLine("1. Tên xe");
            Console.WriteLine("2. Dung tích xi lanh");
            Console.WriteLine("3. Kiểu truyền lực");
            Console.WriteLine("0. Thoát");

            int choice;
            while (true)
            {
                try
                {
                    Console.Write("Nhập lựa chọn bạn muốn thay đổi: ");
                    string chon = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(chon))
                        throw new Exception("Lỗi: Lựa chọn không được để trống!");
                    if (!int.TryParse(chon, out choice))
                        throw new Exception("Lỗi: Lựa chọn phải là số nguyên!");
                    if (choice < 0 || choice > 3)
                        throw new Exception("Lỗi: Lựa chọn phải (0-3)!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            switch (choice)
            {
                case 1:
                    while (true)
                    {
                        try
                        {
                            Console.Write("Nhập tên xe mới: ");
                            string newName = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(newName))
                                throw new Exception("Lỗi: Tên xe không được để trống!");
                            Name = newName;
                            Console.WriteLine("✅ Thay đổi tên xe thành công!");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;

                case 2:
                    while (true)
                    {
                        try
                        {
                            Console.Write("Nhập dung tích xi lanh mới: ");
                            string newCapacityStr = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(newCapacityStr))
                                throw new Exception("Lỗi: Dung tích không được để trống!");
                            if (!double.TryParse(newCapacityStr, out double newCapacity))
                                throw new Exception("Lỗi: Dung tích phải là số!");
                            if (newCapacity <= 0)
                                throw new Exception("Lỗi: Dung tích phải > 0!");
                            Capacity = newCapacity;
                            Console.WriteLine("✅ Thay đổi dung tích thành công!");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;

                case 3:
                    while (true)
                    {
                        try
                        {
                            Console.Write("Nhập kiểu truyền lực mới: ");
                            string newNumStr = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(newNumStr))
                                throw new Exception("Lỗi: Kiểu truyền lực không được để trống!");
                            if (!int.TryParse(newNumStr, out int newNum))
                                throw new Exception("Lỗi: Kiểu truyền lực phải là số!");
                            if (newNum <= 0)
                                throw new Exception("Lỗi: Kiểu truyền lực > 0!");
                            Num = newNum;
                            Console.WriteLine("✅ Thay đổi kiểu truyền lực thành công!");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    break;

                case 0:
                    Console.WriteLine("👋 Đã thoát thay đổi thông tin.");
                    return;
            }

            Console.WriteLine("\n🎯 Thông tin xe sau khi cập nhật:");
            displayInfor();
        }


    }
}
