using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CustomText
{
    [TestFixture]
    public class SyntaxValidatorTests
    {
        [TestCase("abc", true)]
        [TestCase("aabbcc", false)]
        public void ReturnsExpectedBooleanValueWhenValidatingPassword(string text, bool expectedResult)
        {
            bool result = SyntaxValidator.Validate(text);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
