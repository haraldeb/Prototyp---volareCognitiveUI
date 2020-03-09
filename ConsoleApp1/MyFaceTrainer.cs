using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.IO;
using System.Threading;

namespace FaceTraining
{

    public class MyFaceTrainer
    {
        //https://docs.microsoft.com/de-de/azure/cognitive-services/face/quickstarts/csharp-detect-sdk


        public MyFaceTrainer()
        {
            
        }

        public async void Run()
        {
            FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Helper.MyConstants.FACESUBSCRIPTIONKEY), new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = Helper.MyConstants.FACEENDPOINT;

            //PersonGroup erstellen
            string personGroupId = Helper.MyConstants.PERSONGROUPID;
            string personGroupName = Helper.MyConstants.PERSONGROUPNAME;

            try
            {
                Console.WriteLine(string.Format("Versuche PersonGroup mit ID: \"{0}\" zu erstellen", personGroupId));
                faceClient.PersonGroup.CreateAsync(personGroupId, personGroupName).GetAwaiter().GetResult();
                Console.WriteLine(string.Format("PersonGroup mit ID: \"{0}\" erstellt.", personGroupId));
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Fehler beim Anlegen der PersonGroup mit ID: \"{0}\"", personGroupId));
                Console.WriteLine(e.Message);
            }


            //Einlesen der TrainingsDaten
            Console.WriteLine(string.Format("Wo liegen die Trainingsdaten für die PersonGroup mit ID: \"{0}\"?", personGroupId));
            string _inputPath = Console.ReadLine();

            string[] _allDirectories = Directory.GetDirectories(_inputPath);

            foreach (string dir in _allDirectories)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                string _PersonName = string.Empty;
                string _GNDNummer = string.Empty;

                // Wenn Name eine GND-Nummer erhält, diese der FaceTrainingsperson hinzufügen
                if (dirInfo.Name.Split('!').Length > 1)
                {
                    _GNDNummer = dirInfo.Name.Split('!')[0];
                    _PersonName = dirInfo.Name.Split('!')[1].Replace("_", ", ");
                }
                else
                {
                    _PersonName = dirInfo.Name.Replace("_", ", ");

                }

                //Person anlegen
                Console.WriteLine(string.Format("Lege Person mit Namen \"{0}\" an", _PersonName));
                try
                {
                    //Hier
                    Person p = faceClient.PersonGroupPerson.CreateAsync(personGroupId, _PersonName, _GNDNummer).GetAwaiter().GetResult();
                    Console.WriteLine(string.Format("Person \"{0}\" hat die FaceId \"{1}\" erhalten.", _PersonName, p.PersonId));

                    //Person mit Bildern Trainieren
                    FileInfo[] _PersonImages = dirInfo.GetFiles("*.jpg");
                    foreach (FileInfo PersonImage in _PersonImages)
                    {
                        using (Stream imageStream = File.OpenRead(PersonImage.FullName))
                        {
                            Console.WriteLine(string.Format("Füge Person \"{0}\" das Bild \"{1}\" hinzu.", _PersonName, PersonImage.Name));
                            try
                            {
                                faceClient.PersonGroupPerson.AddFaceFromStreamAsync(personGroupId, p.PersonId, imageStream).GetAwaiter().GetResult();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine(string.Format("Bild \"{0}\" hat einen Fehler verursacht.", PersonImage.FullName));
                                File.Move(PersonImage.FullName, PersonImage.FullName.Replace(".jpg", "_f.jpg"));

                            }
                            
                            Thread.Sleep(3000); //Drei Sekunden Pause damit das Azure Free Kontingent nicht aufgebraucht wird.
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("FEHLER:");
                    Console.WriteLine(e.Message);
                }

            }


            //Alle Personen durch, Starte Training
            Console.WriteLine(string.Format("### Personen hinzugefügt, starte mit dem Training ###"));

            faceClient.PersonGroup.TrainAsync(personGroupId).GetAwaiter().GetResult();
            Console.WriteLine(faceClient.PersonGroup.GetTrainingStatusWithHttpMessagesAsync(personGroupId).Status);


            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceClient.PersonGroup.GetTrainingStatusAsync(personGroupId);

                if (trainingStatus.Status != TrainingStatusType.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }

            Console.WriteLine("### Training abgeschlossen ###");
            Console.ReadLine();











        }


    }
}
