 using System;
using System.IO;

namespace TestTask
{
    public class ReadOnlyStream : IReadOnlyStream
    {
        private readonly Stream _localStream;

        /// <summary>
        /// Конструктор класса. 
        /// Т.к. происходит прямая работа с файлом, необходимо 
        /// обеспечить ГАРАНТИРОВАННОЕ закрытие файла после окончания работы с таковым!
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public ReadOnlyStream(string fileFullPath)
        {   
            // TODO : Заменить на создание реального стрима для чтения файла!
            _localStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
        }
                
        /// <summary>
        /// Флаг окончания файла.
        /// </summary>
        public bool IsEof
        {
            get
            {
                if (_localStream.Position == _localStream.Length)
                {
                    return true;
                }
                else
                    return false;
            } // TODO : Заполнять данный флаг при достижении конца файла/стрима при чтении
            
        }

        /// <summary>
        /// Ф-ция чтения следующего символа из потока.
        /// Если произведена попытка прочитать символ после достижения конца файла, метод 
        /// должен бросать соответствующее исключение
        /// </summary>
        /// <returns>Считанный символ.</returns>
        public char ReadNextChar()
        {
            // TODO : Необходимо считать очередной символ из _localStream
            String letter = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмить";
            if (IsEof)
                throw new Exception("Поток достиг конца файла");
            var charepter = (char)_localStream.ReadByte();
            while (!letter.Contains(charepter.ToString().ToLower()))
            {
                charepter = (char)_localStream.ReadByte();
            }
            return charepter;
        }

        /// <summary>
        /// Сбрасывает текущую позицию потока на начало.
        /// </summary>
        public void ResetPositionToStart()
        {
            if (_localStream == null)
            {
                return;
            }
            _localStream.Position = 0;
        }

        public void Dispose()
        {
            _localStream.Dispose();
        }
    }
}
