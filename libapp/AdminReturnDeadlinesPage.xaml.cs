using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
    /// Logika interakcji dla klasy AdminReturnDeadlinesPage.xaml
    /// </summary>
    public partial class AdminReturnDeadlinesPage : Window
    {
        Administrator admin = null;
        public AdminReturnDeadlinesPage(Administrator administrator)
        {
            InitializeComponent();
            admin = administrator;
            LoadData();
        }

        string connectionString = "server=localhost;database=libapp;username=root;password=;";
        private void LoadData()
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmdBorrowing = new MySqlCommand("SELECT borrowing.id AS 'Borrowing ID', books.title AS 'Book', readers.surname AS 'Reader\\'s surname', readers.name AS 'Reader\\'s name', readers.pesel AS 'PESEL number', borrowing.borrow_date AS 'Borrow date', borrowing.return_date AS 'Return date', DATEDIFF(borrowing.return_date, CURDATE()) AS 'Days to return' FROM borrowing, books, readers WHERE borrowing.book = books.id AND borrowing.reader = readers.id ORDER BY borrowing.return_date ASC", connection);
                    MySqlDataAdapter BorrowingAdapter = new MySqlDataAdapter(cmdBorrowing);
                    DataTable borrowingDt = new DataTable();
                    BorrowingAdapter.Fill(borrowingDt);
                    borrowing_table.ItemsSource = borrowingDt.DefaultView;

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
            this.InvalidateVisual();
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (borrowing_table.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)borrowing_table.SelectedItem;
                string selectedValue = selectedRow["Borrowing ID"].ToString();
                id_textbox.Text = selectedValue;
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string borrowingIDText = id_textbox.Text;

            if (string.IsNullOrEmpty(borrowingIDText))
            {
                MessageBox.Show("Complete the empty field.", "libapp");
                return;
            }

            try
            {
                string borrowingID = borrowingIDText;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQueryGetBookTitle = "SELECT book FROM borrowing WHERE id='" + borrowingID + "'";
                    MySqlCommand commandGetBookTitle = new MySqlCommand(sqlQueryGetBookTitle, connection);
                    int bookID = Convert.ToInt32(commandGetBookTitle.ExecuteScalar());

                    string deleteQuery = "DELETE FROM borrowing WHERE id = @borrowingID";
                    MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@borrowingID", borrowingID);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        string updateAmountQuery = "UPDATE books SET amount = amount + 1 WHERE id = @bookId";
                        MySqlCommand updateAmountCommand = new MySqlCommand(updateAmountQuery, connection);
                        updateAmountCommand.Parameters.AddWithValue("@bookId", bookID);
                        updateAmountCommand.ExecuteNonQuery();

                        MessageBox.Show("Book successfully returned.", "libapp");
                    }
                    else
                    {
                        MessageBox.Show("Book not found.", "libapp");
                    }

                    connection.Close();

                    AdminReturnDeadlinesPage adminreturndeadlinespage = new AdminReturnDeadlinesPage(admin);
                    adminreturndeadlinespage.Show();
                    this.Hide();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid borrowing ID format.", "libapp");
            }
            catch (MySqlException)
            {
                MessageBox.Show("Error removing profile.", "libapp");
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
