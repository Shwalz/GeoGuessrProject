namespace GeoGuessrWinForms.Forms
{
    partial class AdminPanelForm
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewMap;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewPreview;
        private ComboBox comboBoxContinent;
        private DataGridView dataGridViewLocations;
        private ComboBox comboBoxDifficulty;
        private Button buttonSave;
        private Button buttonDelete;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Text = "Admin Panel – Add Location";
            this.ClientSize = new System.Drawing.Size(880, 460);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Load += AdminPanelForm_Load;

            // Yandex Map
            webViewMap = new Microsoft.Web.WebView2.WinForms.WebView2
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(380, 220),
                Name = "webViewMap",
                DefaultBackgroundColor = System.Drawing.Color.White
            };
            Controls.Add(webViewMap);

            // Google StreetView Preview
            webViewPreview = new Microsoft.Web.WebView2.WinForms.WebView2
            {
                Location = new System.Drawing.Point(460, 20),
                Size = new System.Drawing.Size(380, 220),
                Name = "webViewPreview",
                DefaultBackgroundColor = System.Drawing.Color.White
            };
            Controls.Add(webViewPreview);

            // Continent ComboBox
            comboBoxContinent = new ComboBox
            {
                Location = new System.Drawing.Point(150, 260),
                Size = new System.Drawing.Size(220, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            comboBoxContinent.Items.AddRange(new[] { "Europe", "Asia", "North America", "South America", "Africa" });
            Controls.Add(comboBoxContinent);

            // Difficulty ComboBox
            comboBoxDifficulty = new ComboBox
            {
                Location = new System.Drawing.Point(480, 260),
                Size = new System.Drawing.Size(220, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            comboBoxDifficulty.Items.AddRange(new[] { "Easy", "Medium", "Hard" });
            Controls.Add(comboBoxDifficulty);

            // Save Button
            buttonSave = new Button
            {
                Text = "Save Location",
                Location = new System.Drawing.Point(220, 310),
                Size = new System.Drawing.Size(180, 40),
                BackColor = System.Drawing.Color.FromArgb(39, 174, 96),
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.Click += buttonSave_Click;
            Controls.Add(buttonSave);

        }

    }
}
