using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPQaunt.Entitest
{
    public class NumberModel
    {
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }
        public int N4 { get; set; }
        public int N5 { get; set; }

        public string GetString() {

            return N1.ToString() + "," + N2.ToString() +","+ N3.ToString() + "," + N4.ToString()+"," + N5.ToString();
        }
    }
}