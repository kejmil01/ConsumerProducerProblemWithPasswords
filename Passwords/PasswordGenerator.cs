using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords
{
    public class PasswordGenerator
    {
        private bool firstPermutation = true;
        private PasswordAlphabet alphabet;
        private char[] password;

        public PasswordGenerator(PasswordAlphabet alphabet)
        {
            if (alphabet == null)
                throw new NullReferenceException();

            this.alphabet = alphabet;
            this.password = alphabet.Text.ToArray();
        }

        public Password GenerateNext()
        {
            if (firstPermutation)
            {
                firstPermutation = false;
                return new Password(new string(password));
            }
            NextPermutation(password);
            return new Password(new string(password));
        }

        private bool NextPermutation(IList<char> password)
        {
            if (password.Count < 2) return false;
            var k = password.Count - 2;

            while (k >= 0 && password[k].CompareTo(password[k + 1]) >= 0) k--;
            if (k < 0)
            {
                this.password = alphabet.Text.ToCharArray();
                return false;
            }

            var l = password.Count - 1;
            while (l > k && password[l].CompareTo(password[k]) <= 0) l--;

            var tmp = password[k];
            password[k] = password[l];
            password[l] = tmp;

            var i = k + 1;
            var j = password.Count - 1;
            while (i < j)
            {
                tmp = password[i];
                password[i] = password[j];
                password[j] = tmp;
                i++;
                j--;
            }

            return true;
        }
    }
}
