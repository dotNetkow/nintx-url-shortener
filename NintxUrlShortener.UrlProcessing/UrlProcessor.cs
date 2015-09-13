using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NintxUrlShortener.UrlProcessing
{
    /// <summary>
    /// Provides ability to encode and decode a URL using bijection and base 62 encoding.
    /// 
    /// Referenced some code/algorithm from: https://stackoverflow.com/questions/742013/how-to-code-a-url-shortener
    /// </summary>
    public class UrlProcessor
    {
        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly int Base = Alphabet.Length;  // 62 (aka total letters in Alphabet)

        /// <summary>
        /// Convert a base 10 id number into a base 62 encoded string.
        /// </summary>
        /// <param name="uniqueId">base 10 id number, such as auto incrementing id from database</param>
        /// <returns>base 62 encoded string</returns>
        public static string Encode(int uniqueId)
        {
            if (uniqueId < 0) throw new ArgumentException("uniqueId must be positive");

            if (uniqueId == 0) return Alphabet[0].ToString();

            var encodedString = string.Empty;

            while (uniqueId > 0)
            {
                // Look up character to use based on remainder
                encodedString += Alphabet[uniqueId % Base];
                // Keep adding characters until no more left
                uniqueId = uniqueId / Base;
            }

            return string.Join(string.Empty, encodedString.Reverse());
        }

        /// <summary>
        /// Given an url code, convert to its base 10 Id number representation.
        /// </summary>
        /// <param name="urlCode">string of chars, numbers in base 62 format</param>
        /// <returns>base 10 id</returns>
        public static int Decode(string urlCode)
        {
            if (string.IsNullOrEmpty(urlCode)) throw new ArgumentNullException("urlCode");

            var i = 0;

            foreach (var c in urlCode)
            {
                i = (i * Base) + Alphabet.IndexOf(c);
            }

            return i;
        }
    }
}
