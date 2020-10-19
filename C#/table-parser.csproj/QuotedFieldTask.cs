using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static string ReadQuotedField(string line, int startIndex)
        {
            StringBuilder token = new StringBuilder();
            string result = "";

            var i = startIndex + 1;

            char stop = line[startIndex];

            while (true)
            {
                if ((line[i] == stop || i == line.Length) && line[i - 1] != '\\')
                    break;

                if (line[i] != '\\')
                    token.Append(line[i]);
                i++;
                if (i == line.Length)
                    break;
            }
            result = token.ToString();

            return new Token(result, startIndex, result.Length);
        }
    }
}
