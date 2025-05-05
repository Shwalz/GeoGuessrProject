using System;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;
using GeoGuessrWinForms.Logic;
using System.Collections.Generic;

namespace GeoGuessrWinForms.Forms
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            comboBoxDifficulty.Items.AddRange(new string[] { "Easy", "Medium", "Hard" });
            comboBoxDifficulty.SelectedIndex = -1;

            trackBarRounds.Minimum = 1;
            trackBarRounds.Maximum = 5;
            trackBarRounds.Value = 3;
            labelRoundsValue.Text = "3";

            dataGridViewLeaderboard.DataSource = LeaderboardStorage.Load();
            dataGridViewLeaderboard.ClearSelection();

            buttonStart.Visible = false;
        }

        private void comboBoxDifficulty_SelectedIndexChanged(object sender, EventArgs e) => ValidateForm();
        private void textBoxNickname_TextChanged(object sender, EventArgs e) => ValidateForm();

        private void trackBarRounds_Scroll(object sender, EventArgs e)
        {
            labelRoundsValue.Text = trackBarRounds.Value.ToString();
        }

        private void ValidateForm()
        {
            buttonStart.Visible = !string.IsNullOrWhiteSpace(textBoxNickname.Text) &&
                                  comboBoxDifficulty.SelectedIndex >= 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var player = new Player { Nickname = textBoxNickname.Text.Trim() };

            var settings = new GameSettings
            {
                Difficulty = comboBoxDifficulty.SelectedItem.ToString(),
                TotalRounds = trackBarRounds.Value
            };

            Hide();
            var gameForm = new GameForm(player, settings);
            gameForm.Show();
        }
    }
}
