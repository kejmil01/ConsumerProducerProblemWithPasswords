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
    public class PasswordProducerTests
    {
        [Test]
        public void ReturnsPasswordProducerObject()
        {
            PasswordAlphabet alphabet = new PasswordAlphabet("abc");
            PasswordProducer pp = new PasswordProducer(alphabet);
            Assert.IsNotNull(pp);

            PasswordGenerator generator = new PasswordGenerator(alphabet);
            pp = new PasswordProducer(generator);
            Assert.IsNotNull(pp);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceArgumentToConstructor()
        {
            PasswordAlphabet alphabet = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordProducer pp = new PasswordProducer(alphabet);
            });

            PasswordGenerator generator = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordProducer pp = new PasswordProducer(generator);
            });
        }

        [Test]
        public void StartProduction_ThrowsExceptionWhenPassingNullReferenceArgument()
        {
            SmartWarehouse<Password> warehoue = null;
            PasswordProducer pp = new PasswordProducer(new PasswordAlphabet("abc"));
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                pp.StartProduction(warehoue);
            });
        }

        [Test]
        public void StopProduction_SuccessfullyStopsProduction()
        {
            SmartWarehouse<Password> warehouse = new SmartWarehouse<Password>(20);
            PasswordProducer pp = new PasswordProducer(new PasswordAlphabet("abc"));
            Assert.DoesNotThrow(delegate()
            {
                pp.StartProduction(warehouse);
                pp.StopProduction();
            });
        }
    }
}
