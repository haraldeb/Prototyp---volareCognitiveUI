using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    /// <summary>
    /// GNDPerson, Abbild der GND-API
    /// </summary>
    public class GNDPerson
    {
        public class ProfessionOrOccupation
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class Gender
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class GndSubjectCategory
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class GeographicAreaCode
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class License
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class DescribedBy
        {
            public string id { get; set; }
            public License license { get; set; }
            public DateTime dateModified { get; set; }
        }

        public class Wikipedia
        {
            public string id { get; set; }
            public string label { get; set; }
        }

        public class PreferredNameEntityForThePerson
        {
            public List<string> forename { get; set; }
            public List<string> surname { get; set; }
        }

        public class Collection
        {
            public string id { get; set; }
            public string abbr { get; set; }
            public string publisher { get; set; }
            public string icon { get; set; }
            public string name { get; set; }
        }

        public class SameA
        {
            public string id { get; set; }
            public Collection collection { get; set; }
        }

        public class Depiction
        {
            public string id { get; set; }
            public string url { get; set; }
            public string thumbnail { get; set; }
        }

        public class GPerson
        {
            public List<ProfessionOrOccupation> professionOrOccupation { get; set; }
            public List<Gender> gender { get; set; }
            public List<string> dateOfDeath { get; set; }
            public List<string> dateOfBirth { get; set; }
            public List<string> type { get; set; }
            public List<GndSubjectCategory> gndSubjectCategory { get; set; }
            public List<string> oldAuthorityNumber { get; set; }
            public List<GeographicAreaCode> geographicAreaCode { get; set; }
            public List<string> biographicalOrHistoricalInformation { get; set; }
            public DescribedBy describedBy { get; set; }
            public string gndIdentifier { get; set; }
            public string id { get; set; }
            public string preferredName { get; set; }
            public List<Wikipedia> wikipedia { get; set; }
            public PreferredNameEntityForThePerson preferredNameEntityForThePerson { get; set; }
            public List<SameA> sameAs { get; set; }
            public List<Depiction> depiction { get; set; }
        }
    }
}
