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

            while (programRunning)
            {
                Console.Clear(); // Rensar konsollen vid varje menyval
                Console.WriteLine("Välkommen till appen för valobservationer!");
                Console.WriteLine("Välj ett alternativ (1-4) tryck sedan ENTER");
                Console.WriteLine(""); // Tom rad för mellanrum
                Console.WriteLine("1. Logga en valobservation");
                Console.WriteLine("2. Visa alla valobservationer");
                Console.WriteLine("3. Ta bort valobservation");
                Console.WriteLine("4. Avsluta");

                string? input = Console.ReadLine();//inmatning från användaren

                if (string.IsNullOrWhiteSpace(input)) // Kontrollera om inmatningen är tom eller endast innehåller blanksteg
                {
                    Console.WriteLine("Inmatningen kan inte vara tom, försök igen.");
                    Console.ReadKey();
                    continue; // Gå tillbaka till början av loopen utan att bearbeta input
                }

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