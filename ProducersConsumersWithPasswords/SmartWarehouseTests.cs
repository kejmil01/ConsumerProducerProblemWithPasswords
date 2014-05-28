using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace ProducersConsumersWithPasswords
{
    [TestFixture]
    public class SmartWarehouseTests
    {
        [Test]
        public void ReturnsSmartWarehouseObject()
        {
            SmartWarehouse<int> warehouse = new SmartWarehouse<int>(2);
            Assert.IsNotNull(warehouse);
        }

        [TestCase(0)]
        [TestCase(-2)]
        public void ThrowsExceptionWhenPassingLessOrEqualToZeroCapacityParameterToConstructor(int capacity)
        {
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                SmartWarehouse<int> warehouse = new SmartWarehouse<int>(capacity);
            });
        }
    }
}
