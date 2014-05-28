using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Passwords;

namespace ProducersConsumersWithPasswords
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartWarehouse<Password> warehouse = new SmartWarehouse<Password>(20);
            PasswordProducer pp = new PasswordProducer(new PasswordAlphabet("abc"));
            PasswordConsumer pc = new PasswordConsumer(new Password("cba"));

            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            distributor.AddProducer(pp);
            distributor.AddConsumer(pc);
            distributor.StartConsumption();

            PasswordConsumer consumer = new PasswordConsumer(new Password("bca"));
            distributor.AddConsumer(consumer);

            distributor.StartProduction();

            PasswordProducer producer = new PasswordProducer(new PasswordAlphabet("abc"));
            distributor.AddProducer(producer);

            Console.ReadLine();
            Console.WriteLine(warehouse.Count);
        }
    }
}