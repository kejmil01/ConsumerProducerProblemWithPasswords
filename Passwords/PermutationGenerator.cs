using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomText
{
    public class PermutationGenerator
    {
        private bool firstPermutation = true;
        private FormattedText alphabet;
        private char[] FormattedText;

        public PermutationGenerator(FormattedText alphabet)
        {
            if (alphabet == null)
                throw new NullReferenceException();

            this.alphabet = alphabet;
            this.FormattedText = alphabet.Text.ToArray();
        }

        public FormattedText GenerateNext()
        {
            if (firstPermutation)
            {
                firstPermutation = false;
                return new FormattedText(new string(FormattedText));
            }
            NextPermutation(FormattedText);
            return new FormattedText(new string(FormattedText));
        }

        private bool NextPermutation(IList<char> FormattedText)
        {
            if (FormattedText.Count < 2) return false;
            var k = FormattedText.Count - 2;

            while (k >= 0 && FormattedText[k].CompareTo(FormattedText[k + 1]) >= 0) k--;
            if (k < 0)
            {
                this.FormattedText = alphabet.Text.ToCharArray();
                return false;
            }

            var l = FormattedText.Count - 1;
            while (l > k && FormattedText[l].CompareTo(FormattedText[k]) <= 0) l--;

            var tmp = FormattedText[k];
            FormattedText[k] = FormattedText[l];
            FormattedText[l] = tmp;

            var i = k + 1;
            var j = FormattedText.Count - 1;
            while (i < j)
            {
                tmp = FormattedText[i];
                FormattedText[i] = FormattedText[j];
                FormattedText[j] = tmp;
                i++;
                j--;
            }

            return true;
        }
    }
}
