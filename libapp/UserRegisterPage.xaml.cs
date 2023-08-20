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
using System.Xml.Linq;

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy UserRegisterPage.xaml
    /// </summary>
    public partial class UserRegisterPage : Window
    {
        Reader us = null;
        public UserRegisterPage()
        {
            InitializeComponent();
        }

        string connectionString = "server=localhost;database=libapp;username=root;password=;";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = name_textbox.Text;
            string userSurname = surname_textbox.Text;
            string userPesel = pesel_textbox.Text;
            string userPhone = phone_textbox.Text;
            string userEmail = email_textbox.Text;
            string userAddress = address_textbox.Text;
            string userBirthday = birthday_datepicker.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userSurname) || string.IsNullOrEmpty(userPesel) || string.IsNullOrEmpty(userPhone) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userAddress) || string.IsNullOrEmpty(userBirthday))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }

            if (userPesel.Length != 11)
            {
                MessageBox.Show("The PESEL number must contain 11 characters.", "libapp");
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

            if (!accept_checkbox.IsChecked.HasValue || !accept_checkbox.IsChecked.Value)
            {
                MessageBox.Show("Accept the terms of the library.", "libapp");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO readers (name, surname, pesel, phone, email, address, birthday) " +
                                         "VALUES (@name, @surname, @pesel, @phone, @email, @address, @birthday)";

                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@name", userName);
                    command.Parameters.AddWithValue("@surname", userSurname);
                    command.Parameters.AddWithValue("@pesel", userPesel);
                    command.Parameters.AddWithValue("@phone", userPhone);
                    command.Parameters.AddWithValue("@email", userEmail);
                    command.Parameters.AddWithValue("@address", userAddress);
                    command.Parameters.AddWithValue("@birthday", userBirthday);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Account successfully registered.", "libapp");
                    connection.Close();

                    UserLoginPage userloginpage = new UserLoginPage();
                    userloginpage.Show();
                    this.Hide();
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) //for duplicate
                {
                    MessageBox.Show("Registration error. The given PESEL already has an account.", "libapp");
                }
                else
                {
                    MessageBox.Show("An error occurred during registration.", "libapp");
                }
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserLoginPage userloginpage = new UserLoginPage();
            userloginpage.Show();
            this.Hide();
        }
    }
}
