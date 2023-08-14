﻿using System;
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
    /// Logika interakcji dla klasy UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Window
    {
        User us = null;
        public UserMainPage(User user)
        {
            InitializeComponent();
            us = user;
        }
    }
}
