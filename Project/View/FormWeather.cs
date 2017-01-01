using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text;
using System.Windows.Forms;

namespace Droid_weather
{

    public partial class FormWeather : Form
    {
        #region Attribute
        // number of days to fetch the forecast for
        protected const string m_numOfDays = "5";

        // location of the XSL file to transform the SOAP response with
        protected const string m_xslForecastTransform = "/xslt/weather_5day.xsl";
        #endregion

        #region Constructor
        public FormWeather()
        {
            InterfaceWeather.ACTION_130_lister_seismes();
            InitializeComponent();
            //populatePage();

            WebClient wc = new WebClient();
            //double dLat = 49.4431;
            //double dLon = 1.099304;
            double dLat = 35.16;
            double dLon = 57.49;
            latLonToOsGrid(dLat, dLon);

            string query = "http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdBrowserClientByDay.php?endPoint1Lat=49.4431&endPoint1Lon=-1.099304&endPoint2Lat=49.4431&endPoint2Lon=-1.099304&format=24+hourly&numDays=7";
            query = "http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php?lat=49.44&lon=1.10&product=time-series&begin=2004-01-01T00:00:00&end=2015-05-14T15:00:00&maxt=maxt&mint=mint";

            //string link = "http://graphical.weather.gov/xml/SOAP_server/ndfdXMLclient.php?whichClient=NDFDgen&lat=" +
            //    string.Format("000.000", dLat) + "&lon=" + string.Format("000.000", dLon);

            // ---------------------------------
            string link = "http://www.webservicex.net/globalweather.asmx/GetWeather?CityName=Rouen&CountryName=France";
            //<string xmlns="http://www.webserviceX.NET">
            //<?xml version="1.0" encoding="utf-16"?> <CurrentWeather> <Location>Rouen, France (LFOP) 49-23N 001-11E 157M</Location> <Time>May 03, 2015 - 10:00 AM EDT / 2015.05.03 1400 UTC</Time> <Wind> from the SW (220 degrees) at 18 MPH (16 KT):0</Wind> <Visibility> greater than 7 mile(s):0</Visibility> <SkyConditions> mostly cloudy</SkyConditions> <Temperature> 66 F (19 C)</Temperature> <DewPoint> 57 F (14 C)</DewPoint> <RelativeHumidity> 72%</RelativeHumidity> <Pressure> 29.59 in. Hg (1002 hPa)</Pressure> <Status>Success</Status> </CurrentWeather>
            //</string>
            // ---------------------------------


            ////////////var page = wc.DownloadString(link);

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(page);

            //var imageLink = doc.DocumentNode.SelectNodes("//td/a[@href]")
            //                    .Select(a => a.Attributes["href"].Value)
            //                    .OrderByDescending(a => a)
            //                    .First();
        }
        #endregion

