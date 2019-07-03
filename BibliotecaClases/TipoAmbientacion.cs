using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class TipoAmbientacion
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public int IdTipoAmbientacion { get; set; }
        public String Descripcion { get; set; }

        public TipoAmbientacion()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.TipoAmbientacion ta = bdd.TipoAmbientacion.First(a => a.IdTipoAmbientacion == IdTipoAmbientacion);
                Descripcion = ta.Descripcion;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TipoAmbientacion> ReadAll()
        {
            try
            {
                List<TipoAmbientacion> lista = new List<TipoAmbientacion>();
                var lista_tam_bdd = bdd.TipoAmbientacion.ToList();
                foreach (ConexionBD.TipoAmbientacion item in lista_tam_bdd)
                {
                    TipoAmbientacion tam = new TipoAmbientacion();
                    tam.IdTipoAmbientacion = item.IdTipoAmbientacion;
                    tam.Descripcion = item.Descripcion;
                    lista.Add(tam);
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
