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
    public partial class Form2 : Form
    {
        private string ConnecrionString = "Data Source=DESKTOP-UU2MMUM\\SQLEXPRESS;Initial Catalog=IT_KubDB;Integrated Security=True;";

        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Form2()
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
                eventsDataGridView.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (eventsDataGridView.CurrentRow != null)
            {
                int eventId = Convert.ToInt32(eventsDataGridView.CurrentRow.Cells[0].Value);
                using (SqlConnection connection = new SqlConnection(ConnecrionString))
                {
                    string query = "DELETE FROM Events WHERE Id=@id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", eventId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (eventsDataGridView.CurrentRow != null)
            {
                int eventId = Convert.ToInt32(eventsDataGridView.CurrentRow.Cells[0].Value);
                using (SqlConnection connection = new SqlConnection(ConnecrionString))
                {
                    string query = "UPDATE Events SET Title=@title, Date=@date, Location=@location, Description=@description WHERE Id=@id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", eventId);
                    command.Parameters.AddWithValue("@title", titleTextBox.Text);
                    command.Parameters.AddWithValue("@date", dateTimePicker.Value);
                    command.Parameters.AddWithValue("@location", locationTextBox.Text);
                    command.Parameters.AddWithValue("@description", descriptionTextBox.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnecrionString))
            {
                string query = "INSERT INTO Events (Title, Date, Location, Description) VALUES (@title, @date, @location, @description)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@title", titleTextBox.Text);
                command.Parameters.AddWithValue("@date", dateTimePicker.Value);
                command.Parameters.AddWithValue("@location", locationTextBox.Text);
                command.Parameters.AddWithValue("@description", descriptionTextBox.Text);

                connection.Open();
                command.ExecuteNonQuery();
                LoadData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                    eventsDataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
