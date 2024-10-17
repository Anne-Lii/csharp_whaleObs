namespace whaleObservationApp
{
    public class Observation
    {
        public string Beskrivning {get; set;}
        public DateTime Datum {get; set;}
        public Observation(string beskrivning)
        {
            Beskrivning = beskrivning;
            Datum = DateTime.Now; //Datumet för när loggningen skapas
        }

        public override string ToString()
        {
            return $"Observation: {Beskrivning}, Datum: {Datum}";
        }
    }
}