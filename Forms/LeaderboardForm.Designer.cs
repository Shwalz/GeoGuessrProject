namespace GeoGuessrWinForms.Forms
{
    partial class LeaderboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelTitle;
        private Label labelAverage;
        private DataGridView dataGridViewLeaderboard;
        private Button buttonPlayAgain;
        private Button buttonExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            labelTitle = new Label();
            labelAverage = new Label();
            dataGridViewLeaderboard = new DataGridView();
            buttonPlayAgain = new Button();
            buttonExit = new Button();

            SuspendLayout();

            ClientSize = new Size(520, 400);
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F);
            Text = "Leaderboard";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.Location = new Point(20, 20);
            labelTitle.Text = "Game Over";

            dataGridViewLeaderboard.Location = new Point(20, 60);
            dataGridViewLeaderboard.Size = new Size(480, 250);
            dataGridViewLeaderboard.ReadOnly = true;
            dataGridViewLeaderboard.AllowUserToAddRows = false;
            dataGridViewLeaderboard.AllowUserToDeleteRows = false;
            dataGridViewLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLeaderboard.BackgroundColor = Color.White;
            dataGridViewLeaderboard.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewLeaderboard.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewLeaderboard.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            labelAverage.AutoSize = true;
            labelAverage.Font = new Font("Segoe UI", 10F);
            labelAverage.Location = new Point(20, 325);
            labelAverage.Text = "Average Score: 0";

            buttonPlayAgain.Text = "Play Again";
            buttonPlayAgain.Size = new Size(100, 35);
            buttonPlayAgain.Location = new Point(280, 320);
            buttonPlayAgain.BackColor = Color.White;
            buttonPlayAgain.FlatStyle = FlatStyle.Flat;
            buttonPlayAgain.FlatAppearance.BorderColor = Color.Gray;
            buttonPlayAgain.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonPlayAgain.Cursor = Cursors.Hand;
            buttonPlayAgain.Click += buttonPlayAgain_Click;

            buttonExit.Text = "Exit";
            buttonExit.Size = new Size(100, 35);
            buttonExit.Location = new Point(400, 320);
            buttonExit.BackColor = Color.White;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.FlatAppearance.BorderColor = Color.Gray;
            buttonExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonExit.Cursor = Cursors.Hand;
            buttonExit.Click += buttonExit_Click;

            Controls.Add(labelTitle);
            Controls.Add(dataGridViewLeaderboard);
            Controls.Add(labelAverage);
            Controls.Add(buttonPlayAgain);
            Controls.Add(buttonExit);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
