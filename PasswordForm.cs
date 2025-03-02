using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class PasswordForm : Form
    {
        private TextBox passwordTextBox;
        private Button okButton;
        private Button cancelButton;
        public string Password { get; private set; }
        public PasswordForm()
        {
            this.Text = "Введите пароль";
            this.Width = 300;
            this.Height = 100;
            CreateControls();
        }
        private void CreateControls()
        {
            passwordTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 20),
                Size = new System.Drawing.Size(260, 20),
                UseSystemPasswordChar = true
            };
            okButton = new Button
            {
                Location = new System.Drawing.Point(10, 50),
                Text = "OK",
                Size = new System.Drawing.Size(75, 25)
            };
            okButton.Click += (sender, e) =>
            {
                Password = passwordTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            };
            cancelButton = new Button
            {
                Location = new System.Drawing.Point(95, 50),
                Text = "Отмена",
                Size = new System.Drawing.Size(75, 25)
            };
            cancelButton.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(okButton);
            this.Controls.Add(cancelButton);
        }

    }
}
