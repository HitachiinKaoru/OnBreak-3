using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BibliotecaClases
{
    public class Cliente
    {
        private OnBreak3Entities bdd = new OnBreak3Entities(); //Conexion BD

        DaoErrores dao = new DaoErrores();
        public DaoErrores retornar() { return dao; }

        //----------------------------------------------------

        private String _rutCliente;
        private String _razonSocial;
        private String _nombreContacto;
        private String _mailContacto;
        private String _direccion;
        private String _telefono;

        public String RutCliente
        {
            get { return _rutCliente; }
            set
            {
                if (value != "" && value.Length >= 11 && value.Length <= 12)
                {
                    _rutCliente = value;
                }
                else
                {
                    dao.AgregarError("- Campo Rut no puede estar Vacío y debe ingresar solo numeros sin digito Verificador");
                }
            }
        }

        public String RazonSocial
        {
            get { return _razonSocial; }
            set
            {
                if (value != "")
                {
                    _razonSocial = value;
                }
                else
                {
                    dao.AgregarError("- Campo RazónSocial no puede estar Vacío");
                }

            }
        }

        public String NombreContacto
        {
            get { return _nombreContacto; }
            set
            {
                if (value != "")
                {
                    _nombreContacto = value;
                }
                else
                {
                    dao.AgregarError("- Campo Nombre Contrato no puede estar Vacío");
                }

            }
        }

        public String MailContacto
        {
            get { return _mailContacto; }
            set
            {
                if (value != "")
                {
                    _mailContacto = value;
                }
                else
                {
                    dao.AgregarError("- Campo Email no puede estar Vacío");
                }
            }
        }

        public String Direccion
        {
            get { return _direccion; }
            set
            {
                if (value != "")
                {
                    _direccion = value;
                }
                else
                {
                    dao.AgregarError("- Campo Dirección no puede estar Vacío");
                }
            }
        }

        public string Telefono
        {
            get { return _telefono; }
            set
            {
                if (value != "" && value.Length >= 9 && value.Length <= 12)
                {
                    _telefono = value;
                }
                else
                {
                    dao.AgregarError("- Campo Teléfono no puede estar Vacío y/o debe tener un largo mínimo de 9 dígitos");

                }

            }
        }

        public int IdActividadEmpresa { get; set; } //foraneas
        public int IdTipoEmpresa { get; set; } //foraneas

        

        public Cliente()
        {

        }


        //METODOS CRUD -------------------------------------------------------------------

        public Boolean Grabar()
        {
            try
            {
                ConexionBD.Cliente cl = new ConexionBD.Cliente();
                CommonBC.Syncronize(this, cl);
                bdd.Cliente.Add(cl);
                bdd.SaveChanges();

                return true;


            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        public bool Eliminar()
        {
            try
            {
                Contrato cont = new Contrato();
                if (cont.verificarContratos() == false)
                {
                    ConexionBD.Cliente cl = bdd.Cliente.Find(RutCliente);
                    cont.RutCliente = cl.RutCliente;
                    bdd.Cliente.Remove(cl);
                    bdd.SaveChanges();

                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        public bool Buscar()
        {
            try
            {
                ConexionBD.Cliente cl =
                bdd.Cliente.First(cli => cli.RutCliente.Equals(RutCliente));
                CommonBC.Syncronize(cl, this);
                return true;

            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        public bool Modificar()
        {
            try
            {
                ConexionBD.Cliente cli = bdd.Cliente.Find(RutCliente);
                CommonBC.Syncronize(this, cli);
                bdd.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }
        }


        //Metodos Listar -----------------------------------
        public bool Read()
        {
            try
            {
                ConexionBD.Cliente cli = bdd.Cliente.Find(RutCliente);
                CommonBC.Syncronize(cli, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }

        public List<Cliente> ReadAll()
        {
            try
            {
                var c = from cli in bdd.Cliente
                        select new Cliente()
                        {
                            RutCliente = cli.RutCliente,
                            RazonSocial = cli.RazonSocial,
                            NombreContacto = cli.NombreContacto,
                            MailContacto = cli.MailContacto,
                            Direccion = cli.Direccion,
                            Telefono = cli.Telefono,
                            IdActividadEmpresa = cli.IdActividadEmpresa,
                            IdTipoEmpresa = cli.IdTipoEmpresa
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
                Logger.Mensaje(ex.Message);
            }
        }

        public List<ListaClientes> ReadAll2()
        {
            try
            {
                var c = from cli in bdd.Cliente
                        join actemp in bdd.ActividadEmpresa on cli.IdActividadEmpresa equals actemp.IdActividadEmpresa
                        join temp in bdd.TipoEmpresa on cli.IdTipoEmpresa equals temp.IdTipoEmpresa

                        select new ListaClientes()
                        {
                            Rut = cli.RutCliente,
                            RazonSocial = cli.RazonSocial,
                            NombreContacto = cli.NombreContacto,
                            MailContacto = cli.MailContacto,
                            Direccion = cli.Direccion,
                            Telefono = cli.Telefono,
                            ActividadEmpresa = actemp.Descripcion,
                            TipoEmpresa = temp.Descripcion
                        };

                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
                Logger.Mensaje(ex.Message);
            }
        }


        //Metodos Filtrar -----------------------------------
        
        //Filtro por Rut
        public List<ListaClientes> FiltroRut(string rut)
        {
            var cl = from cli in bdd.Cliente
                     join temp in bdd.TipoEmpresa on cli.IdTipoEmpresa equals temp.IdTipoEmpresa
                     join acti in bdd.ActividadEmpresa on cli.IdActividadEmpresa equals acti.IdActividadEmpresa
                     where cli.RutCliente == rut

                     select new ListaClientes()
                     {
                         Rut = cli.RutCliente,
                         RazonSocial = cli.RazonSocial,
                         NombreContacto = cli.NombreContacto,
                         MailContacto = cli.MailContacto,
                         Direccion = cli.Direccion,
                         Telefono = cli.Telefono,
                         ActividadEmpresa = acti.Descripcion,
                         TipoEmpresa = temp.Descripcion
                     };

            return cl.ToList();
        }

        //Filtrar por tipo de empresa
        public List<ListaClientes> FiltroEmp(string tip)
        {
            var emp = from cli in bdd.Cliente
                      join temp in bdd.TipoEmpresa on cli.IdTipoEmpresa equals temp.IdTipoEmpresa
                      join acti in bdd.ActividadEmpresa on cli.IdActividadEmpresa equals acti.IdActividadEmpresa
                      where temp.Descripcion == tip

                      select new ListaClientes()
                      {
                          Rut = cli.RutCliente,
                          RazonSocial = cli.RazonSocial,
                          NombreContacto = cli.NombreContacto,
                          MailContacto = cli.MailContacto,
                          Direccion = cli.Direccion,
                          Telefono = cli.Telefono,
                          ActividadEmpresa = acti.Descripcion,
                          TipoEmpresa = temp.Descripcion
                      };

            return emp.ToList();
        }

        //Filtrar por Actividad de la empresa
        public List<ListaClientes> FiltroAct(string act)
        {
            var actividad = from cli in bdd.Cliente
                            join temp in bdd.TipoEmpresa on cli.IdTipoEmpresa equals temp.IdTipoEmpresa
                            join acti in bdd.ActividadEmpresa on cli.IdActividadEmpresa equals acti.IdActividadEmpresa
                            where acti.Descripcion == act

                            select new ListaClientes()
                            {
                                Rut = cli.RutCliente,
                                RazonSocial = cli.RazonSocial,
                                NombreContacto = cli.NombreContacto,
                                MailContacto = cli.MailContacto,
                                Direccion = cli.Direccion,
                                Telefono = cli.Telefono,
                                ActividadEmpresa = acti.Descripcion,
                                TipoEmpresa = temp.Descripcion
                            };

            return actividad.ToList();
        }

    }

    public class ListaClientes
    {
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string TipoEmpresa { get; set; }
        public string ActividadEmpresa { get; set; }

        public ListaClientes()
        {

        }
    }

}
