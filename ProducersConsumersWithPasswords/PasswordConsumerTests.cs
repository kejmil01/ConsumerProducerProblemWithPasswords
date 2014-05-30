using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CustomText;

namespace ProducersConsumersWithPasswords
{
    [TestFixture]
    public class PasswordConsumerTests
    {
        [Test]
        public void ReturnsConsumerObject()
        {
            FormattedText password = new FormattedText("abc");
            PasswordConsumer consumer = new PasswordConsumer(password);
            Assert.IsNotNull(password);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceArgumentToConstructor()
        {
            FormattedText password = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordConsumer consumer = new PasswordConsumer(password);
            });
        }

        [Test]
        public void StartConsumption_ThrowsExceptionWhenPassingNullReferenceArgument()
        {
            SmartWarehouse<FormattedText> warehoue = null;
            PasswordConsumer consumer = new PasswordConsumer(new FormattedText("abc"));
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                consumer.StartConsumption(warehoue);
            });
        }

        [Test]
        public void StartConsumption_ThrowsExceptionWhenConsumptionAlreadyStarted()
        {
            SmartWarehouse<FormattedText> warehoue = new SmartWarehouse<FormattedText>(20);
            PasswordConsumer consumer = new PasswordConsumer(new FormattedText("abc"));
            consumer.StartConsumption(warehoue);
            Assert.Throws(typeof(InvalidOperationException), delegate()
            {
                consumer.StartConsumption(warehoue);
            });
        }
    }
}
