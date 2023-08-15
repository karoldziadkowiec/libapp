using MySql.Data.MySqlClient;
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

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminReadersEditProfilePage.xaml
    /// </summary>
    public partial class AdminReadersEditProfilePage : Window
    {
        Administrator admin = null;
        string selectedPesel = null;
        public AdminReadersEditProfilePage(Administrator administrator, string pesel)
        {
            InitializeComponent();
            admin = administrator;
            selectedPesel = pesel;
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = "server=localhost;database=libapp;username=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT name, surname, pesel, phone, email, address, birthday FROM readers WHERE pesel = '" + selectedPesel + "'", connection);

                    using (MySqlDataReader reader = cmdDataBase.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name_textbox.Text = reader["name"].ToString();
                            surname_textbox.Text = reader["surname"].ToString();
                            pesel_textbox.Text = reader["pesel"].ToString();
                            phone_textbox.Text = reader["phone"].ToString();
                            email_textbox.Text = reader["email"].ToString();
                            birthday_datepicker.Text = reader["birthday"].ToString();
                            address_textbox.Text = reader["address"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected PESEL number.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminMyProfilePage adminmyprofilepage = new AdminMyProfilePage(admin);
            adminmyprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminReadersPage adminreaderspage = new AdminReadersPage(admin);
            adminreaderspage.Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminAddReaderPage adminaddreaderpage = new AdminAddReaderPage(admin);
            adminaddreaderpage.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AdminBorrowBookPage adminborrowbookpage = new AdminBorrowBookPage(admin);
            adminborrowbookpage.Show();
            this.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AdminBookListPage adminbooklistpage = new AdminBookListPage(admin);
            adminbooklistpage.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            AdminReturnDeadlinesPage adminreturndeadlinespage = new AdminReturnDeadlinesPage(admin);
            adminreturndeadlinespage.Show();
            this.Hide();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            AdminAddBookPage adminaddbookpage = new AdminAddBookPage(admin);
            adminaddbookpage.Show();
            this.Hide();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AdminLoginPage adminloginpage = new AdminLoginPage();
            adminloginpage.Show();
            this.Hide();
        }

        private void readers_table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminReaderDataPage adminreaderdatapage = new AdminReaderDataPage(admin, selectedPesel);
            adminreaderdatapage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string name = name_textbox.Text;
            string surname = surname_textbox.Text;
            string pesel = pesel_textbox.Text;
            string phone = phone_textbox.Text;
            string email = email_textbox.Text;
            string birthday = birthday_datepicker.Text;
            string address = address_textbox.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(pesel) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(birthday))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }
            if (pesel.Length != 11)
            {
                MessageBox.Show("The PESEL number must contain 11 characters.", "libapp");
                return;
            }
            if (phone.Length != 9)
            {
                MessageBox.Show("The phone number must contain 9 characters.", "libapp");
                return;
            }
            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Please, enter a valid email.", "libapp");
                return;
            }

            try
            {
                string connectionString = "server=localhost;database=libapp;username=root;password=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE readers SET name='" + name + "', surname='" + surname + "', pesel='" + pesel + "', phone='" + phone + "', email='" + email + "', birthday='" + birthday + "', address='" + address + "' WHERE pesel = '" + selectedPesel + "';";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully corrected the data.", "libapp");
                    connection.Close();

                    AdminReaderDataPage adminreaderdatapage = new AdminReaderDataPage(admin, selectedPesel);
                    adminreaderdatapage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Account edit error. Correct the data.", "libapp");
            }
        }

    }
}
