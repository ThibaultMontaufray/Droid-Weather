using System;

namespace Droid_weather
{
    public class Earthquake
    {
        #region Attribute
        private int _id;
        private DateTime _time;
        private double _latitude;
        private double _longitude;
        private int _depth;
        private string _author;
        private string _catalog;
        private string _contributor;
        private string _contributorID;
        private string _type;
        private double _magnitude;
        private string _magAuthor;
        private string _location;
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        public int Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        public string Catalog
        {
            get { return _catalog; }
            set { _catalog = value; }
        }
        public string Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }
        public string ContributorID
        {
            get { return _contributorID; }
            set { _contributorID = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public double Magnitude
        {
            get { return _magnitude; }
            set { _magnitude = value; }
        }
        public string MagAuthor
        {
            get { return _magAuthor; }
            set { _magAuthor = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        #endregion

        #region Constructor
        public Earthquake()
        {

        }
        #endregion

        #region Methods public

        #endregion

        #region Methods private
        #endregion
    }
}
