using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CustomText
{
    [TestFixture]
    public class FormattedTextTests
    {
        [Test]
        public void ReturnsFormattedTextObject()
        {
            FormattedText p = new FormattedText("abc");
            Assert.IsNotNull(p);
        }

        [Test]
        public void ThrowsExceptionWhenPassingStringWithCharactersThatOccursMoreThanOneTimeToConstructor()
        {
            Assert.Throws(typeof(ArgumentException), delegate()
            {
                FormattedText p = new FormattedText("aa");
            });
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            string testString = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                FormattedText p = new FormattedText(testString);
            });
        }
    }
}
