using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class ActividadEmpresa
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public int IdActividadEmpresa { get; set; }
        public string Descripcion { get; set; }

        public ActividadEmpresa()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.ActividadEmpresa act = bdd.ActividadEmpresa.First(a => a.IdActividadEmpresa == IdActividadEmpresa);
                Descripcion = act.Descripcion;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ActividadEmpresa> ReadAll()
        {
            try
            {
                var lista_act_bdd = bdd.ActividadEmpresa.ToList();
                List<ActividadEmpresa> lista = new List<ActividadEmpresa>();
                
                foreach (ConexionBD.ActividadEmpresa item in lista_act_bdd)
                {
                    ActividadEmpresa act = new ActividadEmpresa();
                    act.IdActividadEmpresa = item.IdActividadEmpresa;
                    act.Descripcion = item.Descripcion;
                    lista.Add(act);
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
