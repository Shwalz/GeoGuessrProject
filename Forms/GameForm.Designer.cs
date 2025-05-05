namespace GeoGuessrWinForms.Forms
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        public System.Windows.Forms.Label labelRound;
        public System.Windows.Forms.Label labelTimer;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewStreetView;
        private Microsoft.Web.WebView2.WinForms.WebView2 webViewMiniMap;
        private Panel panelResult;
        private Label labelDistance;
        private Label labelRoundScore;
        private Label labelTotalScore;

        private Button buttonCheck;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelRound = new Label();
            labelTimer = new Label();
            webViewStreetView = new Microsoft.Web.WebView2.WinForms.WebView2();
            webViewMiniMap = new Microsoft.Web.WebView2.WinForms.WebView2();
            panelResult = new Panel();
            labelDistance = new Label();
            labelRoundScore = new Label();
            labelTotalScore = new Label();
            buttonCheck = new Button();
            ((System.ComponentModel.ISupportInitialize)webViewStreetView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webViewMiniMap).BeginInit();
            panelResult.SuspendLayout();
            SuspendLayout();

            labelRound.AutoSize = true;
            labelRound.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelRound.Location = new Point(20, 10);
            labelRound.Name = "labelRound";
            labelRound.Size = new Size(77, 21);
            labelRound.TabIndex = 0;
            labelRound.Text = "Round: 1";

            labelTimer.AutoSize = true;
            labelTimer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTimer.Location = new Point(150, 10);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(74, 21);
            labelTimer.TabIndex = 1;
            labelTimer.Text = "Time: 90";

            webViewStreetView.AllowExternalDrop = true;
            webViewStreetView.CreationProperties = null;
            webViewStreetView.DefaultBackgroundColor = Color.White;
            webViewStreetView.Location = new Point(20, 50);
            webViewStreetView.Name = "webViewStreetView";
            webViewStreetView.Size = new Size(650, 420);
            webViewStreetView.TabIndex = 2;
            webViewStreetView.ZoomFactor = 1D;

            webViewMiniMap.AllowExternalDrop = true;
            webViewMiniMap.CreationProperties = null;
            webViewMiniMap.DefaultBackgroundColor = Color.White;
            webViewMiniMap.Location = new Point(690, 50);
            webViewMiniMap.Name = "webViewMiniMap";
            webViewMiniMap.Size = new Size(280, 280);
            webViewMiniMap.TabIndex = 3;
            webViewMiniMap.ZoomFactor = 1D;

            panelResult.BackColor = Color.FromArgb(240, 255, 255, 255);
            panelResult.BorderStyle = BorderStyle.FixedSingle;
            panelResult.Controls.Add(labelDistance);
            panelResult.Controls.Add(labelRoundScore);
            panelResult.Controls.Add(labelTotalScore);
            panelResult.Location = new Point(690, 340);
            panelResult.Name = "panelResult";
            panelResult.Size = new Size(280, 100);
            panelResult.TabIndex = 4;
            panelResult.Visible = false;

            labelDistance.Font = new Font("Segoe UI", 9.5F);
            labelDistance.ForeColor = Color.DimGray;
            labelDistance.Location = new Point(10, 10);
            labelDistance.Name = "labelDistance";
            labelDistance.Size = new Size(260, 20);
            labelDistance.TabIndex = 0;

            labelRoundScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRoundScore.Location = new Point(10, 35);
            labelRoundScore.Name = "labelRoundScore";
            labelRoundScore.Size = new Size(260, 20);
            labelRoundScore.TabIndex = 1;

            labelTotalScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalScore.ForeColor = Color.RoyalBlue;
            labelTotalScore.Location = new Point(10, 60);
            labelTotalScore.Name = "labelTotalScore";
            labelTotalScore.Size = new Size(260, 20);
            labelTotalScore.TabIndex = 2;

            buttonCheck.BackColor = Color.White;
            buttonCheck.Cursor = Cursors.Hand;
            buttonCheck.FlatAppearance.BorderColor = Color.Gray;
            buttonCheck.FlatStyle = FlatStyle.Flat;
            buttonCheck.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonCheck.Location = new Point(690, 460);
            buttonCheck.Name = "buttonCheck";
            buttonCheck.Size = new Size(280, 40);
            buttonCheck.TabIndex = 5;
            buttonCheck.Text = "Check";
            buttonCheck.UseVisualStyleBackColor = false;
            buttonCheck.Click += buttonCheck_Click;

            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1000, 600);
            Controls.Add(labelRound);
            Controls.Add(labelTimer);
            Controls.Add(webViewStreetView);
            Controls.Add(webViewMiniMap);
            Controls.Add(panelResult);
            Controls.Add(buttonCheck);
            Font = new Font("Segoe UI", 10F);
            Name = "GameForm";
            Text = "GeoGuessr - Game";
            Load += GameForm_Load;
            ((System.ComponentModel.ISupportInitialize)webViewStreetView).EndInit();
            ((System.ComponentModel.ISupportInitialize)webViewMiniMap).EndInit();
            panelResult.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

    }
}
