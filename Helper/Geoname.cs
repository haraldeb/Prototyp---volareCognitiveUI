using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Timezone
    {
        public int gmtOffset { get; set; }
        public string timeZoneId { get; set; }
        public int dstOffset { get; set; }
    }

    public class AdminCodes1
    {
        public string ISO3166_2 { get; set; }
    }

    public class AlternateName
    {
        public string name { get; set; }
        public string lang { get; set; }
    }

    /// <summary>
    /// Geoname, Abbild der Geonames API
    /// </summary>
    public class Geoname
    {
        public Timezone timezone { get; set; }
        public string asciiName { get; set; }
        public int astergdem { get; set; }
        public string countryId { get; set; }
        public string fcl { get; set; }
        public int srtm3 { get; set; }
        public string adminId2 { get; set; }
        public string adminId3 { get; set; }
        public string countryCode { get; set; }
        public AdminCodes1 adminCodes1 { get; set; }
        public string adminId1 { get; set; }
        public string lat { get; set; }
        public string fcode { get; set; }
        public string continentCode { get; set; }
        public string adminCode2 { get; set; }
        public string adminCode3 { get; set; }
        public string adminCode1 { get; set; }
        public string lng { get; set; }
        public int geonameId { get; set; }
        public string toponymName { get; set; }
        public int population { get; set; }
        public string wikipediaURL { get; set; }
        public string adminName5 { get; set; }
        public string adminName4 { get; set; }
        public string adminName3 { get; set; }
        public List<AlternateName> alternateNames { get; set; }
        public string adminName2 { get; set; }
        public string name { get; set; }
        public string fclName { get; set; }
        public string countryName { get; set; }
        public string fcodeName { get; set; }
        public string adminName1 { get; set; }
    }
}
