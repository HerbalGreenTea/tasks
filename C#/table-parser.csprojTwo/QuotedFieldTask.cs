using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase(@"""Q", 0, @"Q", 2)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            StringBuilder token = new StringBuilder();
            var i = startIndex + 1;
            var lenghtToken = 1;

            while (true)
            {
                lenghtToken++;
                if ((line[i] == line[startIndex] && line[i - 1] != '\\') || (line[i] == line[startIndex] && line[i-1] == '\\' && line[i - 2] == '\\'))
                    break;

                if (line[i] != '\\' || (line[i] == '\\' && line[i - 1] == '\\'))
                    token.Append(line[i]);

                i++;
                if (i == line.Length)
                    break;
            }
            return new Token(token.ToString(), startIndex, lenghtToken);
        }
    }
}





