//Anne-Lii Hansen anha2324@student.miun.se
/*
En app för valobservationer där användaren kan registrera en observation av en val.
*/

using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace whaleObservationApp
{
    class Program
    {
         static ObservationLog observationLog = new ObservationLog(); //Klassen ObservationLog från ObservationLog.cs filen

        static void Main(string[] args)
        {
            bool programRunning = true; //variabel med true false 

            while (programRunning)
            {
                Console.Clear();//Rensar konsollen vid varje menyval
                Console.WriteLine("Välkommen till appen för valobservationer!");
                Console.WriteLine("Välj ett alternativ (1-4) tryck sedan ENTER");
                    Console.WriteLine("");//tom för mellanrum
                Console.WriteLine("1. Logga en valobservation");
                Console.WriteLine("2. Visa alla valobservationer");
                Console.WriteLine("3. Ta bort valobservation");
                Console.WriteLine("4. Avsluta");

                string? input = Console.ReadLine();

                 if (string.IsNullOrWhiteSpace(input)) // Kontrollera om inmatningen är tom eller endast innehåller blanksteg
                {
                    Console.WriteLine("Inmatningen kan inte vara tom, försök igen.");
                    Console.ReadKey();
                    continue; // Gå tillbaka till början av loopen utan att bearbeta input
                }

               switch (input)
               {
                case "1":
                observationLog.LogObservation();//anropar metod för att logga observation
                break;

                case "2":
                observationLog.ShowObservation();//anropar metod för att lista observationer
                break;

                case "3":
                observationLog.RemoveObservation();//anropar metod för att ta bort observation
                break;

                case "4":
                programRunning = false; //avslutar programmet
                break;

                default:
                Console.WriteLine("Ogiltigt val, försök igen.");//felmeddelande
                Console.ReadKey();//väntar på att användaren trycker på en tangent
                break;
               }
            }

            Console.WriteLine("Programmet avslutas...");//meddelar att programmet avslutas
        }
    }
}