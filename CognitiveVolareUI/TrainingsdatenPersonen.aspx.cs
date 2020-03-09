using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Helper;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CognitiveVolareUI
{
    public partial class TrainingsdatenPersonen : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltContent.Text = "<div class=\"erkanntepersonen\">";
            foreach (FaceTrainingPerson p in Helper.TrainImages.GetFaceTrainingPersons())
            {
                ltContent.Text += "<div class=\"erkannteperson\">";
                ltContent.Text += string.Format("<div><img src=\"{0}\" /></div><div><h4><a href=\"TrainingsdatenEinzelperson?name={4}\">{1}</a></h4><p>GND-Nummer: <a href=\"{5}\" target=\"_blank\">{2}</a></p><p>Anzahl Trainingsimages: {3}</p></div>", p.TrainingFaces[0].GetFaceTrainingImageUrl(), p.PersonName, p.GndNumber, p.TrainingFaces.Count, p.TrainingFaces[0].Facetrainfile.Directory.Name,p.GetGNDLink());
                ltContent.Text += "</div>";
            }
            ltContent.Text += "</div>";
        }
    }
}