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
    /// Logika interakcji dla klasy AdminBookListEditBookPage.xaml
    /// </summary>
    public partial class AdminBookListEditBookPage : Window
    {
        Administrator admin = null;
        string selectedTitle = null;
        public AdminBookListEditBookPage(Administrator administrator, string title)
        {
            InitializeComponent();
            admin = administrator;
            selectedTitle = title;
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
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT title, author, publisher, year, time, amount FROM books WHERE title = '" + selectedTitle + "'", connection);

                    using (MySqlDataReader reader = cmdDataBase.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            title_textbox.Text = reader["title"].ToString();
                            author_textbox.Text = reader["author"].ToString();
                            publisher_textbox.Text = reader["publisher"].ToString();
                            year_textbox.Text = reader["year"].ToString();
                            time_textbox.Text = reader["time"].ToString();
                            amount_textbox.Text = reader["amount"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected title.");
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
                string connectionString = "server=localhost;database=libapp;username=root;password=;";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE books SET title = @title, author = @author, "
                                    + "publisher = @publisher, year = @year, time = @time, amount = @amount "
                                    + "WHERE title = @selectedTitle";

                    MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@publisher", publisher);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@time", time);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@selectedTitle", selectedTitle);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully corrected the data.", "libapp");
                    connection.Close();

                    AdminBookListDataPage adminbooklistdatapage = new AdminBookListDataPage(admin, title);
                    adminbooklistdatapage.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Book edit error. Correct the data.\nError: " + ex.Message, "libapp");
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminBookListDataPage adminbooklistdatapage = new AdminBookListDataPage(admin, selectedTitle);
            adminbooklistdatapage.Show();
            this.Hide();
        }

    }
}
