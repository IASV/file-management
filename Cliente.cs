using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace Prog_III_2020_2_sesion_1
{
    class Cliente : Persona
    {
        public static List<Cliente> ListaClientes;
        public static string path = "Files/Cliente.txt";

        public int IdCliente { get; set; }

        public void Add()
        {
            if (ListaClientes == null)
            {
                ListaClientes = new List<Cliente>();
            }

            ListaClientes.Add(this);
            this.Save();
        }

        public void Save()
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Save(this.GetLine());
        }

        private string GetLine()
        {
            return $"{IdCliente},{Cedula},{Nombre},{FechaNacimiento.ToShortDateString()},+" +
                $"{Sexo},{Telefono},{Correo},{Direccion},{EstadoCivil}";
        }

        public void Delete()
        {
            if (Find(this.IdCliente))
            {
                ListaClientes.Remove(this);
            }
        }

        public static void Delete(object data)
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Delete(data);
        }
        public static void Edit(int linea, int i, object data)
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Edit(linea, i, data);
        }

        public static Cliente Parse(string value)
        {
            Cliente a = new Cliente();
            string[] values = value.Split('\t');
            if (values[0] != "")
            {
                a.IdCliente = Convert.ToInt32(values[0]);

                a.Cedula = Convert.ToInt64(values[1]);

                a.Nombre = (string)values[2];

                //a.FechaNacimiento = DateTime.ParseExact(values[3], "d/M/yyyy", null);
                a.FechaNacimiento = DateTime.Parse(values[3]);

                a.Sexo = (Sexo)Sexo.Parse(typeof(Sexo), values[4].ToString());

                a.Telefono = Convert.ToInt64(values[5]);

                a.Correo = (string)values[6];

                a.Direccion = (string)values[7];

                a.EstadoCivil = (EstadoCivil)EstadoCivil.Parse(typeof(EstadoCivil), values[8].ToString());

                
            }

            return a;
        }

        public static void Update(long CedCliente, int NDato)
        {
            if (Find(CedCliente))
            {
                Cliente v = Search(CedCliente);
                if (v != null)
                {
                    switch (NDato)
                    {
                        case 1:
                            Console.Write("\nNueva Cedula: ");
                            v.Cedula = Scanner.NextLong();
                            break;
                        case 2:
                            Console.Write("\nNuevo Nombre: ");
                            v.Nombre = Scanner.NextLine();
                            break;
                        case 3:
                            Console.Write("\nNueva Fecha de nacimiento dd/MM/yyy: ");
                            //v.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);
                            v.FechaNacimiento = DateTime.Parse(Console.ReadLine());
                            break;
                        case 4:
                            Console.Write("\nNuevo Sexo\n1. Femenino.\n2. Masculino.\n:: ");
                            if (Scanner.NextInt() == 1) v.Sexo = Sexo.Femnino;
                            else v.Sexo = Sexo.Masculino;
                            break;
                        case 5:
                            Console.Write("\nNuevo Teléfono: ");
                            v.Telefono = Scanner.NextLong();
                            break;
                        case 6:
                            Console.Write("\nNuevo Correo: ");
                            v.Correo = Scanner.NextLine();
                            break;
                        case 7:
                            Console.Write("\nNueva Dirección: ");
                            v.Direccion = Scanner.NextLine();
                            break;
                        case 8:
                            Console.Write("\nNuevo Estado civil\n1. Soltero.\n2. Casado.\n3. Viudo.\n4. Divorciado.\n5. Union libre.\n:: ");
                            int estado = Scanner.NextInt();
                            for (int i = 0; i < 5; i++)
                            {
                                if (estado - 1 == i)
                                {
                                    v.EstadoCivil = (EstadoCivil)i;
                                    break;
                                };
                            }
                            break;
                    }

                    Edit(ListaClientes.IndexOf(v), NDato, v);
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        /// <summary>
        /// Muestra los datos de todos los Clientees
        /// </summary>

        public static void ToList()
        {
            Console.WriteLine("ID".PadRight(5) + "Cedula".PadRight(12) + "Nombre".PadRight(35) + "Fecha Nacimiento".PadRight(18) +
                "Sexo".PadRight(12) + "Telefono".PadRight(15) + "Correo".PadRight(40) + "Direccion".PadRight(40) +
                "Estado Civil".PadRight(12));

            foreach (Cliente v in Cliente.ListaClientes)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Cliente Cliente in ListaClientes)
        //    {
        //        todos += Cliente.ToString();
        //    }
        //    return todos;
        //}

        /// <summary>
        /// Muestra los datos de un Cliente
        /// </summary>
        public void Show()
        {
            Console.WriteLine(IdCliente.ToString().PadRight(5) + Cedula.ToString().PadRight(12) + Nombre.PadRight(35) + FechaNacimiento.ToShortDateString().PadRight(18) +
                Sexo.ToString().PadRight(12) + Telefono.ToString().PadRight(15) + Correo.PadRight(40) + Direccion.PadRight(40) +
                EstadoCivil.ToString().PadRight(12) );
        }

        public override string ToString()
        {
            return (IdCliente.ToString() + "\t" + Cedula.ToString() + "\t" + Nombre + "\t" + FechaNacimiento.ToShortDateString() + "\t" +
                Sexo.ToString() + "\t" + Telefono.ToString() + "\t" + Correo + "\t" + Direccion + "\t" +
                EstadoCivil.ToString()   + "\n");
        }

        public static bool Find(int CodCliente)
        {
            foreach (Cliente Cliente in ListaClientes)
            {
                if (Cliente.IdCliente == CodCliente) return true;
            }
            return false;
        }

        public static bool Find(long CedCliente)
        {
            foreach (Cliente Cliente in ListaClientes)
            {
                if (Cliente.Cedula == CedCliente) return true;
            }
            return false;
        }

        public static Cliente Search(long CedCliente = 0, int IdCliente = 0, long Telefono = 0)
        {
            foreach (Cliente v in ListaClientes)
            {
                if (v.Cedula == CedCliente | v.IdCliente == IdCliente | v.Telefono == Telefono)
                {
                    return v;
                }
            }
            return null;
        }

        public void SetItems(int i, string value)
        {
            switch (i)
            {
                case 0:
                    IdCliente = Convert.ToInt32(value);
                    break;
                case 1:
                    Cedula = Convert.ToInt64(value);
                    break;
                case 2:
                    Nombre = (string)value;
                    break;
                case 3:
                    //FechaNacimiento = DateTime.ParseExact(value, "d/M/yyyy", null);
                    FechaNacimiento = DateTime.Parse(value);
                    break;
                case 4:
                    Sexo = (Sexo)Sexo.Parse(typeof(Sexo), value.ToString());
                    break;
                case 5:
                    Telefono = Convert.ToInt64(value);
                    break;
                case 6:
                    Correo = (string)value;
                    break;
                case 7:
                    Direccion = (string)value;
                    break;
                case 8:
                    EstadoCivil = (EstadoCivil)EstadoCivil.Parse(typeof(EstadoCivil), value.ToString());
                    break;

            }
        }

        public static void LoadList()
        {
            ListaClientes = new List<Cliente>();
            if (File.Exists("Files/Cliente.txt"))
            {
                StreamReader reader = new StreamReader("Files/Cliente.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');
                    Cliente v = new Cliente();

                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaClientes.Add(v);
                    
                }

                reader.Close();
            }
        }

        public static void CreateCliente()
        {
            Console.WriteLine("\n\t-- Crear clientes ---");
            Cliente v = new Cliente();

            Console.Write("\nCedula: ");
            v.Cedula = Scanner.NextLong();

            Console.Write("\nNombre: ");
            v.Nombre = Scanner.NextLine();

            Console.Write("\nFecha de nacimiento MM/dd/yyy: ");
            //v.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
            v.FechaNacimiento = DateTime.Parse(Console.ReadLine());

            Console.Write("\nSexo\n1. Femenino.\n2. Masculino.\n:: ");
            if (Scanner.NextInt() == 1) v.Sexo = Sexo.Femnino;
            else v.Sexo = Sexo.Masculino;

            Console.Write("\nTeléfono: ");
            v.Telefono = Scanner.NextLong();

            Console.Write("\nCorreo: ");
            v.Correo = Scanner.NextLine();

            Console.Write("\nDirección: ");
            v.Direccion = Scanner.NextLine();

            Console.Write("\nEstado civil\n1. Soltero.\n2. Casado.\n3. Viudo.\n4. Divorciado.\n5. Union libre.\n:: ");
            int estado = Scanner.NextInt();
            for (int i = 0; i < 5; i++)
            {
                if (estado - 1 == i)
                {
                    v.EstadoCivil = (EstadoCivil)i;

                };
            }

            if (ListaClientes.Count != 0) //Si la lista no esta vacia le asigan el id del último + 1
                v.IdCliente = ListaClientes.Last().IdCliente + 1;
            else //Si la lista esta vacia lo pone como el primero
                v.IdCliente = 1;

            v.Add();
            Console.WriteLine("¡Cliente creado con exito!");
        }
        public static void MenuClientes()
        {
            int option;  

            do
            {
                Console.Write("\n\tBienvenido al menú de clienteses\n" +
                    "\t1. Crear clientes.\n" +
                    "\t2. Eliminar clientes.\n" +
                    "\t3. Editar clientes.\n" +
                    "\t4. Listar clienteses.\n" +
                    "\t5. Buscar clientes.\n" +
                    "\t6. Salir.\n" +
                    "\t:: ");

                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        
                        CreateCliente();
                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar clientes ---\nNúmero de cédula del clientes: ");
                        Int64 NCClientes = Scanner.NextLong();

                        if (Find(NCClientes))
                        {
                            Cliente vn = Search(NCClientes);
                            vn.Show();
                            Console.Write("\n¿Borrar cliente?\n\t1. Si.\n\t2. No.\n:: ");
                            if (Scanner.NextInt() == 1)
                            {
                                vn.Delete();
                                Delete(vn);
                                Console.WriteLine("\n¡Proceso realizado con éxito!");
                            }
                            else Console.WriteLine("\n¡Proceso cancelado!");

                        }

                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("\n\t--- Editar datos del cliente ---\n\nNúmero de cédula del cliente: ");
                        long NCedclientes = Scanner.NextLong();
                        Search(NCedclientes).Show();
                        Console.Write("\n\tOpciones a editar:\n" +
                           "\t1.  Cédula.\n" +
                           "\t2.  Nombre.\n" +
                           "\t3.  Fecha de nacimiento.\n" +
                           "\t4.  Sexo.\n" +
                           "\t5.  Teléfono.\n" +
                           "\t6.  Correo.\n" +
                           "\t7.  Dirección.\n" +
                           "\t8.  Estado civil.\n" +
                           "\t:: ");

                        Update(NCedclientes, Scanner.NextInt());

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Lista de clientes ---\n");
                        ToList();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Buscar cliente ---\n");
                        Console.Write("\nSeleccione el método de busqueda:\n " +
                            "\t1. Por número de cédula.\n" +
                            "\t2. Por número de identificación.\n" +
                            "\t3. Por número de teléfono.\n" +
                            "\t:: ");
                        switch (Scanner.NextInt())
                        {
                            case 1:
                                Console.Write("\n\tCédula: ");
                                Search(Scanner.NextLong()).Show();
                                break;
                            case 2:
                                Console.Write("\n\tIdentificación: ");
                                Search(0, Scanner.NextInt()).Show();
                                break;
                            case 3:
                                Console.Write("\n\tTeléfono: ");
                                Search(0, 0, Scanner.NextLong()).Show();
                                break;
                        }
                        break;
                }

            } while (option != 6);
        }

    }
}
