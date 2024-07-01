using FlashcardReminder.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlashcardReminder.Forms
{
    public partial class CardsView : Form
    {
        FlashCard currentCard;
        string languageCode;

        Color originalButtonColor;
        string originalButtonText;

        public CardsView()
        {
            InitializeComponent();
            Globals.timer.Tick += ShowCard;
            Settings.Default.Reload();
            this.Location = Settings.Default.WindowPosition;
            originalButtonColor = btn_check.BackColor;
            originalButtonText = btn_check.Text;

            tbx_input.Text = "";
            lbl_question.Text = "";
            lbl_answer.Text = "";
            lbl_example.Text = "";

            Globals.cardViewForm = this;
            btn_check_Click(null, null);
        }

        public void ShowCard(object sender, EventArgs args)
        {
            Settings.Default.Reload();
            this.Location = Settings.Default.WindowPosition;
            currentCard = Globals.SelectRandomCard(out languageCode);
            lbl_question.Text = currentCard.Question;
            btn_check.BackColor = originalButtonColor;
            Show();
            Globals.timer.Stop();   
        }

        private void HideCard(object sender, EventArgs e)
        {
            Hide();
            tbx_input.Text = "";
            lbl_question.Text = "";
            lbl_answer.Text = "";
            lbl_example.Text = "";
            btn_check.Text = originalButtonText;
            Globals.timer.Start();
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            if (tbx_input.Text.Replace(" ", "").Replace(",", "").Equals(currentCard.Narration, StringComparison.OrdinalIgnoreCase))
                btn_check.BackColor = Color.Green;
            else
                btn_check.BackColor = Color.Red;

            if (!string.IsNullOrEmpty(currentCard.Answer))
                lbl_answer.Text = currentCard.Answer;

            if (!string.IsNullOrEmpty(currentCard.Examples))
                lbl_example.Text = currentCard.Examples;

            btn_check.Text = currentCard.Narration;
            Globals.PlaySound(currentCard.Question, languageCode);
            Timer timer = new Timer() { Interval = Globals.AnswerPrivewTimerMilliseconds };
            timer.Start();
            timer.Tick += (s, ea) =>
            {
                HideCard(s, ea);
                timer.Dispose();
            };
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btn_check_Click(sender, null);
            }
        }

        private void CardsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.WindowPosition = this.Location;
            Settings.Default.Save();
        }

        private void CardsView_VisibleChanged(object sender, EventArgs e)
        {
            Settings.Default.WindowPosition = this.Location;
            Settings.Default.Save();
        }
    }
}
