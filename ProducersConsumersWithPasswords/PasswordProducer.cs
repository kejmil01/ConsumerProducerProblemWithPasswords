using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Passwords;

namespace ProducersConsumersWithPasswords
{
    public class PasswordProducer
    {
        internal class Worker
        {
            public SmartWarehouse<Password> warehouse;
            public Thread thread;
        }

        [ThreadStatic]
        private PasswordGenerator generator;

        private List<Worker> workers = new List<Worker>();
        private object workersLocker = new object();
        private volatile bool cancelationFlag = false;

        public Int32 PasswordAdditionMaximumTime
        {
            get;
            set;
        }

        public PasswordProducer(PasswordAlphabet alphabet)
            : this(new PasswordGenerator(alphabet))
        { }

        public PasswordProducer(PasswordGenerator generator)
        {
            if (generator == null)
                throw new NullReferenceException();
            this.generator = generator;

            PasswordAdditionMaximumTime = 1000;
        }

        public void StartProduction(SmartWarehouse<Password> warehouse)
        {
            if (warehouse == null)
                throw new NullReferenceException();
            lock (workersLocker)
            {
                if (warehouseAlreadyOnList(warehouse))
                    throw new InvalidOperationException("Production is already in progress.");

                cancelationFlag = false;
                AddNewWorker(warehouse);
            }
        }

        private bool warehouseAlreadyOnList(SmartWarehouse<Password> warehouse)
        {
            bool onList = false;
            foreach (Worker w in workers)
            {
                onList = ReferenceEquals(warehouse, w.warehouse);
                if (onList)
                    break;
            }
            return onList;
        }

        private void AddNewWorker(SmartWarehouse<Password> pWarehouse)
        {
            Worker newWorker = new Worker
            {
                warehouse = pWarehouse,
                thread = new Thread(Production)
            };
            workers.Add(newWorker);
            newWorker.thread.Start(workers.Count - 1);
        }

        private void Production(object id)
        {
            int workerID = (int)id;
            while (!cancelationFlag)
            {
                Password password = generator.GenerateNext();
                Console.WriteLine("Producer is trying to add the Password.");
                bool success = workers[workerID].warehouse.TryAdd(password, PasswordAdditionMaximumTime);

                if (success)
                    Console.WriteLine("Producer has added the Password.");
                else
                    Console.WriteLine("Producer was not able to add the Password due to maximum waiting time.");
            }
        }

        public void StopProduction()
        {
            cancelationFlag = true;
            lock (workersLocker)
            {
                for (int i = workers.Count - 1; i >= 0; i--)
                {
                    workers[i].thread.Join();
                    workers.RemoveAt(i);
                }
            }
        }
    }
}
