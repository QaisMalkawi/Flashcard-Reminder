using System;
using System.Windows.Forms;

namespace FlashcardReminder.Forms
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }
        public string Label { get { return label1.Text; } set { label1.Text = value; } }
        public string Value { get { return inputBox.Text; } }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
