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
    public class PasswordProducerTests
    {
        [Test]
        public void ReturnsPasswordProducerObject()
        {
            FormattedText alphabet = new FormattedText("abc");
            PasswordProducer pp = new PasswordProducer(alphabet);
            Assert.IsNotNull(pp);

            PermutationGenerator generator = new PermutationGenerator(alphabet);
            pp = new PasswordProducer(generator);
            Assert.IsNotNull(pp);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceArgumentToConstructor()
        {
            FormattedText alphabet = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordProducer pp = new PasswordProducer(alphabet);
            });

            PermutationGenerator generator = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordProducer pp = new PasswordProducer(generator);
            });
        }

        [Test]
        public void StartProduction_ThrowsExceptionWhenPassingNullReferenceArgument()
        {
            SmartWarehouse<FormattedText> warehoue = null;
            PasswordProducer pp = new PasswordProducer(new FormattedText("abc"));
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                pp.StartProduction(warehoue);
            });
        }

        [Test]
        public void StopProduction_SuccessfullyStopsProduction()
        {
            SmartWarehouse<FormattedText> warehouse = new SmartWarehouse<FormattedText>(20);
            PasswordProducer pp = new PasswordProducer(new FormattedText("abc"));
            Assert.DoesNotThrow(delegate()
            {
                pp.StartProduction(warehouse);
                pp.StopProduction();
            });
        }
    }
}
