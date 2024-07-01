using FlashcardReminder;
using FlashcardReminder.Forms;
using FlashcardReminder.Properties;
using System;
using System.Data;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class SettingsWindow : Form
    {
        public int CardIntervalTimerMilliseconds
        {
            get
            {
                if (cit_seconds.Text.Length == 0) cit_seconds.Text = "0";
                if (cit_minutes.Text.Length == 0) cit_minutes.Text = "0";

                int val = int.Parse(cit_seconds.Text);
                val += int.Parse(cit_minutes.Text) * 60;

                return Math.Max(1000, val * 1000);
            }
        }
        public int AnswerPrivewTimerMilliseconds
        {
            get
            {
                if (apt_seconds.Text.Length == 0) apt_seconds.Text = "0";
                if (apt_minutes.Text.Length == 0) apt_minutes.Text = "0";

                int val = int.Parse(apt_seconds.Text);
                val += int.Parse(apt_minutes.Text) * 60;
                return Math.Max(1000, val * 1000);
            }
        }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.CardIntervalTimerMilliseconds = CardIntervalTimerMilliseconds;
            Globals.AnswerPrivewTimerMilliseconds = AnswerPrivewTimerMilliseconds;

            Globals.timer.Interval = CardIntervalTimerMilliseconds;
            Globals.SaveDecks();
            Globals.timer.Start();

            Globals.cardViewForm.FormBorderStyle = FormBorderStyle.None;
            Globals.cardViewForm.tbx_input.Enabled = true;
            Globals.cardViewForm.btn_check.Text = "Check";
            Globals.cardViewForm.btn_check.Enabled = true;
            Globals.cardViewForm.Hide();

            Settings.Default.CardIntervalTimerMilliseconds = Globals.CardIntervalTimerMilliseconds;
            Settings.Default.AnswerPrivewTimerMilliseconds = Globals.AnswerPrivewTimerMilliseconds;
            Settings.Default.SpeechRate = Globals.speechRate;
            Settings.Default.Save();

            Globals.settingsForm = null;
        }

        private void SettingsWindow_Load(object sender, EventArgs e)
        {
            Globals.timer.Stop();

            // Fill the text boxes with the values from Globals
            (int citMinutes, int citSeconds) = ConvertMillisecondsToMinutesAndSeconds(Globals.CardIntervalTimerMilliseconds);
            cit_minutes.Text = citMinutes.ToString();
            cit_seconds.Text = citSeconds.ToString();

            (int aptMinutes, int aptSeconds) = ConvertMillisecondsToMinutesAndSeconds(Globals.AnswerPrivewTimerMilliseconds);
            apt_minutes.Text = aptMinutes.ToString();
            apt_seconds.Text = aptSeconds.ToString();

            for (int i = 0; i < Globals.flashCardsDecks.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = Globals.flashCardsDecks[i].DeckName + " : " + Globals.flashCardsDecks[i].Cards.Count + " cards";
                checkBox.Name = $"f_{i}";
                checkBox.Checked = Globals.flashCardsDecks[i].DeckEnabled;
                checkBox.Width = 256;
                checkBox.CheckedChanged += (s, ea)=>
                {
                    int ind = int.Parse(checkBox.Name.Split('_')[1]);
                    Globals.flashCardsDecks[ind].DeckEnabled = checkBox.Checked;
                    lbl_count.Text = $"Avilable Decks: {Globals.flashCardsDecks.Count}, Active: {Globals.flashCardsDecks.Where((c) => c.DeckEnabled).Count()}";
                };
                flowLayoutPanel1.Controls.Add(checkBox);
            }

            lbl_count.Text = $"Avilable Decks: {Globals.flashCardsDecks.Count}, Active: {Globals.flashCardsDecks.Where((c) => c.DeckEnabled).Count()}";

            Globals.cardViewForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Globals.cardViewForm.lbl_question.Text = "";
            Globals.cardViewForm.tbx_input.Text = "";
            Globals.cardViewForm.tbx_input.Enabled = false;
            Globals.cardViewForm.btn_check.Text = "This is preview";
            Globals.cardViewForm.btn_check.Enabled = false;

             numericUpDown1.Value = Globals.speechRate;

            if (!Globals.cardViewForm.Visible)
                Globals.cardViewForm.Show();
        }


        private (int minutes, int seconds) ConvertMillisecondsToMinutesAndSeconds(int milliseconds)
        {
            int totalSeconds = milliseconds / 1000;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return (minutes, seconds);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters (like backspace) and digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Invalid character - cancel the event
                e.Handled = true;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            Globals.LoadDecks();
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                flowLayoutPanel1.Controls[i].Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            SettingsWindow_Load(sender, e);
        }

        private void btn_import_Click(object sender, EventArgs e)
        {
            string filePath;
            string userInput;

            filePath = Globals.GetFile("Input File", "Text Files (*.txt)|*.txt|Json Files (*.json)|*.json");
            if (filePath == null) return;

            if(filePath.EndsWith(".txt"))
            {
                InputDialog dialog = new InputDialog();
                dialog.Label = "Language Code";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    userInput = dialog.Value;
                }
                else
                {
                    return;
                }
                dialog.Dispose();

                Globals.OpenTxtFile(filePath, userInput);
                btn_refresh_Click(sender, e);
            }
            else if (filePath.EndsWith(".json"))
            {
                Globals.OpenJsonFile(filePath);
                btn_refresh_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var enabledDecks = Globals.flashCardsDecks.Where((c) => c.DeckEnabled).ToArray();
            if (enabledDecks.Length == 0)
                Globals.PlaySound("こんにちは", Globals.Languages[LanguageCodes.Japanese]);
            else
            {
                if (enabledDecks[0].LanguageCode == Globals.Languages[LanguageCodes.Japanese])
                    Globals.PlaySound("こんにちは", Globals.Languages[LanguageCodes.Japanese]);
                if (enabledDecks[0].LanguageCode == Globals.Languages[LanguageCodes.Arabic])
                    Globals.PlaySound("مرحبا", Globals.Languages[LanguageCodes.Arabic]);
                if (enabledDecks[0].LanguageCode == Globals.Languages[LanguageCodes.English])
                    Globals.PlaySound("Hello", Globals.Languages[LanguageCodes.English]);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Globals.speechRate = ((int)numericUpDown1.Value);
        }
    }
}
