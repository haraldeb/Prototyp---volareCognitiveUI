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
    public partial class Katalogisat : Page
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

                MARCGenerator marcgen = new MARCGenerator(string.Format("https://pid.volare.vorarlberg.at/ImageProxy.ashx?oid={0}&size=3000", _volareOID), _volareOID);

                string _imageUrl = string.Format("https://pid.volare.vorarlberg.at/ImageProxy.ashx?oid={0}&size=980", _volareOID);
                imgShowVolareImage.ImageUrl = _imageUrl;
                imgShowVolareImage.Visible = true;

                ltContent.Text = string.Format("Metadaten im Format MARC21, Regelwerk RDA | <a href=\"{0}\" target=\"_blank\">Als MARC-XML herunterladen</a>", marcgen.GetMarcFileUrl());
                //ltContent.Text += string.Format("<iframe type=\"application/xml\" src=\"{0}\" height=\"600\" width=\"100%\"></iframe>", marcgen.GetMarcFileUrl());
                ltContent.Text += string.Format("<br /><textarea class=\"txtarea\" rows=\"30\" cols=\"150\">{0}</textarea>", marcgen.GetRecordAsString());

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