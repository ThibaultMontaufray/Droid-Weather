namespace Droid_weather
{
    partial class PanelEarth
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this._map = new GMap.NET.WindowsForms.GMapControl();
            this.comboBoxSrc = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxSrc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 26);
            this.panel1.TabIndex = 2;
            // 
            // _map
            // 
            this._map.Bearing = 0F;
            this._map.CanDragMap = true;
            this._map.Dock = System.Windows.Forms.DockStyle.Fill;
            this._map.EmptyTileColor = System.Drawing.Color.Navy;
            this._map.GrayScaleMode = false;
            this._map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this._map.LevelsKeepInMemmory = 5;
            this._map.Location = new System.Drawing.Point(0, 26);
            this._map.MarkersEnabled = true;
            this._map.MaxZoom = 18;
            this._map.MinZoom = 2;
            this._map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this._map.Name = "_map";
            this._map.NegativeMode = false;
            this._map.PolygonsEnabled = true;
            this._map.RetryLoadTile = 0;
            this._map.RoutesEnabled = true;
            this._map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this._map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this._map.ShowTileGridLines = false;
            this._map.Size = new System.Drawing.Size(800, 574);
            this._map.TabIndex = 3;
            this._map.Zoom = 5D;
            // 
            // comboBoxSrc
            // 
            this.comboBoxSrc.FormattingEnabled = true;
            this.comboBoxSrc.Items.AddRange(new object[] {
            "Open Street Map",
            "Google",
            "Bing"});
            this.comboBoxSrc.Location = new System.Drawing.Point(3, 3);
            this.comboBoxSrc.Name = "comboBoxSrc";
            this.comboBoxSrc.Size = new System.Drawing.Size(199, 21);
            this.comboBoxSrc.TabIndex = 0;
            this.comboBoxSrc.SelectedIndexChanged += new System.EventHandler(this.comboBoxSrc_SelectedIndexChanged);
            // 
            // PanelEarth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._map);
            this.Controls.Add(this.panel1);
            this.Name = "PanelEarth";
            this.Size = new System.Drawing.Size(800, 600);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private GMap.NET.WindowsForms.GMapControl _map;
        private System.Windows.Forms.ComboBox comboBoxSrc;
    }
}
