using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;

namespace GeoGuessrWinForms.Forms
{
    public partial class LeaderboardForm : Form
    {
        public LeaderboardForm(List<LeaderboardEntry> allEntries, Player currentPlayer)
        {
            InitializeComponent();

            var sorted = allEntries.OrderByDescending(e => e.Score).ToList();
            var average = sorted.Average(e => e.Score);

            labelTitle.Text = $"Game Over - {currentPlayer.Nickname}";
            labelAverage.Text = $"Average Score: {average:F1}";

            dataGridViewLeaderboard.DataSource = sorted;

            foreach (DataGridViewRow row in dataGridViewLeaderboard.Rows)
            {
                if (row.Cells["Nickname"].Value?.ToString() == currentPlayer.Nickname)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

        private void buttonPlayAgain_Click(object sender, EventArgs e)
        {
            this.Hide();
            var startForm = new StartForm();
            startForm.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
