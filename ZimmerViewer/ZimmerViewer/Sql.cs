using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ZimmerViewer.Model;
using System.Collections.ObjectModel;

namespace ZimmerViewer
{
    static public class Sql
    {
        static String conString = "server=localhost;user=root;password=root;database=Zimmer;SslMode=none;";

        public static ObservableCollection<Parts> GetData(String partCodeFilter, String partNameFilter, String linkPartCodeFilter)
        {
            using (MySqlConnection connection = new MySqlConnection(conString))
            {
                //Формируем список возвращаемых деталей
                ObservableCollection<Parts> data = new ObservableCollection<Parts>();
                connection.Open();
                //Получаем с помощью хранимой процедуры GetAllPartsInfo выборку деталей построчно в 
                //парах с их связанными запчастями в соответствии с ТЗ.
                //Если в поле хранится пустая строка, вместо нее заносим "не найден"
                using (MySqlCommand sqlCmd = new MySqlCommand(String.Format("call GetAllPartsInfo('{0}', '{1}', '{2}');", partCodeFilter, partNameFilter, linkPartCodeFilter), connection))
                {
                    MySqlDataReader reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Parts p = new Parts(reader.GetValue(0).ToString() + "\t" + (reader.GetValue(1).ToString() != "" ? reader.GetValue(1).ToString() : "не найден"), 
                            reader.GetValue(2).ToString() != "" ? reader.GetValue(2).ToString() : "не найден", reader.GetValue(3).ToString() + " " + reader.GetValue(4).ToString());
                        data.Add(p);
                    }
                    connection.Close();
                }
                return data;
            }
        }
    }
}
