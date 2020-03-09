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
    public partial class TrainingsdatenLandeskundlicheElemente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltContent.Text = "<div class=\"erkanntepersonen\">";
            foreach (PlaceTrainingPlace p in Helper.TrainImages.GetPlaceTrainingPlaces())
            {
                ltContent.Text += "<div class=\"erkannteperson\">";
                ltContent.Text += string.Format("<div><img src=\"{0}\" /></div><div><h4><a href=\"TrainingsdatenEinzelLandeskundlicheElement?name={4}\">{1}</a></h4><p>Geonames-ID: <a href=\"{5}\" target=\"_blank\">{2}</a></p><p>Anzahl Trainingsimages: {3}</p></div>", p.TrainingPlaces[0].GetPlaceTrainingImageUrl(), p.PlaceName, p.GeonamesID, p.TrainingPlaces.Count, p.TrainingPlaces[0].PlaceTrainFile.Directory.Name,p.GetGeonamesLink());
                ltContent.Text += "</div>";
            }
            ltContent.Text += "</div>";
        }
    }
}