        private Point latLonToOsGrid(double lat, double lon) 
        {
            //if (!(point instanceof LatLon)) throw new TypeError('point is not LatLon object');

            //// if necessary convert to OSGB36 first
            //if (point.datum != LatLon.datum.OSGB36) point = point.convertDatum(LatLon.datum.OSGB36);

            //var φ = point.lat.toRadians();
            //var λ = point.lon.toRadians();
            double φ = (Math.PI / 180) * lat;
            double λ = (Math.PI / 180) * lon;

            double a = 6377563.396;
            double b = 6356256.909;              // Airy 1830 major & minor semi-axes
            double F0 = 0.9996012717;                             // NatGrid scale factor on central meridian
            //var φ0 = (49).toRadians();
            //var λ0 = (-2).toRadians();  // NatGrid true origin is 49°N,2°W
            double φ0 = 49;
            double λ0 = -2;  // NatGrid true origin is 49°N,2°W
            double N0 = -100000;
            double E0 = 400000;                     // northing & easting of true origin, metres
            double e2 = 1 - (b*b)/(a*a);                          // eccentricity squared
            double n = (a-b)/(a+b);
            double n2 = n * n;
            double n3 = n * n * n;         // n, n², n³

            double cosφ = Math.Cos(φ);
            double sinφ = Math.Sin(φ);
            double ν = a*F0/Math.Sqrt(1-e2*sinφ*sinφ);            // nu = transverse radius of curvature
            double ρ = a*F0*(1-e2)/Math.Pow(1-e2*sinφ*sinφ, 1.5); // rho = meridional radius of curvature
            double η2 = ν/ρ-1;                                    // eta = ?

            double Ma = (1 + n + (5/4)*n2 + (5/4)*n3) * (φ-φ0);
            double Mb = (3*n + 3*n*n + (21/8)*n3) * Math.Sin(φ-φ0) * Math.Cos(φ+φ0);
            double Mc = ((15/8)*n2 + (15/8)*n3) * Math.Sin(2*(φ-φ0)) * Math.Cos(2*(φ+φ0));
            double Md = (35/24)*n3 * Math.Sin(3*(φ-φ0)) * Math.Cos(3*(φ+φ0));
            double M = b * F0 * (Ma - Mb + Mc - Md);              // meridional arc

            double cos3φ = cosφ*cosφ*cosφ;
            double cos5φ = cos3φ*cosφ*cosφ;
            double tan2φ = Math.Tan(φ)*Math.Tan(φ);
            double tan4φ = tan2φ*tan2φ;

            double I = M + N0;
            double II = (ν/2)*sinφ*cosφ;
            double III = (ν/24)*sinφ*cos3φ*(5-tan2φ+9*η2);
            double IIIA = (ν/720)*sinφ*cos5φ*(61-58*tan2φ+tan4φ);
            double IV = ν*cosφ;
            double V = (ν/6)*cos3φ*(ν/ρ-tan2φ);
            double VI = (ν/120) * cos5φ * (5 - 18*tan2φ + tan4φ + 14*η2 - 58*tan2φ*η2);

            double Δλ = λ-λ0;
            double Δλ2 = Δλ * Δλ;
            double Δλ3 = Δλ2 * Δλ;
            double Δλ4 = Δλ3*Δλ;
            double Δλ5 = Δλ4 * Δλ;
            double Δλ6 = Δλ5 * Δλ;

            int N = (int)(I + II*Δλ2 + III*Δλ4 + IIIA*Δλ6);
            int E = (int)(E0 + IV*Δλ + V*Δλ3 + VI*Δλ5);

            //N = Number(N.toFixed(3)); // round to mm precision
            //E = Number(E.toFixed(3));

            return new Point(E, N); // gets truncated to SW corner of 1m grid square
        }


        //private void populatePage()
        //{
        //    string result = System.String.Empty;
        //    string xmlForecast = System.String.Empty;
        //    string formattedForecast = System.String.Empty;
        //    decimal nLat = 0.0M;
        //    decimal nLon = 0.0M;
        //    string strCity = System.String.Empty;
        //    string strState = System.String.Empty;
        //    string element = System.String.Empty;
        //    long zip = 0;
        //    textBoxOut.Clear();
        //    try
        //    {

        //        // check to see if there is a request parameter passed to us with the zip code to
        //        // fetch the forecast 
        //        if (Request["zip"] == null)
        //        {
        //            // no zip code passed, so prompt the user to enter a zip code.
        //            result = "<p>Please enter a zip code to retrieve a " + m_numOfDays + " day forecast.</p>";
        //        }
        //        else
        //        {
        //            // get the zip code from the request. 
        //            zip = long.Parse(Request["zip"].ToString());

        //            // call our function to retrieve the latitude, longitude, city and state.  The web service requires 
        //            // the latitude and longitude as part of its input parameters on the SOAP request.
        //            getDemo(zip, ref nLat, ref nLon, ref strCity, ref strState);

        //            // create an instance of the proxy class that handles the SOAP request/response.
        //            ndfdXML proxy = new ndfdXML();

