using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomText
{
    public class FormattedText : IComparable<FormattedText>
    {
        string text;

        public string Text
        {
            get { return text; }
        }

        public FormattedText(string text)
        {
            if (text == null)
                throw new NullReferenceException();
            if (!SyntaxValidator.Validate(text))
                throw new ArgumentException("Each character in text can occure only once.");
            this.text = text;
        }

        public int CompareTo(FormattedText fText)
        {
            return this.Text.CompareTo(fText.Text);
        }
    }
}
