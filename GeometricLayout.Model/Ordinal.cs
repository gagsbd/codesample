using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricLayout.Model
{
    public class Ordinal
    {
        public Ordinal()
        { }

        public Ordinal(string row, int coloumn)
        {
            Row = row;
            Column = coloumn;
        }
        public string Row { get; set; }
        public int Column { get; set; }
    }
}
