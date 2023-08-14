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
    /// Logika interakcji dla klasy UserLoginPage.xaml
    /// </summary>
    public partial class UserLoginPage : Window
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        User user = null;
        MySqlConnection connection = new MySqlConnection("datasource=localhost;username=root;password=;database=libapp");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string pesel = pesel_textbox.Text;

            MySqlCommand command = new MySqlCommand();

            try
            {
                connection.Open();

                string query = "SELECT * FROM users WHERE pesel = '" + pesel + "'";
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
                            string userPesel = reader.GetString(3);
                            string phone = reader.GetString(4);
                            string email = reader.GetString(5);
                            string address = reader.GetString(6);
                            string birthday = reader.GetString(7);

                            User user = new User(id, name, surname, userPesel, phone, email, address, birthday);
                            UserMainPage userMainPage = new UserMainPage(user);

                            userMainPage.Show();
                            this.Hide();
                        }
                    }
                    else if (string.IsNullOrEmpty(pesel))
                    {
                        MessageBox.Show("Empty pesel field. Please enter the correct details.", "libapp");
                    }
                    else
                    {
                        MessageBox.Show("Please, enter the correct pesel number.", "libapp");
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

    }
}
