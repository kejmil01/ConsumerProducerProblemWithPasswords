using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CustomText;

namespace ProducersConsumersWithPasswords
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordProducer pp = new PasswordProducer(new FormattedText("abc"));
            PasswordConsumer pc = new PasswordConsumer(new FormattedText("cba"));

            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            distributor.AddProducer(pp);
            distributor.AddConsumer(pc);
            distributor.StartConsumption();

            PasswordConsumer consumer = new PasswordConsumer(new FormattedText("bca"));
            distributor.AddConsumer(consumer);

            distributor.StartProduction();

            PasswordProducer producer = new PasswordProducer(new FormattedText("abc"));
            distributor.AddProducer(producer);

            Console.ReadLine();
            Console.WriteLine(warehouse.Count);
        }
    }
}