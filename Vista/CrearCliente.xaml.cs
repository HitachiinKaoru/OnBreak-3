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

    public partial class CrearCliente : MetroWindow
    {
        public CrearCliente()
        {
            InitializeComponent();

            txtDV.IsEnabled = false;
            btnModificar.Visibility = Visibility.Hidden;

            //llenar el combo box con los datos del enumerador
            foreach (ActividadEmpresa item in new ActividadEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdActividadEmpresa;
                cb.descripcion = item.Descripcion;
                cbActividad.Items.Add(cb);
            }
            foreach (TipoEmpresa item in new TipoEmpresa().ReadAll())
            {
                comboBoxId cb = new comboBoxId();
                cb.id = item.IdTipoEmpresa;
                cb.descripcion = item.Descripcion;
                cbTipo.Items.Add(cb);
            }
            cbActividad.SelectedIndex = 0;
            cbTipo.SelectedIndex = 0;
            txtTelefono.Text = "0";
        }

        //Cerrar Sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion ini = new IniciarSesion();
            this.Close();
            ini.ShowDialog();
        }

        //Botón limpiar
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtDV.Clear();
            txtRut.Clear();
            txtRut.IsEnabled = true;
            txtRazon.Clear();
            txtNombre.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtTelefono.Text = "0";
            cbActividad.SelectedIndex = 0;
            cbTipo.SelectedIndex = 0;
            txtRut.Focus();//Mover el cursor a la poscición Rut

            btnModificar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;
            txtRut.IsEnabled = true;

        }

        //Botón '?'
        //llama el listado de clientes
        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            ListadoClientes list = new ListadoClientes(this);
            list.Show();
        }

        //Botón Cancelar/Cerrar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Botón Guardar
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                if (rut.Length == 11)
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                String razonSocial = txtRazon.Text;
                String nombreContacto = txtNombre.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                int telefono = 0;
                if (int.TryParse(txtTelefono.Text, out telefono))
                {

                }
                else
                {
                    return;
                }
                int Actividad = ((comboBoxId)cbActividad.SelectedItem).id;
                int Tipo = ((comboBoxId)cbTipo.SelectedItem).id;
                Cliente c = new Cliente()
                {
                    RutCliente = rut,
                    RazonSocial = razonSocial,
                    NombreContacto = nombreContacto,
                    MailContacto = mail,
                    Direccion = direccion,
                    Telefono = telefono.ToString(),
                    IdActividadEmpresa = Actividad,
                    IdTipoEmpresa = Tipo

                };
                bool resp = c.Grabar();
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));


                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {
                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }


                //-----------------------------------------------------------------------------------------------


            }
            catch (ArgumentException exa)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));

                Logger.Mensaje(ex.Message);
            }
        }



        //Llamado del botón traspasar
        public async void Buscar()
        {
            try
            {
                Cliente c = new Cliente();
                c.RutCliente = txtRut.Text;
                bool buscar = c.Buscar();

                if (buscar)
                {
                    txtRut.Text = c.RutCliente.Substring(0, 10);
                    txtDV.Text = c.RutCliente.Substring(11, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;
                    txtRazon.Text = c.RazonSocial;
                    txtNombre.Text = c.NombreContacto;
                    txtEmail.Text = c.MailContacto;
                    txtDireccion.Text = c.Direccion;
                    txtTelefono.Text = c.Telefono.ToString();
                    ActividadEmpresa ac = new ActividadEmpresa();
                    ac.IdActividadEmpresa = c.IdActividadEmpresa;
                    ac.Read();
                    cbActividad.Text = ac.Descripcion;
                    TipoEmpresa te = new TipoEmpresa();
                    te.IdTipoEmpresa = c.IdTipoEmpresa;
                    te.Read();
                    cbTipo.Text = te.Descripcion;

                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;

                    txtRut.IsEnabled = false;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("No se encontraron resultados!"));
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información"));

                Logger.Mensaje(ex.Message);
            }
        }

        //Botón Buscar (de administrar cliente)
        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Cliente c = new Cliente();
                c.RutCliente = txtRut.Text + "-" + txtDV.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {
                    txtRut.Text = c.RutCliente.Substring(0, 10);
                    txtDV.Text = c.RutCliente.Substring(11, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;
                    txtRazon.Text = c.RazonSocial;
                    txtNombre.Text = c.NombreContacto;
                    txtEmail.Text = c.MailContacto;
                    txtDireccion.Text = c.Direccion;
                    txtTelefono.Text = c.Telefono.ToString();
                    ActividadEmpresa ac = new ActividadEmpresa();
                    ac.IdActividadEmpresa = c.IdActividadEmpresa;
                    ac.Read();
                    cbActividad.Text = ac.Descripcion;
                    TipoEmpresa te = new TipoEmpresa();
                    te.IdTipoEmpresa = c.IdTipoEmpresa;
                    te.Read();
                    cbTipo.Text = te.Descripcion;

                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                     string.Format("No se encontraron resultados!"));
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información! "));

                Logger.Mensaje(ex.Message);

            }
        }

        //Botón modificar
        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                String razonSocial = txtRazon.Text;
                String nombreContacto = txtNombre.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                int telefono = 0;
                if (int.TryParse(txtTelefono.Text, out telefono))
                {

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("Ingrese un número de 9 dígitos"));
                    txtTelefono.Focus();
                    return;
                }
                int Actividad = ((comboBoxId)cbActividad.SelectedItem).id;
                int Tipo = ((comboBoxId)cbTipo.SelectedItem).id;
                Cliente c = new Cliente()
                {
                    RutCliente = rut,
                    RazonSocial = razonSocial,
                    NombreContacto = nombreContacto,
                    MailContacto = mail,
                    Direccion = direccion,
                    Telefono = telefono.ToString(),
                    IdActividadEmpresa = Actividad,
                    IdTipoEmpresa = Tipo

                };
                bool resp = c.Modificar();
                await this.ShowMessageAsync("Mensaje:",
                     string.Format(resp ? "Actualizado" : "No Actualizado"));

                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {

                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }


                //-----------------------------------------------------------------------------------------------

            }
            catch (ArgumentException exa)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Actualizar Datos"));

                Logger.Mensaje(ex.Message);
            }
        }


        //añadir formato al rut

        private void txtRut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text.Length >= 7 && txtRut.Text.Length <= 8)
            {
                string v = new VerificarRut().ValidarRut(txtRut.Text);
                txtDV.Text = v;
                try
                {
                    string rutSinFormato = txtRut.Text;

                    //si el rut ingresado tiene "." o "," o "-" son ratirados para realizar la formula 
                    rutSinFormato = rutSinFormato.Replace(",", "");
                    rutSinFormato = rutSinFormato.Replace(".", "");
                    rutSinFormato = rutSinFormato.Replace("-", "");
                    string rutFormateado = String.Empty;

                    //obtengo la parte numerica del RUT
                    //string rutTemporal = rutSinFormato.Substring(0, rutSinFormato.Length - 1);
                    string rutTemporal = rutSinFormato;
                    //obtengo el Digito Verificador del RUT
                    //string dv = rutSinFormato.Substring(rutSinFormato.Length - 1, 1);

                    Int64 rut;

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rut))
                    {
                        rut = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    rutFormateado = rut.ToString("N0");

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        // rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }

                    //se pasa a mayuscula si tiene letra k
                    rutFormateado = rutFormateado.ToUpper();

                    //la salida esperada para el ejemplo es 99.999.999-K
                    txtRut.Text = rutFormateado;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                txtRut.Text = "";
            }
        }
    }
}
