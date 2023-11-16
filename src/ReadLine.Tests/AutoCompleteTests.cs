using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReadLine.Tests
{
    public class AutoCompleteTests
    {
        internal class TestCompleteHandler : IAutoCompleteHandler
        {
            private readonly char[] _separators;
            private readonly string[] _suggestions;

            public TestCompleteHandler(IEnumerable<char> seperators, IEnumerable<string> suggestions)
            {
                _separators = seperators.ToArray();
                _suggestions = suggestions.ToArray();
            }

            public char[] Separators { get => _separators; set { } }

            public string[] GetSuggestions(string text, int index) => _suggestions;
        }

        [Fact]
        public void SimpleTest()
        {
            var readline = new ReadlineProcessor
            {
                AutoCompletionHandler = new TestCompleteHandler(new[] { ' ' }, new[] { "one", "two", "three" }),
                HistoryEnabled = true
            };

            var ret = readline.Read("\t");
        }
    }
}
