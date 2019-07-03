using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MahApps.Metro.Actions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using MahApps.Metro.IconPacks;

namespace Vista
{
    public partial class MenuPrincipal : MetroWindow
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        //Crear Cliente
        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<CrearCliente>().Any())
            {
                CrearCliente _ver = new CrearCliente();
                _ver.ShowDialog();
            }
                
        }
        
        //Lista Cliente
        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ListadoClientes>().Any())
            {
                ListadoClientes _ver = new ListadoClientes();
                _ver.ShowDialog();
            }
        }

        //Crear Contrato
        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<CrearContrato>().Any())
            {
                CrearContrato _ver = new CrearContrato();
                _ver.ShowDialog();
            }
        }

        //Lista Contrato
        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ListaContrato>().Any())
            {
                ListaContrato _ver = new ListaContrato();
                _ver.ShowDialog();
            }
        }

        //Cerrar Sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion ini = new IniciarSesion();
            this.Close();
            ini.ShowDialog();
        }
    }
}
