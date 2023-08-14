using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libapp
{
    public class User
    {
        public int id;
        public string name;
        public string surname;
        public string pesel;
        public string phone;
        public string email;
        public string address;
        public string birthday;
        public User(int ID, string NAME, string SURNAME, string PESEL, string PHONE, string EMAIL, string ADDRESS, string BIRTHDAY)
        {
            id = ID;
            name = NAME;
            surname = SURNAME;
            pesel = PESEL;
            phone = PHONE;
            email = EMAIL;
            address = ADDRESS;
            birthday = BIRTHDAY;
        }
    }
}
