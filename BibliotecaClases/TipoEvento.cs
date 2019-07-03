using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class TipoEvento
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public int IdTipoEvento { get; set; }
        public string Descripcion { get; set; }

        public TipoEvento()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.TipoEvento te = bdd.TipoEvento.First(a => a.IdTipoEvento == IdTipoEvento);
                Descripcion = te.Descripcion;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TipoEvento> ReadAll()
        {
            try
            {
                List<TipoEvento> lista = new List<TipoEvento>();
                var lista_tb_bdd = bdd.TipoEvento.ToList();
                foreach (ConexionBD.TipoEvento item in lista_tb_bdd)
                {
                    TipoEvento te = new TipoEvento();
                    te.IdTipoEvento = item.IdTipoEvento;
                    te.Descripcion = item.Descripcion;
                    lista.Add(te);
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
