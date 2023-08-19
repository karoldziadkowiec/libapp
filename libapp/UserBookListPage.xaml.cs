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
    /// Logika interakcji dla klasy UserBookListPage.xaml
    /// </summary>
    public partial class UserBookListPage : Window
    {
        Reader us = null;
        public UserBookListPage(Reader user)
        {
            InitializeComponent();
            us = user;
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

                    MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM books", connection);
                    int totalBookCount = Convert.ToInt32(countCmd.ExecuteScalar());
                    count_label.Content = totalBookCount;

                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT title AS 'Book title', author AS 'Author', publisher AS 'Publisher', year AS 'Release date', time AS 'Holding days' FROM books ORDER BY title ASC", connection);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDataBase);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    books_table.ItemsSource = dt.DefaultView;
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
            this.InvalidateVisual();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            UserLoginPage userloginpage = new UserLoginPage();
            userloginpage.Show();
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (books_table.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)books_table.SelectedItem;
                string selectedValue = selectedRow["Book title"].ToString();
                title_textbox.Text = selectedValue;
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string selectedTitle = title_textbox.Text;

            if (!string.IsNullOrEmpty(selectedTitle))
            {
                try
                {
                    string connectionString = "server=localhost;database=libapp;username=root;password=;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string sqlQuery = "SELECT COUNT(*) FROM books WHERE title = @selectedTitle";
                        MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@selectedTitle", selectedTitle);

                        int bookCount = Convert.ToInt32(command.ExecuteScalar());

                        if (bookCount > 0)
                        {
                            UserBookListDataPage userbooklistdatapage = new UserBookListDataPage(us, selectedTitle);
                            userbooklistdatapage.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("The selected book title does not exist.", "libapp");
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "libapp");
                }
            }
            else
            {
                MessageBox.Show("Please, select a valid title before proceeding.");
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UserMainPage usermainpage = new UserMainPage(us);
            usermainpage.Show();
            this.Hide();
        }
    }
}
