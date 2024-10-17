namespace whaleObservationApp
{
    public class Observation
    {
        public string Obs {get; set;}
        public DateTime Datum {get; set;}
        public Observation(string obs)
        {
            Obs = obs;
            Datum = DateTime.Now; //Datumet för när loggningen skapas
        }

        public override string ToString()
        {
            return $"Observation: {Obs}, Datum: {Datum}";
        }
    }
}