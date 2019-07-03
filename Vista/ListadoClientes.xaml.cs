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
using BibliotecaClases;

namespace Vista
{
    public partial class ListadoClientes : MetroWindow
    {
        CrearCliente cl;//recibir a cliente

        CrearContrato cc;//Recibe al Contrato

        ListaContrato lc;//Recibe al ListarContrato

        public ListadoClientes()
        {
            InitializeComponent();
            btnPasar.Visibility = Visibility.Hidden;
            btnPasarAContrato.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden; 

            //llenar el combo box con los datos del enumerador
            foreach (ActividadEmpresa item in new ActividadEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdActividadEmpresa;
                cb.descripcion = item.Descripcion;
                cbActiv.Items.Add(cb);
            }
            foreach (TipoEmpresa item in new TipoEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEmpresa;
                cb.descripcion = item.Descripcion;
                cbTipoEmp.Items.Add(cb);
            }

            cbActiv.SelectedIndex = 0;
            cbTipoEmp.SelectedIndex = 0;


            try
            {
                Cliente cl = new Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }
        }

        //Cerrar Sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion ini = new IniciarSesion();
            this.Close();
            ini.ShowDialog();
        }


        //Llamado desde Contrato
        public ListadoClientes(CrearContrato origen)
        {
            InitializeComponent();

            btnPasar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Hidden;
            btnPasarAContrato.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Visible;

            //llenar el combo box con los datos del enumerador
            foreach (ActividadEmpresa item in new ActividadEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdActividadEmpresa;
                cb.descripcion = item.Descripcion;
                cbActiv.Items.Add(cb);
            }
            foreach (TipoEmpresa item in new TipoEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEmpresa;
                cb.descripcion = item.Descripcion;
                cbTipoEmp.Items.Add(cb);
            }

            try
            {
                Cliente cl = new Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
            }
            cc = origen;
        }

        //Llamado desde el modulo administrar Cliente
        public ListadoClientes(CrearCliente origen)
        {
            InitializeComponent();
            cl = origen;
            btnPasar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Hidden;
            btnPasarAContrato.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

            //llenar el combo box con los datos del enumerador
            foreach (ActividadEmpresa item in new ActividadEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdActividadEmpresa;
                cb.descripcion = item.Descripcion;
                cbActiv.Items.Add(cb);
            }
            foreach (TipoEmpresa item in new TipoEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEmpresa;
                cb.descripcion = item.Descripcion;
                cbTipoEmp.Items.Add(cb);
            }

            try
            {
                Cliente cl = new Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Listar!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }

        }

        //Llamado desde listarContrato
        public ListadoClientes(ListaContrato origen)
        {

            InitializeComponent();

            btnPasar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
            btnPasarAContrato.Visibility = Visibility.Visible;
            btnCrear.Visibility = Visibility.Visible;

            //llenar el combo box con los datos del enumerador
            foreach (ActividadEmpresa item in new ActividadEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdActividadEmpresa;
                cb.descripcion = item.Descripcion;
                cbActiv.Items.Add(cb);
            }
            foreach (TipoEmpresa item in new TipoEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEmpresa;
                cb.descripcion = item.Descripcion;
                cbTipoEmp.Items.Add(cb);
            }

            try
            {
                Cliente cl = new Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
            }
            lc = origen;
        }



        //Botón Salir
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Botón Pasar
        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {

                if (cl == null)
                {
                    ListaClientes cli = (ListaClientes)dgLista.SelectedItem;
                    cc.txtBuscarCliente.Text = cli.Rut;
                    cc.Buscar();
                }
                else
                {
                    ListaClientes cli = (ListaClientes)dgLista.SelectedItem;
                    string rutbuscar;
                    rutbuscar = cl.txtRut + "-" + cl.txtDV;
                    cl.txtRut.Text = cli.Rut;
                    cl.Buscar();

                }

                Close();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al traspasar la Información"));

                Logger.Mensaje(ex.Message);
            }


        }

        //Botón Filtrar rut
        private async void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtFiltroRut.Text;

                List<ListaClientes> lc = new Cliente().FiltroRut(rut);
                dgLista.ItemsSource = lc;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));

                Logger.Mensaje(ex.Message);

                dgLista.Items.Refresh();
            }
        }

        //Botón filtrar tipoEmpresa
        private async void btnFiltrarTipo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                comboBoxId tipo = (comboBoxId)cbTipoEmp.SelectedItem;
                List<ListaClientes> lf = new Cliente().FiltroEmp(tipo.descripcion);
                dgLista.ItemsSource = lf;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al filtrar la Información"));

                Logger.Mensaje(ex.Message);
                dgLista.Items.Refresh();
            }

        }

        //Botón filtrar Actividad Empresa
        private async void btnFiltrarAct_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                comboBoxId act = (comboBoxId)cbActiv.SelectedItem;
                List<ListaClientes> lf = new Cliente().FiltroAct(act.descripcion);
                dgLista.ItemsSource = lf;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al filtrar la Información"));

                Logger.Mensaje(ex.Message);
                dgLista.Items.Refresh();
            }

        }

        //Botón Eliminar
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ListaClientes cli = (ListaClientes)dgLista.SelectedItem;
                var x = await this.ShowMessageAsync("Eliminar Datos de Cliente " + cli.Rut,
                         "¿Desea eliminar al Cliente?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    Cliente cl = new Cliente();
                    cl.RutCliente = cli.Rut;
                    bool resp = cl.Eliminar();
                    if (resp)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Cliente Eliminado"));

                        dgLista.ItemsSource =
                        cl.ReadAll2();
                        dgLista.Items.Refresh();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Lo Sentimos:",
                          string.Format("El cliente tiene contratos asociados, por tanto no se puede Eliminar"));
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format("Operación Cancelada"));
                }
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Eliminar la Información"));

                Logger.Mensaje(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Cliente cl = new Cliente();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();
        }

        private async void btnPasarContrato_Click(object sender, RoutedEventArgs e)
        {
            btnPasarAContrato.Visibility = Visibility.Visible;
            try
            {

                if (cl == null)
                {
                    ListaClientes clie = (ListaClientes)dgLista.SelectedItem;
                    lc.txtfiltroRut.Text = clie.Rut;
                    lc.BuscarCliente();

                }
                else
                {

                    ListaClientes clie = (ListaClientes)dgLista.SelectedItem;
                    lc.txtfiltroRut.Text = clie.Rut;
                    lc.BuscarCliente();

                }


                Close();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al traspasar la Información"));

                Logger.Mensaje(ex.Message);
            }
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            CrearCliente cliente = new CrearCliente();
            cliente.Show();
        }

    }
}
