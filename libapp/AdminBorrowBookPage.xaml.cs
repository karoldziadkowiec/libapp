using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
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
    /// Logika interakcji dla klasy AdminBorrowBookPage.xaml
    /// </summary>
    public partial class AdminBorrowBookPage : Window
    {
        Administrator admin = null;
        public AdminBorrowBookPage(Administrator administrator)
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

                    MySqlCommand cmdBooks = new MySqlCommand("SELECT title AS 'Book title', author AS 'Author', time AS 'Holding days' FROM books WHERE amount >= 1 ORDER BY title ASC", connection);
                    MySqlDataAdapter booksAdapter = new MySqlDataAdapter(cmdBooks);
                    DataTable booksDt = new DataTable();
                    booksAdapter.Fill(booksDt);
                    books_table.ItemsSource = booksDt.DefaultView;

                    MySqlCommand cmdReaders = new MySqlCommand("SELECT surname AS 'Surname', name AS 'Name', pesel AS 'PESEL number' FROM readers ORDER BY surname ASC", connection);
                    MySqlDataAdapter readersAdapter = new MySqlDataAdapter(cmdReaders);
                    DataTable readersDt = new DataTable();
                    readersAdapter.Fill(readersDt);
                    readers_table.ItemsSource = readersDt.DefaultView;
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
            this.InvalidateVisual();
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
            string book = book_textbox.Text;
            string reader = reader_textbox.Text;

            if (string.IsNullOrEmpty(book) || string.IsNullOrEmpty(reader))
            {
                MessageBox.Show("Complete the empty fields.", "libapp");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQueryGetBookId = "SELECT id FROM books WHERE title='" + book + "'";
                    MySqlCommand commandGetBarberId = new MySqlCommand(sqlQueryGetBookId, connection);
                    int BookId = Convert.ToInt32(commandGetBarberId.ExecuteScalar());

                    string sqlQueryGetReaderId = "SELECT id FROM readers WHERE pesel='" + reader + "'";
                    MySqlCommand commandGetServiceId = new MySqlCommand(sqlQueryGetReaderId, connection);
                    int ReaderId = Convert.ToInt32(commandGetServiceId.ExecuteScalar());

                    string timeQuery = "SELECT time FROM books WHERE title = @book";
                    MySqlCommand timeCommand = new MySqlCommand(timeQuery, connection);
                    timeCommand.Parameters.AddWithValue("@book", book);
                    int time = Convert.ToInt32(timeCommand.ExecuteScalar());

                    //borrowing.borrow_date
                    DateTime currentDate = DateTime.Now;

                    //borrowing.return_date + books.time
                    DateTime returnDate = currentDate.AddDays(time);

                    string borrowSqlQuery = "INSERT INTO borrowing (book, reader, borrow_date, return_date) VALUES (@book, @reader, @borrow_date, @return_date)";
                    MySqlCommand borrowCommand = new MySqlCommand(borrowSqlQuery, connection);
                    borrowCommand.Parameters.AddWithValue("@book", BookId);
                    borrowCommand.Parameters.AddWithValue("@reader", ReaderId);
                    borrowCommand.Parameters.AddWithValue("@borrow_date", currentDate.ToString("yyyy-MM-dd hh:mm:ss"));
                    borrowCommand.Parameters.AddWithValue("@return_date", returnDate.ToString("yyyy-MM-dd hh:mm:ss"));
                    borrowCommand.ExecuteNonQuery();

                    // Updating amount in the 'books' table
                    string updateAmountQuery = "UPDATE books SET amount = amount - 1 WHERE id = @bookId";
                    MySqlCommand updateAmountCommand = new MySqlCommand(updateAmountQuery, connection);
                    updateAmountCommand.Parameters.AddWithValue("@bookId", BookId);
                    updateAmountCommand.ExecuteNonQuery();

                    MessageBox.Show("Book successfully borrowed in the system.", "libapp");
                    connection.Close();

                    AdminReturnDeadlinesPage adminreturndeadlinespage = new AdminReturnDeadlinesPage(admin);
                    adminreturndeadlinespage.Show();
                    this.Hide();
                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Borrowing error", "libapp");
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            AdminMainPage adminmainpage = new AdminMainPage(admin);
            adminmainpage.Show();
            this.Hide();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (books_table.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)books_table.SelectedItem;
                string selectedValue = selectedRow["Book title"].ToString();
                book_textbox.Text = selectedValue;
            }
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (readers_table.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)readers_table.SelectedItem;
                string selectedValue = selectedRow["PESEL number"].ToString();
                reader_textbox.Text = selectedValue;
            }
        }
    }
}
