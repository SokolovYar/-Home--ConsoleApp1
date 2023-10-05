using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Money
    {
        protected int _USDWhole;
        protected int _cents;
        public Money(int uSDWhole, int cents)
        {
            USDWhole = uSDWhole;
            Cents = cents;
        }
        public Money() {}
    
        public void Print ()
        {
            Console.WriteLine($"${_USDWhole},{_cents}");
        }

        public int USDWhole { get { return _USDWhole; } set { _USDWhole = value; } }
        public int Cents { get { return _cents; }
            set {
                if (value < 0 || value > 100 ) throw new Exception("Wrong input");
                _cents = value;
                 
            } 
        }
        public Money ToMoney()
        {
            return new Money(this._USDWhole, this._cents);
        }
        public override string ToString ()
        {
            return $"${_USDWhole},{_cents}";
        }
        public static bool operator < (Money M1, Money M2)
        {
            return (M1._USDWhole * 100 + M1._cents) < (M2._USDWhole * 100 + M2._cents) ? true : false;

        }
        public static bool operator > (Money M1, Money M2)
        {
            return (M1._USDWhole * 100 + M1._cents) > (M2._USDWhole * 100 + M2._cents) ? true : false;
        }
    }
    internal class Product : Money 
    {
        public Product(int uSDWhole, int cents) : base(uSDWhole, cents) {}
        public void DecreasePrice(Money money)
        {
            //проверка, если попытаться снизить цену ниже, чем у продукта выбросит ошибку
            if (money > this.ToMoney()) throw new Exception("Price can`t be lower than $0.00");
            if (money.Cents > this.Cents)
            {
                this._cents = this._cents + 100 - money.Cents;
                this._USDWhole = this._USDWhole - money.USDWhole - 1;
                return;
            }
            this._cents -= money.Cents;
            this._USDWhole -= money.USDWhole;
        }
        public override string ToString()
        {
            return $"Product price ${_USDWhole},{_cents}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Money MyMoney = new Money(100,50);
            Product MyProduct = new Product(1000, 0);

            Console.WriteLine("Current product price:");
            Console.WriteLine(MyProduct);
            MyProduct.DecreasePrice(MyMoney);
            Console.WriteLine("Product price after decreases at " + MyMoney);
            Console.WriteLine(MyProduct);
        }
    }
}
