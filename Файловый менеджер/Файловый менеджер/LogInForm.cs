using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Файловый_менеджер
{
    public partial class LogInForm : Form
    {
        Authorization authorization;
        public LogInForm()
        {
            InitializeComponent();
            authorization = new Authorization();
            textBoxPassword.PasswordChar = '♡';

            //попробовал десериализоваться, чтобы сравнить пароль и имя пользователя
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream configs = new FileStream("configs.txt", FileMode.OpenOrCreate))
                {
                    authorization = (Authorization)formatter.Deserialize(configs);
                }
            }
            catch (Exception) { }
        }

        //регистрация нового пользователя (но прошлый теряется), все настройки скинуты до дефолтныых
        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            authorization.userName = textBoxUserName.Text;
            authorization.password = textBoxPassword.Text;

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream configs = new FileStream("configs.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(configs, authorization);
            }

            this.Hide();
            new MainForm().Show();
        }

        //попытка входа существующего пользователя
        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            string inputUserName = textBoxUserName.Text;
            string inputPassword = textBoxPassword.Text;

            if (inputPassword == authorization.password && inputUserName == authorization.userName)
            {
                this.Hide();

                BinaryFormatter formater = new BinaryFormatter();
                using (FileStream configs = new FileStream("configSettings.txt", FileMode.OpenOrCreate))
                {
                    Settings.GetCurrent((Settings)formater.Deserialize(configs));
                }

                new MainForm().Show();  
            }
            else
            {
                MessageBox.Show("Солнышко, такого пользователя не существует :(");
                textBoxUserName.Text = "";
                textBoxPassword.Text = "";
            }
        }
    }
}
