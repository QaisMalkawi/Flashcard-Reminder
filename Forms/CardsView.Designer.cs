namespace FlashcardReminder.Forms
{
    partial class CardsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_question = new System.Windows.Forms.Label();
            this.tbx_input = new System.Windows.Forms.TextBox();
            this.btn_check = new System.Windows.Forms.Button();
            this.lbl_answer = new System.Windows.Forms.Label();
            this.lbl_example = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_question
            // 
            this.lbl_question.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_question.Font = new System.Drawing.Font("Ubuntu", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_question.Location = new System.Drawing.Point(0, 0);
            this.lbl_question.Name = "lbl_question";
            this.lbl_question.Size = new System.Drawing.Size(448, 56);
            this.lbl_question.TabIndex = 1;
            this.lbl_question.Text = "おはよう";
            this.lbl_question.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_input
            // 
            this.tbx_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_input.BackColor = System.Drawing.Color.Gray;
            this.tbx_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbx_input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbx_input.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_input.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tbx_input.Location = new System.Drawing.Point(0, 190);
            this.tbx_input.Name = "tbx_input";
            this.tbx_input.Size = new System.Drawing.Size(448, 33);
            this.tbx_input.TabIndex = 2;
            this.tbx_input.Text = "ohayou";
            this.tbx_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // btn_check
            // 
            this.btn_check.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_check.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_check.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_check.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.btn_check.Location = new System.Drawing.Point(0, 224);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(448, 32);
            this.btn_check.TabIndex = 3;
            this.btn_check.Text = "Check";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // lbl_answer
            // 
            this.lbl_answer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_answer.Font = new System.Drawing.Font("Ubuntu", 16F, System.Drawing.FontStyle.Bold);
            this.lbl_answer.Location = new System.Drawing.Point(0, 56);
            this.lbl_answer.Name = "lbl_answer";
            this.lbl_answer.Size = new System.Drawing.Size(448, 50);
            this.lbl_answer.TabIndex = 4;
            this.lbl_answer.Text = "Good Morning";
            this.lbl_answer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Example
            // 
            this.lbl_example.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_example.Font = new System.Drawing.Font("Ubuntu", 16F, System.Drawing.FontStyle.Bold);
            this.lbl_example.Location = new System.Drawing.Point(0, 106);
            this.lbl_example.Name = "lbl_Example";
            this.lbl_example.Size = new System.Drawing.Size(448, 81);
            this.lbl_example.TabIndex = 5;
            this.lbl_example.Text = "おはようございます";
            this.lbl_example.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CardsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(448, 256);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_example);
            this.Controls.Add(this.lbl_answer);
            this.Controls.Add(this.btn_check);
            this.Controls.Add(this.tbx_input);
            this.Controls.Add(this.lbl_question);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardsView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardsView_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.CardsView_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_question;
        public System.Windows.Forms.TextBox tbx_input;
        public System.Windows.Forms.Button btn_check;
        public System.Windows.Forms.Label lbl_answer;
        public System.Windows.Forms.Label lbl_example;
    }
}

