using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class FaceTrainingImage
    {
        private FileInfo _facetrainfile;
        private bool _isValidTrainFile = false;

        /// <summary>
        /// Fileinfo zur Trainingsdatei
        /// </summary>
        public FileInfo Facetrainfile { get => _facetrainfile; }

        /// <summary>
        /// Trainingsstatus
        /// </summary>
        public bool IsValidTrainFile { get => _isValidTrainFile; }

        /// <summary>
        /// Konsturktor für Trainingsdatei
        /// </summary>
        /// <param name="pFaceTrainFile">Fileinfo zur Trainingsdatei</param>
        /// <param name="pIsValidTrainFile">Trainingsstatus</param>
        public FaceTrainingImage(FileInfo pFaceTrainFile, bool pIsValidTrainFile)
        {
            _facetrainfile = pFaceTrainFile;
            _isValidTrainFile = pIsValidTrainFile;
        }

        /// <summary>
        /// URL zur Trainingsdatei
        /// </summary>
        /// <returns>URL zur Trainingsdatei als string</returns>
        public string GetFaceTrainingImageUrl()
        {
            return Helper.MyConstants.HTTPTRAINDATAURL + "face/" + Facetrainfile.Directory.Name + "/" + Facetrainfile.Name;
        }


    }
}
