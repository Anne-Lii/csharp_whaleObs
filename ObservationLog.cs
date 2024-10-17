using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace whaleObservationApp
{
    public class ObservationLog
    {
        private List<Observation> observationer = new List<Observation>();

        private readonly string filePath = "data.txt";

        public ObservationLog()
        {
            //laddar in observationer från fil vid start
            LoadObservationsFromFile();
        }


        //Metod för att lägga till en observation
        public void LogObservation()
        {
            Console.Clear();//Rensar konsollen

            //ber användaren att skriva in datum för observationen
            Console.WriteLine("Datum för observationen (ÅÅÅÅ-MM-DD): ");
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Ogiltigt datumformat, försök igen (format: åååå-mm-dd).");
            }

            //ber användaren skriva in tid
            Console.Write("Ange tidpunkt för observationen (t.ex. 10:30): ");
            string? time = Console.ReadLine();

            //ber användaren att skriva in plats
            Console.Write("Ange plats för observationen (t.ex. Kusten, Havet): ");
            string? place = Console.ReadLine();

            //ber användaren att skriva in väder
            Console.Write("Ange väder vid observationen (t.ex. Soligt, Molnigt, Regnigt): ");
            string? weather = Console.ReadLine();

            //ber användaren att skriva in valart
            Console.Write("Ange typ av val som observerades (t.ex. Blåval, Kaskelot, Knölval): ");
            string? whale = Console.ReadLine();

            //ber användaren att skriva in antal valar
            Console.Write("Ange hur många valar som observerades: ");
            int number;

            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Console.WriteLine("Ogiltigt antal, försök igen.");
                Console.Write("Ange hur många valar som observerades: ");
            }

            Observation newObservation = new Observation(date, time, place, weather, whale, number);
            observationer.Add(newObservation);//lägger till nya observationen till listan

            Console.WriteLine("Observation tillagd! ");
            SaveObservationsToFile(); //sparar till JSON-filen
            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }


        //Metod för att visa observationer
        public void ShowObservation()
        {
            Console.Clear();//Rensar konsollen

            if (observationer.Count == 0)
            {
                Console.WriteLine("Det finns inga observationer.");
            }
            else
            {
                ShowAllObservation();
            }

            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }


        //Metod för att ta bort en observation
        public void RemoveObservation()
        {
            Console.Clear();//Rensar konsollen

            if (observationer.Count == 0)
            {
                Console.WriteLine("Det finns inga observationer.");
            }
            else
            {
                ShowAllObservation();

                Console.Write("Ange numret på observationen du vill ta bort:");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= observationer.Count)
                {
                    observationer.RemoveAt(index - 1);
                    Console.WriteLine("Observation borttagen!");
                    SaveObservationsToFile(); //sparar till JSON-filen

                }
                else
                {
                    Console.WriteLine("Ogiltigt val, försök igen!");
                }
            }

            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }


        //Metod för att visa alla observationer
        public void ShowAllObservation()
        {
            Console.WriteLine("Valobservationer: ");
            for (int i = 0; i < observationer.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {observationer[i]}");
            }
        }


        //Metod för att spara observationer till JSON-fil
        private void SaveObservationsToFile()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(observationer, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($" Fel vid sparande av data: {ex.Message}");
            }
        }


        //Metod för att läsa in observationer från JSON-filen
        private void LoadObservationsFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    observationer = JsonConvert.DeserializeObject<List<Observation>>(jsonData) ?? new List<Observation>();
                }
                else
                {
                    //skapar en ny tom JSON-fil med en tom lista
                    File.WriteAllText(filePath, "[]");
                }


            }
            catch (System.Exception ex)
            {
                Console.WriteLine($" Fel vid inläsning av data: {ex.Message}");
            }
        }
    }
}