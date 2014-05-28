using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Passwords

{
    [TestFixture]
    public class PasswordGeneratorTests
    {

        [Test]
        public void ReturnsPasswordGeneratorObject()
        {
            PasswordAlphabet alphabet = new PasswordAlphabet("abc");
            PasswordGenerator generator = new PasswordGenerator(alphabet);
            Assert.IsNotNull(generator);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            PasswordAlphabet alphabet = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordGenerator generator = new PasswordGenerator(alphabet);
            });
        }

        [Test]
        public void GenerateNext_GeneratesPasswordFromAlphabet()
        {
            PasswordAlphabet alphabet = new PasswordAlphabet("ab");
            PasswordGenerator generator = new PasswordGenerator(alphabet);
            Password password = generator.GenerateNext();

            Assert.IsNotNull(password);
            Assert.AreEqual("ab", password.Text);
            password = generator.GenerateNext();
            Assert.AreEqual("ba", password.Text);
            password = generator.GenerateNext();
            Assert.AreEqual("ab", password.Text);
        }
    }
}
