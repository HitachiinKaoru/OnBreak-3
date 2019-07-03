using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class TipoEmpresa
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public int IdTipoEmpresa { get; set; }
        public string Descripcion { get; set; }

        public TipoEmpresa()
        {
                
        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.TipoEmpresa te = bdd.TipoEmpresa.First(a => a.IdTipoEmpresa == IdTipoEmpresa);
                Descripcion = te.Descripcion;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TipoEmpresa> ReadAll()
        {
            try
            {
                List<TipoEmpresa> lista = new List<TipoEmpresa>();
                var lista_temp_bdd = bdd.TipoEmpresa.ToList();
                foreach (ConexionBD.TipoEmpresa item in lista_temp_bdd)
                {
                    TipoEmpresa temp = new TipoEmpresa();
                    temp.IdTipoEmpresa = item.IdTipoEmpresa;
                    temp.Descripcion = item.Descripcion;
                    lista.Add(temp);
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
