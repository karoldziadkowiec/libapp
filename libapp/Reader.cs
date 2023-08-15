using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libapp
{
    public class Reader
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string pesel { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string birthday { get; set; }
        public Reader(int ID, string NAME, string SURNAME, string PESEL, string PHONE, string EMAIL, string ADDRESS, string BIRTHDAY)
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