        //            // make the SOAP request
        //            xmlForecast = proxy.NDFDgenByDay(nLat, nLon, DateTime.Now, m_numOfDays, formatType.Item12hourly);

        //            // transform the SOAP response using an XSL transform
        //            formattedForecast = transformForecast(xmlForecast, m_xslForecastTransform);

        //            // display the transformed forecast
        //            result = "<p><table style='border-collapse: collapse;' border=1 bordercolor=#111111 cellpadding=5 cellspacing=0 width=800px align=center>" +
        //                      "<tr class=subtitle1>" +
        //                      "    <td>";
        //            result += "      <strong>" + strCity + ", " + strState + "&nbsp;&nbsp;" + Request["zip"].ToString() + "</strong>";
        //            result += "    </td></tr></table>";

        //            result += formattedForecast;
        //        }
        //        textBoxOut.AppendText(result);
        //    }
        //    catch (Exception)
        //    {
        //        textBoxOut.AppendText("<p>Are you sure you entered a valid zip code?</p>");
        //    }
        //}

        //// function to transform the XML response from the web service
        //private string transformForecast(string xmlForecast, string strXsl)
        //{
        //    string result = System.String.Empty;
        //    string resultDoc = System.String.Empty;

        //    try
        //    {
        //        XslTransform xsl = new XslTransform();
        //        xsl.Load(Request.PhysicalApplicationPath + strXsl);
        //        XmlDocument xml = new XmlDocument();
        //        xml.LoadXml(xmlForecast);
        //        StringBuilder sb = new StringBuilder();
        //        StringWriter sw = new StringWriter(sb);
        //        XmlTextWriter wrtr = new XmlTextWriter(sw);

        //        xsl.Transform(xml.CreateNavigator(), null, wrtr, null);
        //        result = sb.ToString();

        //        return result;
        //    }
        //    catch (Exception excp)
        //    {
        //        return "<p>transformForecast: An error occurred: " + excp.ToString() + "</p>";
        //    }
        //}

        //// function to fetch the latitude, longitude, city, and state for a given zip code.
        //private void getDemo(long zipCode, ref decimal latitude, ref decimal longitude, ref string city, ref string state)
        //{
        //    string strSQL = System.String.Empty;

        //    MySQLConnection dbConn = new MySQLConnection(ConfigurationSettings.AppSettings["mySqlDsn"]);
        //    dbConn.Open();

        //    strSQL = "SELECT city, state, latitude, longitude" +
        //         " FROM  soccerwrek.zipcodes" +
        //         " WHERE zip = '" + zipCode.ToString() + "'";

        //    try
        //    {
        //        IDbCommand queryCmd = null;
        //        IDataReader reader = null;
        //        queryCmd = dbConn.CreateCommand();
        //        queryCmd.CommandText = strSQL;
        //        reader = queryCmd.ExecuteReader();
        //        reader.Read();

        //        if (reader["city"] != null)
        //            city = reader["city"].ToString();

        //        if (reader["state"] != null)
        //            state = reader["state"].ToString();

        //        if (reader["latitude"] != null)
        //            latitude = decimal.Parse(reader["latitude"].ToString());

        //        if (reader["longitude"] != null)
        //            longitude = decimal.Parse(reader["longitude"].ToString());

        //    }
        //    catch (System.IndexOutOfRangeException)
        //    {
        //        throw (new Exception("Zipcode not found"));
        //    }
        //    catch (Exception excp)
        //    {
        //        throw (excp);
        //    }
        //    finally
        //    {
        //        if (dbConn != null)
        //        {
        //            dbConn.Close();
        //            dbConn = null;
        //        }
        //    }
        //}

        //// button handler
        //private void btnSubmit_Click(object sender, System.EventArgs e)
        //{
        //    Response.Redirect("weatherForecast.aspx?zip=" + txtZip.Text);
        //}
    }
}
