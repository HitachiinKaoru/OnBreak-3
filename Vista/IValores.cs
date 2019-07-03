using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vista
{
    interface IValores
    {
        double valorModalidad(); //precio base
        int cantModalidad(); //personal base
        double valorCoffeAsistentes();
        double valorCoffePersonal();

        double valorCocktailAsistentes();
        double valorCocktailPersonal();
        double valorCocktailAmbientacion();
        double valorCocktailMusica();


        double valorCenas(); //valor conceptos de cenas
    }
}
