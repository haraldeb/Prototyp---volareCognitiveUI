using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CustomVisionValue
    {
        private string _geonamesNumber = string.Empty;
        private string _Name = string.Empty;
        private double _confidence = 0.0;
        private double _lat = 0.0;
        private double _lon = 0.0;
        private string _geonamesLink = string.Empty;
        private string _googleMapsDiv = string.Empty;

        /// <summary>
        /// Gibt die Geonames-ID zurück
        /// </summary>
        public string GeonamesNumber { get => _geonamesNumber; }
        /// <summary>
        /// Gibt den Namen des erkannten Objektes zurück
        /// </summary>
        public string Name { get => _Name;  }
        /// <summary>
        /// Gibt die Erkennungsgenauigkeit in Prozent des erkannten Objektes zurück
        /// </summary>
        public double Confidence { get => _confidence;  }

        /// <summary>
        /// Geografische Breite
        /// </summary>
        public double Lat { get => _lat;  }
        /// <summary>
        /// Geografische Höhe
        /// </summary>
        public double Lon { get => _lon; }
        /// <summary>
        /// Link zum Geonames Datensatz
        /// </summary>
        public string GeonamesLink { get => _geonamesLink; }
        /// <summary>
        /// Kartendarstellung als DIV-Container mittels GoogleMaps
        /// </summary>
        public string GoogleMapsHTMLDiv { get => _googleMapsDiv; }


        /// <summary>
        /// Wert eines erkannten landeskundlichen Objektes
        /// </summary>
        /// <param name="pPredictionTag">Beschreibung des erkannten Objektes</param>
        /// <param name="pConfidence">Genauigkeit der Erkennung</param>
        public CustomVisionValue(string pPredictionTag, double pConfidence)
        {
            try
            {
                // Geonames-ID aus TAG extrahieren
                _geonamesNumber = pPredictionTag.Split('!')[0];

                // Name aus TAG extrahieren
                _Name = pPredictionTag.Split('!')[1].Replace('_',' ');

                // Erkennungsgenauigkeit in Prozent
                _confidence = pConfidence * 100;
                _geonamesLink = MyConstants.GEONAMESURLBASIS + GeonamesNumber;
                if (_confidence > 20)
                {
                    // Koordinaten aus Geonames-API auslesen
                    GetLatLonFromGeonames();
                    if (_lat != 0 )
                    {
                        //Google-Maps Kartendarstellung
                        _googleMapsDiv = "<div id=\"map\"></div><script>function initMap() { var position = {lat: " + _lat.ToString().Replace(',','.') + ", lng: " + _lon.ToString().Replace(',', '.') + "}; var map = new google.maps.Map(document.getElementById(\'map\'), {zoom: 12, center: position}); var marker = new google.maps.Marker({position: position, map: map, title: \'" + _Name + "\'}); } </script><script async defer src=\"https://maps.googleapis.com/maps/api/js?key=" + MyConstants.GOOGLEMAPSAPIKEY + "&callback=initMap\"></script>";
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("CustomVisionValue.cs - CustomVisionValue", e.Message);
            }
        }

        /// <summary>
        /// Koordinate aus Geonames-API auslesen
        /// </summary>
        private void GetLatLonFromGeonames()
        {
            try
            {
                // Geonames API Abfragen
                Geoname mygeoname = JsonConvert.DeserializeObject<Geoname>(new WebClient().DownloadString(MyConstants.GEONAMESAPIURL + GeonamesNumber + "&username=" + MyConstants.GEONAMESUSERNAME + "&style=full"));

                // Geografische Breite auslesen
                Double.TryParse(mygeoname.lat.Replace('.',','), out _lat);
                // Geografische Höhe auslesen
                Double.TryParse(mygeoname.lng.Replace('.', ','), out _lon);
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("CustomVisionValue.cs - GetLatLonFromGeonames", e.Message);
            }
        }

    }
}
