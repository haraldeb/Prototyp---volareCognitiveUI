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
    public partial class Objekte : Page
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
                ObjectHelper oHelper = new ObjectHelper(_volareOID, _imageUrl);

                ltContent.Text = "<div class=\"erkannteobjekte\">";

                foreach (ObjectValue p in oHelper.GetImageObjects())
                {
                    ltContent.Text += string.Format("<div class=\"erkanntesobjekt\"><div><img src=\"{0}\" /></div><div><h4>{1}</h1><p>Confidence: {2}%</div></div>", p.GetObjectExcerptUrl(), p.Value, p.Confidence);
                }

                ltContent.Text += "</div>";

                ltContent.Text += "<h3>Kategorien</h3>";
                foreach (ObjectValue category in oHelper.GetImageCategories())
                {
                    ltContent.Text += "<p>" + category.Value + " (Confidence: " + category.Confidence + "%) </p>";
                }

                ltContent.Text += "<h3>Tags</h3>";
                foreach (ObjectValue tags in oHelper.GetImageTags())
                {
                    ltContent.Text += "<p>" + tags.Value + " (Confidence: " + tags.Confidence + "%) </p>";
                }

                ltContent.Text += "<h3>Zusammenfassung</h3>";
                ltContent.Text += "<p>" + oHelper.GetImageSummary().Value + " (Confidence: " + oHelper.GetImageSummary().Confidence + "%)</p>";

                if (oHelper.IsImageBlackWhite())
                {
                    ltContent.Text += "<h3>Farbmodus</h3>";
                    ltContent.Text += "<p>Graustufen</p>";
                }
                else
                {
                    ltContent.Text += "<h3>Farbmodus</h3>";
                    ltContent.Text += "<p>Coloriert</p>";
                }

                ltContent.Text += "<h3>Dominante Vordergrundfarbe</h3>";
                ltContent.Text += "<p>" + oHelper.GetDominantColorForeground() + "</p><span style=\"border: 1px solid black; background-color:" + oHelper.GetDominantColorForeground() + ";\">&nbsp;&nbsp;&nbsp;</span>";

                ltContent.Text += "<h3>Dominante Hintergrundfarbe</h3>";
                ltContent.Text += "<p>" + oHelper.GetDominantColorBackground() + "</p><span style=\"border: 1px solid black; background-color:" + oHelper.GetDominantColorBackground() + ";\">&nbsp;&nbsp;&nbsp;</span>";

                ltContent.Text += "<h3>Dominante Farben</h3>";
                foreach (string color in oHelper.GetDominantColors())
                {
                    ltContent.Text += "<p>" + color + "</p><span style=\"border: 1px solid black; background-color:" + color + ";\">&nbsp;&nbsp;&nbsp;</span>";
                }


                ltContent.Text += "<h3>Akzentfarbe</h3>";
                ltContent.Text += "<p>Hex: #" + oHelper.GetAccentColor() + "</p><span style=\"border: 1px solid black; background-color:#" + oHelper.GetAccentColor() + ";\">&nbsp;&nbsp;&nbsp;</span>";


                ltContent.Text += "<h3>Erkannter Text</h3>";
                ltContent.Text += "<p>" + oHelper.GetImageText() + "</p>";
                ltContent.Text += "<div class=\"erkannteobjekte\">";

                foreach (ObjectValue p in oHelper.GetTextObjects())
                {
                    ltContent.Text += string.Format("<div class=\"erkanntesobjekt\"><div><img src=\"{0}\" /></div><div><h4>{1}</h1></div></div>", p.GetObjectExcerptUrl(), p.Value);
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