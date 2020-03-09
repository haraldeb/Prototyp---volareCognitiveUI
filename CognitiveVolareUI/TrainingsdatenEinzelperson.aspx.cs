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
    public partial class TrainingsdatenEinzelperson : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _name = string.Empty;

            try
            {
                _name = Request.QueryString["name"].ToString();
            }
            catch (Exception)
            {

               
            }

            foreach (FaceTrainingPerson p in Helper.TrainImages.GetFaceTrainingPersons())
            {
                if (p.PathName == _name)
                {
                    lblName.Text = p.PersonName;
                    lblGND.Text = string.Format("GND-Nummer: <a href=\"{0}\" target=\"_blank\">{1}</a>", p.GetGNDLink(), p.GndNumber);
                    ltContent.Text = "<div class=\"erkanntepersonen\">";
                    foreach (FaceTrainingImage item in p.TrainingFaces)
                    {
                        string _isvalid = "redbg";
                        string _kistatus = "Ungültig und im Modell nicht aufgenommen";
                        if (item.IsValidTrainFile)
                        {
                            _isvalid = "greenbg";
                            _kistatus = "Gültig und im Modell aufgenommen";
                        }
                            

                        ltContent.Text += string.Format("<div class=\"erkannteperson {0}\">",_isvalid);
         

                        ltContent.Text += string.Format("<div><img src=\"{0}\" /></div><div><p>Status: {1}</p></div>", item.GetFaceTrainingImageUrl(), _kistatus);
                        ltContent.Text += "</div>";
                    }

                    ltContent.Text += "</div>";
                }


                


               
            }
            

        }

       
    }
}