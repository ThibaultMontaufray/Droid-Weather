using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools4Libraries;

namespace Droid_weather
{
    public class InterfaceWeather
    {
        #region Attribute
        private static InterfaceWeather _intWeather;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public InterfaceWeather()
        {

        }
        #endregion

        #region Methods public
        public static void ACTION_130_lister_seismes()
        {
            if (_intWeather == null) {  _intWeather = new InterfaceWeather(); }
            _intWeather.GetEarthquake();
        }
        #endregion

        #region Methods private  
        private List<Earthquake> GetEarthquake(DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), int latitudeMin = -70, int latitudeMax = 70, int longitudeMin = -170, int longitudeMax = 170, int depthMin = 10, int depthMax = 900, int magnitudeMin = 1, int magnitudeMax = 90)
        {
            string[] headers = null;
            string[] tab = null;
            Earthquake earthquake = null;
            List<Earthquake> earthquakes = new List<Earthquake>();
            if (startDate.Year == 1) { startDate = new DateTime(1900, 1, 1, 0, 0, 1); }
            if (endDate.Year == 1) { endDate = new DateTime(2999, 12, 30, 23, 59, 59); }

            string url = string.Format("http://www.edusismo.org/ws/event/query?&starttime={0}&endtime={1}&minlat={2}&maxlat={3}&minlon={4}&maxlon={5}&mindepth={6}&maxdepth={7}&minmag={8}&maxmag={9}&sta=*&net=*&loc=*&cha=*", startDate.ToString("yyyy-MM-ddTHH:mm:ss"), endDate.ToString("yyyy-MM-ddTHH:mm:ss"), latitudeMin, latitudeMax, longitudeMin, longitudeMax, depthMin, depthMax, magnitudeMin, magnitudeMax);
            string page = Droid_web.Web.GetPage(url);

            if (!string.IsNullOrEmpty(page))
            {
                foreach (string row in page.Split('\n'))
                {
                    if (!row.StartsWith("# ") && !row.StartsWith("#param"))
                    {
                        tab = row.Split('|');
                        if (row.StartsWith("#"))
                        {
                            headers = new string[row.Split('|').Length];
                            for (int i = 0; i < tab.Length; i++)
                            {
                                headers[i] = tab[i].Replace("#", string.Empty).Trim();
                            }
                        }
                        else if (headers != null)
                        {
                            earthquake = new Earthquake();
                            for (int i = 0; i < tab.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(tab[i]))
                                switch (headers[i])
                                    {
                                        case "EventID":
                                            earthquake.Id = int.Parse(tab[i]);
                                            break;
                                        case "Time":
                                            earthquake.Time = DateTime.Parse(tab[i]);
                                            break;
                                        case "Latitude":
                                            earthquake.Latitude = double.Parse(tab[i]);
                                            break;
                                        case "Longitude":
                                            earthquake.Longitude = double.Parse(tab[i]);
                                            break;
                                        case "Depth":
                                            earthquake.Depth = int.Parse(tab[i]);
                                            break;
                                        case "Author":
                                            earthquake.Author = tab[i];
                                            break;
                                        case "Catalog":
                                            earthquake.Catalog = tab[i];
                                            break;
                                        case "Contributor":
                                            earthquake.Contributor = tab[i];
                                            break;
                                        case "ContributorID":
                                            earthquake.ContributorID = tab[i];
                                            break;
                                        case "MagType":
                                            earthquake.Type = tab[i];
                                            break;
                                        case "Magnitude":
                                            earthquake.Magnitude = double.Parse(tab[i]);
                                            break;
                                        case "MagAuthor":
                                            earthquake.MagAuthor = tab[i];
                                            break;
                                        case "EventLocationName":
                                            earthquake.Location = tab[i];
                                            break;
                                    }
                            }
                            earthquakes.Add(earthquake);
                        }
                    }
                }
            }
            return earthquakes;
        }
        #endregion
    }
}
