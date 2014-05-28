using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords
{

    public class PasswordAlphabet
    {
        private string text;

        public string Text
        {
            get { return text; }
        }

        public PasswordAlphabet(string text)
        {
            if (text == null)
                throw new NullReferenceException();

            if (!SyntaxValidator.Validate(text))
                throw new ArgumentException("Each character in alphabet can occure only once.");

            this.text = text;
        }
    }
}
