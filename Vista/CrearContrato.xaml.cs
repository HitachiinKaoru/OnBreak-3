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
using WpfCalendarioHora;
using WebService;

namespace Vista
{
    public partial class CrearContrato : MetroWindow
    {
        double uf = new WebService.Service1().Uf();
        public CrearContrato()
        {
            InitializeComponent();
            lblNumero.Content = DateTime.Now.ToString("yyyyMMddHHmm");
            lblUf.Content = "$" + uf;
            this.cboTipo.SelectedItem = null;
            btnTerminar.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Hidden;


            //LLENAR COMBO BOX TIPO EVENTO
            foreach (TipoEvento item in new TipoEvento().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEvento;
                cb.descripcion = item.Descripcion;
                cboTipo.Items.Add(cb);
            }

            //LLENAR CB MODALIDAD SERVICIO

            foreach (ModalidadServicio item in new ModalidadServicio().ReadAll())
            {
                comboBoxString cb = new comboBoxString();
                cb.id = item.IdModalidad;
                cb.descripcion = item.Nombre;
                cbModalidad.Items.Add(cb);
            }

            cboTipo.SelectedIndex = 0;
            cbModalidad.SelectedIndex = 0;

            cbModalidad.IsEnabled = false;
        }

        //Cerrar Sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion ini = new IniciarSesion();
            this.Close();
            ini.ShowDialog();
        }

        //METODO CALCULO
        public double calculo()
        {
            // ModalidadServicio mod = new ModalidadServicio();
            double valorc = double.Parse(lblValorBase.Content.ToString())
            + double.Parse(lblAsistentes.Content.ToString())
            + double.Parse(lblPersonalAdicional.Content.ToString());


            return valorc;
        }


