using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_Objetos
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlEmpresaEmpleados c = new ControlEmpresaEmpleados();
            // c.getCEO();

            //c.getEmpleadosOrdenados();

            //c.getEmpleadosOrdenadosInversa();

            //c.getEmpleadosMendez();

            //le pido al usuario la empresa
            Console.WriteLine("introduce id de la empresa:");

            string entrada = Console.ReadLine();
            try
            {
                int entradaId = Convert.ToInt32(entrada);

                c.getEmpleadosEmpresa(entradaId);

            }catch(Exception e)
            {
                Console.WriteLine( " error al introducir el id");
            }
        }
    }

    //........................................................................
    class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public void getDatosEmpresa()
        {
            Console.WriteLine("empresa {0} con Id {1}", Nombre, Id);
        }

    }
    //........................................................................
    class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Cargo { get; set; }

        public double Salario { get; set; }


        //clave foranea que relaciona empleado y empresa
        public int EmpresaId { get; set; }



        public void getDatosEmpleado()
        {
            Console.WriteLine("empleado {0} con Id {1}, cargo {2} con salario {3} " +
                "y pertenece a empresa {4}", Nombre, Id, Cargo, Salario, EmpresaId);
        }

    }

    //clase donde almaceno empresas y empleados...............................
    class ControlEmpresaEmpleados
    {
        public List<Empresa> listaEmpresas;

        public List<Empleado> listaEmpleados;

      public  ControlEmpresaEmpleados()
        {
            listaEmpresas = new List<Empresa>();

            listaEmpleados = new List<Empleado>();

            listaEmpresas.Add(new Empresa { Id =1, Nombre = "Garcia" });
            listaEmpresas.Add(new Empresa { Id =2, Nombre = "Mendez" });

            listaEmpleados.Add(new Empleado { Id =1, Nombre = "teresa", Cargo = "ceo", EmpresaId = 1, Salario = 150000 });
            listaEmpleados.Add(new Empleado { Id =2, Nombre = "juan", Cargo = "ceo", EmpresaId = 2, Salario = 150001 });
            listaEmpleados.Add(new Empleado { Id =3, Nombre = "olga", Cargo = "co-ceo", EmpresaId = 1, Salario = 150002 });
            listaEmpleados.Add(new Empleado { Id =4, Nombre = "mari", Cargo = "co-ceo", EmpresaId = 2, Salario = 150003 });



        }


        public void getCEO()
        {
            IEnumerable<Empleado> ceos = from empleado in listaEmpleados where empleado.Cargo == "ceo" select empleado;

            foreach (Empleado i in ceos)
            {
                i.getDatosEmpleado();
            }
        }

        public void getEmpleadosOrdenados()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados orderby empleado.Nombre select empleado;

            foreach (Empleado i in empleados)
            {
                i.getDatosEmpleado();
            }
        }

        public void getEmpleadosOrdenadosInversa()
        {
            IEnumerable<Empleado> empleados = from empleado in listaEmpleados orderby empleado.Nombre descending select empleado;

            foreach (Empleado i in empleados)
            {
                i.getDatosEmpleado();
            }
        }

        public void getEmpleadosMendez()
        {
            IEnumerable<Empleado> empleadosM = from empleado in listaEmpleados join empresa in listaEmpresas
                                              on empleado.EmpresaId equals empresa.Id
                                              where empresa.Nombre=="Mendez"
                                              select empleado;

            foreach (Empleado i in empleadosM)
            {
                i.getDatosEmpleado();
            }
        }

        public void getEmpleadosEmpresa(int Id)
        {

            
            IEnumerable<Empleado> empleadosM = from empleado in listaEmpleados
                                               join empresa in listaEmpresas
            on empleado.EmpresaId equals empresa.Id
                                               where empresa.Id==Id
                                               select empleado;

            foreach (Empleado i in empleadosM)
            {
                i.getDatosEmpleado();
            }
        }
    }

}


