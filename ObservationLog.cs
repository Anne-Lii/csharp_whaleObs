using System;
using System.Collections.Generic;

namespace whaleObservationApp
{
    public class ObservationLog
    {
        private List<Observation> observationer = new List<Observation>();
        
        //Metod för att lägga till en observation
        public void LogObservation()
        {
            Console.Clear();//Rensar konsollen
            Console.WriteLine("Beskriv observationen; ");//ber användaren att beskriva observationen
            string? beskrivning = Console.ReadLine();//input från användaren
            Observation newObservation = new Observation(beskrivning);
            observationer.Add(newObservation);//lägger till nya observationen till listan

            Console.WriteLine("Observation tillagd! ");
            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }

        //Metod för att lägga till en observation
        public void ShowObservation()
        {
            Console.Clear();//Rensar konsollen

            if (observationer.Count == 0)
            {
                Console.WriteLine("Det finns inga observationer.");
            } else
            {
                Console.WriteLine("Valobservationer: ");
                for (int i = 0; i < observationer.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {observationer[i]}");
                }
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
            } else
            {
                Console.WriteLine("Valobservationer: ");
                for (int i = 0; i < observationer.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {observationer[i]}");
                }

                Console.Write("Ange numret på observationen du vill ta bort:");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <=observationer.Count)
                {
                    observationer.RemoveAt(index - 1);
                    Console.WriteLine("Observation borttagen!");

                } else
                {
                    Console.WriteLine("Ogiltigt val, försök igen!");
                }
            }

            Console.ReadKey();//väntar på att användaren trycker på en tangent för att återgå till meny
        }
    }
}