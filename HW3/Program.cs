using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Clerk: {Name}";
    }
}

public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }

    public Item(string name, double price, double discount = 0.0)
    {
        Name = name;
        Price = price;
        Discount = discount;
    }

    public override string ToString()
    {
        return $"{Name} - Price: {Price:C2}, Discount: {Discount:C2}";
    }
}

public class BillLine
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public BillLine(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public double LineTotal(bool preferred)
    {
        double unitPrice = Item.Price;
        if (preferred) unitPrice -= Item.Discount;
        return unitPrice * Quantity;
    }

    public double LineDiscount()
    {
        return Item.Discount * Quantity;
    }

    public override string ToString()
    {
        return $"{Item.Name} x{Quantity} | Unit: {Item.Price:C2}, Discount: {Item.Discount:C2}";
    }
}

public class GroceryBill
{
    protected Employee Clerk { get; set; }
    protected List<BillLine> Items { get; set; }

    public GroceryBill(Employee clerk)
    {
        Clerk = clerk;
        Items = new List<BillLine>();
    }

    public void Add(BillLine line)
    {
        Items.Add(line);
    }

    public virtual double Total
    {
        get
        {
            double total = 0.0;
            foreach (var line in Items)
            {
                total += line.LineTotal(false); // không tính giảm giá
            }
            return total;
        }
    }

    public virtual void PrintReceipt()
    {
        Console.WriteLine("===== RECEIPT =====");
        Console.WriteLine(Clerk);
        foreach (var line in Items)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine($"Total (no discount): {Total:C2}");
    }
}

public class DiscountBill : GroceryBill
{
    public bool Preferred { get; set; }

    public DiscountBill(Employee clerk, bool preferred) : base(clerk)
    {
        Preferred = preferred;
    }

    public override double Total
    {
        get
        {
            double total = 0.0;
            foreach (var line in Items)
            {
                total += line.LineTotal(Preferred);
            }
            return total;
        }
    }

    public int DiscountCount
    {
        get
        {
            if (!Preferred) return 0;
            int count = 0;
            foreach (var line in Items)
            {
                if (line.Item.Discount > 0)
                    count += line.Quantity;
            }
            return count;
        }
    }

    public double DiscountAmount
    {
        get
        {
            if (!Preferred) return 0.0;
            double totalDiscount = 0.0;
            foreach (var line in Items)
            {
                totalDiscount += line.LineDiscount();
            }
            return totalDiscount;
        }
    }

    public double DiscountPercent
    {
        get
        {
            if (!Preferred) return 0.0;
            double original = base.Total;
            return (original == 0) ? 0.0 : (DiscountAmount / original) * 100;
        }
    }

    public override void PrintReceipt()
    {
        Console.WriteLine("===== DISCOUNT RECEIPT =====");
        Console.WriteLine(Clerk);
        foreach (var line in Items)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine($"Total before discount: {base.Total:C2}");
        Console.WriteLine($"Discount amount: {DiscountAmount:C2}");
        Console.WriteLine($"Discount count: {DiscountCount} items");
        Console.WriteLine($"Discount percent: {DiscountPercent:F2}%");
        Console.WriteLine($"Total after discount: {Total:C2}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Employee clerk = new Employee("Alice");

        // Tạo các item
        Item candy = new Item("Candy", 1.35, 0.25);
        Item milk = new Item("Milk", 2.50, 0.50);
        Item bread = new Item("Bread", 1.80, 0.0);

        // Hóa đơn thường
        GroceryBill bill = new GroceryBill(clerk);
        bill.Add(new BillLine(candy, 2));
        bill.Add(new BillLine(milk, 1));
        bill.Add(new BillLine(bread, 3));
        bill.PrintReceipt();

        Console.WriteLine();

        // Hóa đơn khách hàng ưu tiên
        DiscountBill dbill = new DiscountBill(clerk, true);
        dbill.Add(new BillLine(candy, 2));
        dbill.Add(new BillLine(milk, 1));
        dbill.Add(new BillLine(bread, 3));
        dbill.PrintReceipt();
    }
}
