using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libapp
{
    public class Administrator
    {
        public int id;
        public string name;
        public string surname;
        public string login;
        public string password;
        public string pesel;
        public Administrator(int ID, string NAME, string SURNAME, string LOGIN, string PASSWORD, string PESEL)
        {
            id = ID;
            name = NAME;
            surname = SURNAME;
            login = LOGIN;
            password = PASSWORD;
            pesel = PESEL;
        }
    }
}

