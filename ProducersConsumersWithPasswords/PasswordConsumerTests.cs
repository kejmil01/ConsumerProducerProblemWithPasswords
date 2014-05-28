using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Passwords;

namespace ProducersConsumersWithPasswords
{
    [TestFixture]
    public class PasswordConsumerTests
    {
        [Test]
        public void ReturnsConsumerObject()
        {
            Password password = new Password("abc");
            PasswordConsumer consumer = new PasswordConsumer(password);
            Assert.IsNotNull(password);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceArgumentToConstructor()
        {
            Password password = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordConsumer consumer = new PasswordConsumer(password);
            });
        }

        [Test]
        public void StartConsumption_ThrowsExceptionWhenPassingNullReferenceArgument()
        {
            SmartWarehouse<Password> warehoue = null;
            PasswordConsumer consumer = new PasswordConsumer(new Password("abc"));
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                consumer.StartConsumption(warehoue);
            });
        }

        [Test]
        public void StartConsumption_ThrowsExceptionWhenConsumptionAlreadyStarted()
        {
            SmartWarehouse<Password> warehoue = new SmartWarehouse<Password>(20);
            PasswordConsumer consumer = new PasswordConsumer(new Password("abc"));
            consumer.StartConsumption(warehoue);
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                consumer.StartConsumption(warehoue);
            });
        }
    }
}
