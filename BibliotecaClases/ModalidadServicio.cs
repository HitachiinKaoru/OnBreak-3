using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class ModalidadServicio
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public string IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }//foranea

        public string Nombre { get; set; }

        public double ValorBase { get; set; }

        public int PersonalBase { get; set; }

        public ModalidadServicio()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.ModalidadServicio mser = bdd.ModalidadServicio.First(a => a.IdModalidad == IdModalidad);
                Nombre = mser.Nombre;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ModalidadServicio> ReadAll()
        {
            try
            {
                var c = from mod in bdd.ModalidadServicio
                        join tip in bdd.TipoEvento on mod.IdTipoEvento equals tip.IdTipoEvento

                         select new ModalidadServicio()
                        {
                            IdModalidad = mod.IdModalidad,
                            IdTipoEvento = tip.IdTipoEvento,
                            Nombre = mod.Nombre,
                            ValorBase = mod.ValorBase,
                            PersonalBase = mod.PersonalBase,
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
