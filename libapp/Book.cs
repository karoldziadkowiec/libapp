using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libapp
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public string year { get; set; }
        public string time { get; set; }
        public int amount { get; set; }
        public Book(int ID, string TITLE, string AUTHOR, string PUBLISHER, string YEAR, string TIMER, int AMOUNT)
        {
            id = ID;
            title = TITLE;
            author = AUTHOR;
            publisher = PUBLISHER;
            year = YEAR;
            time = TIMER;
            amount = AMOUNT;
        }
    }
}
