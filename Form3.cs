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
    public partial class Form3 : Form
    {
        private string ConnecrionString = "Data Source=DESKTOP-UU2MMUM\\SQLEXPRESS;Initial Catalog=IT_KubDB;Integrated Security=True;";

        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Form3()
        {
            InitializeComponent();

            LoadData();

        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(ConnecrionString))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Events", connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            SearchEvents(searchTerm);
        }

        private void SearchEvents(string searchTerm)
        {
            string ConnecrionString = "Data Source=DESKTOP-UU2MMUM\\SQLEXPRESS;Initial Catalog=IT_KubDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(ConnecrionString))
            {
                string query = "SELECT * FROM Events WHERE Title LIKE @Title";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", "%" + searchTerm + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
