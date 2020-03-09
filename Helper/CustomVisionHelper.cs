using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CustomVisionHelper
    {

        private List<CustomVisionValue> _myCustomVisionValues = new List<CustomVisionValue>();
        public List<CustomVisionValue> MyCustomVisionValues { get => _myCustomVisionValues; }

        /// <summary>
        /// Prüft ein Eingangsbild auf landeskundliche Elemente
        /// </summary>
        /// <param name="pImageUrl">öffentlich erreichbare URL zum Image</param>
        public CustomVisionHelper(string pImageUrl)
        {
            try
            {
                // Http-Client für Call
                var client = new HttpClient();

                // Prediction-Key im Header mitgeben
                client.DefaultRequestHeaders.Add("Prediction-Key", MyConstants.CUSTOMVISIONPREDICTIONSUBSCRIPTIONKEY);

                // Prediction URL
                string url = MyConstants.CUSTOMVISIONPREDICTIONENDPOINT;

                // Antwort
                HttpResponseMessage response;

                // Image URL als Payload mitgeben
                string stringPayload = "{ \"Url\": \"" + pImageUrl + "\"}";
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                // Antwort abwarten
                response = client.PostAsync(MyConstants.CUSTOMVISIONPREDICTIONENDPOINT, httpContent).Result;

                // Antwort Deserialisieren und neues CustomVisionPrediction-Objekt erstellen
                JsonSerializer serializer = new JsonSerializer();
                Helper.CustomVisionPrediction custom = JsonConvert.DeserializeObject<Helper.CustomVisionPrediction>(response.Content.ReadAsStringAsync().Result.ToString());

                //Alle erkannten Objekte durchgehen und der Liste hinzufügen
                foreach (Prediction item in custom.predictions)
                {
                    _myCustomVisionValues.Add(new CustomVisionValue(item.tagName, item.probability));
                }
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("CustomVisionHelper.cs - CustomVisionHelper", e.Message);
            }
        }
    }
}
