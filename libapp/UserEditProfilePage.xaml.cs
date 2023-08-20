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
    /// Logika interakcji dla klasy UserEditProfilePage.xaml
    /// </summary>
    public partial class UserEditProfilePage : Window
    {
        Reader us = null;
        public UserEditProfilePage(Reader user)
        {
            InitializeComponent();
            us = user;
            LoadData();
        }

        private void LoadData()
        {
            int userID = us.id;

            string connectionString = "server=localhost;database=libapp;username=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT name, surname, pesel, phone, email, address, birthday FROM readers WHERE id = '" + userID + "'", connection);

                    using (MySqlDataReader reader = cmdDataBase.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name_textbox.Text = reader["name"].ToString();
                            surname_textbox.Text = reader["surname"].ToString();
                            pesel_label.Content = us.pesel;
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
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            UserReturnDeadlinesPage userreturndeadlinespage = new UserReturnDeadlinesPage(us);
            userreturndeadlinespage.Show();
            this.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            UserBookListPage userbooklistpage = new UserBookListPage(us);
            userbooklistpage.Show();
            this.Hide();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            UserLoginPage userloginpage = new UserLoginPage();
            userloginpage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            int userID = us.id;
            string userName = name_textbox.Text;
            string userSurname = surname_textbox.Text;
            string userPhone = phone_textbox.Text;
            string userEmail = email_textbox.Text;
            string userBirthday = birthday_datepicker.Text;
            string userAddress = address_textbox.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userSurname) || string.IsNullOrEmpty(userPhone) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userAddress) || string.IsNullOrEmpty(userBirthday))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }

            if (userPhone.Length != 9)
            {
                MessageBox.Show("The phone number must contain 9 characters.", "libapp");
                return;
            }

            if (!userEmail.Contains('@') || !userEmail.Contains('.'))
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

                    string updateQuery = "UPDATE readers SET name=@name, surname=@surname, phone=@phone, email=@email, "
                                         + "birthday=@birthday, address=@address WHERE id=@userID";

                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@name", userName);
                    command.Parameters.AddWithValue("@surname", userSurname);
                    command.Parameters.AddWithValue("@phone", userPhone);
                    command.Parameters.AddWithValue("@email", userEmail);
                    command.Parameters.AddWithValue("@birthday", userBirthday);
                    command.Parameters.AddWithValue("@address", userAddress);

                    command.ExecuteNonQuery();

                    us.name = userName;
                    us.surname = userSurname;
                    us.phone = userPhone;
                    us.email = userEmail;
                    us.birthday = userBirthday;
                    us.address = userAddress;

                    MessageBox.Show("Successfully corrected the data.", "libapp");
                    connection.Close();

                    UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
                    usermyprofilepage.Show();
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Account edit error. Correct the data.", "libapp");
            }
        }


        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
        }
    }
}
