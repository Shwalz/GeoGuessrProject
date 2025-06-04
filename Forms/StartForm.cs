using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GeoGuessrWinForms.Models;
using GeoGuessrWinForms.Logic;

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
            comboBoxContinent.Items.AddRange(new string[] { "Europe", "Asia", "America", "Africa", "Oceania" });
            comboBoxContinent.SelectedIndex = -1;

            comboBoxDifficulty.Items.Clear();
            comboBoxDifficulty.Enabled = false;

            trackBarRounds.Minimum = 1;
            trackBarRounds.Maximum = 5;
            trackBarRounds.Value = 3;
            labelRoundsValue.Text = "3";

            dataGridViewLeaderboard.DataSource = LeaderboardStorage.Load();
            dataGridViewLeaderboard.ClearSelection();

            buttonStart.Visible = false;

            comboBoxContinent.SelectedIndexChanged += comboBoxContinent_SelectedIndexChanged;
            comboBoxDifficulty.SelectedIndexChanged += comboBoxDifficulty_SelectedIndexChanged;
            textBoxNickname.TextChanged += textBoxNickname_TextChanged;
        }

        private void comboBoxContinent_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDifficulty.Items.Clear();
            comboBoxDifficulty.Items.AddRange(new string[] { "Easy", "Medium", "Hard" });
            comboBoxDifficulty.Enabled = true;
            comboBoxDifficulty.SelectedIndex = -1;

            ValidateForm();
        }

        private void comboBoxDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void textBoxNickname_TextChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void trackBarRounds_Scroll(object sender, EventArgs e)
        {
            labelRoundsValue.Text = trackBarRounds.Value.ToString();
        }

        private void ValidateForm()
        {
            buttonStart.Visible =
                !string.IsNullOrWhiteSpace(textBoxNickname.Text) &&
                comboBoxContinent.SelectedIndex >= 0 &&
                comboBoxDifficulty.SelectedIndex >= 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var player = new Player { Nickname = textBoxNickname.Text.Trim() };

            var settings = new GameSettings
            {
                Continent = comboBoxContinent.SelectedItem.ToString(),
                Difficulty = comboBoxDifficulty.SelectedItem.ToString(),
                TotalRounds = trackBarRounds.Value
            };

            Hide();
            var gameForm = new GameForm(player, settings);
            gameForm.Show();
        }

        private void buttonAdminPanel_Click(object sender, EventArgs e)
        {
            var adminForm = new AdminPanelForm();
            adminForm.ShowDialog();
        }
    }
}
