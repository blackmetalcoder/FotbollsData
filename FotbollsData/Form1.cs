using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
namespace FotbollsData
{
    public partial class Form1 : Form
    {
        public string sConn = "Server=tcp:vlqwv4swf2.database.windows.net,1433;Database=dbApp;User ID=sapjappl@vlqwv4swf2;Password=Olle8910;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XMLSOCCERLiga myObject;           
            XmlSerializer mySerializer =  new XmlSerializer(typeof(XMLSOCCERLiga));           
            FileStream myFileStream = new FileStream(@"C:\temp\soccer\Ligor.xml", FileMode.Open);           
            myObject = (XMLSOCCERLiga)mySerializer.Deserialize(myFileStream);
            dataGridView1.DataSource = myObject.League;
            foreach (var item in myObject.League)
            {
                using (SqlConnection con = new SqlConnection(sConn))
                {
                    string Liga = item.Name;
                    string Land = item.Country;
                    string Live = item.Livescore;
                    string Id = item.Id;
                    string sSQL = "INSERT INTO Ligor (Id, Name, Country, Livescore) VALUES (" + Id + ", '" + Liga + "', '" + Land + "' ,'" + Live +  "')";
                    using (SqlCommand cmd = new SqlCommand(sSQL, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
                 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XMLSOCCERTeam myObject;
            XmlSerializer mySerializer = new XmlSerializer(typeof(XMLSOCCERTeam));
            FileStream myFileStream = new FileStream(@"C:\temp\soccer\Teams.xml", FileMode.Open);
            myObject = (XMLSOCCERTeam)mySerializer.Deserialize(myFileStream);
            dataGridView1.DataSource = myObject.Team;
            foreach (var item in myObject.Team)
            {
                using (SqlConnection con = new SqlConnection(sConn))
                {
                    string Team_Id = item.Team_Id.ToString();
                    string Country = item.Country;
                    string HomePageURL = item.HomePageURL;
                    string Name = item.Name;
                    string Stadium = item.Stadium;
                    string WIKILink = item.WIKILink;
                    string sSQL = "INSERT INTO Teams (Team_Id, Country, HomePageURL, Name, Stadium, WIKILink) VALUES (" + Team_Id + ", '" + Country + "' ,'" + HomePageURL +  "', '" + Name + "', '"  + Stadium + "', '" + WIKILink + "')";
                    using (SqlCommand cmd = new SqlCommand(sSQL, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            MessageBox.Show("Klar");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XMLSOCCERMatcher myObject;
            XmlSerializer mySerializer = new XmlSerializer(typeof(XMLSOCCERMatcher));
            FileStream myFileStream = new FileStream(@"C:\temp\soccer\fixtures.xml", FileMode.Open);
            myObject = (XMLSOCCERMatcher)mySerializer.Deserialize(myFileStream);
            dataGridView1.DataSource = myObject.Match;
            foreach (var item in myObject.Match)
            {
                using (SqlConnection con = new SqlConnection(sConn))
                {
                    string svar;
                    string sDatum = item.Date.Substring(0, 10) + " " + item.Date.Substring(11, 8);
                    string Date = sDatum;
                    string AwayGoals = "0";
                    svar = Test(item.AwayGoals);
                    if (svar == "S")
                        {
                        AwayGoals = item.AwayGoals;
                    }
                    string AwayTeam = string.Empty;
                    svar = Test(item.AwayTeam);
                    if (svar == "S")
                    {
                        AwayTeam = item.AwayTeam.ToString().Replace("'", "");
                    }
                    string AwayTeamRedCardDetails = string.Empty;
                    svar = Test(item.AwayTeamRedCardDetails);
                    if (svar == "S")
                    {
                        AwayTeamRedCardDetails = item.AwayTeamRedCardDetails;
                    }
                    string AwayTeamYellowCardDetails = string.Empty;
                    svar = Test(item.AwayTeamYellowCardDetails);
                    if (svar == "S")
                    {
                        AwayTeamYellowCardDetails = item.AwayTeamYellowCardDetails;
                    }
                    string AwayTeam_Id = item.AwayTeam_Id;
                    string HomeGoals = "0";
                    svar = Test(item.HomeGoals);
                    if (svar == "S")
                    {
                        HomeGoals = item.HomeGoals;
                    }
                    string HomeTeam = item.HomeTeam.ToString().Replace("'", "");
                    string HomeTeamRedCardDetails = string.Empty;
                    svar = Test(item.HomeTeamRedCardDetails);
                    if (svar == "S")
                    {
                        HomeTeamRedCardDetails = item.HomeTeamRedCardDetails;
                    }
                    string HomeTeamYellowCardDetails = string.Empty;
                    svar = Test(item.HomeTeamYellowCardDetails);
                    if (svar == "S")
                    {
                        HomeTeamYellowCardDetails = item.HomeTeamYellowCardDetails;
                    }
                    string HomeTeam_Id = item.HomeTeam_Id;
                    string league = item.League;
                    string Location = item.Location.ToString().Replace("'", "");
                    string Round = item.Round;
                    string Time = item.Time;
                    string matchID = item.Id;
                    bool laggTill = finns(matchID);
                   // bool laggTill = true;
                    if (laggTill)
                    {
                        string sSQL = "INSERT INTO Fixtures (Date, League, Round, HomeTeam, HomeTeam_Id, AwayTeam, AwayTeam_Id, Location, HomeGoals, AwayGoals, HomeTeamYellowCardDetails,AwayTeamYellowCardDetails, HomeTeamRedCardDetails, AwayTeamRedCardDetails, Id)" +
                   "VALUES ('" + Date + "', '" + league + "' ,'" + Round + "', '" + HomeTeam + "', " + HomeTeam_Id + ", '" + AwayTeam + "'," + AwayTeam_Id + ",'" + Location + "', " + HomeGoals + ", " + AwayGoals + ", '" + HomeTeamYellowCardDetails + "', '" + AwayTeamYellowCardDetails + "', '" + HomeTeamRedCardDetails + "', '" + AwayTeamRedCardDetails + "'," + matchID + ")";
                        using (SqlCommand cmd = new SqlCommand(sSQL, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                   
                }
            }
            MessageBox.Show("Klar");
        }
        public static bool finns(string id)
        {
            bool ny = false;
            string sConn = "Server=tcp:vlqwv4swf2.database.windows.net,1433;Database=dbApp;User ID=sapjappl@vlqwv4swf2;Password=Olle8910;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
            using (SqlConnection con = new SqlConnection(sConn))
            {
                
                string sSQL = "SELECT * From Fixtures  WHERE Id = " + id;
                using (SqlCommand cmd = new SqlCommand(sSQL, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        ny = false;
                    }
                    else
                    {
                        ny = true;
                    }
                    cmd.Connection.Close();
                }
            }
            return ny;
        }
        public static String Test(string s)
        {
            if (String.IsNullOrEmpty(s))
                return "Tom";
            else
                return "S";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string svar;
            string sSQL = "Select Fixtures.Idnr, Fixtures.Id, Fixtures.HomeTeamYellowCardDetails, " +
                          "Fixtures.AwayTeamYellowCardDetails, Fixtures.HomeTeamRedCardDetails, " +
                          "Fixtures.AwayTeamRedCardDetails, Fixtures.HomeLineupGoalkeeper, " +
                          "Fixtures.AwayLineupGoalkeeper, Fixtures.HomeLineupDefense, " +
                          "Fixtures.AwayLineupDefense, Fixtures.HomeLineupMidfield, " +
                          "Fixtures.AwayLineupMidfield, Fixtures.HomeLineupForward, " +
                          "Fixtures.AwayLineupForward, Fixtures.HomeLineupSubstitutes, " +
                          "Fixtures.AwayLineupSubstitutes, Fixtures.HomeGoalDetails, " +
                          "Fixtures.AwayGoalDetails From Fixtures";
            using (SqlConnection con = new SqlConnection(sConn))
            {
                using (SqlCommand cmd = new SqlCommand(sSQL, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            svar = Test(reader["HomeTeamYellowCardDetails"].ToString());
                            string sHomeTeamYellowCardDetails = " ";
                            if (svar == "S")
                            {
                                sHomeTeamYellowCardDetails = reader["HomeTeamYellowCardDetails"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayTeamYellowCardDetails"].ToString());
                            string sAwayTeamYellowCardDetails = string.Empty;
                            if (svar == "S")
                            {
                                sAwayTeamYellowCardDetails = reader["AwayTeamYellowCardDetails"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeTeamRedCardDetails"].ToString());
                            string sHomeTeamRedCardDetails = string.Empty;
                            if (svar == "S")
                            {
                                sHomeTeamRedCardDetails = reader["HomeTeamRedCardDetails"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayTeamRedCardDetails"].ToString());
                            string sAwayTeamRedCardDetails = string.Empty;
                            if (svar == "S")
                            {
                                sAwayTeamRedCardDetails = reader["AwayTeamRedCardDetails"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeLineupDefense"].ToString());
                            string sHomeLineupDefense = string.Empty;
                            if (svar == "S")
                            {
                                sHomeLineupDefense = reader["HomeLineupDefense"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayLineupDefense"].ToString());
                            string sAwayLineupDefense = string.Empty;
                            if (svar == "S")
                            {
                                reader["AwayLineupDefense"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeLineupMidfield"].ToString());
                            string sHomeLineupMidfield = string.Empty;
                            if (svar == "S")
                            {
                                sHomeLineupMidfield = reader["HomeLineupMidfield"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayLineupMidfield"].ToString());
                            string sAwayLineupMidfield = string.Empty;
                            if (svar == "S")
                            {
                                sAwayLineupMidfield = sAwayLineupMidfield = reader["AwayLineupMidfield"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeLineupForward"].ToString());
                            string sHomeLineupForward = string.Empty;
                            if (svar == "S")
                            {
                                sHomeLineupForward = reader["HomeLineupForward"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayLineupForward"].ToString());
                            string sAwayLineupForward = string.Empty;
                            if (svar == "S")
                            {
                                sAwayLineupForward = reader["AwayLineupForward"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeLineupSubstitutes"].ToString());
                            string sHomeLineupSubstitutes = string.Empty;
                            if (svar == "S")
                            {
                                sHomeLineupSubstitutes = reader["HomeLineupSubstitutes"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayLineupSubstitutes"].ToString());
                            string sAwayLineupSubstitutes = string.Empty;
                            if (svar == "S")
                            {
                                sAwayLineupSubstitutes = reader["AwayLineupSubstitutes"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["HomeGoalDetails"].ToString());
                            string sHomeGoalDetails = string.Empty;
                            if (svar == "S")
                            {
                                sHomeGoalDetails = reader["HomeGoalDetails"].ToString().Replace(";", " ");
                            }
                            svar = Test(reader["AwayGoalDetails"].ToString());
                            string sAwayGoalDetails = string.Empty;
                            if (svar == "S")
                            {
                                sAwayGoalDetails = reader["AwayGoalDetails"].ToString().Replace(";", " ");
                            }
                            string sUpate = "UPDATE Fixtures SET HomeTeamYellowCardDetails = '" + sHomeTeamYellowCardDetails + "', " +
                                "AwayTeamYellowCardDetails = '" + sAwayTeamYellowCardDetails + "', " +
                                "HomeTeamRedCardDetails = '" + sHomeTeamRedCardDetails + "', " +
                                "AwayTeamRedCardDetails = '" + sAwayTeamRedCardDetails + "', " +
                                "HomeLineupDefense = '" + sHomeLineupDefense + "', " +
                                "AwayLineupDefense = '" + sAwayLineupDefense + "', " +
                                "HomeLineupMidfield = '" + sHomeLineupMidfield + "', " +
                                "AwayLineupMidfield = '" + sAwayLineupMidfield + "', " +
                                "HomeLineupForward = '" + sHomeLineupForward + "', " +
                                "AwayLineupForward = '" + sAwayLineupForward + "', " +
                                "HomeLineupSubstitutes = '" + sHomeLineupSubstitutes + "', " +
                                "AwayLineupSubstitutes = '" + sAwayLineupSubstitutes + "', " +
                                "HomeGoalDetails = '" + sHomeGoalDetails + "', " +
                                "AwayGoalDetails = '" + sAwayGoalDetails + "' " +
                                " WHERE Id = " + reader["Id"];
                            using (SqlConnection con2 = new SqlConnection(sConn))
                            {
                                using (SqlCommand cmd2 = new SqlCommand(sUpate, con2))
                                {
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.Connection.Open();
                                    cmd2.ExecuteNonQuery();
                                    cmd2.Connection.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Finns inte
                        }
                        
                        //cmd.Connection.Close();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XMLSOCCERMatcher myObject;
            XmlSerializer mySerializer = new XmlSerializer(typeof(XMLSOCCERMatcher));

            FileStream myFileStream = new FileStream(@"C:\temp\soccer\fixtures.xml", FileMode.Open);
            myObject = (XMLSOCCERMatcher)mySerializer.Deserialize(myFileStream);
            dataGridView1.DataSource = myObject.Match;
            foreach (var item in myObject.Match)
            {
                using (SqlConnection con = new SqlConnection(sConn))
                {
                    string svar;
                   DateTime d = DateTime.Parse(item.Date.ToString());
                    d = d.AddHours(-2);
                    string S1 = d.ToString();
                    //string sDatum = item.Date.Substring(0, 10) + " " + item.Date.Substring(11, 8);
                    string sDatum = S1.Substring(0, 10) + " " + S1.Substring(11, 8);
                    string Date = sDatum;
                    string matchID = item.Id;
                    string tid = item.Time;
                    string sSQL = "UPDATE Fixtures SET Date = '" + Date + "', HomeTeam = '" + item.HomeTeam.ToString().Replace("'", "") + "', AwayTeam = '" + item.AwayTeam.ToString().Replace("'", "") + "' WHERE Id = " + matchID;
                    using (SqlCommand cmd = new SqlCommand(sSQL, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime iDag = System.DateTime.Now.Date;
            textBox1.Text = iDag.Month.ToString();
        }
    }
}
