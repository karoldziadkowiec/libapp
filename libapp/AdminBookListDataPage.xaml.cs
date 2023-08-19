using MySql.Data.MySqlClient;
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

namespace libapp
{
    /// <summary>
    /// Logika interakcji dla klasy AdminBookListDataPage.xaml
    /// </summary>
    public partial class AdminBookListDataPage : Window
    {
        Administrator admin = null;
        string selectedTitle = null;
        public AdminBookListDataPage(Administrator administrator, string title)
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
                            title_label.Content = reader["title"].ToString();
                            author_label.Content = reader["author"].ToString();
                            publisher_label.Content = reader["publisher"].ToString();
                            year_label.Content = reader["year"].ToString();
                            time_label.Content = reader["time"].ToString();
                            amount_label.Content = reader["amount"].ToString();
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
            AdminBookListEditBookPage adminbooklisteditbookpage = new AdminBookListEditBookPage(admin, selectedTitle);
            adminbooklisteditbookpage.Show();
            this.Hide();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminBookListPage adminbooklistpage = new AdminBookListPage(admin);
            adminbooklistpage.Show();
            this.Hide();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this book?", "libapp", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string connectionString = "server=localhost;database=libapp;username=root;password=;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "DELETE FROM books WHERE title = @title";
                        MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@title", selectedTitle);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Book successfully removed.", "libapp");
                        }
                        else
                        {
                            MessageBox.Show("Book not found.", "libapp");
                        }

                        connection.Close();

                        AdminBookListPage adminbooklistpage = new AdminBookListPage(admin);
                        adminbooklistpage.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("Error removing profile.", "libapp");
                }
            }
            else
            {
                this.InvalidateVisual();
            }
        }
    }
}
