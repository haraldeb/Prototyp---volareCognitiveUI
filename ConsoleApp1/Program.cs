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
    class Program
    {
        static void Main(string[] args)
        {
            //MyFaceDetect mfd = new MyFaceDetect();
            //mfd.Run();
            //DebugHA.JustDebug();

            MyFaceTrainer train = new MyFaceTrainer();
            train.Run();
            //Console.ReadLine();
            //MyFaceIdentify id = new MyFaceIdentify();
            //id.Run();

        }
    }
}
