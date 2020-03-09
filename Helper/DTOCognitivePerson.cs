using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class DTOCognitivePerson
    {
        private List<CognitivePerson> _cognitivePeople = new List<CognitivePerson>();
        private string _KIMessage = string.Empty;

        public List<CognitivePerson> CognitivePeople { get => _cognitivePeople; set => _cognitivePeople = value; }
        /// <summary>
        /// Antwort der KI, meist Fehlermeldung
        /// </summary>
        public string KIMessage { get => _KIMessage; set => _KIMessage = value; }


        /// <summary>
        /// Gibt das geschätzte Aufnahmedatum zurück, wenn individualisierte Personen erkannt wurden. Ist keine Person individualisiert, so wird 0 zurückgegeben
        /// </summary>
        /// <returns>Geschätzes Bildaufnahmedatum</returns>
        public int GetEstimatedYear()
        {
            List<int> _estimatedYears = new List<int>();
            int _returnval = 0;

            foreach (CognitivePerson p in _cognitivePeople)
            {
                if (p.GetEstimatedImageYear() != 0)
                    _estimatedYears.Add(p.GetEstimatedImageYear());
            }

            if (_estimatedYears.Count > 0)
                _returnval = int.Parse(_estimatedYears.Average().ToString());

            return _returnval;
        }
    }
}
