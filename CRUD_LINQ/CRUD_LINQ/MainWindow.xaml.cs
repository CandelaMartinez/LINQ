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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace CRUD_LINQ
{
  
    public partial class MainWindow : Window
    {

        //creo data context en base al archivo .dbml
        DataClasses1DataContext dataContext;


        public MainWindow()
        {
            InitializeComponent();

            //conexion con el origen de datos
            string miConexion = ConfigurationManager.ConnectionStrings["CRUD_LINQ.Properties.Settings.CrudLinqSQL"].ConnectionString;


            //establecer conexion con archivo que permite mapear
            dataContext = new DataClasses1DataContext(miConexion);

            //InsertaEmpresas();

            //InsertaEmpleados();

            //InsertaCargos();

            //InsertaEmpleadoCargo();

            //ActualizaEmpleado();



            //EliminaEmpleado();
        }

        public void InsertaEmpresas()
        {
            //borre todo lo que hay
           // dataContext.ExecuteCommand("delete from Empresa");


            //ya tengo la tabla empresa en el modelo de datos .dbml
            Empresa candelaMartinez = new Empresa();
            candelaMartinez.Nombre = "Candela Martinez";

            Empresa mM = new Empresa();
           mM.Nombre = "Marian Moreno";

            //inserte este objeto en la tabla empresa
            dataContext.Empresa.InsertOnSubmit(candelaMartinez);
            dataContext.Empresa.InsertOnSubmit(mM);
            //tome efecto
            dataContext.SubmitChanges();

            //mostrar en el dataGrid lo que acabo de insertar
            Principal.ItemsSource = dataContext.Empresa;

        }

        public void InsertaEmpleados()
        {
            //creo empresas porque tengo que relacionar empleados con empresas
            //expresion lambda: el primer objeto del dataContext cuya variable em sea igual al nombre que le paso por parametro
            Empresa candelaMartinez = dataContext.Empresa.First(em =>em.Nombre.Equals("Candela Martinez"));

            Empresa nM = dataContext.Empresa.First(em => em.Nombre.Equals("Marian Moreno"));

            //creo objetos
            List<Empleado> listaEmpleados = new List<Empleado>();

            listaEmpleados.Add(new Empleado { Nombre = "sara", Apellido = "x", EmpresaId = candelaMartinez.Id });

            listaEmpleados.Add(new Empleado { Nombre = "toni", Apellido = "b", EmpresaId = candelaMartinez.Id });

            listaEmpleados.Add(new Empleado { Nombre = "ana", Apellido = "y", EmpresaId = nM.Id });

            listaEmpleados.Add(new Empleado { Nombre = "puri", Apellido = "g", EmpresaId = nM.Id });


            //inserto todo lo que hay en la lista
            dataContext.Empleado.InsertAllOnSubmit(listaEmpleados);

            //toma efecto
            dataContext.SubmitChanges();

            //verlo desde la aplicacion grafica
            Principal.ItemsSource = dataContext.Empleado;


        }

        public void InsertaCargos()
        {
            dataContext.Cargo.InsertOnSubmit(new Cargo {NombreCargo="Director/a" });
            dataContext.Cargo.InsertOnSubmit(new Cargo {NombreCargo = "Administrativo/a" });

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.Cargo;


        }

        public void InsertaEmpleadoCargo()
        {
            //creo la variable sara, el primer empleado con ese nombre dentro del datacontext lo guarde dentro de esa variable
            Empleado sara = dataContext.Empleado.First(em=> em.Nombre.Equals("sara"));
            Empleado toni = dataContext.Empleado.First(em => em.Nombre.Equals("toni"));
            Empleado ana = dataContext.Empleado.First(em => em.Nombre.Equals("ana"));
            Empleado puri = dataContext.Empleado.First(em => em.Nombre.Equals("puri"));

            //cargos
            Cargo cDirector = dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Director/a"));
            Cargo cAdm= dataContext.Cargo.First(cg => cg.NombreCargo.Equals("Administrativo/a"));

            //relacionar desde codigo las dos tablas
       
            List<CargoEmpleado> listaCargosEmpleados = new List<CargoEmpleado>();
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = sara, Cargo=cDirector });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = toni, Cargo = cDirector });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = ana, Cargo = cAdm });
            listaCargosEmpleados.Add(new CargoEmpleado { Empleado = puri, Cargo = cAdm });


            dataContext.CargoEmpleado.InsertAllOnSubmit(listaCargosEmpleados);

            dataContext.SubmitChanges();

            Principal.ItemsSource = dataContext.CargoEmpleado;
            
        }

        //metodo para actualizar un registro
        public void ActualizaEmpleado()
        {
            Empleado Sara = dataContext.Empleado.First(em => em.Nombre.Equals("sara"));
            Sara.Nombre = "sara antonia";
            dataContext.SubmitChanges();
            Principal.ItemsSource = dataContext.Empleado;
        }

        //borrar un registro
        public void EliminaEmpleado()
        {
            Empleado Toni = dataContext.Empleado.First(em => em.Nombre.Equals("toni"));
            dataContext.Empleado.DeleteOnSubmit(Toni);
            dataContext.SubmitChanges();
            Principal.ItemsSource = dataContext.Empleado;

        }
    }
}
