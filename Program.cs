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
        static List<Observation> observationer = new List<Observation>(); //Klassen Observation från Observation.cs filen
        static void Main(string[] args)
        {
            bool programRunning = true; //variabel med true false 

            while (programRunning)
            {
                Console.Clear();//Rensar konsollen vid varje menyval
                Console.WriteLine("Välkommen till appen för valobservationer!");
                Console.WriteLine("Välj ett alternativ (1-4)");
                Console.WriteLine("1. Logga en valobservation");
                Console.WriteLine("2. Visa alla valobservationer");
                Console.WriteLine("3. Ta bort valobservation");
                Console.WriteLine("4. Avsluta");

                string? input = Console.ReadLine();

               switch (input)
               {
                case "1":
                LoggObservation();//anropar metod för att logga observation
                break;

                case "2":
                ShowObservation();//anropar metod för att lista observationer
                break;

                case "3":
                RemoveObservation();//anropar metod för att ta bort observation
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