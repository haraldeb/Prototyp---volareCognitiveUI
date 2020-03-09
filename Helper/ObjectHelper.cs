using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorService;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Helper
{

    public class ObjectHelper
    {
        private ImageAnalysis _results;
        private RecognizeTextHeaders _recognationTextHeaders;
        private TextOperationResult _textOperationResults;
        private TranslatorClient _translatorClient = new TranslatorClient(MyConstants.TRANSLATIONSUBSCRIPTIONKEY);
        private ComputerVisionClient _client;
        private int _imageWidth = 0;
        private int _imageHeight = 0;
        private string _volareID = string.Empty;

        public int ImageWidth { get => _imageWidth;  }
        public int ImageHeight { get => _imageHeight; }

        /// <summary>
        /// Objekt- und Texterkennung mit Hilfe des Azure-Standard-Modells. Texte werden von der KI in Englisch zurückgegeben, hier aber entsprechend übersetzt
        /// </summary>
        /// <param name="pVolareID">volare ID</param>
        /// <param name="pImageUrl">öffentlich zugängliche url zum bild</param>
        public ObjectHelper(string pVolareID, string pImageUrl)
        {
            _client = Authenticate(MyConstants.OBJECTENDPOINT, MyConstants.OBJECTSUBSCRIPTIONKEY);
            _volareID = pVolareID;

            //Kategorien, Beschreibung, Bildtyp, Tags, Farbmodus und Objekte erkennen
            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
            {
              VisualFeatureTypes.Categories,
              VisualFeatureTypes.Description,
              VisualFeatureTypes.ImageType,
              VisualFeatureTypes.Tags,
              VisualFeatureTypes.Color,
              VisualFeatureTypes.Objects,
            };

            // Details: Berühmtheiten und landschaftliche Objekte
            List<Details> details = new List<Details>()
            {
              Details.Celebrities,
              Details.Landmarks,
            };

            // Keine Berühmtheiten in der Beschreibung
            List<DescriptionExclude> descriptionExcludes = new List<DescriptionExclude>()
            {
                DescriptionExclude.Celebrities,
            };



            // Bild analysieren
            _results = _client.AnalyzeImageAsync(pImageUrl, features, details, null, descriptionExcludes, default(System.Threading.CancellationToken)).GetAwaiter().GetResult();

            // OCR durchführen
            _recognationTextHeaders = _client.RecognizeTextAsync(pImageUrl,TextRecognitionMode.Printed,default(System.Threading.CancellationToken)).GetAwaiter().GetResult();
            _textOperationResults = GetTextResult();

            // Bild breite und höhe
            _imageWidth = _results.Metadata.Width;
            _imageHeight = _results.Metadata.Height;
        }

        /// <summary>
        /// Bildbeschreibung
        /// </summary>
        /// <returns>Beschreibungstext, aus dem Englischen übersetzt</returns>
        public ObjectValue GetImageSummary()
        {
            ObjectValue _myOV = null;

            try
            {
                var response = _translatorClient.TranslateAsync(_results.Description.Captions.First().Text, to: MyConstants.TRANSLATIONLANGUAGE).GetAwaiter().GetResult();
                _myOV = new ObjectValue(_volareID, response.Translation.Text, _results.Description.Captions.First().Confidence, ImageWidth, _imageHeight);

            }
            catch (Exception e)
            {
                Logger.WriteLogLine("ObjectHelper.cs - GetImageSummary", e.Message);
            }
            return _myOV;       
        }

        /// <summary>
        /// Erkannte Kategorien
        /// </summary>
        /// <returns>List von Kategorien mit Erkennungsgenauigkeit, aus dem Englischen übersetzt </returns>
        public List<ObjectValue> GetImageCategories()
        {
            List<ObjectValue> _categories = new List<ObjectValue>();

            foreach (var category in _results.Categories)
            {
                try
                {
                    var response = _translatorClient.TranslateAsync(category.Name, to: MyConstants.TRANSLATIONLANGUAGE).GetAwaiter().GetResult();
                    _categories.Add(new ObjectValue(_volareID, response.Translation.Text, category.Score, ImageWidth, _imageHeight));
                }
                catch (Exception e)
                {
                    Logger.WriteLogLine("ObjectHelper.cs - GetImageCategories", e.Message);
                }
            }
            return _categories;
        }

        /// <summary>
        /// Erkannte Tags
        /// </summary>
        /// <returns>List von Tags mit Erkennungsgenauigkeit, aus dem Englischen übersetzt </returns>
        public List<ObjectValue> GetImageTags()
        {
            List<ObjectValue> _tags = new List<ObjectValue>();

            foreach (var tag in _results.Tags)
            {
                try
                {
                    var response = _translatorClient.TranslateAsync(tag.Name, to: MyConstants.TRANSLATIONLANGUAGE).GetAwaiter().GetResult();
                    _tags.Add(new ObjectValue(_volareID, response.Translation.Text, tag.Confidence, ImageWidth, _imageHeight));
                }
                catch (Exception e)
                {
                    Logger.WriteLogLine("ObjectHelper.cs - GetImageTags", e.Message);
                }
            }
            return _tags;
        }


        /// <summary>
        /// Erkannte Objekte mit Bounding-Box
        /// </summary>
        /// <returns>List von Objekten mit Erkennungsgenauigkeit, aus dem Englischen übersetzt</returns>
        public List<ObjectValue> GetImageObjects()
        {
            List<ObjectValue> _objects = new List<ObjectValue>();

            foreach (var obj in _results.Objects)
            {
                try
                {
                    // Personen ausschließen
                    if (obj.ObjectProperty.ToLower() != "person")
                    {
                        var response = _translatorClient.TranslateAsync(obj.ObjectProperty, to: MyConstants.TRANSLATIONLANGUAGE).GetAwaiter().GetResult();
                        _objects.Add(new ObjectValue(_volareID, response.Translation.Text, obj.Confidence, ImageWidth, _imageHeight, obj.Rectangle.X, obj.Rectangle.W, obj.Rectangle.Y, obj.Rectangle.H));
                    }
                     
                }
                catch (Exception e)
                {
                    Logger.WriteLogLine("ObjectHelper.cs - GetImageObjects", e.Message);
                }
            }
            return _objects;
        }


        /// <summary>
        /// Farbschema des Eingangsbilds
        /// </summary>
        /// <returns>true: Bild ist schwarz-weiß, false: Bild ist farbig</returns>
        public bool IsImageBlackWhite()
        {
            try
            {
                return _results.Color.IsBWImg;
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("ObjectHelper.cs - IsImageBlackWhite", e.Message);
                return false;
            }
        }

            /// <summary>
            /// Dominierende Vordergrundfarbe
            /// </summary>
            /// <returns>Farbe als String</returns>
            public string GetDominantColorForeground()
            {
                try
                {
                    return _results.Color.DominantColorForeground;
                }
                catch (Exception e)
                {
                    Logger.WriteLogLine("ObjectHelper.cs - GetDominantColorForeground", e.Message);
                    return "Unbekannt";
                }
            }

        /// <summary>
        /// Dominierende Hintergrundfarbe
        /// </summary>
        /// <returns>Farbe als String</returns>
        public string GetDominantColorBackground()
        {
            try
            {
                return _results.Color.DominantColorBackground;
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("ObjectHelper.cs - GetDominantColorBackground", e.Message);
                return "Unbekannt";
            }
        }

        /// <summary>
        /// Dominierende Farben
        /// </summary>
        /// <returns>Farben als String</returns>
        public List<string> GetDominantColors()
        {
            try
            {
                return _results.Color.DominantColors.ToList();
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("ObjectHelper.cs - GetDominantColor", e.Message);
                return new List<string>();
            }
        }


        /// <summary>
        /// Akzentfarbe
        /// </summary>
        /// <returns>Farbe als HEX-Wert</returns>
        public string GetAccentColor()
        {
            try
            {
                return _results.Color.AccentColor;
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("ObjectHelper.cs - GetAccentColor", e.Message);
                return "Unbekannt";
            }
        }


        /// <summary>
        /// Mittels OCR erkannter Text auf dem Eingangsbild
        /// </summary>
        /// <returns>Kompletten Text als String</returns>
        public string GetImageText()
        {
            if (_textOperationResults.Status == TextOperationStatusCodes.Succeeded)
            {
                string _returnValue = string.Empty;
                foreach (var item in _textOperationResults.RecognitionResult.Lines)
                {
                    _returnValue += item.Text + " ";
                }

                return _returnValue.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Liefert die Textobjekte samt Bounding Box der einzelnen Wörter
        /// </summary>
        /// <returns>Einzele Wörter</returns>
        public List<ObjectValue> GetTextObjects()
        {

            List<ObjectValue> _texts = new List<ObjectValue>();

            if (_textOperationResults.Status == TextOperationStatusCodes.Succeeded)
            {
                try
                {
                    //Alle Zeilen durchgehen
                    foreach (var line in _textOperationResults.RecognitionResult.Lines)
                    {
                        //Alle Wörter durchgehen
                        foreach (var item in line.Words)
                        {
                            // Koordinaten der X-Achse zusammenfassen
                            List<double> xvals = new List<double>();
                            xvals.Add(item.BoundingBox[0]);
                            xvals.Add(item.BoundingBox[2]);
                            xvals.Add(item.BoundingBox[4]);
                            xvals.Add(item.BoundingBox[6]);

                            // Koordinaten der Y-Achse zusammenfassen
                            List<double> yvals = new List<double>();
                            yvals.Add(item.BoundingBox[1]);
                            yvals.Add(item.BoundingBox[3]);
                            yvals.Add(item.BoundingBox[5]);
                            yvals.Add(item.BoundingBox[7]);

                            // Linke obere Ecke definieren (für Imageserver)
                            double x = xvals.Min();
                            double y = yvals.Min();
                            // Breite der Bounding-Box definieren (für Imageserver)
                            double w = xvals.Max() - xvals.Min();
                            // Höhe der Bounding-Box definieren (für Imageserver)
                            double h = yvals.Max() - yvals.Min();

                            // Für die Berechnung der Steigung der Geraden die Deta-Werte berechnen
                            double deltax = item.BoundingBox[2] - item.BoundingBox[0];
                            double deltay = item.BoundingBox[3] - item.BoundingBox[1];

                            // Text-Objekt der Liste hinzufügen, inkl. Ausschnitt und Steigung
                            _texts.Add(new ObjectValue(_volareID, item.Text, 100, _imageWidth, _imageHeight, x, w, y, h, Math.Sin(deltay / deltax)));
                        }                       
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLogLine("ObjectHelper.cs - GetImageObjects", e.Message);
                }
            }
            
            return _texts;
        }


        private ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
              { Endpoint = endpoint };
            return client;
        }

        /// <summary>
        /// Text Result von OCR-Engine abholen
        /// </summary>
        /// <returns>TextOperationResult der OCR Engine</returns>
        private TextOperationResult GetTextResult()
        {
            TextOperationResult res = _client.GetTextOperationResultAsync(_recognationTextHeaders.OperationLocation.Substring(_recognationTextHeaders.OperationLocation.Length - 36)).GetAwaiter().GetResult();

            // Warten bis Lauf fertig
            int i = 0;
            int maxRetries = 20;
            while ((res.Status == TextOperationStatusCodes.Running ||
                    res.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries)
            {
                Task.Delay(500);
                res = _client.GetTextOperationResultAsync(_recognationTextHeaders.OperationLocation.Substring(_recognationTextHeaders.OperationLocation.Length - 36)).GetAwaiter().GetResult();
            }
            return res;
        }

        public void Dispose()
        {
            _translatorClient.Dispose();
            _client.Dispose();
        }
    }
}

