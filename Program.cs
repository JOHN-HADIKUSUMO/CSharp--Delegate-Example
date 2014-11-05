using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    public enum CreditCard
    {
        Visa = 0,
        Master = 1,
        AMEX = 2
    }
    public class Payment
    {
        private int weight;
        private int distance;
        private CreditCard cctype;
        private int numberofbooks;
        private double priceeach;

        delegate double delegateCalculateShipmentFee(int weight,int distance);
        delegate double delegateCalculateCreditCardFee(CreditCard cctype);
        delegate double delegateCalculateBooks(int number, double price);
        public double Calculate()
        {
            double totalcharge;

            delegateCalculateShipmentFee shipmentfee = delegate(int weight, int distance) {
                return weight * 1.39 * distance;
            };

            delegateCalculateCreditCardFee ccfee = delegate(CreditCard cctype) {
                double commission;
                switch(cctype)
                {
                    case CreditCard.Visa:
                        commission = 0.02;
                        break;
                    case CreditCard.Master:
                        commission = 0.025;
                        break;
                    default:
                        commission = 0.0325;
                        break;
                }
                return commission;
            };

            delegateCalculateBooks bookscost = delegate(int number, double price){
                return number * price;
            };

            totalcharge = shipmentfee.Invoke(this.weight, this.distance);
            totalcharge += bookscost.Invoke(this.numberofbooks, this.priceeach);
            totalcharge += totalcharge + (totalcharge * ccfee.Invoke(this.cctype));
            

            return totalcharge;
        }

        public Payment(int weight,int distance,CreditCard cctype,int numberofbooks,double priceeach)
        {
            this.weight = weight;
            this.distance = distance;
            this.cctype = cctype;
            this.numberofbooks = numberofbooks;
            this.priceeach = priceeach;
        }

    }

    class Program
    {

        
        static void Main(string[] args)
        {
            Payment payment = new Payment(2,21,CreditCard.Master,6,17.25);
            Console.WriteLine("Total Charge :{0}", payment.Calculate());
            Console.ReadLine();

        }
    }
}
