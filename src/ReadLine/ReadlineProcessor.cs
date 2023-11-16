using Internal.ReadLine;
using Internal.ReadLine.Abstractions;
using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadlineProcessor
    {
        private List<string> _history;

        public ReadlineProcessor()
        {
            _history = new List<string>();
        }

        public void AddHistory(params string[] text) => _history.AddRange(text);

        public IReadOnlyList<string> GetHistory() => _history;

        public void ClearHistory() => _history = new List<string>();

        public bool HistoryEnabled { get; set; }

        public IAutoCompleteHandler AutoCompletionHandler { internal get; set; }

        public string Read(string prompt = "", string @default = "")
        {
            Console.Write(prompt);
            var keyHandler = new KeyHandler(new ConsoleAbstraction(), _history, AutoCompletionHandler);
            string text = GetText(keyHandler);

            if (String.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(@default))
            {
                text = @default;
            }
            else
            {
                if (HistoryEnabled)
                    _history.Add(text);
            }

            return text;
        }

        public string ReadPassword(string prompt = "")
        {
            Console.Write(prompt);
            KeyHandler keyHandler = new(new ConsoleAbstraction() { PasswordMode = true }, null, null);
            return GetText(keyHandler);
        }

        private string GetText(KeyHandler keyHandler)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyHandler.Handle(keyInfo);
                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return keyHandler.Text;
        }
    }
}
