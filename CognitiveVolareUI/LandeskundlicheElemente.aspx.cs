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
    public partial class LandeskundlicheElemente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnErkenneObjekte_Click(object sender, EventArgs e)
        {
            //Prüfen ob korrekte volare-oid übergeben wurde

            string _volareOID = HttpUtility.HtmlDecode(txtVolareObjektId.Text);

            if (_volareOID.StartsWith("o:") && PhaidraAPI.Phaidra.ErmittlePhaidraObjektTyp(_volareOID) == "Image")
            {

                string _imageUrl = string.Format("https://pid.volare.vorarlberg.at/ImageProxy.ashx?oid={0}&size=980", _volareOID);
                imgShowVolareImage.ImageUrl = _imageUrl;
                imgShowVolareImage.Visible = true;

                //Objekterkennung
                CustomVisionHelper cvhHelper = new CustomVisionHelper(_imageUrl);

                ltContent.Text = "<div class=\"erkannteobjekte\">";

                foreach (CustomVisionValue cvv in cvhHelper.MyCustomVisionValues)
                {
                    if (cvv.Confidence > 98)
                    {
                        //ltContent.Text += string.Format("<div class=\"erkanntesobjekt\"><div><h1>{0}</h1><p>Confidence: {1}%</p></div></div><div class=\"erkanntesobjekt\">{2}</div>", cvv.Name, cvv.Confidence, cvv.GoogleMapsHTMLDiv);
                        ltContent.Text += string.Format("<div><h1>{0}</h1><p>Geonames-ID: <a href=\"{1}\" target=\"_blank\">{2}</a></p><p>Confidence: {3}%</p></div>", cvv.Name, cvv.GeonamesLink, cvv.GeonamesNumber, cvv.Confidence, cvv.GoogleMapsHTMLDiv);
                    }
                }
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