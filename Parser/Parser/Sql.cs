using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Parser
{
    static class Sql
    {
        static String conString = "server=localhost;user=root;password=root;database=Zimmer;SslMode=none;"; //Строка соединения с БД
        static String insertPartCmd = "INSERT INTO Parts VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');";   //Команда добавления в таблицу Parts новой детали
        static String insertLinkedPartCmd = "INSERT INTO LinkParts VALUES ('{0}', '{1}', '{2}');";  //Команда добавления в таблицу LinkParts новой связанной детали
        static String insertLinkCmd = "INSERT INTO Links VALUES ('{0}', '{1}');";   //Команда добавления в таблицу Links связи между деталью и связанными с ней делатями

        /// <summary>
        /// Выполнить команду в БД.
        /// </summary>
        public static bool ExecuteCommand(String command)
        {
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                connection.Open();
                using (MySqlCommand sqlCmd = new MySqlCommand(command, connection))
                {
                    sqlCmd.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
        }

        /// <summary>
        /// Добавление детали в БД. Возвращает true при успешном добавлении или если делать уже 
        /// находится в БД.
        /// </summary>
        public static bool InsertPart(String code, String url, String articul, String brand, String name, String characteristics)
        {
            try
            {
                //выполняем команду с соответствующими аргументами
                ExecuteCommand(String.Format(insertPartCmd, code, url, articul, brand, name, characteristics));
            }
            catch(MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    Console.WriteLine(">[denied] Запчасть " + code + " уже присутствуйет в БД.");
                    return true;
                }
                else
                {
                    Console.WriteLine(">[denied] Запчасть " + code + " не была добавлена в БД:\n"+ex.Message);
                    return false;
                }
            }
            Console.WriteLine(">[succ] Запчасть " + code + " добавлена в БД.");
            return true;
        }

        /// <summary>
        /// Добавление связанной детали к уже имеющейся. 
        /// Добавляет связанную деталь в таблицу LinkParts и отношение между ней и 
        /// оригинальной деталью в таблицу Links.
        /// </summary>
        public static bool InsertLinkPart(String code, LinkedPart lp)
        {
            try
            {
                ExecuteCommand(String.Format(insertLinkedPartCmd, lp.code, lp.producer, lp.type));
            }
            catch (MySqlException ex)
            {
                if (ex.Number != 1062)
                {
                    Console.WriteLine(">[err] Связанная запчасть " + lp.code + " не была добавлена в БД:\n" + ex.Message);
                    return false;
                }
            }
            try
            {
                ExecuteCommand(String.Format(insertLinkCmd, code, lp.code));
            }
            catch (MySqlException ex)
            {
                if (ex.Number != 1062)
                {
                    Console.WriteLine(">[err] Не удалось добавить связь между " + code + " и " + lp.code + ":\n" + ex.Message);
                    return false;
                }
            }
            return true;
        }
    }
}
