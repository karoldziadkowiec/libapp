﻿using MySql.Data.MySqlClient;
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
    /// Logika interakcji dla klasy AdminAddReaderPage.xaml
    /// </summary>
    public partial class AdminAddReaderPage : Window
    {
        Administrator admin = null;
        public AdminAddReaderPage(Administrator administrator)
        {
            InitializeComponent();
            admin = administrator;
        }

        string connectionString = "server=localhost;database=libapp;username=root;password=;";
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
            this.InvalidateVisual();
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

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string name = name_textbox.Text;
            string surname = surname_textbox.Text;
            string pesel = pesel_textbox.Text;
            string phone = phone_textbox.Text;
            string email = email_textbox.Text;
            string address = address_textbox.Text;
            string birthday = birthday_datepicker.Text;

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

                    string sqlQuery = "INSERT INTO readers (name, surname, pesel, phone, email, address, birthday) VALUES (@name, @surname, @pesel, @phone, @email, @address, @birthday)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@pesel", pesel);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@birthday", birthday);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Reader successfully registered.", "libapp");
                    connection.Close();

                    AdminReadersPage adminreaderspage = new AdminReadersPage(admin);
                    adminreaderspage.Show();
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

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }
    }
}
