using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Passwords
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void ReturnsPasswordObject()
        {
            Password p = new Password("abc");
            Assert.IsNotNull(p);
        }

        [Test]
        public void ThrowsExceptionWhenPassingStringWithCharactersThatOccursMoreThanOneTimeToConstructor()
        {
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                Password p = new Password("aa");
            });
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            string testString = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                Password p = new Password(testString);
            });
        }
    }
}
