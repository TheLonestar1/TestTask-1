namespace TestTask
{
    /// <summary>
    /// Тип букв
    /// </summary>
    public enum CharType
    {
        /// <summary>
        /// Гласные
        /// </summary>
        Vowel,

        /// <summary>
        /// Согласные
        /// </summary>
        Consonants
    }

    public static class CharTypeExtensions
    {
        public const string Vowel = "аоиеёэыуюяaioe";

        public static CharType FindTypeLetter(LetterStats letterStats)
        {
            string letter = letterStats.Letters.ToLower();
            return Vowel.Contains(letter[0].ToString()) ? CharType.Vowel : CharType.Consonants;
        }
    }
}
