using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zadanie_4
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Console.WriteLine("Wprowadź imię:");
            user.Name = Console.ReadLine();

            Console.WriteLine("Wprowadź nazwisko:");
            user.Surname = Console.ReadLine();

            Console.WriteLine("Wprowadź wiek:");
            int age = 0;
            bool success = false;

            while (!success)
            {
                success = Int32.TryParse(Console.ReadLine().ToString(), out age);

                if (!success)
                {
                    Console.WriteLine("Wiek musi być liczbą cakowitą, proszę wprowadzić ponownie:");
                }
            }

            user.Age = age;
            Console.WriteLine("Wprowadź miasto:");
            user.City = Console.ReadLine();
            string mainPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            CreateFakeNews(mainPath, user);
            ReadSchema(mainPath, user);

            Console.ReadKey();
        }
        //podpunkt a
        public static void CreateFakeNews(string mainPath, User user)
        {
            string fileName = "";
            string pattern = "^[a-zA-Z][a-zA-Z0-9]*$";
            bool success = false;
            string outputPath = "";

            Console.WriteLine("Wprowadź nazwę pliku:");
            while (!success)
            {
                fileName = Console.ReadLine();
                success = Regex.IsMatch(fileName, pattern);

                if (!success)
                    Console.WriteLine("Nazwa pliku może zawierać jedynie znaki alfanumeryczne, bez znaków specjalnych:");
            }
            string headline = "Dziennik Wybrany: {0}-letni {1} {2} twierdzi, że został uprowadzony przez kosmitów.\n";
            string news = "{0}. {1}-letni {2} {3} został tymczasowo odprowadzony do aresztu niezwłocznie" +
                           "po tym jak utrudniał pracę funkcjonariuszom policji poprzez permanentne i usilne domaganie się" +
                           "policyjnej ochrony, ze względu na porwanie przez nieznane istoty obce.";

            outputPath = Path.Combine(mainPath, "OutputFiles");

            if (!File.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            string outputFileDirectory = Path.Combine(mainPath,"OutputFiles", fileName + ".txt");

            using (StreamWriter file = File.CreateText(outputFileDirectory))
            {
                file.WriteLine(headline, user.Age, user.Name, user.Surname);
                file.WriteLine(news, user.City, user.Age, user.Name, user.Surname);
            }
        }
        //podpunkt b
        public static void ReadSchema(string mainPath, User user)
        {
            string inputPath = Path.Combine(mainPath, "InputFiles", "szablon.txt");

            if (File.Exists(inputPath))
            {
                //microsoft docs ISO 8859-2 Central European; Central European (ISO) - code 28592
                string[] schemaLines = File.ReadAllLines(inputPath, Encoding.GetEncoding(28592));

                if (schemaLines.Length == 3)
                {
                    Console.WriteLine(schemaLines[0], user.Age, user.Name, user.Surname);
                    Console.WriteLine(schemaLines[2], user.City, user.Age, user.Name, user.Surname);
                }
            }
        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
