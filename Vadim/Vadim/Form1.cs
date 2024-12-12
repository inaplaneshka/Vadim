using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vadim
{
    public partial class Form1 : Form
    {
        private string ConnecrionString = "Data Source=DESKTOP-UU2MMUM\\SQLEXPRESS;Initial Catalog=IT_KubDB;Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();

            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Username = textBox1.Text;
            string Password = textBox2.Text;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Неправильное Имя пользователя или пароль. Пожалуйста попробуйте снова", "Ошибка", MessageBoxButtons.OK);
            }

            using (SqlConnection conn = new SqlConnection(ConnecrionString))
            {
                conn.Open();

                string querry = "SELECT COUNT(*) FROM UserAdmin WHERE Username = @UserName AND UserPass = @UserPass";

                using (SqlCommand cmd = new SqlCommand(querry, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", Username);
                    cmd.Parameters.AddWithValue("@UserPass", Password);

                    int count = (int)cmd.ExecuteScalar();

                    
                    if (count > 0)
                    {
                        DialogResult result = MessageBox.Show("Вход выполнен успешно! Добро пожаловать, систему управления мероприятий", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            Form2 form2 = new Form2();
                            form2.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправильное Имя пользователя или пароль. Пожалуйста попробуйте снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    

                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }


    }
}

