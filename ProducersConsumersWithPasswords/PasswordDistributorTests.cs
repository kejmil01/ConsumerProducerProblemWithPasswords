using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using CustomText;

namespace ProducersConsumersWithPasswords
{
    [TestFixture]
    public class PasswordDistributorTests
    {
        [Test]
        public void ReturnsPasswordDistributorObject()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            Assert.IsNotNull(distributor);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            SmartWarehouse<FormattedText> warehouse = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordDistributor distributor = new PasswordDistributor(warehouse);
            });
        }

        [Test]
        public void AddProducer_ThrowsExceptionWhenPassingNullReference()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                distributor.AddProducer(null);
            });
        }

        [Test]
        public void AddConsumer_ThrowsExceptionWhenPassingNullReference()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                distributor.AddConsumer(null);
            });
        }

        [Test]
        public void AddProducer_ThrowsExceptionWhenPassingSameProducerMoreThanOnce()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            PasswordProducer producer = new PasswordProducer(new FormattedText("abc"));
            distributor.AddProducer(producer);
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                distributor.AddProducer(producer);
            });
        }

        [Test]
        public void AddConsumer_ThrowsExceptionWhenPassingSameConsumerMoreThanOnce()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            PasswordConsumer consumer = new PasswordConsumer(new FormattedText("cba"));
            distributor.AddConsumer(consumer);
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                distributor.AddConsumer(consumer);
            });
        }

        [Test]
        public void StartProduction_ThrowsExceptionWhenTryingToCallMethodWhenProductionIsNotStopped()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            PasswordProducer producer = new PasswordProducer(new FormattedText("abc"));
            producer.PasswordAdditionMaximumTime = 20;
            distributor.AddProducer(producer);

            distributor.StartProduction();
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                distributor.StartProduction();
            });
            producer.StopProduction();
        }

        [Test]
        public void StartConsumption_ThrowsExceptionWhenTryingToCallMethodWhenConsumptionIsNotStopped()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordDistributor distributor = new PasswordDistributor(warehouse);
            PasswordConsumer consumer = new PasswordConsumer(new FormattedText("cba"));
            consumer.PasswordTakingMaximumTime = 20;
            distributor.AddConsumer(consumer);

            distributor.StartConsumption();
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                distributor.StartConsumption();
            });
            Thread.Sleep(2);
            consumer.StopConsumption();
        }
    }
}
