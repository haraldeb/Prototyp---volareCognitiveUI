using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helper
{
    public static class TrainImages
    {
        /// <summary>
        /// Liefert Trainingsdaten aller Personen
        /// </summary>
        /// <returns>Liste an FacetrainingPersons</returns>
        public static List<FaceTrainingPerson> GetFaceTrainingPersons()
        {
            List<FaceTrainingPerson> _myTrainPersons = new List<FaceTrainingPerson>();

            try
            {
                // Trainingsverzeichnis auslesen
                string[] _trainDirs = Directory.GetDirectories(Helper.MyConstants.PATHTOPERSONTRAINIMAGES);
                foreach (string _trainDir in _trainDirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(_trainDir);
                    FileInfo[] trainImages = dirInfo.GetFiles();

                    FaceTrainingPerson facetrainpers;

                    // Wenn Name eine GND-Nummer erhält, diese der FaceTrainingsperson hinzufügen
                    if (dirInfo.Name.Split('!').Length > 1)
                        facetrainpers = new FaceTrainingPerson(dirInfo.Name.Split('!')[1].Replace("_", ", "), dirInfo.Name, dirInfo.Name.Split('!')[0]);
                    else
                        facetrainpers = new FaceTrainingPerson(dirInfo.Name.Replace("_", ", "), dirInfo.Name);

                    // Trainingsimages der Person hinzufügen
                    foreach (FileInfo trainFile in trainImages)
                    {
                        FaceTrainingImage fti;

                        // Prüfen ob KI das Bild akzeptiert hat
                        if (trainFile.Name.Contains("_f"))
                            fti = new FaceTrainingImage(trainFile, false);
                        else
                            fti = new FaceTrainingImage(trainFile, true);

                        facetrainpers.TrainingFaces.Add(fti);
                    }
                    _myTrainPersons.Add(facetrainpers);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("Class TrainImages", e.Message);

            }

            return _myTrainPersons;




        }



        public static List<PlaceTrainingPlace> GetPlaceTrainingPlaces()
        {
            List<PlaceTrainingPlace> _myTrainPlaces = new List<PlaceTrainingPlace>();

            try
            {
                // Trainingsverzeichnis auslesen
                string[] _trainDirs = Directory.GetDirectories(Helper.MyConstants.PATHTOPLACETRAINIMATES);
                foreach (string _trainDir in _trainDirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(_trainDir);
                    FileInfo[] trainImages = dirInfo.GetFiles();

                    PlaceTrainingPlace placetrainplace;

                    // Wenn Name eine Geonames-Nummer erhält, diese dem Place hinzufügen
                    if (dirInfo.Name.Split('!').Length > 1)
                        placetrainplace = new PlaceTrainingPlace(dirInfo.Name.Split('!')[1].Replace("_", " "), dirInfo.Name, dirInfo.Name.Split('!')[0]);
                    else
                        placetrainplace = new PlaceTrainingPlace(dirInfo.Name.Replace("_", " "), dirInfo.Name);

                    // Trainingsimages der Person hinzufügen
                    foreach (FileInfo trainFile in trainImages)
                    {
                        PlaceTrainingImage pti;

                        // Prüfen ob KI das Bild akzeptiert hat
                        if (trainFile.Name.Contains("_f"))
                            pti = new PlaceTrainingImage(trainFile, false);
                        else
                            pti = new PlaceTrainingImage(trainFile, true);

                        placetrainplace.TrainingPlaces.Add(pti);
                    }
                    _myTrainPlaces.Add(placetrainplace);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("Class TrainImages", e.Message);

            }

            return _myTrainPlaces;




        }

    }
}
