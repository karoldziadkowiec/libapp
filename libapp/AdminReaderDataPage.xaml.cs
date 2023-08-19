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
    /// Logika interakcji dla klasy AdminReaderDataPage.xaml
    /// </summary>
    public partial class AdminReaderDataPage : Window
    {
        Administrator admin = null;
        string selectedPesel = null;

        public AdminReaderDataPage(Administrator administrator, string pesel)
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

                    string sqlQueryGetReaderID = "SELECT id FROM readers WHERE pesel='" + selectedPesel + "'";
                    MySqlCommand commandGetReaderID = new MySqlCommand(sqlQueryGetReaderID, connection);
                    int readerID = Convert.ToInt32(commandGetReaderID.ExecuteScalar());
                    MySqlCommand cmdDataBase = new MySqlCommand("SELECT name, surname, pesel, phone, email, address, birthday FROM readers WHERE pesel = '" + selectedPesel + "'", connection);
                    
                    MySqlCommand cmdBorrowing = new MySqlCommand("SELECT books.title AS 'Book', books.author AS 'Author', DATEDIFF(borrowing.return_date, CURDATE()) AS 'Days to return', borrowing.borrow_date AS 'Borrow date', borrowing.return_date AS 'Return date' FROM borrowing, books, readers WHERE borrowing.reader='" + readerID + "' AND borrowing.book = books.id AND borrowing.reader = readers.id ORDER BY borrowing.return_date ASC", connection);
                    MySqlDataAdapter BorrowingAdapter = new MySqlDataAdapter(cmdBorrowing);
                    DataTable borrowingDt = new DataTable();
                    BorrowingAdapter.Fill(borrowingDt);
                    readers_table.ItemsSource = borrowingDt.DefaultView;
                    using (MySqlDataReader reader = cmdDataBase.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name_label.Content = reader["name"].ToString();
                            surname_label.Content = reader["surname"].ToString();
                            pesel_label.Content = reader["pesel"].ToString();
                            phone_label.Content = reader["phone"].ToString();
                            email_label.Content = reader["email"].ToString();
                            address_label.Content = reader["address"].ToString();
                            birthday_label.Content = reader["birthday"].ToString();
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
            AdminReadersPage adminreaderspage = new AdminReadersPage(admin);
            adminreaderspage.Show();
            this.Hide();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            AdminReadersEditProfilePage adminreaderseditprofilepage = new AdminReadersEditProfilePage(admin, selectedPesel);
            adminreaderseditprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this account?", "libapp", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string connectionString = "server=localhost;database=libapp;username=root;password=;";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "DELETE FROM readers WHERE pesel = @pesel";
                        MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@pesel", selectedPesel);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Account successfully removed.", "libapp");
                        }
                        else
                        {
                            MessageBox.Show("Account not found.", "libapp");
                        }

                        connection.Close();

                        AdminReadersPage adminreaderspage = new AdminReadersPage(admin);
                        adminreaderspage.Show();
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
