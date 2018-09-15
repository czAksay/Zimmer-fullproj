using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    static class Core
    {
        static int numOfThreads = 4;            //кол-во потоков
        static String inFilePath = "in.txt";    //путь к файлу исходных данных
        static string[] codes;                  //коды товаров из исходного файла

        static String[] mainPatterns = { @"<td class=ProdBra>(.+?)</td>",   //шаблон поиска бренда товара
                                      @"<td class=ProdArt>(\S+)",           //шаблон поиска артикула товара
                                      @"<td></td><td class=ProdName>(.+?)</td>",    //шаблон поиска названия товара
                                      @"<li class=tabsPr.*uarid=(\d*)>Дополнительная информация</li>" };    //шаблон поиска id товара, по которому производится 
                                                                                                            //загрузка дополнительной информации о нем.

        static String[] addonPatterns = { @"(<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>(.*?)</table></td><td>)",   //Шаблон поиска xml-элемента с характеристиками товара
                                          @"(<table class=proptab><tr class=head><td colspan=2>Связанные номера:</td>" +
                                          "<td>Тип номера</td></tr>(.*?)</table></td></tr>)" };      //Шаблон поиска xml-элемента с номерами связанных запчастей

        static String linkSeparator = @"<tr>(.*?)</tr>";    //Шаблон для разделения связанных запчастей друг от друга

        static String[] linkPatterns = { @"<td class=tarig>(.*?)</td>", //шаблон поиска марки связанного товара
                                  @"<td><a href=.*?>(.*?)</a></td>",    //шаблон поиска номера связанного товара
                                  @"<td><span.*?>(.*?)</span></td>"};   //шаблон поиска типа номера связанного товара

        public static String[] MainPatterns { get { return mainPatterns; } }
        public static String[] AddonPatterns { get { return addonPatterns; } }
        public static String LinkSeparator { get { return linkSeparator; } }
        public static String[] LinkPatterns { get { return linkPatterns; } }

        public static int NumberOfThreads { get { return numOfThreads; } }
        public static String InputFile { get { return inFilePath; } }
        
        public static bool LoadCodes(string[] _codes)
        {
            if (_codes.Length <= 0)
            {
                return false;
            }
            codes = _codes;
            return true;
        }

        public static string[] GetCodes { get { return codes; } }

        /// <summary>
        /// Очищает артикул товара от точек и пробелов.
        /// </summary>
        public static String ClearCode(String code)
        {
            String[] nums = code.Split('.', ' ');
            String returnNumber = "";
            foreach (String num in nums)
            {
                returnNumber += num;
            }
            return returnNumber;
        }
    }
}
