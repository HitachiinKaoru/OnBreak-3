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
    public partial class ListaContrato : MetroWindow
    {
        private CrearCliente cli = new CrearCliente();
        CrearContrato cc;//recibir a crear contrato
        public ListaContrato()
        {
            InitializeComponent();
            btnPasar.Visibility = Visibility.Hidden;

            //COMBOBOX EVENTO
            foreach (TipoEvento item in new TipoEvento().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEvento;
                cb.descripcion = item.Descripcion;
                cbofilTipoContrato.Items.Add(cb);
            }

            //LLENAR CB MODALIDAD SERVICIO

            foreach (ModalidadServicio item in new ModalidadServicio().ReadAll())
            {
                comboBoxString cb = new comboBoxString();
                cb.id = item.IdModalidad;
                cb.descripcion = item.Nombre;
                cbFiltroModalidad.Items.Add(cb);
            }

            cbofilTipoContrato.SelectedIndex = 0;
            cbFiltroModalidad.SelectedIndex = 0;


            try
            {
                Contrato co = new Contrato();
                dgvLista.ItemsSource = co.ReadAll2();
                dgvLista.Items.Refresh();

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

        //Llamado Ventana Contrato
        public ListaContrato(CrearContrato origen)
        {
            InitializeComponent();
            cc = origen;

            //COMBO BOX////////////////////////////////////////
            foreach (TipoEvento item in new TipoEvento().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEvento;
                cb.descripcion = item.Descripcion;
                cbofilTipoContrato.Items.Add(cb);
            }


            try
            {
                Contrato co = new Contrato();
                dgvLista.ItemsSource = co.ReadAll2();
                dgvLista.Items.Refresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Listar");
            }
            //////////////////////////////////////////////////////
        }

        //THIS
        public ListaContrato(CrearCliente cli)
        {
            this.cli = cli;
        }

        //salir
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPasar_Click(object sender, RoutedEventArgs e)
        {

            if (btnPasar.Visibility == Visibility.Hidden)
            {
                btnPasar.Visibility = Visibility.Visible;
            }
            else
            {
                btnPasar.Visibility = Visibility.Hidden;
            }

        }






        //NÚMERO
        private async void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numero = txtfiltroNumero.Text;
                List<ListaContratos> lcon = new Contrato().FiltroNumeroContrato(numero);
                dgvLista.ItemsSource = lcon;

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje", "error al Filtrar Información");
                Logger.Mensaje(ex.Message);
                dgvLista.Items.Refresh();
            }
        }


        //CLIENTE
        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string rut = txtfiltroRut.Text;

                List<ListaContratos> lcl = new Contrato().FiltroRut(rut);
                dgvLista.ItemsSource = lcl;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje", "error al Filtrar Información");
                Logger.Mensaje(ex.Message);
                dgvLista.Items.Refresh();

            }

        }
        //TIPOEVENTO
        private async void btnFiltrarTipo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                comboBoxId tipoE = (comboBoxId)cbofilTipoContrato.SelectedItem;

                List<ListaContratos> lf = new Contrato().FiltroTipoEvento(tipoE.descripcion);
                dgvLista.ItemsSource = lf;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje", "error al Filtrar Información");
                Logger.Mensaje(ex.Message);
                dgvLista.Items.Refresh();
            }

        }

        private async void btnPasar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnPasar.Visibility == Visibility.Hidden)
                {
                    btnPasar.Visibility = Visibility.Hidden;

                }


                if (cc == null)
                {
                    ListaContratos con = (ListaContratos)dgvLista.SelectedItem;
                    cc.txtNumero.Text = con.Numero;
                    cc.BuscarContrato();
                }
                else
                {

                    ListaContratos con = (ListaContratos)dgvLista.SelectedItem;
                    cc.txtNumero.Text = con.Numero;
                    cc.BuscarContrato();

                    if (con.Realizado == true)
                    {
                        cc.rbSi.IsChecked = false;
                        cc.rbNo.IsChecked = true;
                        //BLOQUEAR EDITAR EL CONTRATO
                        cc.txtNumero.IsEnabled = false;
                        cc.txtBuscarCliente.IsEnabled = false;
                        cc.txtNumero.IsEnabled = false;
                        //Convert.ToDateTime(txtNumero).ToString("dd/MM/yyyy HH:mm")
                        cc.txtNumero.IsEnabled = false;
                        cc.txtBuscarCliente.IsEnabled = false;
                        cc.lblNumero.IsEnabled = false;
                        cc.cbModalidad.IsEnabled = false;
                        cc.cboTipo.IsEnabled = false;
                        cc.txtObservaciones.IsEnabled = false;
                        cc.txtNumeroAsistentes.IsEnabled = false;
                        cc.txtPersonalAdicional.IsEnabled = false;
                        cc.dpFechaInicio1.bloquear();
                        cc.dpFechaTermino.bloquear();
                    }
                    else
                    {
                        cc.rbSi.IsChecked = true;
                        cc.rbNo.IsChecked = false;
                        //BLOQUEAR EDITAR EL CONTRATO
                        cc.txtNumero.IsEnabled = true;
                        cc.txtBuscarCliente.IsEnabled = true;
                        cc.txtNumero.IsEnabled = true;
                        //Convert.ToDateTime(txtNumero).ToString("dd/MM/yyyy HH:mm")
                        cc.txtNumero.IsEnabled = true;
                        cc.txtBuscarCliente.IsEnabled = true;
                        cc.lblNumero.IsEnabled = true;
                        cc.cbModalidad.IsEnabled = true;
                        cc.cboTipo.IsEnabled = true;
                        cc.txtObservaciones.IsEnabled = true;
                        cc.txtNumeroAsistentes.IsEnabled = true;
                        cc.txtPersonalAdicional.IsEnabled = true;

                    }

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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            ListadoClientes cli = new ListadoClientes(this);
            cli.Show();

        }

        //FILTRAR MODALIDAD
        private async void btnFiltrarModalidad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                comboBoxString mod = (comboBoxString)cbFiltroModalidad.SelectedItem;

                List<ListaContratos> lf = new Contrato().FiltroModalidad(mod.descripcion);
                dgvLista.ItemsSource = lf;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje", "error al Filtrar Información");
                Logger.Mensaje(ex.Message);
                dgvLista.Items.Refresh();
            }
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            Contrato co = new Contrato();
            dgvLista.ItemsSource = co.ReadAll2();
            dgvLista.Items.Refresh();
        }


        //Busca cliente en listado de clientes.
        public async void BuscarCliente()
        {
            try
            {
                Cliente c = new Cliente();
                c.RutCliente = txtfiltroRut.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {
                    txtfiltroRut.Text = c.RutCliente;
                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                     string.Format("Cliente no encontrado"));
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al Buscar"));

                Logger.Mensaje(ex.Message);
            }
        }


    }
}
