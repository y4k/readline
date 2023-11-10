using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Static class for simple access where only a single Readline processor is used
    /// </summary>
    public static class ReadLine
    {
        private static readonly ReadlineProcessor _instance = new();

        public static bool HistoryEnabled
        {
            get => _instance.HistoryEnabled;
            set => _instance.HistoryEnabled = value;
        }

        public static IAutoCompleteHandler AutoCompletionHandler { internal get => _instance.AutoCompletionHandler; set => _instance.AutoCompletionHandler = value; }

        public static void AddHistory(params string[] text) => _instance.AddHistory(text);

        public static IReadOnlyList<string> GetHistory() => _instance.GetHistory();

        public static void ClearHistory() => _instance.ClearHistory();

        public static string Read(string prompt = "", string @default = "") => _instance.Read(prompt, @default);

        public static string ReadPassword(string prompt = "") => _instance.ReadPassword(prompt);
    }
}
