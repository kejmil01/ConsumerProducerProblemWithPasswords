using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords
{
    public class Password
    {
        string text;

        public string Text
        {
            get { return text; }
        }

        public Password(string text)
        {
            if (text == null)
                throw new NullReferenceException();
            if (!SyntaxValidator.Validate(text))
                throw new ArgumentException("Each character in password can occure only once.");
            this.text = text;
        }

        public int CompareTo(Password password)
        {
            return this.Text.CompareTo(password.Text);
        }
    }
}
