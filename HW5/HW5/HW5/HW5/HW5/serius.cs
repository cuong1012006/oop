using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5
{
    internal class Serius:Motor
    {
        private int warranty; // thời gian bảo hành
        public Serius() : base() { }
        public Serius(string code, string name, double capacity, int num, int warranty) : base(code, name, capacity, num)
        {
            this.warranty = warranty;
        }
        public int Warranty
        {
            get { return warranty; }
            set
            {
                warranty = value;
            }
        }
        public override void inputInfor()
        {
            base.inputInfor();
            while (true)
            {
                try
                {
                    Console.Write("Nhập thời gian bảo hành: ");
                    string thoiGian = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(thoiGian))
                        throw new Exception("Lỗi: Thời gian bảo hành được để trống!");
                    if (!int.TryParse(thoiGian, out warranty))
                        throw new Exception("Lỗi: Thời gian bảo hành kí tự đặc biệt hoặc chữ cái!");
                    if (warranty <= 0)
                        throw new Exception("Lỗi: Thời gian bảo hành > 0!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public override void displayInfor()
        {
            base.displayInfor();
            Console.WriteLine($"Thời gian bảo hành: {warranty} tháng!");
        }
    }
}
