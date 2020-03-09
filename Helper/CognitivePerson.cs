using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class CognitivePerson
    {
        private IdentifyResult _idres_ = new IdentifyResult();
        private Person _person = new Person();
        private DetectedFace _face = new DetectedFace();
        private int _yearOfBirth = 0;
        private int _yearOfDeath = 0;
        private string _profession = string.Empty;

        /// <summary>
        /// Holt die Berufsbezeichnung aus der GND zur Person
        /// </summary>
        /// <returns>Erste Berufsbezeichnung aus der GND, wenn vorhanden. Sonst string.Empty</returns>
        public string GetProfession()
        {
            if (_profession == string.Empty)
            {
                HoleLebensdaten();
            }
            return _profession;

        }

        /// <summary>
        /// Holt das Gebrutsjahr aus der GND zur Person
        /// </summary>
        /// <returns>Geburtsjahr, wenn vorhanden. Ansonsten 0</returns>
        public int GetYearOfBirth()
        {
            if (_person.UserData != null && _yearOfBirth == 0)
            {
                HoleLebensdaten();
            }
            return _yearOfBirth;
        }


        /// <summary>
        /// Holt das Sterbejahr aus der GND zur Person
        /// </summary>
        /// <returns>Sterbejahr, wenn vorhanden. Ansonsten 0</returns>
        public int GetYearOfDeath()
        {
            if (_person.UserData != null && _yearOfDeath == 0)
            {
                HoleLebensdaten();
            }
            return _yearOfDeath;
        }


        /// <summary>
        /// Schätzt das Aufnahmedatum des Eingangsbildes anhand des geschätzten Alters der Person und den aus der GND verfügbaren Lebensdaten
        /// </summary>
        /// <returns>geschätzes Bildaufnahmejahr, wenn Berechnung möglich. Ansonsten 0</returns>
        public int GetEstimatedImageYear()
        {
            if (GetYearOfBirth() != 0)
            {
                return GetYearOfBirth() + int.Parse(_face.FaceAttributes.Age.ToString());
            }
            else
            {
                return 0;
            }
        }


        public Person pers { get => _person; set => _person = value; }
        public IdentifyResult IdentRes { get => _idres_; set => _idres_ = value; }
        public DetectedFace DetFace { get => _face; set => _face = value; }

        /// <summary>
        /// Holt die Lebensdaten (Geburts- und Sterbejahr sowie die Berufsbezeichnung) aus der GND
        /// </summary>
        private void HoleLebensdaten()
        {
            try
            {
                // Json herunterladen
                GNDPerson.GPerson mygndperson = JsonConvert.DeserializeObject<GNDPerson.GPerson>(new WebClient().DownloadString(MyConstants.GNDAPIURL + _person.UserData + ".json"));
                // Geburtsjahr auslesen
                int.TryParse(mygndperson.dateOfBirth[0].Substring(0,4), out _yearOfBirth);
                // Sterbejahr auslesen
                int.TryParse(mygndperson.dateOfDeath[0].Substring(0,4), out _yearOfDeath);
                // erste Berufsbezeichnung aus dem Array
                _profession = mygndperson.professionOrOccupation.First().label;
            }
            catch (Exception e)
            {
                Logger.WriteLogLine("CognitivePerson.cs - HoleLebensdaten(), GND-Abruf", e.Message);
            }

        }
     
    }
}
