using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class PlaceTrainingImage
    {
        private FileInfo _placetrainfile;
        private bool _isValidTrainFile = false;

        public FileInfo PlaceTrainFile { get => _placetrainfile; }
        public bool IsValidTrainFile { get => _isValidTrainFile; }
        /// <summary>
        /// Trainingsbild für landeskundliches Element
        /// </summary>
        /// <param name="pPlaceTrainFile">Bildatei</param>
        /// <param name="pIsValidTrainFile">Validität</param>
        public PlaceTrainingImage(FileInfo pPlaceTrainFile, bool pIsValidTrainFile)
        {

            _placetrainfile = pPlaceTrainFile;
            _isValidTrainFile = pIsValidTrainFile;

        }
        /// <summary>
        /// Generiert eine BildURL zum Trainingsbild
        /// </summary>
        /// <returns>URL</returns>
        public string GetPlaceTrainingImageUrl()
        {
            return Helper.MyConstants.HTTPTRAINDATAURL + "place/" + PlaceTrainFile.Directory.Name + "/" + PlaceTrainFile.Name;
        }


    }
}
