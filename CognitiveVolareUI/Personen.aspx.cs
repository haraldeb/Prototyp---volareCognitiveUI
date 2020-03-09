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
    public partial class Personen : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnErkennePersonen_Click(object sender, EventArgs e)
        {
            //Prüfen ob korrekte volare-oid übergeben wurde

            string _volareOID = HttpUtility.HtmlDecode(txtVolareObjektId.Text);

            if (_volareOID.StartsWith("o:") && PhaidraAPI.Phaidra.ErmittlePhaidraObjektTyp(_volareOID) == "Image")
            {

                imgShowVolareImage.ImageUrl = string.Format("https://pid.volare.vorarlberg.at/ImageProxy.ashx?oid={0}&size=980", _volareOID);
                imgShowVolareImage.Visible = true;

                //Personenerkennung
                DTOCognitivePerson _personen = Helper.FaceHelper.IdentifyFaces(string.Format("https://pid.volare.vorarlberg.at/ImageProxy.ashx?oid={0}&size=3000", _volareOID));

                //Image holen
                string imUrl = "https://volare.vorarlberg.at/preview/" + _volareOID + "/ImageManipulator/boxImage/3000/jpg";
                System.Net.WebRequest myRequest = System.Net.WebRequest.Create(imUrl);
                System.Net.WebResponse myResponse = myRequest.GetResponse();
                System.IO.Stream responseStream = myResponse.GetResponseStream();
                Bitmap bitmap2 = new Bitmap(responseStream);
                int breite = bitmap2.Width;
                int hoehe = bitmap2.Height;
                ltContent.Text = "<div class=\"erkanntepersonen\">";
                List<int> _estimatedYears = new List<int>();

                foreach (CognitivePerson p in _personen.CognitivePeople)
                {
                    double x = ((Double)p.DetFace.FaceRectangle.Left / (Double)breite);
                    double y = ((Double)p.DetFace.FaceRectangle.Top / (Double)hoehe);
                    double w = ((Double)p.DetFace.FaceRectangle.Width / (Double)breite);
                    double z = ((Double)p.DetFace.FaceRectangle.Width / (Double)hoehe);
                    string link = string.Format("https://imageserver.phaidra.org/iipsrv/iipsrv.fcgi?FIF={4}&HEI=800&RGN={0},{1},{2},{3}&QLT=98&CVT=jpeg", x.ToString().Replace(',', '.'), y.ToString().Replace(',', '.'), w.ToString().Replace(',', '.'), z.ToString().Replace(',', '.'), PhaidraAPI.ImageServerHash.GetImageServerPath(_volareOID));
                    double confidence = 0.0;
                    double age = 0.0;

                    if (p.IdentRes.Candidates.Count > 0)
                        confidence = p.IdentRes.Candidates[0].Confidence;


                    if (p.DetFace.FaceAttributes.Age != null)
                        age = Double.Parse(p.DetFace.FaceAttributes.Age.ToString());

                    ltContent.Text += string.Format("<div class=\"erkannteperson\"><div><img src=\"{0}\" /></div><div><h4>{1}</h1>", link, p.pers.Name);
                    if (p.pers.UserData != null)
                        ltContent.Text += string.Format("<p>GND-Nummer: <a target=\"_blank\" href=\"http://d-nb.info/gnd/{0}\">{0}</a></p>", p.pers.UserData);
                    if (p.pers.Name != "Unbekannt")
                        ltContent.Text += string.Format("<p>Confidence: {0}%</p>", confidence * 100);
                    ltContent.Text += string.Format("<p>Geschätztes Alter: {0}</p>", age);
                    if (p.GetYearOfBirth() != 0)
                        ltContent.Text += string.Format("<p>Geburtsjahr: {0}</p>", p.GetYearOfBirth());
                    if (p.GetYearOfDeath() != 0)
                        ltContent.Text += string.Format("<p>Sterbejahr: {0}</p>", p.GetYearOfDeath());

                    ltContent.Text += "</div></div>";

                }

                lblErrorMessage.Text = _personen.KIMessage;

                // Datierung ausgeben, wenn möglich
                lblEstYear.Text = string.Empty;
                if (_personen.GetEstimatedYear() != 0)
                    lblEstYear.Text = string.Format("<hr />Datierungsversuch über das geschätzte Alter der Personen mit den verfügbaren Lebensdaten aus der GND:<br />Das Bild könnte um das Jahr {0} entstanden sein.", _personen.GetEstimatedYear().ToString());
                else
                    lblEstYear.Text = string.Format("<hr />Keine per GND individualisierte Person erkannt, Bilddatierung nicht möglich");

                ltContent.Text += "</div>";
            }
            else
            {
                ltContent.Text = string.Format("<p style=\"color: red;\">ACHTUNG: Unter der ID {0} ist kein Objekt vom Typ Bild (Image) verfügbar. Bitte nur Objekt-IDs für Einzelbilder, nicht für Collections verwenden.</p>", _volareOID);
            }



        }

        protected void txtVolareObjektId_TextChanged(object sender, EventArgs e)
        {

        }


    }
}