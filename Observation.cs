namespace whaleObservationApp
{
    public class Observation
    {
        public DateTime Date { get; set; } // Datum för observationen
        public string Time { get; set; } // Tidpunkt för observationen (som text, exempel: "10:30")
        public string Place { get; set; } // Plats för observationen (exempel: "Kusten")
        public string Weather { get; set; } // Väderförhållanden (exempel: "Soligt")
        public string Whale { get; set; } // Typ av val (exempel: "Blåval")
        public int Number { get; set; } // Antal observerade valar


        public Observation(DateTime date, string time, string place, string weather, string whale, int number)
        {
            Date = date;
            Time = time;
            Place = place;
            Weather = weather;
            Whale = whale;
            Number = number;
        }

        public override string ToString()
        {
            return $"Datum: {Date.ToShortDateString()}, Tid: {Time}, Plats: {Place}, Väder: {Weather}, Valart: {Whale}, Antal: {Number}";
        }
    }
}