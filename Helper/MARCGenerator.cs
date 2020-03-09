using MARC4J.Net;
using MARC4J.Net.MARC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class MARCGenerator
    {
        private string _urlToImage = string.Empty;
        private string _marcFilePath = string.Empty;
        private string _marcFileName = string.Empty;
        private string _oid = string.Empty;

        private string _recordASString = string.Empty;

        /// <summary>
        /// Liefert die URL zum Marc-XML-File
        /// </summary>
        /// <returns>url zum File als string</returns>
        public string GetMarcFileUrl()
        {
            return MyConstants.URLTOMARCFILES + _marcFileName;
        }

        /// <summary>
        /// Liefert den Marc-Record als String
        /// </summary>
        /// <returns>Marc-Record als string</returns>
        public string GetRecordAsString()
        {
            return _recordASString;
        }

        /// <summary>
        /// Generiert aus volare Objekt-ID und Bild-URL ein MARC21 Katalogisat
        /// </summary>
        /// <param name="pUrlToImage">öffentlich erreichbare URL zum Bild</param>
        /// <param name="pOid">volare Objekt ID</param>
        public MARCGenerator(string pUrlToImage, string pOid)
        {

            _oid = pOid;
            Guid _guid = Guid.NewGuid();
            _marcFileName = _guid.ToString() + ".xml"; 
            _marcFilePath = MyConstants.PATHTOMARCFILES + _marcFileName;

            //Standardkatalogisat erstellen
            using (var fs2 = new FileStream(_marcFilePath, FileMode.OpenOrCreate))
            {
                using (var writer = new MarcXmlWriter(fs2, "UTF-8"))
                {
                    var record = MarcFactory.Instance.NewRecord();

                    //Personenerkennung
                    DTOCognitivePerson _personen = Helper.FaceHelper.IdentifyFaces(pUrlToImage);

                    //Objekterkennung
                    ObjectHelper _objekte = new ObjectHelper(pOid, pUrlToImage);

                    //Landeskundliche Elemente
                    CustomVisionHelper _landeskundliches = new CustomVisionHelper(pUrlToImage);

                    //Feld 007 
                    record.AddVariableField(MarcFactory.Instance.NewControlField("007", string.Format("kv-ci|")));
                    record.AddVariableField(MarcFactory.Instance.NewControlField("007", string.Format("gs-c|||||")));

                    // Feld 008
                    if (_personen.GetEstimatedYear() != 0)
                        record.AddVariableField(MarcFactory.Instance.NewControlField("008", string.Format("150204q{0}----xx----------------f-zxx-d", _personen.GetEstimatedYear())));
                    else
                        record.AddVariableField(MarcFactory.Instance.NewControlField("008", string.Format("150204q19002020xx----------------f-zxx-d", _personen.GetEstimatedYear())));


                    // Feld 040
                    var _040 = MarcFactory.Instance.NewDataField("040", ' ', ' ');
                    _040.AddSubfield(MarcFactory.Instance.NewSubfield('a', "AT-VLB"));
                    _040.AddSubfield(MarcFactory.Instance.NewSubfield('b', "ger"));
                    _040.AddSubfield(MarcFactory.Instance.NewSubfield('d', "AT-VLB"));
                    _040.AddSubfield(MarcFactory.Instance.NewSubfield('e', "rda_VBV_Version_02"));
                    record.AddVariableField(_040);


                    // Feld 24510, Titel
                    var _24510 = MarcFactory.Instance.NewDataField("245", '1', '0');
                    _24510.AddSubfield(MarcFactory.Instance.NewSubfield('a', _objekte.GetImageSummary().Value));
                    record.AddVariableField(_24510);

                    // Feld 264 1
                    if (_personen.GetEstimatedYear() != 0)
                    {
                        var _264 = MarcFactory.Instance.NewDataField("264", ' ', '0');
                        _264.AddSubfield(MarcFactory.Instance.NewSubfield('c', _personen.GetEstimatedYear().ToString()));
                        _264.AddSubfield(MarcFactory.Instance.NewSubfield('9', "Anhand der abgebildeten Personen geschätzt"));
                        record.AddVariableField(_264);
                    }

                    //Feld 300
                    var _300 = MarcFactory.Instance.NewDataField("300", ' ', ' ');
                    _300.AddSubfield(MarcFactory.Instance.NewSubfield('a', "1 Digitales Bild"));
                    if (_objekte.IsImageBlackWhite())
                        _300.AddSubfield(MarcFactory.Instance.NewSubfield('b', "schwarz-weiß"));
                    else
                        _300.AddSubfield(MarcFactory.Instance.NewSubfield('b', "farbig"));

                    _300.AddSubfield(MarcFactory.Instance.NewSubfield('c', string.Format("{0} x {1} Pixel", _objekte.ImageWidth, _objekte.ImageHeight)));
                    record.AddVariableField(_300);

                    //Feld 336 (RDA!)
                    var _336 = MarcFactory.Instance.NewDataField("336", ' ', ' ');
                    _336.AddSubfield(MarcFactory.Instance.NewSubfield('a', "unbewegtes Bild"));
                    _336.AddSubfield(MarcFactory.Instance.NewSubfield('b', "sti"));
                    _336.AddSubfield(MarcFactory.Instance.NewSubfield('2', "rdacontent"));
                    record.AddVariableField(_336);

                    //Feld 337 (RDA!)
                    var _337 = MarcFactory.Instance.NewDataField("337", ' ', ' ');
                    _337.AddSubfield(MarcFactory.Instance.NewSubfield('a', "projizierbar"));
                    _337.AddSubfield(MarcFactory.Instance.NewSubfield('b', "g"));
                    _337.AddSubfield(MarcFactory.Instance.NewSubfield('2', "rdamedia"));
                    record.AddVariableField(_337);

                    //Feld 338 (RDA!)
                    var _338 = MarcFactory.Instance.NewDataField("338", ' ', ' ');
                    _338.AddSubfield(MarcFactory.Instance.NewSubfield('a', "Digitalisat"));
                    _338.AddSubfield(MarcFactory.Instance.NewSubfield('b', "gs"));
                    _338.AddSubfield(MarcFactory.Instance.NewSubfield('2', "rdacarrier"));
                    record.AddVariableField(_338);

                    //Feld 600 (Person)
                    foreach (CognitivePerson p in _personen.CognitivePeople)
                    {
                        if (p.pers.Name != "Unbekannt")
                        {
                            var _60014 = MarcFactory.Instance.NewDataField("600", '1', '4');
                            _60014.AddSubfield(MarcFactory.Instance.NewSubfield('a', p.pers.Name));
                            if (p.GetYearOfBirth() > 0)
                            {
                                if (p.GetYearOfDeath() > 0)
                                    _60014.AddSubfield(MarcFactory.Instance.NewSubfield('d', string.Format("{0}-{1}", p.GetYearOfBirth(), p.GetYearOfDeath())));
                                else
                                    _60014.AddSubfield(MarcFactory.Instance.NewSubfield('d', string.Format("{0}-", p.GetYearOfBirth())));
                            }
                            if (p.GetProfession() != string.Empty)
                                _60014.AddSubfield(MarcFactory.Instance.NewSubfield('g', p.GetProfession()));

                            if (p.pers.UserData != null)
                                _60014.AddSubfield(MarcFactory.Instance.NewSubfield('4', "(DE-588)" + p.pers.UserData));

                            record.AddVariableField(_60014);
                        }
                    }

                    // Feld 520 9, Tags
                    string _tags = string.Empty;
                    foreach (ObjectValue tags in _objekte.GetImageTags())
                    {
                        if (tags.Confidence > 50)
                            _tags += tags.Value + "; ";
                    }
                    if (_tags.Length > 1)
                        _tags = _tags.Substring(0, _tags.Length - 2);

                    var _5209 = MarcFactory.Instance.NewDataField("520", '9', ' ');
                    _5209.AddSubfield(MarcFactory.Instance.NewSubfield('a', _tags));
                    record.AddVariableField(_5209);


                    // Feld 520 9, Erkannter Text
                    if(_objekte.GetImageText() != string.Empty)
                    {
                        var _5209_2 = MarcFactory.Instance.NewDataField("520", '9', ' ');
                        _5209_2.AddSubfield(MarcFactory.Instance.NewSubfield('a', "Abgebildeter Text: " + _objekte.GetImageText()));
                        record.AddVariableField(_5209_2);
                    }

                    //Feld 659, Landeskundliche Elemente
                    foreach (CustomVisionValue cvv in _landeskundliches.MyCustomVisionValues)
                    {
                        if (cvv.Confidence > 98)
                        {
                            var _659 = MarcFactory.Instance.NewDataField("659", ' ', ' ');
                            _659.AddSubfield(MarcFactory.Instance.NewSubfield('a', cvv.Name));
                            if (cvv.GeonamesNumber.Length > 1)
                                _659.AddSubfield(MarcFactory.Instance.NewSubfield('u', cvv.GeonamesLink));
                            if (cvv.Lat != 0)
                                _659.AddSubfield(MarcFactory.Instance.NewSubfield('b', cvv.Lat.ToString().Replace(',','.') + ", " + cvv.Lon.ToString().Replace(',', '.')));
                            record.AddVariableField(_659);
                        }
                    }

                    //856 LINK
                    var _856 = MarcFactory.Instance.NewDataField("856", '7', '0');
                    _856.AddSubfield(MarcFactory.Instance.NewSubfield('u', "https://pid.volare.vorarlberg.at/" + _oid));
                    _856.AddSubfield(MarcFactory.Instance.NewSubfield('z', "Digitalisat"));
                    _856.AddSubfield(MarcFactory.Instance.NewSubfield('2', "file"));
                    record.AddVariableField(_856);

                    //948 LINK
                    var _948 = MarcFactory.Instance.NewDataField("948", ' ', ' ');
                    _948.AddSubfield(MarcFactory.Instance.NewSubfield('a', "Vorarlberg-Sammlungen"));
                    record.AddVariableField(_948);

                    //948 LINK
                    var _931 = MarcFactory.Instance.NewDataField("931", ' ', ' ');
                    _931.AddSubfield(MarcFactory.Instance.NewSubfield('a', _oid));
                    record.AddVariableField(_931);

                    //TYP LINK
                    var _typ = MarcFactory.Instance.NewDataField("TYP", ' ', ' ');
                    _typ.AddSubfield(MarcFactory.Instance.NewSubfield('a', "Bildmaterial"));
                    record.AddVariableField(_typ);

                    _recordASString = record.ToString();

                    // Marc-XML-File auf Filesystem schreiben
                    writer.Write(record);

                }
            }

        }
    }
}
