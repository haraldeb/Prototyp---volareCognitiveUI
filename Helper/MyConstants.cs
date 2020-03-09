using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class MyConstants
    {
        // Gesichtserkennung
        private const string _FaceSubscriptionKey = "xxxxx";
        private const string _FaceEndpoint = "https://xxxxx.cognitiveservices.azure.com";
        private const string _PersonGroupID = "xxxxx";
        private const string _PersonGroupName = "Vorarlberger Personen";
        private const string _PathToPersonTrainImages = @"xxxxx";

        // Übersetzung
        private const string _TranslationSubscriptionKey = "xxxxx";
        private const string _translationLanguage = "de";

        // Landeskundliche Elemente (Custom Vision)
        private const string _CustomVisionPredictionSubscriptionKey = "xxxxx";
        private const string _CustomVisionPredictionEndpoint = "https://xxxxx.cognitiveservices.azure.com/customvision/v3.0/Prediction/xxxxx/classify/iterations/Iteration7/url";
        private const string _PathToPlaceTrainImages = @"xxxxx";

        // Objekterkennung (Microsoft Standardmodell)
        private const string _ObjectSubscriptionKey = "xxxxx";
        private const string _ObjectEndpoint = "https://xxxxx.cognitiveservices.azure.com/";
        
        // URL zu Trainingsdaten (für Darstellung in Web-UI)
        private const string _httpTrainDataUrl = "https://vlb-content.vorarlberg.at/xxxxx/";

        // Geonames, Username für API-Call
        private const string _geonamesAPIUserName = "xxxxx";
        private const string _geonamesAPIUrl = "http://api.geonames.org/getJSON?formatted=true&geonameId=";
        private const string _geonamesUrlBasis = "http://geonames.org/";


        //Google Maps API Key (für Kartendarstellung landeskundlicher Elemente)
        private const string _googleMapsAPIKey = "xxxxx";

        //Speicherort für MARC-Dateien
        private const string _PathToMARCFiles = @"xxxxx";
        private const string _UrlToMARCFiles = "https://vlb-content.vorarlberg.at/xxxxx/xxxxx/";

        //GND API
        private const string _GNDApiUrl = "https://lobid.org/gnd/";
        private const string _GNDUrlBasis = "http://d-nb.info/gnd/";

        //Logfile
        private const string _PathLogFile = @"xxxxx\logfile.txt";

        public static string FACESUBSCRIPTIONKEY { get => _FaceSubscriptionKey; }
        public static string OBJECTSUBSCRIPTIONKEY { get => _ObjectSubscriptionKey; }
        public static string TRANSLATIONSUBSCRIPTIONKEY { get => _TranslationSubscriptionKey; }
        public static string CUSTOMVISIONPREDICTIONSUBSCRIPTIONKEY { get => _CustomVisionPredictionSubscriptionKey; }
        public static string FACEENDPOINT { get => _FaceEndpoint; }
        public static string OBJECTENDPOINT { get => _ObjectEndpoint; }
        public static string CUSTOMVISIONPREDICTIONENDPOINT { get => _CustomVisionPredictionEndpoint; }
        public static string PERSONGROUPID { get => _PersonGroupID; }
        public static string PERSONGROUPNAME { get => _PersonGroupName; }
        public static string PATHTOPERSONTRAINIMAGES { get => _PathToPersonTrainImages; }
        public static string PATHTOPLACETRAINIMATES { get => _PathToPlaceTrainImages; }
        public static string HTTPTRAINDATAURL { get => _httpTrainDataUrl; }
        public static string TRANSLATIONLANGUAGE { get => _translationLanguage; }
        public static string GEONAMESUSERNAME { get => _geonamesAPIUserName; }
        public static string GEONAMESURLBASIS { get => _geonamesUrlBasis; }
        public static string GEONAMESAPIURL { get => _geonamesAPIUrl; }
        public static string GOOGLEMAPSAPIKEY { get => _googleMapsAPIKey; }
        public static string PATHTOMARCFILES { get => _PathToMARCFiles; }
        public static string URLTOMARCFILES { get => _UrlToMARCFiles; }
        public static string GNDAPIURL { get => _GNDApiUrl; }
        public static string GNDURLBASIS { get => _GNDUrlBasis; }
        public static string PATHTOLOGFILE { get => _PathLogFile; }
        
    }
}
