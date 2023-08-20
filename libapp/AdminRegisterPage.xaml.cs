using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminRegisterPage.xaml
    /// </summary>
    public partial class AdminRegisterPage : Window
    {
        public AdminRegisterPage()
        {
            InitializeComponent();
        }

        Administrator administrator = null;
        string connectionString = "server=localhost;database=libapp;username=root;password=;";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textbox.Text;
            string password = password_box.Password;
            string name = name_textbox.Text;
            string surname = surname_textbox.Text;
            string pesel = pesel_textbox.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(pesel))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }
            if (login.Length < 5 || login.Length > 15)
            {
                MessageBox.Show("The login must contain from 5 to 15 characters.", "libapp");
                return;
            }
            if (password.Length < 5 || password.Length > 20)
            {
                MessageBox.Show("The password must contain between 5 and 20 characters.", "libapp");
                return;
            }
            if (pesel.Length != 11)
            {
                MessageBox.Show("The PESEL number must contain 11 characters.", "libapp");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "INSERT INTO administrators (login, password, name, surname, pesel) VALUES (@login, @password, @name, @surname, @pesel)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@pesel", pesel);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Account successfully registered.", "libapp");
                    connection.Close();

                    AdminLoginPage adminLoginPage = new AdminLoginPage();
                    adminLoginPage.Show();
                    this.Hide();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) //for duplicate
                {
                    MessageBox.Show("Registration error. The given login already has an account.", "libapp");
                }
                else
                {
                    MessageBox.Show("An error occurred during registration.", "libapp");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminLoginPage adminloginpage = new AdminLoginPage();
            adminloginpage.Show();
            this.Hide();
        }
    }
}
