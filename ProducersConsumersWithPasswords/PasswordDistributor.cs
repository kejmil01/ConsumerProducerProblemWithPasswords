using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Passwords;

namespace ProducersConsumersWithPasswords
{
    public class PasswordDistributor
    {
        private List<PasswordProducer> producers = new List<PasswordProducer>();
        private List<PasswordConsumer> consumers = new List<PasswordConsumer>();
        private SmartWarehouse<Password> warehouse;

        public bool ProductionStarted
        {
            get;
            private set;
        }

        public bool ConsumptionStarted
        {
            get;
            private set;
        }

        private object synchronizationObject = new object();
        private int passwordFoundByConsumerCounter = 0;

        public PasswordDistributor(SmartWarehouse<Password> warehouse)
        {
            if (warehouse == null)
                throw new NullReferenceException();
            this.warehouse = warehouse;
        }

        public void AddProducer(PasswordProducer producer)
        {
            if (producer == null)
                throw new NullReferenceException();
            if(producers.Contains(producer))
                    throw new ArgumentException("Producer is already on list.");

            producers.Add(producer);

            lock (synchronizationObject)
            {
                if (ProductionStarted)
                    producer.StartProduction(warehouse);
            }
        }

        public void AddConsumer(PasswordConsumer consumer)
        {
            if (consumer == null)
                throw new NullReferenceException();
            if(consumers.Contains(consumer))
                throw new ArgumentException("Consumer is already on list.");

            consumers.Add(consumer);
            consumer.OnDesiredPasswordFound += (Action)OnPasswordFoundByConsumer;

            lock (synchronizationObject)
            {
                if (ConsumptionStarted)
                    consumer.StartConsumption(warehouse);
            }
        }

        private void OnPasswordFoundByConsumer()
        {
            lock (synchronizationObject)
            {
                passwordFoundByConsumerCounter++;
                if (passwordFoundByConsumerCounter >= consumers.Count)
                {
                    passwordFoundByConsumerCounter = 0;
                    foreach (PasswordProducer p in producers)
                    {
                        PasswordProducer producer = p;
                        Task.Run(() =>
                            {
                                producer.StopProduction();
                            });
                    }
                    ProductionStarted = false;
                    ConsumptionStarted = false;
                }
            }
        }

        public void StartProduction()
        {
            foreach (PasswordProducer p in producers)
                p.StartProduction(warehouse);
            ProductionStarted = true;
        }

        public void StartConsumption()
        {
            foreach (PasswordConsumer c in consumers)
                c.StartConsumption(warehouse);
            ConsumptionStarted = true;
        }
    }
}
