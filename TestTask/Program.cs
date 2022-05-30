using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace TestTask
{
    public class Program
    {

        /// <summary>
        /// Программа принимает на входе 2 пути до файлов.
        /// Анализирует в первом файле кол-во вхождений каждой буквы (регистрозависимо). Например А, б, Б, Г и т.д.
        /// Анализирует во втором файле кол-во вхождений парных букв (не регистрозависимо). Например АА, Оо, еЕ, тт и т.д.
        /// По окончанию работы - выводит данную статистику на экран.
        /// </summary>
        /// <param name="args">Первый параметр - путь до первого файла.
        /// Второй параметр - путь до второго файла.</param>
        static void Main(string[] args)
        {
            IReadOnlyStream inputStream1 = GetInputStream(args[0]);
            IReadOnlyStream inputStream2 = GetInputStream(args[1]);

            IList<LetterStats> singleLetterStats = FillSingleLetterStats(inputStream1);
            IList<LetterStats> doubleLetterStats = FillDoubleLetterStats(inputStream2);

            RemoveCharStatsByType(singleLetterStats, CharType.Vowel);
            RemoveCharStatsByType(doubleLetterStats, CharType.Consonants);

            PrintStatistic(singleLetterStats);
            PrintStatistic(doubleLetterStats);

            Console.ReadKey();
            // TODO : Необжодимо дождаться нажатия клавиши, прежде чем завершать выполнение программы.

        }

        /// <summary>
        /// Ф-ция возвращает экземпляр потока с уже загруженным файлом для последующего посимвольного чтения.
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        /// <returns>Поток для последующего чтения.</returns>
        private static IReadOnlyStream GetInputStream(string fileFullPath)
        {
            return new ReadOnlyStream(fileFullPath);
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения каждой буквы.
        /// Статистика РЕГИСТРОЗАВИСИМАЯ!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        private static IList<LetterStats> FillSingleLetterStats(IReadOnlyStream stream)
        {
            stream.ResetPositionToStart();
            List<LetterStats> lettersStatsForOneLetter = new List<LetterStats>();
            while (!stream.IsEof)
            {          
                char oneLetter = stream.ReadNextChar();
                LetterStats latterStat;
                latterStat = lettersStatsForOneLetter.Find(letter => letter.Letters == oneLetter.ToString());
                if (latterStat == null)
                {
                    latterStat = new LetterStats(oneLetter.ToString());

                    lettersStatsForOneLetter.Add(latterStat);
                   
                }
                else
                   IncStatistic(latterStat);
                // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - регистрозависимый.
            }
            //return ???;
            return lettersStatsForOneLetter;
        }

        /// <summary>
        /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения парных букв.
        /// В статистику должны попадать только пары из одинаковых букв, например АА, СС, УУ, ЕЕ и т.д.
        /// Статистика - НЕ регистрозависимая!
        /// </summary>
        /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
        /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
        private static IList<LetterStats> FillDoubleLetterStats(IReadOnlyStream stream)
        {
            stream.ResetPositionToStart();
            List<LetterStats> letterStatForPair = new List<LetterStats>();
            LetterStats PairLetter;
            String pair = "";
            while (!stream.IsEof)
            {
                char OneLetter = stream.ReadNextChar();

                if (pair.EndsWith(OneLetter.ToString().ToLower(), StringComparison.OrdinalIgnoreCase))
                    pair += OneLetter;
                else
                    pair += OneLetter;
                if (pair.Length == 2 && pair.EndsWith(pair[0].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    PairLetter = letterStatForPair.Find(letter => letter.Letters.ToLower() == pair.ToLower());
                    if (PairLetter == null)
                    {
                        PairLetter = new LetterStats(pair);
                        letterStatForPair.Add(PairLetter);
                    }
                    else
                        IncStatistic(PairLetter);
                    pair = "";
                }
                else if ( pair.Length == 2)pair = "";
                // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - НЕ регистрозависимый.
            }
            //return ???;
            return letterStatForPair;
        }

        /// <summary>
        /// Ф-ция перебирает все найденные буквы/парные буквы, содержащие в себе только гласные или согласные буквы.
        /// (Тип букв для перебора определяется параметром charType)
        /// Все найденные буквы/пары соответствующие параметру поиска - удаляются из переданной коллекции статистик.
        /// </summary>
        /// <param name="letters">Коллекция со статистиками вхождения букв/пар</param>
        /// <param name="charType">Тип букв для анализа</param>
        private static void RemoveCharStatsByType(IList<LetterStats> letters, CharType charType)
        {
            // TODO : Удалить статистику по запрошенному типу букв.

            for (int i = 0; i < letters.Count; i++)
            {
                if (CharTypeExtensions.FindTypeLetter(letters[i]) == charType)
                {
                    letters.Remove(letters[i]);
                    i--;
                }
            }
        }

        /// <summary>
        /// Ф-ция выводит на экран полученную статистику в формате "{Буква} : {Кол-во}"
        /// Каждая буква - с новой строки.
        /// Выводить на экран необходимо предварительно отсортировав набор по алфавиту.
        /// В конце отдельная строчка с ИТОГО, содержащая в себе общее кол-во найденных букв/пар
        /// </summary>
        /// <param name="letters">Коллекция со статистикой</param>
        private static void PrintStatistic(IEnumerable<LetterStats> letters)
        {
            var sortingLetters = letters.OrderBy(l => l.Letters);
            int sum = 0;
            foreach (LetterStats letterStats in sortingLetters)
            {
                Console.WriteLine(letterStats.Letters + " - " + letterStats.Count);
                sum += letterStats.Count;
            }
            
            Console.WriteLine("Finaly =  " + letters.Count() + " " + sum);            
            // TODO : Выводить на экран статистику. Выводить предварительно отсортировав по алфавиту!
            //throw new NotImplementedException();
        }
        /// <summary>
        /// Метод увеличивает счётчик вхождений по переданной структуре.
        /// </summary>
        /// <param name="letterStats"></param>
        private static void IncStatistic(LetterStats letterStats)
        {
            letterStats.Count++;         
        }
    }
}
