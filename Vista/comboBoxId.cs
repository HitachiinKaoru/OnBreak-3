using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//id es INT, se usa para Actividad Empresa/ Tipo Empresa / Tipo Evento
namespace Vista
{
    internal class comboBoxId
    {
        public int id { get; set; }
        public string descripcion { get; set; }


        public comboBoxId()
        {

        }

        public override string ToString()
        {
            return descripcion;
        }
    }

}
