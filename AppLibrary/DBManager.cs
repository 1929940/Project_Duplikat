using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace AppLibrary
{
    public class DBManager
    {
        public static SQLiteConnection Connect()
        {
            return new SQLiteConnection("Data Source=database.db");
        }
        public static void UploadMain(string pesel, string nazwisko, string imie)
        {
            using (var cnn = Connect())
            {
                string statement = "INSERT INTO mainDB (Pesel, Nazwisko, Imie) VALUES (@pesel,@nazwisko,@imie);";
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand(statement, cnn);
                command.Parameters.AddWithValue("@pesel", pesel);
                command.Parameters.AddWithValue("@nazwisko", nazwisko);
                command.Parameters.AddWithValue("@imie", imie);
                command.ExecuteNonQuery();
            }
        }
        public static void UploadDuplicate(string pesel, string nazwisko, string imie)
        {
            using (var cnn = Connect())
            {
                string statement = "INSERT INTO duplicatesDB (Pesel, Nazwisko, Imie) VALUES (@pesel,@nazwisko,@imie);";
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand(statement, cnn);
                command.Parameters.AddWithValue("@pesel", pesel);
                command.Parameters.AddWithValue("@nazwisko", nazwisko);
                command.Parameters.AddWithValue("@imie", imie);
                command.ExecuteNonQuery();
            }
        }
        public static void GetDuplicates()
        {
            SQLiteDataReader reader;
            string path = @"D:\result-Project_Duplicat.txt";
            using (var cnn = Connect())
            {
                cnn.Open();
                string statement = "SELECT * FROM mainDB WHERE Pesel NOT IN (SELECT Pesel FROM duplicatesDB);";
                SQLiteCommand command = new SQLiteCommand(statement, cnn);
                reader = command.ExecuteReader();
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    while (reader.Read())
                    {
                        string data = String.Format("{0}, {1}, {2}", reader["Pesel"], reader["Nazwisko"], reader["Imie"]);
                        sw.WriteLine(data);
                    }
                };

            }
            //return output;

            //System.Data.SQLite.SQLiteDataReader reader = DBManager.GetDuplicates();
        }

    }
}
