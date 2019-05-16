using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace AppLibrary
{
    public class Library
    {
        public static List<PersonModel> list1 = new List<PersonModel>();
        public static List<PersonModel> list2 = new List<PersonModel>();

        public static void UploadPDF(string path, List<PersonModel> list)
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

                        list.Add(new PersonModel(Pesel, Nazwisko, Imie));

                        Pesel = "BŁĄD:PESEL";
                        Nazwisko = "BŁĄD:Nazwisko";
                        Imie = "BŁĄD:Imię";
                    }
                }
                //Inser BackGroundWorker.Update here
                //Or just a delegate method
                Debug.WriteLine("Finished Page: {0}", i);
            }
        }

        public static List<PersonModel> GenerateResult()
        {
            var output = list1.Except(list2, new PersonComparer());
            if (output.Count() == 0)
            {
                output = list2.Except(list1, new PersonComparer());
            }

            return output.ToList<PersonModel>();
        }
        public static void PrintResults(List<PersonModel> input)
        {
            string path = "result-Project_Duplicat.txt";

            using (StreamWriter sw = new StreamWriter(path, false))
            {
                int counter = 1;
                foreach (var item in input)
                {
                    sw.WriteLine(String.Format("{0:00}. {1}", counter++, item));
                }
            }
            System.Diagnostics.Process.Start(@"result-Project_Duplicat.txt");
        }
    }
}
