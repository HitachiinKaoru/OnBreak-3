using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class Cenas
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public String Numero { get; set; }

        public int IdTipoAmbientacion { get; set; } //foranea


        private Boolean _musicaAmbiental;
        public Boolean MusicaAmbiental
        {
            get { return _musicaAmbiental; }
            set { _musicaAmbiental = value; }
        }


        private Boolean _localOnBreak;

        public Boolean LocalOnBreak
        {
            get { return _localOnBreak; }
            set { _localOnBreak = value; }
        }


        private Boolean _otroLocalOnBreak;

        public Boolean OtroLocalOnBreak
        {
            get { return _otroLocalOnBreak; }
            set { _otroLocalOnBreak = value; }
        }

        private Double _valorArriendo;

        public Double ValorArriendo
        {
            get { return _valorArriendo; }
            set { _valorArriendo = value; }
        }

        public Cenas()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.Cenas cen = bdd.Cenas.Find(Numero);
                CommonBC.Syncronize(cen, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }

        public List<Cenas> ReadAll()
        {
            try
            {
                var c = from cen in bdd.Cenas
                        //join ta in bdd.TipoAmbientacion on cen.TipoAmbientacion equals ta.IdTipoAmbientacion
                        select new Cenas()
                        {
                            Numero = cen.Numero,
                            IdTipoAmbientacion = cen.IdTipoAmbientacion,
                            MusicaAmbiental = cen.MusicaAmbiental,
                            LocalOnBreak = cen.LocalOnBreak,
                            OtroLocalOnBreak = cen.OtroLocalOnBreak,
                            ValorArriendo = cen.ValorArriendo
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //lista de cenas DESCRIPCION (por si hay que mostrar una lista de cenas)
        public List<ListaCenas> ReadAll2()
        {
            try
            {
                var c = from cen in bdd.Cenas
                        join ta in bdd.TipoAmbientacion on cen.IdTipoAmbientacion equals ta.IdTipoAmbientacion

                        select new ListaCenas()
                        {
                            Numero = cen.Numero,
                            IdTipoAmbientacion = ta.Descripcion,
                            MusicaAmbiental = cen.MusicaAmbiental,
                            LocalOnBreak = cen.LocalOnBreak,
                            OtroLocalOnBreak = cen.OtroLocalOnBreak,
                            ValorArriendo = cen.ValorArriendo
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
    public class ListaCenas
    {
        public String Numero { get; set; }
        public String IdTipoAmbientacion { get; set; }
        public Boolean MusicaAmbiental { get; set; }
        public Boolean LocalOnBreak { get; set; }
        public Boolean OtroLocalOnBreak { get; set; }
        public double ValorArriendo { get; set; }

        public ListaCenas()
        {

        }
    }
}
