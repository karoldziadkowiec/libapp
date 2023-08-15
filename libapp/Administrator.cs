using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libapp
{
    public class Administrator
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string pesel { get; set; }
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

