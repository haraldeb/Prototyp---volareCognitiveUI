using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ObjectValue
    {
        private string _value = string.Empty;
        private double _confidence = 0.0;
        private double _x = 0.0;
        private double _w = 0.0;
        private double _y = 0.0;
        private double _h = 0.0;
        private double _imgwidth = 0.0;
        private double _imgheight = 0.0;
        private string _volareID = string.Empty;
        private double _rot = 0;

        /// <summary>
        /// Erstellt ein neues Objekt
        /// </summary>
        /// <param name="pVolareID">volare-ID</param>
        /// <param name="pValue">Wert</param>
        /// <param name="pConfidence">Erkennungsgenauigkeit</param>
        /// <param name="pImgWidth">Bildbreite</param>
        /// <param name="pImgHeight">Bildhöhe</param>
        public ObjectValue(string pVolareID, string pValue, double pConfidence, double pImgWidth, double pImgHeight)
        {
            _volareID = pVolareID;
            _value = pValue;
            _confidence = pConfidence;
            _imgwidth = pImgWidth;
            _imgheight = pImgHeight;
        }

        /// <summary>
        /// Erstellt ein neues Objekt
        /// </summary>
        /// <param name="pVolareID">volare-ID</param>
        /// <param name="pValue">Wert</param>
        /// <param name="pConfidence">Erkennungsgenauigkeit</param>
        /// <param name="pImgWidth">Bildbreite</param>
        /// <param name="pImgHeight">Bildhöhe</param>
        /// <param name="pX">X-Koordinate der Linken oberen Ecke</param>
        /// <param name="pW">Breite der BoundingBox</param>
        /// <param name="pY">Y-Koordinate der Linken oberen Ecke</param>
        /// <param name="pH">Höhe der BoundingBox</param>
        public ObjectValue(string pVolareID, string pValue, double pConfidence, double pImgWidth, double pImgHeight, double pX, double pW, double pY, double pH)
        {
            _volareID = pVolareID;
            _value = pValue;
            _confidence = pConfidence;
            _imgwidth = pImgWidth;
            _imgheight = pImgHeight;
            _x = pX;
            _w = pW;
            _y = pY;
            _h = pH;
        }
        /// <summary>
        /// Erstellt ein neues Objekt
        /// </summary>
        /// <param name="pVolareID">volare-ID</param>
        /// <param name="pValue">Wert</param>
        /// <param name="pConfidence">Erkennungsgenauigkeit</param>
        /// <param name="pImgWidth">Bildbreite</param>
        /// <param name="pImgHeight">Bildhöhe</param>
        /// <param name="pX">X-Koordinate der Linken oberen Ecke</param>
        /// <param name="pW">Breite der BoundingBox</param>
        /// <param name="pY">Y-Koordinate der Linken oberen Ecke</param>
        /// <param name="pH">Höhe der BoundingBox</param>
        /// <param name="PRot">Drehung des Bildes</param>
        public ObjectValue(string pVolareID, string pValue, double pConfidence, double pImgWidth, double pImgHeight, double pX, double pW, double pY, double pH, double PRot)
        {
            _volareID = pVolareID;
            _value = pValue;
            _confidence = pConfidence;
            _imgwidth = pImgWidth;
            _imgheight = pImgHeight;
            _x = pX;
            _w = pW;
            _y = pY;
            _h = pH;
            _rot = PRot;
        }


        /// <summary>
        /// Liefert die Image-Server URL des gewünschten Bildbereiches
        /// </summary>
        /// <returns>URL zum Bildausschnitt</returns>
        public string GetObjectExcerptUrl()
        {
            double x = ((Double)_x / (Double)_imgwidth);
            double y = ((Double)_y / (Double)_imgheight);
            double w = ((Double)_w / (Double)_imgwidth);
            double h = ((Double)_h / (Double)_imgheight);

            return string.Format("https://imageserver.phaidra.org/iipsrv/iipsrv.fcgi?FIF={4}&HEI=800&RGN={0},{1},{2},{3}&ROT={5}&QLT=99&CVT=jpeg", x.ToString().Replace(',', '.'), y.ToString().Replace(',', '.'), w.ToString().Replace(',', '.'), h.ToString().Replace(',', '.'), PhaidraAPI.ImageServerHash.GetImageServerPath(_volareID), 0);
        }
        /// <summary>
        /// Wert des Inhalts
        /// </summary>
        public string Value { get => _value;}

        /// <summary>
        /// Erkennungsgenauigkeit in Prozent, auf zwei Stellen gerundet
        /// </summary>
        public double Confidence { get => Math.Round(_confidence*100, 2); }

        /// <summary>
        /// X-Koordinate der linken oberen Ecke
        /// </summary>
        public double X { get => _x; }
        /// <summary>
        /// Breite der Bounding-Box
        /// </summary>
        public double W { get => _w; }
        /// <summary>
        /// Y-Koordinate der linken oberen Ecke
        /// </summary>
        public double Y { get => _y; }
        /// <summary>
        /// Höhe der Bounding-Box
        /// </summary>
        public double H { get => _h; }
    }
}
