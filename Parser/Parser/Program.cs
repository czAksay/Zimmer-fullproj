using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Parser
{
    public struct LinkedPart
    {
        public String code;
        public String producer;
        public String type;
    }

    class Program
    {
        static void Main(string[] args)
        {
            //загружаем файл исходных данных.
            switch (LoadInputFile())
            {
                case -1:
                    Console.WriteLine("Ошибка при попытке создания файла исходных данных (" +Core.InputFile+ "). Создайте его вручную и поместите в него коды.\nПрограмма будет завершена. Нажмите любую клавишу . . . ");
                    Console.ReadKey(true);
                    return;
                case -2:
                    Console.WriteLine("Исходный файл (" + Core.InputFile + ") пуст. Поместите в него коды.\nПрограмма будет завершена. Нажмите любую клавишу . . . ");
                    Console.ReadKey(true);
                    return;
            }

            //Запускаем нужное количество потоков. Каждый поток 
            //начинает свою работу в функции ThreadStartWork
            Thread[] threads = new Thread[Core.NumberOfThreads];
            for (int i = 0; i < Core.NumberOfThreads; i++)
            {
                threads[i] = new Thread(ThreadStartWork);
                threads[i].Start(i);
            }
        }



        /// <summary>
        /// Функция работы потоков. Каждый поток берет себе каждую i-тую строку из файла исходных данных (где i - это
        /// количество потоков). Используя заранее подготовленные шаблоны поиска в классе Core, поток получает
        /// со странички все данные товара и добавляет их в БД. Если данных не хватает, в лог-файл поместится запись
        /// об ошибке, но код товара будет добавлен в БД.
        /// </summary>
        static void ThreadStartWork(object threadNumber)
        {
            Console.WriteLine("> Поток номер [" + ((int)threadNumber + 1) + "] стартовал.");
            for (int i = (int)threadNumber; i < Core.GetCodes.Length; i += Core.NumberOfThreads)
            {
                //получаем код с которым сейчас работает поток
                String currentCode = Core.GetCodes[i];
                if (currentCode == "")
                    continue;
                string url = @"https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/" + currentCode + "/";

                if (Log.UrlExistsInLog(url))
                {
                    Console.WriteLine(">[skip] " + currentCode + " присутствует в log-файле со статусом ОК.");
                    continue;
                }

                String[] results = new String[0];   //результаты поиска на страничке имени товара, бренда, 
                                                    //артикула и ссылки на страничку с характеристиками и связанными деталями товара
                String[] resultsOfAddons = new String[0];   //результаты поиска xml-код характеристик товара (индекс 0) и 
                                                            //связанных деталей товара (индекс 1)
                bool succ = true;

                succ = GetDataFromUrl(url, Core.MainPatterns, ref results) && succ;
                //ссылка на характеристики + связанные детали товара. Они хранятся на отдельной страничке.
                string addonsUrl = @"https://otto-zimmermann.com.ua/autoparts/addons/props/component.php?of=" + results[3];
                succ = GetDataFromUrl(addonsUrl, Core.AddonPatterns, ref resultsOfAddons) && succ;

                if (!succ)
                {
                    Log.Push(url, LogState.Error);
                    Console.WriteLine(">[err] Невозможно получить данные запчасти " + currentCode + ".");
                }

                //Получаем список всех связанных запчастей
                List<LinkedPart> links = GetAllLinkedParts(resultsOfAddons[1]);

                //Добавляем все в БД.
                if (!Sql.InsertPart(currentCode, url, results[1], results[0], results[2], resultsOfAddons[0]))
                {
                    Log.Push(url, LogState.Error);
                    continue;
                }
                foreach (LinkedPart lp in links)
                {
                    Sql.InsertLinkPart(currentCode, lp);
                }

                if (succ)
                    Log.Push(url, LogState.Ok);
            }
            Thread.Sleep(2000);
        }


        /// <summary>
        /// Принимает в качестве аргумента xml-элемент, содержащий данные о связанных запчастях
        /// какой-либо запчасти. Выбирает эти данные с пом. регулярных выражений
        /// и возвращает их в виде списка.
        /// </summary>
        static List<LinkedPart> GetAllLinkedParts(String linksXml)
        {
            
            List<LinkedPart> linksXmls = new List<LinkedPart>();
            Regex regex = new Regex(Core.LinkSeparator, RegexOptions.Singleline);
            Match match = regex.Match(linksXml);
            foreach (Match m in regex.Matches(linksXml))
            {
                String linkBlock = m.Groups[1].Value;
                LinkedPart lp = new LinkedPart();
                for (int i = 0; i < Core.LinkPatterns.Length; i ++)
                {
                    Match m2 = Regex.Match(linkBlock, Core.LinkPatterns[i]);
                    switch (i)
                    {
                        case 0:
                            lp.producer = m2.Groups[1].Value;
                            break;
                        case 1:
                            lp.code = Core.ClearCode(m2.Groups[1].Value);
                            break;
                        case 2:
                            lp.type = m2.Groups[1].Value;
                            break;
                    }
                }
                linksXmls.Add(lp);
            }
            return linksXmls;
        }


        /// <summary>
        /// Принимает в качестве аргументов url и шаблоны для поиска данных на страничке. 
        /// Данные находятся с пом. регулярных выражений
        /// </summary>
        static bool GetDataFromUrl(String url, String[] patterns, ref String[] results)
        {
            results = new String[patterns.Length];
            String response;
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                response = client.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            for (int j = 0; j < patterns.Length; j++)
            {
                Regex regex = new Regex(patterns[j] , RegexOptions.Singleline);
                Match match = regex.Match(response);
                results[j] = match.Groups[1].Value;
            }
            foreach (String res in results)
            {
                if (res == "" || res == null)
                {
                    return false;
                }
            }
            return true; 
        }


        /// <summary>
        /// Загрузка кодов из файла исходных данных в программу.
        /// </summary>
        static int LoadInputFile()
        {
            string dashes = new string('-', 16);
            Console.WriteLine(dashes + " Parser " + dashes);
            Console.WriteLine("> Загрузка файла кодов.");
            if (!File.Exists(Core.InputFile))
            {
                try
                {
                    FileStream fs = File.Create(Core.InputFile);
                    fs.Close();
                }
                catch
                {
                    return -1;
                }
            }
            if (!Core.LoadCodes(File.ReadAllLines(Core.InputFile, Encoding.GetEncoding(1251))))
            {
                return -2;
            }
            return 0;
        }
    }
}