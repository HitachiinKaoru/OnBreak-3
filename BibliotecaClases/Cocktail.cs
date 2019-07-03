using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class Cocktail
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        public String Numero { get; set; }

        public int IdTipoAmbientacion { get; set; }

        private Boolean _ambientacion;

        public Boolean Ambientacion
        {
            get { return _ambientacion; }
            set { _ambientacion = value; }
        }

        private Boolean _musicaAmbiental;

        public Boolean MusicaAmbiental
        {
            get { return _musicaAmbiental; }
            set { _musicaAmbiental = value; }
        }

        private Boolean _musicaCliente;

        public Boolean MusicaCliente
        {
            get { return _musicaCliente; }
            set { _musicaCliente = value; }
        }


        public Cocktail()
        {

        }

        //METODOS
        public bool Read()
        {
            try
            {
                ConexionBD.Cocktail coc = bdd.Cocktail.Find(Numero);
                CommonBC.Syncronize(coc, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }

        public List<Cocktail> ReadAll()
        {
            try
            {
                var c = from cen in bdd.Cocktail
                        join tam in bdd.TipoAmbientacion on cen.IdTipoAmbientacion equals tam.IdTipoAmbientacion
                        select new Cocktail()
                        {
                            Numero = cen.Numero,
                            IdTipoAmbientacion = tam.IdTipoAmbientacion,
                            Ambientacion = cen.Ambientacion,
                            MusicaAmbiental = cen.MusicaAmbiental,
                            MusicaCliente = cen.MusicaCliente

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //lista de cenas DESCRIPCION (por si hay que mostrar una lista de cenas)
        public List<ListaCocktail> ReadAll2()
        {
            try
            {
                var c = from cen in bdd.Cocktail
                        join tam in bdd.TipoAmbientacion on cen.IdTipoAmbientacion equals tam.IdTipoAmbientacion

                        select new ListaCocktail()
                        {
                            Numero = cen.Numero,
                            IdTipoAmbientacion = tam.Descripcion,
                            Ambientacion = cen.Ambientacion,
                            MusicaAmbiental = cen.MusicaAmbiental,
                            MusicaCliente = cen.MusicaCliente
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    public class ListaCocktail
    {
        public String Numero { get; set; }
        public String IdTipoAmbientacion { get; set; }
        public Boolean Ambientacion { get; set; }
        public Boolean MusicaAmbiental { get; set; }
        public Boolean MusicaCliente { get; set; }

        public ListaCocktail()
        {

        }
    }
}

