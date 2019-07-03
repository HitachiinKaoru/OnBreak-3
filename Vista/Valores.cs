using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaClases;
using WebService;

namespace Vista
{
    public class Valores : IValores
    {
        private Contrato contraTemp;
        private ModalidadServicio mod;
        double uf = new WebService.Service1().Uf(); //valor uf
        CrearContrato con = new CrearContrato();

        public Valores(Contrato con)
        {
            contraTemp = con;
        }

        public double valorModalidad(CrearContrato con)
        {
            double mser = 0;
            //string moda = ((comboBoxString)con.cbModalidad.SelectedItem).id;

            switch (contraTemp.IdModalidad)
            {
                case string.Equals("CB001"):
                    mser = valorSer();
                    break;
            }
            return mser;
        }

            private double valorSer(CrearContrato co)
        {
            string moda = ((comboBoxString)co.cbModalidad.SelectedItem).id;
            int cant = 0;
            //int personal = 0;

            if (moda.Equals("CB001"))
            {
                cant = 3;
              //  personal = 2;
            }
            if (moda.Equals("CB002"))
            {
                cant = 8;
                //personal = 6;
            }
            if (moda.Equals("CB003"))
            {
                cant = 12;
                //personal = 6;
            }
            if (moda.Equals("CE001"))
            {
                cant = 25;
                //personal = 10;
            }
            if (moda.Equals("CE002"))
            {
                cant = 35;
                //personal = 14;
            }
            if (moda.Equals("CO001"))
            {
                cant = 6;
                //personal = 4;
            }
            if (moda.Equals("CO002"))
            {
                cant = 10;
                //personal = 5;
            }

            double valor = (double)(cant * uf);
            return valor;
        }




        //------------------------------------------------------------

        public double valorCenas()
        {
            throw new NotImplementedException();
        }

        public double valorModalidad()
        {
            throw new NotImplementedException();
        }

        public int cantModalidad()
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------
        public double valorCoffeAsistentes()
        {

            double asi = double.Parse(con.txtNumeroAsistentes.Text);

            double n = 0;
            if (asi >= 1 && asi <= 20)
            {
                n = 3;
            }
            if (asi >= 21 && asi <= 50)
            {
                n = 5;
            }
            if (asi > 50)
            {
                double c = asi - 50;
                n = 5;
                double r = (c / 20);
                n = n + r;

            }
            double v = (int)(n * uf);

            return v;
        }

        public double valorCoffePersonal()
        {
            ModalidadServicio mod = new ModalidadServicio();
            double personal = double.Parse(con.txtPersonalAdicional.Text);

            double cant_uf = 0;


            if (personal == 2)
            {
                cant_uf = 2;
            }
            if (personal == 3)
            {
                cant_uf = 3;
            }
            if (personal == 4)
            {
                cant_uf = 3.5;
            }
            if (personal > mod.PersonalBase)
            {
                double cantidad = personal - mod.PersonalBase;
                cant_uf = 3.5;

                double extra = (cantidad * 0.5);
                cant_uf = cant_uf + extra;

            }

            double v = (double)(cant_uf * uf);
            return v;
        }

        //--------------------------------------------------
        public double valorCocktailAsistentes()
        {
            double asi = double.Parse(con.txtNumeroAsistentes.Text);

            double n = 0;
            if (asi >= 1 && asi <= 20)
            {
                n = 4;
            }
            if (asi >= 21 && asi <= 50)
            {
                n = 6;
            }
            if (asi > 50)
            {
                double c = asi - 50;
                n = 5;
                double r = (c / 20);
                n = n + r;

            }
            double v = (int)(n * uf);

            return v;
        }

        public double valorCocktailPersonal()
        {
            ModalidadServicio mod = new ModalidadServicio();
            double personal = double.Parse(con.txtPersonalAdicional.Text);

            double cant_uf = 0;


            if (personal == 2)
            {
                cant_uf = 2;
            }
            if (personal == 3)
            {
                cant_uf = 3;
            }
            if (personal == 4)
            {
                cant_uf = 3.5;
            }
            if (personal > mod.PersonalBase)
            {
                double cantidad = personal - mod.PersonalBase;
                cant_uf = 3.5;

                double extra = (cantidad * 0.5);
                cant_uf = cant_uf + extra;

            }

            double v = (double)(cant_uf * uf);
            return v;
        }

        public double valorCocktailAmbientacion()
        {
            int moda = ((comboBoxId)con.cbAmbientacion.SelectedItem).id;
            double tipo = 0;

            if (moda == 10)
            {
                tipo = 2;
            }
            if (moda == 20)
            {
                tipo = 5;
            }
            if (moda == 30)
            {
                tipo = 0;
            }

            double v = (double)(tipo * uf);
            return v;
        }

        public double valorCocktailMusica()
        {
            throw new NotImplementedException();
        }
    }
}
