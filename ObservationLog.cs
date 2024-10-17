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

            //kontrollerar att datum är rätt inmatat med TryParse
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Ogiltigt datum eller datumformat, försök igen (format: åååå-mm-dd).");
            }

            //ber användaren skriva in tid och kontrollerar formatet
            Console.Write("Ange tidpunkt för observationen (t.ex. 10:30): ");
            string? time;
            TimeSpan validTime;
            while (!TimeSpan.TryParseExact(Console.ReadLine(), @"hh\:mm", null, out validTime))
            {
                Console.WriteLine("Ogiltigt tidsformat, försök igen (format: tt:mm).");
                Console.Write("Ange tidpunkt för observationen (t.ex. 10:30): ");
            }
            time = validTime.ToString(@"hh\:mm"); // Formaterar tiden till rätt format


            //ber användaren att skriva in plats plus felmeddelande vid tom inmatning
            Console.Write("Ange plats för observationen (t.ex. Kusten, Havet): ");
            string? place;
            do
            {
                place = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(place))
                {
                    Console.WriteLine("Du måste fylla i en plats, försök igen.");
                }
            } while (string.IsNullOrWhiteSpace(place));


            //ber användaren att skriva in valart plus felmeddelande vid tom inmatning
            Console.Write("Ange valart (t.ex. Kaskelot, Grönlandsval): ");
            string? whale;
            do
            {
                whale = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(whale))
                {
                    Console.WriteLine("Du måste fylla i valart, försök igen.");
                }
            } while (string.IsNullOrWhiteSpace(whale));


            //ber användaren att skriva in antal valar
            Console.Write("Ange hur många valar som observerades: ");
            int number;

            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Console.WriteLine("Ogiltigt antal, försök igen.");
                Console.Write("Ange hur många valar som observerades: ");
            }

            Observation newObservation = new Observation(date, time, place, whale, number);
            observationer.Add(newObservation);//lägger till nya observationen till listan

            Console.WriteLine("Observation tillagd! ");
            SaveObservationsToFile(); //sparar till JSON-filen
            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn");
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

            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn");
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

                Console.Write("ObservationsID du vill ta bort:");

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

            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn");
            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }


        //Metod för att visa alla observationer
        public void ShowAllObservation()
        {
            Console.WriteLine("Valobservationer: ");
            Console.WriteLine();// tom rad för extra mellanrum
            for (int i = 0; i < observationer.Count; i++)
            {
                Console.WriteLine($"ObservationsID: {i + 1}");
                Console.WriteLine($"Datum och tid: {observationer[i].Date.ToShortDateString()} {observationer[i].Time}");
                Console.WriteLine($"Plats: {observationer[i].Place}");
                Console.WriteLine($"Valart: {observationer[i].Whale}");
                Console.WriteLine($"Antal individer: {observationer[i].Number}");
                Console.WriteLine(); // Tom rad för att separera varje observation
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