using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy AdminLoginPage.xaml
    /// </summary>
    public partial class AdminLoginPage : Window
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        Administrator administrator = null;
        MySqlConnection connection = new MySqlConnection("datasource=localhost;username=root;password=;database=libapp");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textbox.Text;
            string password = password_box.Password;

            MySqlCommand command = new MySqlCommand();

            try
            {
                connection.Open();

                string query = $"SELECT * FROM administrators WHERE login = '{login}' AND password = '{password}'";

                command.CommandText = query;
                command.Connection = connection;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string surname = reader.GetString(2);
                            string adminLogin = reader.GetString(3);
                            string adminPassword = reader.GetString(4);
                            string pesel = reader.GetString(5);

                            administrator = new Administrator(id, name, surname, adminLogin, adminPassword, pesel);
                            AdminMainPage adminMainPage = new AdminMainPage(administrator);

                            adminMainPage.Show();
                            this.Hide();
                        }
                    }
                    else if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Empty login fields. Please enter the correct details.", "libapp");
                    }
                    else
                    {
                        MessageBox.Show("Please, enter the correct login details.", "libapp");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "libapp");
            }
            finally
            {
                connection.Close();
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminRegisterPage adminregisterpage = new AdminRegisterPage();
            adminregisterpage.Show();
            this.Hide();
        }
    }
}
