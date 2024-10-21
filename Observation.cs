/*
Anne-Lii Hansen anha2324@student.miun.se
En app för valobservationer där användaren kan registrera en observation av en val.
*/

namespace whaleObservationApp
{
    public class Observation
    {
        public DateTime Date { get; set; } // Datum för observationen (format: "ÅÅÅÅ-MM-DD")
        public string Time { get; set; } // Tidpunkt för observationen (format: "10:30")
        public string Place { get; set; } // Fritext för plats
        public string Whale { get; set; } // Typ av val (exempel: "Blåval")
        public int Number { get; set; } // Antal observerade valar

        // Konstruktor för observation med plats
        public Observation(DateTime date, string time, string place, string whale, int number)
        {
            Date = date;
            Time = time;
            Place = place;
            Whale = whale;
            Number = number;
        }

        public override string ToString()
        {
            return $"Datum: {Date.ToShortDateString()}, Tid: {Time}, Plats: {Place}, Valart: {Whale}, Antal: {Number}";
        }
    }
}
