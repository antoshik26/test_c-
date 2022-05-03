using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class baracod
    {
        public int _id { get; set; }
        public string _barcod_value { get; set; }
        public DateTime _date { get; set; }

        public baracod(string barcod_value, DateTime date)
        {
            _barcod_value = barcod_value;
            _date = date;
        }
    }
}
