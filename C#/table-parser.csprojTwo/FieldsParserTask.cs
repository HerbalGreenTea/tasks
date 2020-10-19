using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase(@"""test \""""", new[] { @"test """ })]
        [TestCase(@"""test", new[] { "test" })]
        [TestCase(@"'test \''", new[] { "test '" })]
        [TestCase(@"""test""""test""", new[] { "test", "test" })]
        [TestCase(@"test""test""", new[] { "test", "test" })]
        [TestCase(@"""test""test", new[] { "test", "test" })]
        [TestCase(@"""te\'st""", new[] { "te'st" })]
        [TestCase(@"'test\""'", new[] { @"test""" })]
        [TestCase(@"test  test", new[] { "test", "test" })]
        [TestCase(@"""test\\""", new[] { @"test\" })]
        [TestCase(@"", new string[0])]
        [TestCase(@"''", new[] { "" })]
        [TestCase(@" ""test""", new[] { "test" })]
        [TestCase(@"""test ", new[] { "test " })]
        [TestCase(@"'a\'\\'", new[] { @"a'\" })]
        [TestCase("a ", new[] { "a" })]
        [TestCase("ab'a'", new[] { "ab", "a" })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            List<Token> tokens = new List<Token>();
            var index = 0;

            while (index != line.Length)
            {
                if (SkipSpace(index, line) == -1)
                    return tokens;
                else
                    index = AddToken(line, SkipSpace(index, line), tokens);
            }
            return tokens;
        }

        public static int SkipSpace(int index, string line)
        {
            while (line[index] == ' ')
            {
                if (index + 1 == line.Length)
                    return -1;
                index++;
            }
            return index;
        }

        public static int AddToken(string line, int index, List<Token> tokens)
        {
            Token temp;

            if (line[index] == '\'' || line[index] == '\"')
                temp = ReadQuotedField(line, index);
            else
                temp = ReadField(line, index);

            tokens.Add(temp);

            return temp.GetIndexNextToToken();
        }

        private static Token ReadField(string line, int startIndex)
        {
            StringBuilder token = new StringBuilder();

            var i = startIndex;
            var tempLength = 0;

            while (line[i] != ' ' && line[i] != '\"' && line[i] != '\'')
            {
                tempLength++;
                token.Append(line[i]);
                i++;
                if (i == line.Length)
                    break;
            }

            var result = token.ToString();
            return new Token(result, startIndex, tempLength);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            // этот метод был в файле
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}