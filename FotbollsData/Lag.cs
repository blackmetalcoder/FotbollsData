using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FotbollsData
{
    [XmlRoot(ElementName = "Team")]
    public class Team
    {
        [XmlElement(ElementName = "Team_Id")]
        public string Team_Id { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "Stadium")]
        public string Stadium { get; set; }
        [XmlElement(ElementName = "HomePageURL")]
        public string HomePageURL { get; set; }
        [XmlElement(ElementName = "WIKILink")]
        public string WIKILink { get; set; }
    }

    [XmlRoot(ElementName = "XMLSOCCER.COM")]
    public class XMLSOCCERTeam {
		[XmlElement(ElementName = "Team")]
        public List<Team> Team { get; set; }
    [XmlElement(ElementName = "AccountInformation")]
    public string AccountInformation { get; set; }
}
}
