using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTraining
{
    public class DebugHA
    {
        public static void JustDebug()
        {
            try
            {
                FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Helper.MyConstants.FACESUBSCRIPTIONKEY), new System.Net.Http.DelegatingHandler[] { });
                faceClient.Endpoint = Helper.MyConstants.FACEENDPOINT;

                //faceClient.PersonGroup.CreateAsync("vlbg", "vorarlberg").GetAwaiter().GetResult();
                //Person p = faceClient.PersonGroupPerson.CreateAsync("vlbg", "Harry").GetAwaiter().GetResult();

                //    using (Stream imageStream = File.OpenRead(@"E:\TRAININGSDATEN\FACE\12965664X_Grabherr_Elmar\233c523c8213e3783035956bfe26d545e61cffc0.jpg"))
                //    {
                //        faceClient.PersonGroupPerson.AddFaceFromStreamAsync("vlbg", p.PersonId, imageStream).GetAwaiter().GetResult();
                //    }

                //    faceClient.PersonGroup.TrainAsync("vlbg").GetAwaiter().GetResult();
                //    Console.WriteLine(faceClient.PersonGroup.GetTrainingStatusWithHttpMessagesAsync("vlbg").Status);

                faceClient.PersonGroup.TrainAsync(Helper.MyConstants.PERSONGROUPID).GetAwaiter().GetResult();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


     

    }
}
