using System;
using System.Diagnostics;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace AppLibrary
{
    public class TextOperations
    {
        public static void PdfToString(string path)
        {
            PdfReader reader = new PdfReader(path);


            string Pesel = "";
            string Nazwisko = "";
            string Imie = "";
            int counter = 0;

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                string page = PdfTextExtractor.GetTextFromPage(reader, i);

                StringReader strReader = new StringReader(page);
                string line;
                while ((line = strReader.ReadLine()) != null)
                {
                    if (line.Contains("PESEL:"))
                    {
                        if (line.Length >= 7)
                        {
                            Pesel = line.Substring(7).Trim();
                            counter++;
                        }
                    }
                    else if (line.Length == 11 && Int32.TryParse(line[0].ToString(), out int result))
                    {
                        Pesel = line.Trim();
                        counter++;
                    }
                    else if (line.Contains("Imię:"))
                    {
                        if (line.Length >= 6)
                        {
                            Imie = line.Substring(6).Trim();
                        }
                        counter++;
                    }
                    else if (line.Contains("Nazwisko:"))
                    {
                        if (line.Length >= 10)
                        {
                            Nazwisko = line.Substring(10).Trim();
                        }
                        counter++;
                    }

                    if (counter == 3)
                    {

                        counter = 0;

                        //Here add to dbs
                        try
                        {
                            DBManager.UploadMain(Pesel, Nazwisko, Imie);
                        }
                        catch (System.Data.SQLite.SQLiteException)
                        {
                            try
                            {
                                DBManager.UploadDuplicate(Pesel, Nazwisko, Imie);
                            }
                            catch (System.Data.SQLite.SQLiteException ex)
                            {

                            }
                       
                        }

                        //Debug.WriteLine(Nazwisko);
                        //Debug.WriteLine(Imie);
                        //Debug.WriteLine(Pesel);
                        //Debug.WriteLine("");

                        Pesel = "BŁĄD:PESEL";
                        Nazwisko = "BŁĄD:Nazwisko";
                        Imie = "BŁĄD:Imię";
                    }
                }
                Debug.WriteLine("Finished Page: {0}", i);
            }
        }

        public static void GenerateReport()
        {
            //System.Data.SQLite.SQLiteDataReader reader = DBManager.GetDuplicates();

            //string path = @"D:\result-Project_Duplicat.txt";

            //using (StreamWriter sw = new StreamWriter(path, false))
            //{
            //    while (reader.Read())
            //    {
            //        string data = String.Format("{0}, {1}, {2}", reader["Pesel"], reader["Nazwisko"], reader["Imie"]);
            //        sw.Write(data);
            //    }
            //};
        }
    }
}
