using System;
using System.Collections.Generic;
using System.Linq;

namespace TriangleCalculations
{
    public class NameNumberConverter
    {
        /// <summary>
        /// Lookup dictionary of the order-letter combination
        /// </summary>
        private static readonly Dictionary<int, char> LookupDictionary = new Dictionary<int, char>()
        {
            { 1,'A'},
            { 2,'B'},
            { 3,'C'},
            { 4,'D'},
            { 5,'E'},
            { 6,'F'}
        };
        /// <summary>
        /// Looks up column order number given the letter
        /// </summary>
        /// <param name="columnLetter">Letter</param>
        /// <returns></returns>
        public static int ConvertLetterToNumber(char columnLetter)
        {
            if (!LookupDictionary.ContainsValue(columnLetter))
                throw new ArgumentOutOfRangeException(nameof(columnLetter), "Provided " + columnLetter + " is out of bounds.  Valid letters are A-F");
            return LookupDictionary.FirstOrDefault(l => l.Value == columnLetter).Key;
        }
        /// <summary>
        /// Looks up letter given the column order number
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns>Number</returns>
        public static char ConvertNumberToLetter(int columnNumber)
        {
            if (!LookupDictionary.ContainsKey(columnNumber))
                throw new ArgumentOutOfRangeException(nameof(columnNumber), "Provided " + columnNumber + " is out of bounds.  Valid numbers are 1-6");
            return LookupDictionary.FirstOrDefault(l => l.Key == columnNumber).Value;
        }

        /// <summary>
        /// Given string representation of triangle name returns letter in the name as a character and
        /// number in the name as int
        /// </summary>
        /// <param name="name">String representation for triangle notation</param>
        /// <returns>Tuple with Item1 for letter and Item2 for number in the name</returns>
        public static Tuple<char, int> ParseTriangleName(string name)
        {
            if (String.IsNullOrEmpty(name) || name.Length < 2)
                throw new ArgumentException("Provided string does not match required naming conventions. The string must contain a letter and a number, thus be at least 2 characters long.");
            int columnNumber;
            bool success = int.TryParse(name.Substring(1), out columnNumber);
            if (!success)
                throw new ArgumentException("Provided string does not match required naming conventions. The string must contain a letter and a number.");
            return new Tuple<char, int>(name[0], columnNumber);
        }
        /// <summary>
        /// Concatinates character and number and returns string representation of the
        /// triangle name
        /// </summary>
        /// <param name="letter">Letter</param>
        /// <param name="columnNumber">Number</param>
        /// <returns></returns>
        public static string ConvertToTriangleName(char letter, int columnNumber)
        {
            return letter.ToString() + columnNumber;
        }

    }
}
