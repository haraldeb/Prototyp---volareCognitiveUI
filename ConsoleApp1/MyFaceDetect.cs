using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.IO;


namespace FaceTraining
{
    public class MyFaceDetect
    {
        //https://docs.microsoft.com/de-de/azure/cognitive-services/face/quickstarts/csharp-detect-sdk
        private const string subscriptionKey = "5ff7d6dde0184ecc9e35d7fa6aebb58f";
        private const string faceEndpoint = "https://volareface.cognitiveservices.azure.com";
        private const string localImagePath = @"C:\Users\lvng\Downloads\volare_o_161021.jpg";
        private const string remoteImageUrl = "https://imageserver.phaidra.org/iipsrv/iipsrv.fcgi?FIF=/volare/5/d/5d5735874b782379399dd7fc4b57dec7d5984f59.tif&HEI=600&RGN=0.32033426183844,0.134909596662031,0.373259052924791,0.461752433936022&QLT=98&CVT=jpeg";

        private static readonly FaceAttributeType[] faceAttributes = { FaceAttributeType.Age, FaceAttributeType.Gender };


        public MyFaceDetect()
        {
                  

        }

        public void Run()
        {

             FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = faceEndpoint;

            Console.WriteLine("Faces being detected ...");
            var t1 = DetectRemoteAsync(faceClient, remoteImageUrl);
            var t2 = DetectLocalAsync(faceClient, localImagePath);

            Task.WhenAll(t1, t2).Wait(5000);
            //Task.WhenAll(t1).Wait(5000);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }

        // Detect faces in a remote image
        private static async Task DetectRemoteAsync(
            FaceClient faceClient, string imageUrl)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Console.WriteLine("\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                return;
            }

            try
            {
                IList<DetectedFace> faceList =
                    await faceClient.Face.DetectWithUrlAsync(
                        imageUrl, true, false, faceAttributes);

                DisplayAttributes(GetFaceAttributes(faceList, imageUrl), imageUrl);
            }
            catch (APIErrorException e)
            {
                Console.WriteLine(imageUrl + ": " + e.Message);
            }
        }

        // Detect faces in a local image
        private static async Task DetectLocalAsync(FaceClient faceClient, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine(
                    "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                return;
            }

            try
            {
                using (Stream imageStream = File.OpenRead(imagePath))
                {
                    IList<DetectedFace> faceList =
                            await faceClient.Face.DetectWithStreamAsync(
                                imageStream, true, false, faceAttributes);
                    DisplayAttributes(
                        GetFaceAttributes(faceList, imagePath), imagePath);
                }
            }
            catch (APIErrorException e)
            {
                Console.WriteLine(imagePath + ": " + e.Message);
            }
        }

        private static string GetFaceAttributes(
    IList<DetectedFace> faceList, string imagePath)
        {
            string attributes = string.Empty;

            foreach (DetectedFace face in faceList)
            {
                double? age = face.FaceAttributes.Age;
                string gender = face.FaceAttributes.Gender.ToString();
                attributes += gender + " " + age + "   ";
            }

            return attributes;
        }

        private static void DisplayAttributes(string attributes, string imageUri)
        {
            Console.WriteLine(imageUri);
            Console.WriteLine(attributes + "\n");
        }
    }

}

