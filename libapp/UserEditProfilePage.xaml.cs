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
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            UserMyProfilePage usermyprofilepage = new UserMyProfilePage(us);
            usermyprofilepage.Show();
            this.Hide();
        }
    }
}
