using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Parser
{
    enum LogState { Ok, Error};
    static class Log
    {
        static String logFilePath = "parser.log";       //Путь к лог-файлу
        static String logSeparator = "\t|\t";           //Разделитель url/статуса/даты в лог-файле

        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();    //локер для предотвращения ошибок чтения/записи лог файла потоками
        public static String LogFile { get { return logFilePath; } }


        /// <summary>
        /// Проверяет, в наличии ли url товара в лог-файле со статусом 'OK'. Возвращает false если нет.
        /// </summary>
        public static bool UrlExistsInLog(string url)
        {
            if (File.Exists(logFilePath))
            {
                locker.EnterReadLock();
                using (StreamReader sr = new StreamReader(logFilePath))
                {
                    bool found = false;
                    string res = sr.ReadLine();
                    while (res != null)
                    {
                        String[] elements = Regex.Split(res, logSeparator, RegexOptions.IgnoreCase);
                        if (elements[0] == url && elements[2].ToLower() == LogState.Ok.ToString().ToLower())
                        {
                            found = true;
                            break;
                        }
                        res = sr.ReadLine();
                    }
                    sr.Close();
                    locker.ExitReadLock();
                    return found;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Запись в лог-файл url с определенным статусом.
        /// </summary>
        public static void Push (string url, LogState state)
        {
            locker.EnterWriteLock();
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine(url + logSeparator + state.ToString() + logSeparator + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    sw.Close();
                }
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
    }
}
