/*
Anne-Lii Hansen anha2324@student.miun.se
En app för valobservationer där användaren kan registrera en observation av en val.
*/

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

        // Metod för att lägga till en observation
        public void LogObservation()
        {
            Console.Clear();

            // Datum för observationen
            Console.WriteLine("Datum för observationen (ÅÅÅÅ-MM-DD): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Ogiltigt datum eller datumformat, försök igen (format: åååå-mm-dd).");
            }

            // Tidpunkt för observationen
            Console.Write("Ange tidpunkt för observationen (t.ex. 10:30): ");
            string? time;
            TimeSpan validTime;
            while (!TimeSpan.TryParseExact(Console.ReadLine(), @"hh\:mm", null, out validTime))
            {
                Console.WriteLine("Ogiltigt tidsformat, försök igen (format: tt:mm).");
            }
            time = validTime.ToString(@"hh\:mm");

            // Plats för observationen
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

            // Valart
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

            // Antal valar
            Console.Write("Ange hur många valar som observerades: ");
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Console.WriteLine("Ogiltigt antal, försök igen.");
            }

            // Skapa ny observation
            Observation newObservation = new Observation(date, time, place, whale, number);
            observationer.Add(newObservation);

            Console.WriteLine("Observation tillagd!");
            SaveObservationsToFile();
            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn");
            Console.ReadKey();
        }

        // Metod för att visa alla observationer
        public void ShowAllObservation()
        {

            // Inläsning av observationer vid start
            LoadObservationsFromFile();

            Console.Clear();
            if (observationer.Count == 0)
            {
                Console.WriteLine("Det finns inga observationer.");
            }
            else
            {
                Console.WriteLine("Valobservationer:");
                Console.WriteLine("");//tom rad
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
            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn.");
            Console.ReadKey();
        }

        public void RemoveObservation()
        {
            Console.Clear(); // Rensar konsollen

            if (observationer.Count == 0)
            {
                Console.WriteLine("Det finns inga observationer.");
            }
            else
            {
                for (int i = 0; i < observationer.Count; i++)
                {
                    Console.WriteLine($"ObservationsID: {i + 1} - {observationer[i].Whale}, {observationer[i].Place}");
                }

                Console.WriteLine("");
                Console.Write("Ange ID för den observation du vill ta bort :");
                Console.WriteLine("Eller tryck ENTER för att komma tillbaka till menyn");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ingen observation har tagits bort. Tryck ENTER för att återgå till menyn.");
                    Console.ReadKey(); // Väntar på att användaren trycker på en tangent för att återgå till menyn
                    return;
                }

                if (int.TryParse(input, out int index) && index > 0 && index <= observationer.Count)
                {
                    observationer.RemoveAt(index - 1); // Tar bort observationen med det valda ID:et

                    if (observationer.Count == 0)
                    {
                        Console.WriteLine("Alla observationer har raderats.");
                        File.WriteAllText(filePath, "[]"); // Rensa filen genom att skriva en tom JSON-lista
                    }
                    else
                    {
                        SaveObservationsToFile(); // Sparar listan till fil efter att en observation har tagits bort
                    }

                    Console.WriteLine("Observation borttagen!");
                }
                else
                {
                    Console.WriteLine("Ogiltigt ID, försök igen!");
                }
            }

            Console.WriteLine("Tryck på valfri tangent för att återvända till menyn");
            Console.ReadKey(); // Väntar på att användaren trycker på en tangent för att återgå till menyn
        }


        // Metod för att spara observationer till JSON-fil
        private void SaveObservationsToFile()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(observationer, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av data: {ex.Message}");
            }
        }

        // Metod för att läsa in observationer från JSON-filen
        public void LoadObservationsFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    observationer.Clear();//Rensar listan innan ny inläsning
                    string jsonData = File.ReadAllText(filePath);
                    observationer = JsonConvert.DeserializeObject<List<Observation>>(jsonData) ?? new List<Observation>();
                }
                else
                {
                    File.WriteAllText(filePath, "[]");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning av data: {ex.Message}");
            }
        }
    }
}
