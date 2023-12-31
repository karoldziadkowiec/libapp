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
    /// Logika interakcji dla klasy AdminAddBookPage.xaml
    /// </summary>
    public partial class AdminAddBookPage : Window
    {
        Administrator admin = null;
        public AdminAddBookPage(Administrator administrator)
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
            this.InvalidateVisual();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AdminLoginPage adminloginpage = new AdminLoginPage();
            adminloginpage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string title = title_textbox.Text;
            string author = author_textbox.Text;
            string publisher = publisher_textbox.Text;
            string year = year_textbox.Text;
            int.TryParse(time_textbox.Text, out int time);

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(publisher) || string.IsNullOrEmpty(year) || time == 0)
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }

            if (time < 30 || time > 100)
            {
                MessageBox.Show("The Holding time must be set between 30 and 100 days.", "libapp");
                return;
            }

            if (!int.TryParse(amount_textbox.Text, out int amount))
            {
                MessageBox.Show("Please enter a valid amount value", "libapp");
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Amount must be a positive number.", "libapp");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "INSERT INTO books (title, author, publisher, year, time, amount) VALUES (@title, @author, @publisher, @year, @time, @amount)";
                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@publisher", publisher);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Book successfully added to the system.", "libapp");
                    connection.Close();

                    AdminBookListPage adminbooklistpage = new AdminBookListPage(admin);
                    adminbooklistpage.Show();
                    this.Hide();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Addition error", "libapp");
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
