using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CustomText

{
    [TestFixture]
    public class PermutationGeneratorTests
    {

        [Test]
        public void ReturnsPermutationGeneratorObject()
        {
            FormattedText alphabet = new FormattedText("abc");
            PermutationGenerator generator = new PermutationGenerator(alphabet);
            Assert.IsNotNull(generator);
        }

        [Test]
        public void ThrowsExceptionWhenPassingNullReferenceToConstructor()
        {
            FormattedText alphabet = null;
            Assert.Throws(typeof(NullReferenceException), delegate()
            {
                PermutationGenerator generator = new PermutationGenerator(alphabet);
            });
        }

        [Test]
        public void GenerateNext_GeneratesPasswordFromAlphabet()
        {
            FormattedText alphabet = new FormattedText("ab");
            PermutationGenerator generator = new PermutationGenerator(alphabet);
            FormattedText password = generator.GenerateNext();

            Assert.IsNotNull(password);
            Assert.AreEqual("ab", password.Text);
            password = generator.GenerateNext();
            Assert.AreEqual("ba", password.Text);
            password = generator.GenerateNext();
            Assert.AreEqual("ab", password.Text);
        }
    }
}
