using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista
{
    internal class comboBoxString
    {
        public string id { get; set; }
        public string descripcion { get; set; }


        public comboBoxString()
        {

        }

        public override string ToString()
        {
            return descripcion;
        }
    }
}
