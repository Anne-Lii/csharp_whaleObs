/*
Anne-Lii Hansen anha2324@student.miun.se
En app för valobservationer där användaren kan registrera en observation av en val.
*/

using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;

namespace whaleObservationApp
{
    public class WhaleData
    {
        public string WhaleType { get; set; } = string.Empty; // Tilldelar standardvärde
        public string Place { get; set; } = string.Empty; // Tilldelar standardvärde
        public float TimeOfYear { get; set; } //månad 1 - 12
    }

    public class WhalePrediction
    {
        public string Place { get; set; } = string.Empty; // Tilldelar standardvärde
        public float[] Score { get; set; } = Array.Empty<float>(); //största chansen för valobservation
    }


    public class WhalePredictionModel
    {
        private readonly string modelPath = "whaleModel.zip";
        private readonly MLContext mlContext;
        private ITransformer? trainedModel; // Nullable eftersom det tilldelas senare

        public WhalePredictionModel()
        {
            mlContext = new MLContext();
        }

        public void TrainModel(IEnumerable<WhaleData> data)
        {
            Console.WriteLine("Startar träning av modell...");

            var dataView = mlContext.Data.LoadFromEnumerable(data);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(WhaleData.Place)) // Konverterar 'Place' till en Key
                .Append(mlContext.Transforms.Text.FeaturizeText("WhaleTypeFeatures", nameof(WhaleData.WhaleType))) // FeaturizeText för WhaleType
                .Append(mlContext.Transforms.Concatenate("Features", "WhaleTypeFeatures", "TimeOfYear")) // Kombinerar features
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")) // Tränare för flervalsklassificering
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "Label")); // Konverterar tillbaka nyckeln till sitt ursprungliga värde

            // Träna modellen
            Console.WriteLine("Tränar modellen (detta kan ta flera minuter)...");
            trainedModel = pipeline.Fit(dataView);

            // Spara modellen till fil
            Console.WriteLine("Sparar modellen...");
            mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

            Console.WriteLine("Modell tränad och sparad till " + modelPath);
        }

        // Predict-metod
        public string PredictBestPlace(string whaleType, out int predictedMonth)
        {
            if (trainedModel == null) //kontrollerar om modellen redan är tränad eller ej
            {
                // Försök att ladda den sparade modellen om den inte redan är tränad
                if (File.Exists(modelPath))
                {
                    trainedModel = mlContext.Model.Load(modelPath, out _); 
                }
                else
                {
                    throw new InvalidOperationException("Modellen har inte tränats.");
                }
            }

            //mapping nummer till månad
            string[] month = {"Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "Oktober", "November", "December" };

            // Gör förutsägelse
            var predictionEngine = mlContext.Model.CreatePredictionEngine<WhaleData, WhalePrediction>(trainedModel);

            var currentMonth = DateTime.Now.Month;
            var prediction = predictionEngine.Predict(new WhaleData { WhaleType = whaleType, TimeOfYear = currentMonth });

            predictedMonth = currentMonth;

            // Om Score är en array, hitta det högsta värdet och returnera motsvarande plats
            if (prediction.Score != null && prediction.Score.Length > 0)
            {
                // Hitta indexet för den högsta poängen
                int bestIndex = Array.IndexOf(prediction.Score, prediction.Score.Max());

                // Använd bestIndex för att mappa till en plats
                // Här måste du ha en logik för att koppla bestIndex till den rätta platsen.
                // För tillfället returnerar vi indexet som sträng, men det ska mappas till plats
                return $"Plats med index {bestIndex}"; // Detta bör justeras beroende på hur din modell hanterar platser
            }

            return "Ingen plats förutsagd"; // Om ingen poäng fanns, returnera felmeddelande
        }
    }
}