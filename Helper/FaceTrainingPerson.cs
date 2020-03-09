using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    /// <summary>
    /// Individualisierte Person 
    /// </summary>
    public class FaceTrainingPerson
    {
        private List<FaceTrainingImage> _trainingFaces = new List<FaceTrainingImage>();
        private string _personName = string.Empty;
        private string _gndNumber = string.Empty;
        private string _pathName = string.Empty;

        /// <summary>
        /// Liste aller Trainingsbilder zur Person
        /// </summary>
        public List<FaceTrainingImage> TrainingFaces { get => _trainingFaces; set => _trainingFaces = value; }

        /// <summary>
        /// Name der Person
        /// </summary>
        public string PersonName { get => _personName; }

        /// <summary>
        /// GND Nummer der Person
        /// </summary>
        public string GndNumber { get => _gndNumber; }

        /// <summary>
        /// Pfad zu den Trainingsdaten
        /// </summary>
        public string PathName { get => _pathName; }

        /// <summary>
        /// Person, nicht mit der GND individualisiert
        /// </summary>
        /// <param name="pPersonName">Name der Person</param>
        /// <param name="pPathName">Pfad zu den Trainingsdaten</param>
        public FaceTrainingPerson(string pPersonName, string pPathName)
        {
            _personName = pPersonName;
            _pathName = pPathName;
        }
        /// <summary>
        /// Person, mittels der GND individualisiert
        /// </summary>
        /// <param name="pPersonName">Name der Person</param>
        /// <param name="pPathName">Pfad zu den Trainingsdaten</param>
        /// <param name="pGndNumber">GND Nummer</param>
        public FaceTrainingPerson(string pPersonName, string pPathName, string pGndNumber)
        {
            _personName = pPersonName;
            _pathName = pPathName;
            _gndNumber = pGndNumber;
        }

        /// <summary>
        /// Gibt den Link zur GND aus
        /// </summary>
        /// <returns>GND Link als string</returns>
        public string GetGNDLink()
        {
            if (_gndNumber != string.Empty)
                return MyConstants.GNDURLBASIS + _gndNumber;
            else
                return string.Empty;
        }
    }
}
