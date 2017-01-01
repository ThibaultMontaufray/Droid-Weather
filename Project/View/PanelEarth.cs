using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;

namespace Droid_weather
{
    public partial class PanelEarth : UserControl
    {
        #region Attribute
        private List<KeyValuePair<double, double>> _coordinates;
        #endregion

        #region Properties
        public List<KeyValuePair<double, double>> Coordinaites
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
        #endregion

        #region Constructor
        public PanelEarth()
        {
            _coordinates = new List<KeyValuePair<double, double>>();
            InitializeComponent();

            _map.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            _map.SetPositionByKeywords("Maputo, Mozambique");

            _map.Position = new PointLatLng(-25.971684, 32.589759);
        }
        #endregion

        #region Methods public
        #endregion

        #region Methods private
        private void LoadCoordinates()
        {
            foreach (var item in _coordinates)
            {
            }
        }
        #endregion

        #region Event
        private void comboBoxSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxSrc.Text)
            {
                case "Bing":
                    _map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
                    break;
                case "Open Street Map":
                    _map.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
                    break;
                case "Google":
                    _map.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                    break;
            }
            _map.Refresh();
        }
        #endregion
    }
}
