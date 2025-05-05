using System.Drawing;
using System.Windows.Forms;

namespace GeoGuessrWinForms.Forms
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label labelNickname;
        private TextBox textBoxNickname;

        private Label labelDifficulty;
        private ComboBox comboBoxDifficulty;

        private Label labelRounds;
        private TrackBar trackBarRounds;
        private Label labelRoundsValue;

        private Button buttonStart;

        private DataGridView dataGridViewLeaderboard;
        private Label labelLeaderboard;

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

            labelDifficulty = new Label();
            comboBoxDifficulty = new ComboBox();

            labelRounds = new Label();
            labelRoundsValue = new Label();
            trackBarRounds = new TrackBar();

            buttonStart = new Button();

            labelLeaderboard = new Label();
            dataGridViewLeaderboard = new DataGridView();

            SuspendLayout();

            ClientSize = new Size(800, 500);
            BackgroundImage = Image.FromFile("Resources/star_background.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
            Font = new Font("Segoe UI", 10F);
            Text = "GeoGuessr - Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            Load += StartForm_Load;

            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color whiteText = Color.White;

            labelLeaderboard.Text = "Leaderboard";
            labelLeaderboard.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelLeaderboard.ForeColor = whiteText;
            labelLeaderboard.BackColor = transparent;
            labelLeaderboard.Location = new Point(30, 20);
            labelLeaderboard.Size = new Size(200, 30);

            dataGridViewLeaderboard.Location = new Point(30, 60);
            dataGridViewLeaderboard.Size = new Size(250, 380);
            dataGridViewLeaderboard.ReadOnly = true;
            dataGridViewLeaderboard.AllowUserToAddRows = false;
            dataGridViewLeaderboard.AllowUserToDeleteRows = false;
            dataGridViewLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLeaderboard.BackgroundColor = Color.FromArgb(240, 240, 240);
            dataGridViewLeaderboard.DefaultCellStyle.BackColor = Color.White;
            dataGridViewLeaderboard.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewLeaderboard.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dataGridViewLeaderboard.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewLeaderboard.BorderStyle = BorderStyle.FixedSingle;

            labelNickname.Text = "Nickname";
            labelNickname.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelNickname.ForeColor = whiteText;
            labelNickname.BackColor = transparent;
            labelNickname.TextAlign = ContentAlignment.MiddleCenter;
            labelNickname.Location = new Point(300, 100);
            labelNickname.Size = new Size(200, 30);

            textBoxNickname.Location = new Point(300, 135);
            textBoxNickname.Size = new Size(200, 30);
            textBoxNickname.BackColor = Color.White;
            textBoxNickname.BorderStyle = BorderStyle.FixedSingle;
            textBoxNickname.Font = new Font("Segoe UI", 10F);
            textBoxNickname.TextChanged += textBoxNickname_TextChanged;

            labelDifficulty.Text = "Select Difficulty";
            labelDifficulty.ForeColor = whiteText;
            labelDifficulty.BackColor = transparent;
            labelDifficulty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDifficulty.Location = new Point(550, 100);
            labelDifficulty.Size = new Size(200, 25);

            comboBoxDifficulty.Location = new Point(550, 130);
            comboBoxDifficulty.Size = new Size(200, 30);
            comboBoxDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDifficulty.BackColor = Color.White;
            comboBoxDifficulty.FlatStyle = FlatStyle.Flat;
            comboBoxDifficulty.SelectedIndexChanged += comboBoxDifficulty_SelectedIndexChanged;

            labelRounds.Text = "Rounds:";
            labelRounds.ForeColor = whiteText;
            labelRounds.BackColor = transparent;
            labelRounds.Location = new Point(550, 180);
            labelRounds.Size = new Size(100, 25);

            labelRoundsValue.Text = "3";
            labelRoundsValue.ForeColor = whiteText;
            labelRoundsValue.BackColor = transparent;
            labelRoundsValue.Location = new Point(630, 180);
            labelRoundsValue.Size = new Size(40, 25);

            trackBarRounds.Location = new Point(550, 210);
            trackBarRounds.Size = new Size(200, 45);
            trackBarRounds.Minimum = 1;
            trackBarRounds.Maximum = 5;
            trackBarRounds.Value = 3;
            trackBarRounds.TickStyle = TickStyle.BottomRight;
            trackBarRounds.Scroll += trackBarRounds_Scroll;

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

            Controls.Add(labelLeaderboard);
            Controls.Add(dataGridViewLeaderboard);
            Controls.Add(labelNickname);
            Controls.Add(textBoxNickname);
            Controls.Add(labelDifficulty);
            Controls.Add(comboBoxDifficulty);
            Controls.Add(labelRounds);
            Controls.Add(labelRoundsValue);
            Controls.Add(trackBarRounds);
            Controls.Add(buttonStart);

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

            comboBoxDifficulty.FlatStyle = FlatStyle.Flat;
            comboBoxDifficulty.BackColor = Color.FromArgb(30, 30, 30);
            comboBoxDifficulty.ForeColor = Color.White;
            comboBoxDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDifficulty.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            trackBarRounds.BackColor = Color.FromArgb(30, 30, 30);
            trackBarRounds.TickStyle = TickStyle.Both;
            trackBarRounds.ForeColor = Color.White;
            trackBarRounds.Minimum = 1;
            trackBarRounds.Maximum = 5;
            trackBarRounds.Value = 3;


            ResumeLayout(false);
        }

    }
}
