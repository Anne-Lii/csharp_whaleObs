﻿/*
Anne-Lii Hansen anha2324@student.miun.se
En app för valobservationer där användaren kan registrera en observation av en val.
*/

using System;
using System.Diagnostics;
using System.Collections.Generic;// För att kunna använda List

namespace whaleObservationApp
{
    class Program
    {
        static ObservationLog observationLog = new ObservationLog(); // Klassen ObservationLog från ObservationLog.cs-filen

        static void Main(string[] args)
        {
            bool programRunning = true; // Variabel med true/false för att hålla programmet igång

            //Tränar modellen med data från JSON-filen
            observationLog.TrainModelFromFile();

            while (programRunning)
            {
                Console.Clear(); // Rensar konsollen vid varje menyval
                Console.WriteLine("Välkommen till appen för valobservationer!");
                Console.WriteLine("Välj ett alternativ (1-5) tryck sedan ENTER");
                Console.WriteLine(""); // Tom rad för mellanrum
                Console.WriteLine("1. Logga en valobservation");
                Console.WriteLine("2. Visa alla valobservationer");
                Console.WriteLine("3. Ta bort valobservation");
                Console.WriteLine("4. Förutsäg bästa platsen för valart");
                Console.WriteLine("5. Avsluta");

                string? input;

                // Loopa tills användaren matar in en giltig inmatning
                do
                {
                    input = Console.ReadLine(); // Inmatning från användaren

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Inmatningen kan inte vara tom, försök igen.");
                    }

                } while (string.IsNullOrWhiteSpace(input)); // Fortsätt att fråga tills inmatning inte är tom

                switch (input)
                {
                    case "1":
                        observationLog.LogObservation(); // Anropar metod för att logga observation
                        break;

                    case "2":
                        observationLog.ShowAllObservation(); // Anropar metod för att visa observationer
                        break;

                    case "3":
                        observationLog.RemoveObservation(); // Anropar metod för att ta bort observation
                        break;

                    case "4":
                        Console.Clear(); // rensar konsollen
                        string whaleType ;

                        do
                        {
                            Console.WriteLine("Ange valart du vill förutse bästa tid och plats att se: ");
                              whaleType = Console.ReadLine() ?? string.Empty; // Om null, tilldelas tom sträng

                              if (string.IsNullOrWhiteSpace(whaleType))
                              {
                                Console.WriteLine("Du måste skriva en valart, försök igen.");
                              }
                        } while (string.IsNullOrWhiteSpace(whaleType));
                        
                      

                        var whalePredictionModel = new WhalePredictionModel();

                        //förutsägelse för plats och månad för observation
                        var predictedPlace = whalePredictionModel.PredictBestPlace(whaleType, out int predictedMonth);

                        Console.WriteLine($"Den bästa platsen att se en {whaleType} är: {predictedPlace}");
                        Console.WriteLine($"Den bästa tiden på året är månad: {predictedMonth}");
                        Console.WriteLine("Tryck på valfri tangent för att återvända till menyn.");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Clear(); // rensar konsollen
                        Console.WriteLine("Är du säker på att du vill stänga av?");
                        Console.WriteLine("Tryck på valfri tangent för att stänga av");
                        Console.ReadKey(); // Vänta på att användaren trycker på en tangent innan programmet avslutas
                        programRunning = false; // Avslutar programmet
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen."); // Felmeddelande vid ogiltigt val
                        Console.ReadKey(); // Väntar på att användaren trycker på en tangent
                        break;
                }
            }

            Console.WriteLine("Programmet avslutas..."); // Meddelar att programmet avslutas
        }
    }
}