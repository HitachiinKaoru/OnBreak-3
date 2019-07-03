using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class CoffeeBreak
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public String Numero { get; set; }
        public Boolean Vegetariana { get; set; }

        public CoffeeBreak()
        {

        }
    }
}
