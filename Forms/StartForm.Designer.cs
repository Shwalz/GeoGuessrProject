using System.Drawing;
using System.Windows.Forms;

namespace GeoGuessrWinForms.Forms
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label labelNickname;
        private TextBox textBoxNickname;

        private Label labelContinent;
        private ComboBox comboBoxContinent;

        private Label labelDifficulty;
        private ComboBox comboBoxDifficulty;

        private Label labelRounds;
        private Label labelRoundsValue;
        private TrackBar trackBarRounds;

        private Button buttonStart;
        private Button buttonAdminPanel;

        private Label labelLeaderboard;
        private DataGridView dataGridViewLeaderboard;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            labelNickname = new Label();
            textBoxNickname = new TextBox();

            labelContinent = new Label();
            comboBoxContinent = new ComboBox();

            labelDifficulty = new Label();
            comboBoxDifficulty = new ComboBox();

            labelRounds = new Label();
            labelRoundsValue = new Label();
            trackBarRounds = new TrackBar();

            buttonStart = new Button();
            buttonAdminPanel = new Button();

            labelLeaderboard = new Label();
            dataGridViewLeaderboard = new DataGridView();

            SuspendLayout();

            // Form settings
            ClientSize = new Size(800, 500);
            BackgroundImage = Image.FromFile("Resources/star_background.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            Font = new Font("Segoe UI", 10F);
            Text = "GeoGuessr - Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            Load += StartForm_Load;

            Color whiteText = Color.White;

            // Leaderboard label
            labelLeaderboard.Text = "Leaderboard";
            labelLeaderboard.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelLeaderboard.ForeColor = whiteText;
            labelLeaderboard.BackColor = Color.Transparent;
            labelLeaderboard.Location = new Point(30, 20);
            labelLeaderboard.Size = new Size(200, 30);

            // Leaderboard grid
            dataGridViewLeaderboard.Location = new Point(30, 60);
            dataGridViewLeaderboard.Size = new Size(250, 380);
            dataGridViewLeaderboard.ReadOnly = true;
            dataGridViewLeaderboard.AllowUserToAddRows = false;
            dataGridViewLeaderboard.AllowUserToDeleteRows = false;
            dataGridViewLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLeaderboard.BorderStyle = BorderStyle.None;
            dataGridViewLeaderboard.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewLeaderboard.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dataGridViewLeaderboard.DefaultCellStyle.ForeColor = Color.White;
            dataGridViewLeaderboard.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70);
            dataGridViewLeaderboard.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewLeaderboard.RowHeadersVisible = false;
            dataGridViewLeaderboard.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridViewLeaderboard.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewLeaderboard.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewLeaderboard.EnableHeadersVisualStyles = false;

            // Nickname label
            labelNickname.Text = "Nickname";
            labelNickname.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelNickname.ForeColor = whiteText;
            labelNickname.BackColor = Color.Transparent;
            labelNickname.TextAlign = ContentAlignment.MiddleCenter;
            labelNickname.Location = new Point(300, 100);
            labelNickname.Size = new Size(200, 30);

            // Nickname textbox
            textBoxNickname.Location = new Point(300, 135);
            textBoxNickname.Size = new Size(200, 30);
            textBoxNickname.BackColor = Color.White;
            textBoxNickname.BorderStyle = BorderStyle.FixedSingle;
            textBoxNickname.Font = new Font("Segoe UI", 10F);
            textBoxNickname.TextChanged += textBoxNickname_TextChanged;

            // Continent label
            labelContinent.Text = "Continent:";
            labelContinent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelContinent.ForeColor = whiteText;
            labelContinent.BackColor = Color.Transparent;
            labelContinent.Location = new Point(550, 40);
            labelContinent.Size = new Size(200, 25);

            // Continent combo box
            comboBoxContinent.Location = new Point(550, 70);
            comboBoxContinent.Size = new Size(200, 30);
            comboBoxContinent.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxContinent.FlatStyle = FlatStyle.Flat;
            comboBoxContinent.BackColor = Color.FromArgb(30, 30, 30);
            comboBoxContinent.ForeColor = Color.White;
            comboBoxContinent.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            comboBoxContinent.SelectedIndexChanged += comboBoxContinent_SelectedIndexChanged;

            // Difficulty label
            labelDifficulty.Text = "Select Difficulty";
            labelDifficulty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDifficulty.ForeColor = whiteText;
            labelDifficulty.BackColor = Color.Transparent;
            labelDifficulty.Location = new Point(550, 100);
            labelDifficulty.Size = new Size(200, 25);

            // Difficulty combo box
            comboBoxDifficulty.Location = new Point(550, 130);
            comboBoxDifficulty.Size = new Size(200, 30);
            comboBoxDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDifficulty.FlatStyle = FlatStyle.Flat;
            comboBoxDifficulty.BackColor = Color.FromArgb(30, 30, 30);
            comboBoxDifficulty.ForeColor = Color.White;
            comboBoxDifficulty.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            comboBoxDifficulty.SelectedIndexChanged += comboBoxDifficulty_SelectedIndexChanged;

            // Rounds label
            labelRounds.Text = "Rounds:";
            labelRounds.ForeColor = whiteText;
            labelRounds.BackColor = Color.Transparent;
            labelRounds.Location = new Point(550, 180);
            labelRounds.Size = new Size(100, 25);

            // Rounds value
            labelRoundsValue.Text = "3";
            labelRoundsValue.ForeColor = whiteText;
            labelRoundsValue.BackColor = Color.Transparent;
            labelRoundsValue.Location = new Point(630, 180);
            labelRoundsValue.Size = new Size(40, 25);

            // Rounds trackbar
            trackBarRounds.Location = new Point(550, 210);
            trackBarRounds.Size = new Size(200, 45);
            trackBarRounds.Minimum = 1;
            trackBarRounds.Maximum = 5;
            trackBarRounds.Value = 3;
            trackBarRounds.TickStyle = TickStyle.Both;
            trackBarRounds.BackColor = Color.FromArgb(30, 30, 30);
            trackBarRounds.ForeColor = Color.White;
            trackBarRounds.Scroll += trackBarRounds_Scroll;

            // Start button
            buttonStart.Text = "Start Game";
            buttonStart.Location = new Point(325, 380);
            buttonStart.Size = new Size(150, 45);
            buttonStart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonStart.BackColor = Color.MediumSlateBlue;
            buttonStart.ForeColor = Color.White;
            buttonStart.FlatStyle = FlatStyle.Flat;
            buttonStart.FlatAppearance.BorderColor = Color.White;
            buttonStart.Visible = false;
            buttonStart.Click += buttonStart_Click;

            // Admin button
            buttonAdminPanel.Text = "Admin Panel";
            buttonAdminPanel.Location = new Point(30, 450);
            buttonAdminPanel.Size = new Size(250, 30);
            buttonAdminPanel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            buttonAdminPanel.BackColor = Color.DarkSlateGray;
            buttonAdminPanel.ForeColor = Color.White;
            buttonAdminPanel.FlatStyle = FlatStyle.Flat;
            buttonAdminPanel.Click += buttonAdminPanel_Click;

            // Add controls
            Controls.Add(labelLeaderboard);
            Controls.Add(dataGridViewLeaderboard);
            Controls.Add(labelNickname);
            Controls.Add(textBoxNickname);
            Controls.Add(labelContinent);
            Controls.Add(comboBoxContinent);
            Controls.Add(labelDifficulty);
            Controls.Add(comboBoxDifficulty);
            Controls.Add(labelRounds);
            Controls.Add(labelRoundsValue);
            Controls.Add(trackBarRounds);
            Controls.Add(buttonStart);
            Controls.Add(buttonAdminPanel);

            ResumeLayout(false);
        }
    }
}
