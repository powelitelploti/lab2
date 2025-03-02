using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class DataSecurityManager
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        public void EncryptFile()
        {
            using (openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Title = "Выберите файл для шифрования";
                openFileDialog.Filter = "Все файлы (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    var password = GetPassword();
                    if (File.Exists(filePath))
                    {
                        byte[] salt = new byte[32];
                        using (var rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(salt);
                        }
                        using (var aes = Aes.Create())
                        {
                            var key = new Rfc2898DeriveBytes(password, salt, 100000).GetBytes(32);
                            var iv = new Rfc2898DeriveBytes(password, salt, 100000).GetBytes(16);
                            aes.Key = key;
                            aes.IV = iv;
                            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                            {
                                using (var outputStream = File.Create(filePath + ".enc"))
                                {
                                    using (var inputStream = File.OpenRead(filePath))
                                    {
                                        inputStream.CopyTo(outputStream);
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Файл зашифрован.");
                    }
                }
            }
        }
        public void DecryptFile()
        {
            using (openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Title = "Выберите зашифрованный файл";
                openFileDialog.Filter = "Зашифрованные файлы (*.enc)|*.enc";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var encryptedFilePath = openFileDialog.FileName;
                    var password = GetPassword();
                    if (File.Exists(encryptedFilePath))
                    {
                        using (var aes = Aes.Create())
                        {
                            var salt = new byte[32];
                            var iv = new byte[16];
                            using (var inputStream = File.OpenRead(encryptedFilePath))
                            {
                                inputStream.Read(salt, 0, salt.Length);
                                inputStream.Read(iv, 0, iv.Length);
                                var key = new Rfc2898DeriveBytes(password, salt, 100000).GetBytes(32);
                                aes.Key = key;
                                aes.IV = iv;
                                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                                {
                                    using (var outputStream = File.Create(encryptedFilePath + ".dec"))
                                    {
                                        using (var stream = new CryptoStream(outputStream, decryptor,
                                        CryptoStreamMode.Write))
                                        {
                                            inputStream.CopyTo(stream);
                                        }
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Файл расшифрован.");
                    }
                }
            }
        }
        private string GetPassword()
        {
            using (var passwordForm = new PasswordForm())
            {
                if (passwordForm.ShowDialog() == DialogResult.OK)
                {
                    return passwordForm.Password;
                }
                return string.Empty;
            }
        }
    }
}
