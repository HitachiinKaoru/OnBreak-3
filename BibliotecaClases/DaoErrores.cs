using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class DaoErrores
    {
        private List<string> errores;

        public DaoErrores()
        {
            if (errores == null)
            {
                errores = new List<string>();
            }
        }

        public bool AgregarError(string er)
        {
            errores.Add(er);
            return true;
        }

        public List<string> ListarErrores()
        {
            return errores;
        }
    }
}