        //BOTON CALCULO CONTRATO
        private async void btnCalculo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                lblTotal.Content = calculo().ToString();
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Debe ingresar valor de asistentes y/o personal base"));
                Logger.Mensaje(ex.Message);
            }

        }


        //BORRAR ESTE DESPUÉS

        DateTime fechac = DateTime.Now;
        DateTime fechat = DateTime.Now;


        //CREAR CONTRATO
        private async void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpFechaInicio1.recuperar() <= dpFechaTermino.recuperar())
                {

                    String numero = lblNumero.Content.ToString();
                    DateTime creacion = fechac;
                    bool realizado;
                    DateTime termino;
                    if (rbSi.IsChecked == true)
                    {
                        realizado = false;
                        termino = dpFechaTermino.recuperarFecha();

                    }
                    else
                    {
                        realizado = true;
                        termino = dpFechaTermino.recuperarFecha();

                    }


                    //EVENTO

                    //inicio
                    DateTime fechaHoraInicio = dpFechaInicio1.recuperar();

                    DateTime fechaHoraTermino = dpFechaTermino.recuperar();


                    //////

                    int asistentes = 0;
                    if (int.TryParse(txtNumeroAsistentes.Text, out asistentes))
                    {

                    }
                    else
                    {
                        await this.ShowMessageAsync("Mensaje:",
                          string.Format("Ingrese sólo números"));
                        txtNumeroAsistentes.Focus();
                        return;
                    }

                    int personalAdicional = 0;
                    if (int.TryParse(txtPersonalAdicional.Text, out personalAdicional))
                    {

                    }
                    else
                    {
                        await this.ShowMessageAsync("Mensaje:",
                          string.Format("Ingrese sólo números"));
                        txtPersonalAdicional.Focus();
                        return;
                    }

                    //CB
                    int evento = ((comboBoxId)cboTipo.SelectedItem).id;
                    string idMod = ((comboBoxString)cbModalidad.SelectedItem).id;

                    String observaciones = txtObservaciones.Text;
                    double valorc = double.Parse(lblTotal.Content.ToString());
                    String rutCliente = txtBuscarCliente.Text;

                    Contrato con = new Contrato()
                    {

                        Numero = numero,
                        Creacion = creacion,
                        Termino = termino,
                        RutCliente = rutCliente,
                        IdModalidad = idMod,
                        IdTipoEvento = evento,
                        FechaHoraInicio = fechaHoraInicio,
                        FechaHoraTermino = fechaHoraTermino,
                        Asistentes = asistentes,
                        PersonalAdicional = personalAdicional,
                        Realizado = realizado,
                        ValorTotalContrato = calculo(),
                        Observaciones = observaciones,

                    };

                    bool resp = con.Grabar();
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format(resp ? "Guardado" : "No guardado"));

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error: Fecha de Termino es menor a Fecha de Inicio"));
                }
            }
            catch (ArgumentException exa)
            {
                await this.ShowMessageAsync("Mensaje:", string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));

                Logger.Mensaje(ex.Message);
            }

        }


        //limpiar
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            btnCrear.Visibility = Visibility.Visible;
            btnModificar.Visibility = Visibility.Hidden;
            btnTerminar.Visibility = Visibility.Hidden;

            txtBuscarCliente.Clear();
            lblNombreCliente.Visibility = Visibility.Hidden;
            txtNumero.Clear();
            lblNumero.Content = DateTime.Now.ToString("yyyyMMddHHmm");
            txtBuscarCliente.Clear();
            cboTipo.SelectedIndex = 0;
            cbModalidad.SelectedIndex = 0;
            cbModalidad.IsEnabled = false;

            //MÉTODO LIMPIAR USERCONTROL
            dpFechaInicio1.limpiar();
            dpFechaTermino.limpiar();

            cboTipo.SelectedItem = 0;

            txtNumeroAsistentes.Clear();
            txtPersonalAdicional.Clear();

            txtObservaciones.Clear();
            txtBuscarCliente.Focus();
            rbSi.IsChecked = true;
            rbNo.IsChecked = false;

            ////DESBLOQUEAR EDITAR EL CONTRATO
            if (!txtBuscarCliente.IsEnabled)
            {
                btnCrear.Visibility = Visibility.Visible;
                btnModificar.Visibility = Visibility.Hidden;
                btnTerminar.Visibility = Visibility.Hidden;
                txtNumero.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;
                txtNumero.IsEnabled = true;
                txtNumero.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;
                lblNumero.IsEnabled = true;

                //MÉTODO DESBLOQUEAR USERCONTROL
                dpFechaInicio1.desbloquear();
                dpFechaTermino.desbloquear();

                txtNumeroAsistentes.IsEnabled = true;
                txtPersonalAdicional.IsEnabled = true;
                cboTipo.IsEnabled = true;
                txtObservaciones.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;
            }

        }

        //listar numero contrato
        private void btnListadoNum_Click(object sender, RoutedEventArgs e)
        {
            ListaContrato con = new ListaContrato(this);
            con.Show();
        }

        //listar cliente
        private void btnListadoCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoClientes cli = new ListadoClientes(this);
            cli.Show();
        }

        //salir
        private void button_Click(object sender, RoutedEventArgs e)
        {
            ////DESBLOQUEAR EDITAR EL CONTRATO
            if (!txtBuscarCliente.IsEnabled)
            {
                txtNumero.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;
                txtNumero.IsEnabled = true;
                txtNumero.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;
                lblNumero.IsEnabled = true;

                //MÉTODO DESBLOQUEAR USERCONTROL
                dpFechaInicio1.desbloquear();
                dpFechaTermino.desbloquear();

                txtNumeroAsistentes.IsEnabled = true;
                txtPersonalAdicional.IsEnabled = true;
                cboTipo.IsEnabled = true;
                txtObservaciones.IsEnabled = true;
                txtBuscarCliente.IsEnabled = true;

            }
            this.Close();
        }



        //BUSCAR CONTRATO de traspasar
        private async void btnBuscarContrato_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Contrato c = new Contrato();
                Cliente clie = new Cliente();
                c.Numero = txtNumero.Text;
                bool buscar = c.Buscar();
                if (c.Realizado == true)
                {
                    rbSi.IsChecked = false;
                    rbNo.IsChecked = true;
                    //BLOQUEAR EDITAR EL CONTRATO
                    txtNumero.IsEnabled = false;
                    txtBuscarCliente.IsEnabled = false;
                    txtNumero.IsEnabled = false;
                    txtNumero.IsEnabled = false;
                    txtBuscarCliente.IsEnabled = false;
                    lblNumero.IsEnabled = false;
                    cbModalidad.IsEnabled = false;
                    cboTipo.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    txtNumeroAsistentes.IsEnabled = false;
                    txtPersonalAdicional.IsEnabled = false;
                    dpFechaInicio1.bloquear();
                    dpFechaTermino.bloquear();

                }
                else
                {
                    rbSi.IsChecked = true;
                    rbNo.IsChecked = false;
                    //BLOQUEAR EDITAR EL CONTRATO
                    txtNumero.IsEnabled = true;
                    txtBuscarCliente.IsEnabled = true;
                    txtNumero.IsEnabled = true;
                    txtNumero.IsEnabled = true;
                    txtBuscarCliente.IsEnabled = true;
                    lblNumero.IsEnabled = true;
                    cbModalidad.IsEnabled = true;
                    cboTipo.IsEnabled = true;
                    txtObservaciones.IsEnabled = true;
                    txtNumeroAsistentes.IsEnabled = true;
                    txtPersonalAdicional.IsEnabled = true;

                }


                if (buscar)
                {

                    txtBuscarCliente.Text = c.RutCliente;

                    dpFechaInicio1.datos(c.FechaHoraInicio);
                    dpFechaTermino.datos(c.FechaHoraTermino);

                    dpFechaInicio1.datos(c.FechaHoraInicio);
                    dpFechaTermino.datos(c.FechaHoraTermino);

                    txtNumeroAsistentes.Text = c.Asistentes.ToString();
                    txtPersonalAdicional.Text = c.PersonalAdicional.ToString();

                    TipoEvento tip = new TipoEvento();
                    tip.IdTipoEvento = c.IdTipoEvento;
                    tip.Read();
                    cboTipo.Text = tip.Descripcion;

                    ModalidadServicio mod = new ModalidadServicio();
                    mod.IdModalidad = c.IdModalidad;
                    mod.Read();
                    cbModalidad.Text = mod.Nombre;

                    txtObservaciones.Text = c.Observaciones;
                    lblNumero.Content = txtNumero.Text;
                    lblNombreCliente.Visibility = Visibility.Visible;
                    lblTotal.Content = calculo();
                    btnModificar.Visibility = Visibility.Visible;
                    btnTerminar.Visibility = Visibility.Visible;

                    btnCrear.Visibility = Visibility.Hidden;

                    lblNombreCliente.Content = clie.NombreContacto;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("Contrato no encontrado"));
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al Buscar"));

                Logger.Mensaje(ex.Message);
            }

        }

        //MÉTODO BUSCAR CONTRATO botón buscar

        public async void BuscarContrato()
        {
            try
            {
                Contrato c = new Contrato();
                c.Numero = txtNumero.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {
                    txtBuscarCliente.Text = c.RutCliente;

                    dpFechaInicio1.datos(c.FechaHoraInicio);
                    dpFechaTermino.datos(c.FechaHoraTermino);

                    dpFechaInicio1.datos(c.FechaHoraInicio);
                    dpFechaTermino.datos(c.FechaHoraTermino);

                    txtNumeroAsistentes.Text = c.Asistentes.ToString();
                    txtPersonalAdicional.Text = c.PersonalAdicional.ToString();

                    TipoEvento tip = new TipoEvento();
                    tip.IdTipoEvento = c.IdTipoEvento;
                    tip.Read();
                    cboTipo.Text = tip.Descripcion;

                    ModalidadServicio mod = new ModalidadServicio();
                    mod.IdModalidad = c.IdModalidad;
                    mod.Read();
                    cbModalidad.Text = mod.Nombre;
                    lblTotal.Content = calculo();

                    txtObservaciones.Text = c.Observaciones;
                    lblNumero.Content = txtNumero.Text;
                    btnModificar.Visibility = Visibility.Visible;
                    btnTerminar.Visibility = Visibility.Visible;
                    btnCrear.Visibility = Visibility.Hidden;

                    Cliente cl = new Cliente();
                    lblNombreCliente.Content = cl.NombreContacto;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("Contrato no encontrado"));
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al Buscar"));

                Logger.Mensaje(ex.Message);
            }

        }

        //BUSCAR CLIENTE
        private async void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = new Cliente();
                c.RutCliente = txtBuscarCliente.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {

                    lblNombreCliente.Content = c.NombreContacto;

                    lblNombreCliente.Visibility = Visibility.Visible;

                    lblNombreCliente.Visibility = Visibility.Visible;

                    btnCrear.Visibility = Visibility.Visible;

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


        //Busca cliente en listado de clientes
        public async void Buscar()
        {
            try
            {
                Cliente c = new Cliente();
                c.RutCliente = txtBuscarCliente.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {
                    lblNombreCliente.Content = c.NombreContacto;
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



        //MODIFICAR
        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    String numero = lblNumero.Content.ToString();
                    DateTime creacion = fechac;
                    bool realizado;
                    DateTime termino;
                    if (rbSi.IsChecked == true)
                    {
                        realizado = false;
                        termino = dpFechaTermino.recuperarFecha();

                    }
                    else
                    {
                        realizado = true;
                        termino = dpFechaTermino.recuperarFecha();
                    }

                    //////

                    int asistentes = 0;
                    if (int.TryParse(txtNumeroAsistentes.Text, out asistentes))
                    {

                    }
                    else
                    {
                        await this.ShowMessageAsync("Mensaje:",
                          string.Format("Ingrese sólo números"));
                        txtNumeroAsistentes.Focus();
                        return;
                    }

                    int personalAdicional = 0;
                    if (int.TryParse(txtPersonalAdicional.Text, out personalAdicional))
                    {

                    }
                    else
                    {
                        await this.ShowMessageAsync("Mensaje:",
                          string.Format("Ingrese sólo números"));
                        txtPersonalAdicional.Focus();
                        return;
                    }

                    //CB
                    int evento = ((comboBoxId)cboTipo.SelectedItem).id;
                    string idMod = ((comboBoxString)cbModalidad.SelectedItem).id;

                    String observaciones = txtObservaciones.Text;
                    double valorc = double.Parse(lblTotal.Content.ToString());
                    String rutCliente = txtBuscarCliente.Text;

                    Contrato nuevo_con = new Contrato()
                    {

                        Numero = numero,
                        Creacion = creacion,
                        Termino = termino,
                        RutCliente = rutCliente,
                        IdModalidad = idMod,
                        IdTipoEvento = evento,
                        FechaHoraInicio = dpFechaInicio1.recuperar(),
                        FechaHoraTermino = dpFechaTermino.recuperar(),
                        Asistentes = asistentes,
                        PersonalAdicional = personalAdicional,
                        Realizado = realizado,
                        ValorTotalContrato = valorc,
                        Observaciones = observaciones,
                    };

                    bool resp = nuevo_con.Modificar();
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format(resp ? "Contrato Modificado" : "Contrato No Modificado"));

            }
            catch (ArgumentException exa)
            {
                MessageBox.Show(exa.Message);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error"));

                Logger.Mensaje(ex.Message);
            }

        }

        //TERMINAR CONTRATO
        private async void btnTerminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x =
             await this.ShowMessageAsync("Advertencia", "¿Desea Dar Término al Contrato?",
                     MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {

                    String numero = lblNumero.Content.ToString();
                    DateTime creacion = fechac;
                    bool realizado = true;
                    DateTime termino = DateTime.Now;
                    rbNo.IsChecked = true;
                    rbSi.IsChecked = false;

                    //EVENTO

                    //inicio
                    DateTime fechaHoraInicio = dpFechaInicio1.recuperar();

                    //termino
                    DateTime fechaFinTermino = dpFechaTermino.recuperar();


                    //////
                    int asistentes = int.Parse(txtNumeroAsistentes.Text);
                    int personalAdicional = int.Parse(txtPersonalAdicional.Text);

                    int evento = ((comboBoxId)cboTipo.SelectedItem).id;
                    string idMod = ((comboBoxString)cbModalidad.SelectedItem).id;

                    String observaciones = txtObservaciones.Text;
                    double valorc = double.Parse(lblTotal.Content.ToString());
                    String rutCliente = txtBuscarCliente.Text;

                    Contrato con_mod = new Contrato()
                    {
                        Numero = numero,
                        Creacion = creacion,
                        Termino = termino,
                        RutCliente = rutCliente,
                        IdModalidad = idMod,
                        IdTipoEvento = evento,
                        FechaHoraInicio = fechaHoraInicio,
                        FechaHoraTermino = fechaFinTermino,
                        Asistentes = asistentes,
                        PersonalAdicional = personalAdicional,
                        Realizado = realizado,
                        ValorTotalContrato = valorc,
                        Observaciones = observaciones,
                    };

                    //BLOQUEAR EDITAR EL CONTRATO
                    txtNumero.IsEnabled = false;
                    txtBuscarCliente.IsEnabled = false;
                    txtNumero.IsEnabled = false;
                    txtNumero.IsEnabled = false;
                    txtBuscarCliente.IsEnabled = false;
                    lblNumero.IsEnabled = false;
                    cbModalidad.IsEnabled = false;
                    cboTipo.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    txtNumeroAsistentes.IsEnabled = false;
                    txtPersonalAdicional.IsEnabled = false;
                    dpFechaInicio1.bloquear();
                    dpFechaTermino.bloquear();

                    bool resp = con_mod.Modificar();

                    await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Contrato Terminado" : "Contrato No Terminado"));
                    btnTerminar.Visibility = Visibility.Hidden;
                    btnModificar.Visibility = Visibility.Hidden;
                    btnCrear.Visibility = Visibility.Hidden;
                }

            }
            catch (ArgumentException exa)
            {
                MessageBox.Show(exa.Message);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Guardado"));
                MessageBox.Show("Error");
                Logger.Mensaje(ex.Message);
            }

        }




        //valor evento base

        //Valor asistentes
        private async void txtNumeroAsistentes_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            if (txtNumeroAsistentes.Text != null)
            {
                //WebService.Service1 WS = new WebService.Service1();
                //double uf = WS.Uf();


                //double asi = 0;
                //if (double.TryParse(txtNumeroAsistentes.Text, out asi))
                //{

                //}
                //else
                //{

                //    txtNumeroAsistentes.Focus();
                //    return;
                //}

                //double n = 0;

                //if (asi >= 1 && asi <= 20)
                //{
                //    n = 3;
                //}
                //if (asi >= 21 && asi <= 50)
                //{
                //    n = 5;
                //}
                //if (asi > 50)
                //{
                //    double c = asi - 50;
                //    n = 5;
                //    double r = (c / 20);
                //    n = n + r;

                //}
                //double v = (n * uf);
                //lblAsistentes.Content = v.ToString();
            }
            else
            {
                txtNumeroAsistentes.Text = "0";
            }

        }



        //valor personal adicional
        private async void txtPersonalAdicional_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (txtPersonalAdicional.Text != null)
            {
                ModalidadServicio mod = new ModalidadServicio();
                double personal = 0;
                if (double.TryParse(txtPersonalAdicional.Text, out personal))
                {

                }
                else
                {

                    txtPersonalAdicional.Focus();
                    return;
                }
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
                lblPersonalAdicional.Content = v.ToString();
            }
        }

        private void cboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int contenido = ((comboBoxId)cboTipo.SelectedItem).id;

            ModalidadServicio mod = new ModalidadServicio();
            cbModalidad.Items.Clear();
            cbModalidad.IsEnabled = true;
            foreach (var item in mod.ReadAll().Where(con => con.IdTipoEvento == contenido).ToList())
            {
                comboBoxString cb = new comboBoxString();
                cb.id = item.IdModalidad;
                cb.descripcion = item.Nombre;
                cbModalidad.Items.Add(cb);
            }
            cbModalidad.SelectedIndex = 0;
        }



        private void btnMasHoraInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbModalidad_LostFocus(object sender, SelectionChangedEventArgs e)
        {
            if (cbModalidad.SelectedItem != null)
            {
            //    string moda = ((comboBoxString)cbModalidad.SelectedItem).id;
            //    int cant = 0;
            //    int personal = 0;

            //    if (moda.Equals("CB001"))
            //    {
            //        cant = 3;
            //        personal = 2;
            //    }
            //    if (moda.Equals("CB002"))
            //    {
            //        cant = 8;
            //        personal = 6;
            //    }
            //    if (moda.Equals("CB003"))
            //    {
            //        cant = 12;
            //        personal = 6;
            //    }
            //    if (moda.Equals("CE001"))
            //    {
            //        cant = 25;
            //        personal = 10;
            //    }
            //    if (moda.Equals("CE002"))
            //    {
            //        cant = 35;
            //        personal = 14;
            //    }
            //    if (moda.Equals("CO001"))
            //    {
            //        cant = 6;
            //        personal = 4;
            //    }
            //    if (moda.Equals("CO002"))
            //    {
            //        cant = 10;
            //        personal = 5;
            //    }

            //    double valor = (double)(cant * uf);
            //    lblPersonalBase.Content = personal.ToString();
            //    lblValorBase.Content = valor.ToString();
            }

        }

    }
}
