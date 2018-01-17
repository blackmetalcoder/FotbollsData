using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FotbollsData
{
    [XmlRoot(ElementName = "League")]
    public class League
    {
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "Historical_Data")]
        public string Historical_Data { get; set; }
        [XmlElement(ElementName = "Fixtures")]
        public string Fixtures { get; set; }
        [XmlElement(ElementName = "Livescore")]
        public string Livescore { get; set; }
        [XmlElement(ElementName = "NumberOfMatches")]
        public string NumberOfMatches { get; set; }
        [XmlElement(ElementName = "LatestMatch")]
        public string LatestMatch { get; set; }
    }

    [XmlRoot(ElementName = "XMLSOCCER.COM")]
    public class XMLSOCCERLiga {
		[XmlElement(ElementName = "League")]
    public List<League> League { get; set; }
    [XmlElement(ElementName = "AccountInformation")]
    public string AccountInformation { get; set; }
}
}
