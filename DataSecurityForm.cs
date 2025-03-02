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
    public partial class DataSecurityForm : Form
    {
        private DataSecurityManager dataSecurityManager;
        private Button encryptButton;
        private Button decryptButton;
        public DataSecurityForm()
        {
            this.Text = "Управление безопасностью данных";
            this.Width = 300;
            this.Height = 150;
            CreateControls();
            dataSecurityManager = new DataSecurityManager();
        }
        private void CreateControls()
        {
            encryptButton = new Button
            {
                Location = new System.Drawing.Point(10, 20),
                Text = "Зашифровать файл",
                Size = new System.Drawing.Size(120, 25)
            };
            encryptButton.Click += (sender, e) => dataSecurityManager.EncryptFile();
            decryptButton = new Button
            {
                Location = new System.Drawing.Point(140, 20),
                Text = "Расшифровать файл",
                Size = new System.Drawing.Size(120, 25)
            };
            decryptButton.Click += (sender, e) => dataSecurityManager.DecryptFile();
            this.Controls.Add(encryptButton);
            this.Controls.Add(decryptButton);
        }
    }
}