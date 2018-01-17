using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FotbollsData
{
    [XmlRoot(ElementName = "Match")]
    public class Match
    {
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "League")]
        public string League { get; set; }
        [XmlElement(ElementName = "Round")]
        public string Round { get; set; }
        [XmlElement(ElementName = "HomeTeam")]
        public string HomeTeam { get; set; }
        [XmlElement(ElementName = "HomeTeam_Id")]
        public string HomeTeam_Id { get; set; }
        [XmlElement(ElementName = "HomeGoals")]
        public string HomeGoals { get; set; }
        [XmlElement(ElementName = "AwayTeam")]
        public string AwayTeam { get; set; }
        [XmlElement(ElementName = "AwayTeam_Id")]
        public string AwayTeam_Id { get; set; }
        [XmlElement(ElementName = "AwayGoals")]
        public string AwayGoals { get; set; }
        [XmlElement(ElementName = "Time")]
        public string Time { get; set; }
        [XmlElement(ElementName = "Location")]
        public string Location { get; set; }
        [XmlElement(ElementName = "HomeTeamYellowCardDetails")]
        public string HomeTeamYellowCardDetails { get; set; }
        [XmlElement(ElementName = "AwayTeamYellowCardDetails")]
        public string AwayTeamYellowCardDetails { get; set; }
        [XmlElement(ElementName = "HomeTeamRedCardDetails")]
        public string HomeTeamRedCardDetails { get; set; }
        [XmlElement(ElementName = "AwayTeamRedCardDetails")]
        public string AwayTeamRedCardDetails { get; set; }
    }

    [XmlRoot(ElementName = "XMLSOCCER.COM")]
    public class XMLSOCCERMatcher {
		[XmlElement(ElementName = "Match")]
        public List<Match> Match { get; set; }
    [XmlElement(ElementName = "AccountInformation")]
    public string AccountInformation { get; set; }
}
}
