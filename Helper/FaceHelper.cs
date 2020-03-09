using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Helper
{
    public class FaceHelper
    {
        /// <summary>
        /// Identifiziert Personen auf einem Bild
        /// </summary>
        /// <param name="pImageUrl">öffentlich zugängliche URL zum Bild</param>
        /// <returns>Datentransferobjekt mit den erkannten Personen</returns>
        public static DTOCognitivePerson IdentifyFaces(string pImageUrl)
        {
            DTOCognitivePerson _personen = new DTOCognitivePerson();

            FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(MyConstants.FACESUBSCRIPTIONKEY), new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = MyConstants.FACEENDPOINT;
            string personGroupId = MyConstants.PERSONGROUPID;

            // Alter und Geschlecht erkennen
            FaceAttributeType[] faceAttributes = { FaceAttributeType.Age, FaceAttributeType.Gender };

            try
            {
                // Gesichter erkennen
                var faces = faceClient.Face.DetectWithUrlAsync(pImageUrl, true, false, faceAttributes).GetAwaiter().GetResult();
                var faceids = faces.Select(e => (Guid)e.FaceId).ToList();
                // Gesichter idetifizieren
                var identifyResults = faceClient.Face.IdentifyAsync(faceids, personGroupId).GetAwaiter().GetResult();
                for (int i = 0; i < identifyResults.Count; i++)
                {
                    CognitivePerson myPers = new CognitivePerson();
                    myPers.DetFace = faces[i];
                    myPers.IdentRes = identifyResults[i];
                    if (identifyResults[i].Candidates.Count > 0)
                    {
                        var candidateId = identifyResults[i].Candidates[0].PersonId;
                        myPers.pers = faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId).GetAwaiter().GetResult();
                    }
                    else
                    {
                        // Wenn nicht individualisierbar, "Unbekannt" 
                        myPers.pers = new Person(new Guid(), "Unbekannt");
                    }
                    _personen.CognitivePeople.Add(myPers);
                }

            }
            catch (Exception e)
            {
                // Bei Fehler KI Message zurückgeben
                _personen.KIMessage = e.Message;
            }
            return _personen;
        }

    }
}