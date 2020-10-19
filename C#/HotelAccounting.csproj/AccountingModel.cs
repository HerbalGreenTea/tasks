using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get { return price;  }
            set
            {
                if (value >= 0)
                {
                    price = value;
                    Notify(nameof(Price));
                    CalculateTotal(Price, NightsCount, Discount);
                }
                else throw new ArgumentException();
            }
        }

        public int NightsCount
        {
            get { return nightsCount;  }
            set
            {
                if (value > 0)
                {
                    nightsCount = value;
                    Notify(nameof(NightsCount));
                    CalculateTotal(Price, NightsCount, Discount);
                }
                else throw new ArgumentException();
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                Notify(nameof(Discount));
                CalculateTotal(Price, NightsCount, Discount);
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                if (value >= 0)
                {
                    total = value;
                    Notify(nameof(Total));
                    if (value != Price * NightsCount * (1 - Discount / 100))
                    {
                        discount = ((Price * NightsCount - Total) * 100) / (Price * NightsCount);
                        Notify(nameof(Discount));
                    }
                }
                else throw new ArgumentException();
            }
        }

        public void CalculateTotal (double price, double nightsCount, double discount)
        {
            Total = price * nightsCount * (1 - discount / 100);
        }
    }
}
