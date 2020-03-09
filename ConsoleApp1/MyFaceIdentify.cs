using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;


namespace FaceTraining
{
    public class MyFaceIdentify
    {
        //https://docs.microsoft.com/de-de/azure/cognitive-services/face/quickstarts/csharp-detect-sdk

        private static readonly FaceAttributeType[] faceAttributes = { FaceAttributeType.Age, FaceAttributeType.Gender };

        public MyFaceIdentify()
        {
                  

        }

        public void Run()
        {
          

            FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Helper.MyConstants.SubscriptionKey), new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = Helper.MyConstants.FACEENDPOINT;

            //string testImageFile = @"C:\Users\lvng\Desktop\EberleH2.jpg";
            //string testImageFile = @"C:\Users\lvng\Desktop\wallner.jpg";
            string testImageFile = @"C:\Users\lvng\Desktop\volare_o_128485.jpg";
            Console.WriteLine("Starte Bildidentifizierung für: " + testImageFile);
            string personGroupId = Helper.MyConstants.PERSONGROUPID;

            using (Stream s = File.OpenRead(testImageFile))
            {
                try
                {
                    var faces = faceClient.Face.DetectWithStreamAsync(s, true).GetAwaiter().GetResult();
                    //var faces = faceClient.Face.DetectWithUrlAsync(url, true).GetAwaiter().GetResult();
                    var faceids = faces.Select(e => (Guid)e.FaceId).ToList();
                    var identifyResults = faceClient.Face.IdentifyAsync(faceids, personGroupId).GetAwaiter().GetResult();
                    foreach (var result in identifyResults)
                    {
                        if (result.Candidates.Count == 0)
                        {
                            Console.WriteLine("Niemand Identifiziert");

                        }
                        else
                        {
                            var candidateId = result.Candidates[0].PersonId;
                            var person = faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId).GetAwaiter().GetResult();
                            Console.WriteLine("Identifiziert als " + person.Name + " (" + result.Candidates[0].Confidence*100 +"% Sicherheit)");
                        }
                    }
                     
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    
                }
                Console.ReadLine();

            }



        }






    }

}

