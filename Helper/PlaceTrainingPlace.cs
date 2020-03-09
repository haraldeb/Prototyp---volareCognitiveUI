using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class PlaceTrainingPlace
    {
        private List<PlaceTrainingImage> _trainingPlaces = new List<PlaceTrainingImage>();
        private string _placeName = string.Empty;
        private string _geonames = string.Empty;
        private string _pathName = string.Empty;


        public List<PlaceTrainingImage> TrainingPlaces { get => _trainingPlaces; set => _trainingPlaces = value; }
        public string PlaceName { get => _placeName; }
        public string GeonamesID { get => _geonames; }
        public string PathName { get => _pathName; }



        /// <summary>
        /// Erstelt ein neues Landeskundliches Element
        /// </summary>
        /// <param name="pPlaceName">Name</param>
        /// <param name="pPathName">Pfad zum Bild als String</param>
        public PlaceTrainingPlace(string pPlaceName, string pPathName)
        {
            _placeName = pPlaceName;
            _pathName = pPathName;
        }
        /// <summary>
        /// Erstelt ein neues, via geonames verortes landeskundliches Element
        /// </summary>
        /// <param name="pPlaceName">Name</param>
        /// <param name="pPathName">Pfad zum Bild als String</param>
        /// <param name="pGeonamesID">Geonames ID</param>
        public PlaceTrainingPlace(string pPlaceName, string pPathName, string pGeonamesID)
        {
            _placeName = pPlaceName;
            _pathName = pPathName;
            _geonames = pGeonamesID;


        }

        /// <summary>
        /// Liefert die URL zum entsprechenden Geonames-Eintrag
        /// </summary>
        /// <returns></returns>
        public string GetGeonamesLink()
        {
            if (_geonames != string.Empty)
                return MyConstants.GEONAMESURLBASIS + _geonames;
            else
                return string.Empty;

        }


    }
}
