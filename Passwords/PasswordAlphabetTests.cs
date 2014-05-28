using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Passwords
{
    [TestFixture]
    public class PasswordAlphabetTests
    {
        [Test]
        public void ReturnsPasswordAlphabetObject()
        {
            PasswordAlphabet a = new PasswordAlphabet("abc");
            Assert.IsNotNull(a);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            string testText = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PasswordAlphabet a = new PasswordAlphabet(testText);
            });
        }

        [Test]
        public void ThrowsExceptionWhenPassingTextWithCharactersThatOccursMoreThanOnceToConstructor()
        {
            string testText = "aabbcc";
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                PasswordAlphabet a = new PasswordAlphabet(testText);
            });
        }
    }
}
