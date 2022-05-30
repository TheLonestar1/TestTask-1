namespace TestTask
{
    /// <summary>
    /// Статистика вхождения буквы/пары букв
    /// </summary>
    public class LetterStats
    {
        /// <summary>
        /// Буква/Пара букв для учёта статистики.
        /// </summary>
        private string _letters { get; }

        /// <summary>
        /// Кол-во вхождений буквы/пары.
        /// </summary>
        private int _count { get; set; }

        public LetterStats(string letters)
        {
            _letters = letters;
            _count = 1;
        }

        public string Letters { get { return _letters; } }
        public int Count { get { return _count; } set { _count = value; } }
    }
}
