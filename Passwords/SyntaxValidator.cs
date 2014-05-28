using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords
{
    /// <summary>
    /// A class that validates whether the each character in text occurs in text only once.
    /// </summary>
    public class SyntaxValidator
    {
        public static bool Validate(string text)
        {
            bool validated = true;
            Parallel.For(0, text.Length, (characterNumber, loopState) =>
                {
                    if (Occurence(text, text[characterNumber]) > 1)
                    {
                        validated = false;
                        loopState.Stop();
                        return;
                    }
                });

            return validated;
        }

        private static int Occurence(string text, char chr)
        {
            int i = 0, count = 0;
            while ((i = text.IndexOf(chr, i)) != -1)
            {
                count++;
                i++;
            }
            return count;
        }
    }
}
