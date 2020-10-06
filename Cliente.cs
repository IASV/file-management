using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    class Cliente : Persona
    {
        public static List<Cliente> ListaClientes;

        public int IdCliente { get; set; }

        public void Add()
        {
            if (ListaClientes == null)
            {
                ListaClientes = new List<Cliente>();
            }

            ListaClientes.Add(this);

            Save();
        }

        private void Save()
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter("Files/Cliente.txt", true);

            writer.WriteLine(Cedula.ToString() + "," + Nombre + "," + FechaNacimiento.ToShortDateString() + "," +
                Sexo.ToString() + "," + Telefono.ToString() + "," + Correo + "," + Direccion + "," +
                EstadoCivil.ToString() + "," + IdCliente.ToString());

            writer.Close();
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
            using (StreamWriter fileWrite = new StreamWriter("Files/temp.txt", true))
            {
                using (StreamReader fielRead = new StreamReader("Files/Cliente.txt"))
                {
                    String line;

                    while ((line = fielRead.ReadLine()) != null)
                    {
                        string[] datos = line.Split(new char[] { ',' });
                        string[] dateValues = (data.ToString()).Split('\t');
                        if (datos[0].ToString() != dateValues[0].ToString())
                        {
                            fileWrite.WriteLine(line);
                        }

                    }
                }
            }

            //aqui se renombrea el archivo temporal
            File.Delete("Files/Cliente.txt");
            File.Move("Files/temp.txt", "Files/Cliente.txt");
        }
        public static void Edit(int linea, int i, object data, string Archivo)
        {
            string[] All = File.ReadAllLines(Archivo);
            string[] Lines = (All[linea]).Split(',');
            string[] date = (data.ToString()).Split('\t');
            Lines[i] = date[i];
            string dataText = "";
            for (int j = 0; j < Lines.Length; j++)
            {
                dataText += Lines[j];
                if (j < Lines.Length) dataText += ",";
            }

            All[linea] = dataText;

            File.WriteAllLines(Archivo, All);
        }

        public static Cliente Parse(string value)
        {
            Cliente a = new Cliente();
            string[] values = value.Split('\t');

            a.Cedula = Convert.ToInt64(values[0]);

            a.Nombre = (string)values[1];

            a.FechaNacimiento = DateTime.ParseExact(values[2], "dd/MM/yyyy", null);

            a.Sexo = (Sexo)Sexo.Parse(typeof(Sexo), values[3].ToString());

            a.Telefono = Convert.ToInt64(values[4]);

            a.Correo = (string)values[5];

            a.Direccion = (string)values[6];

            a.EstadoCivil = (EstadoCivil)EstadoCivil.Parse(typeof(EstadoCivil), values[7].ToString());

            a.IdCliente = Convert.ToInt32(values[8]);

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
                            v.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);
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

                    Edit(ListaClientes.IndexOf(v), NDato - 1, v, "Files/Cliente.txt");
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        /// <summary>
        /// Muestra los datos de todos los Clientees
        /// </summary>

        public static void ToList()
        {

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
            Console.WriteLine(Cedula.ToString().PadRight(12) + Nombre.PadRight(35) + FechaNacimiento.ToShortDateString().PadLeft(12) +
                Sexo.ToString().PadRight(12).PadLeft(15) + Telefono.ToString().PadLeft(12).PadRight(15) + Correo.PadRight(40) + Direccion.PadRight(40) +
                EstadoCivil.ToString().PadRight(12) + IdCliente.ToString().PadLeft(5));
        }

        public override string ToString()
        {
            return (Cedula.ToString() + "\t" + Nombre + "\t" + FechaNacimiento.ToShortDateString() + "\t" +
                Sexo.ToString() + "\t" + Telefono.ToString() + "\t" + Correo + "\t" + Direccion + "\t" +
                EstadoCivil.ToString() + "\t" + IdCliente.ToString());
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
                    Cedula = Convert.ToInt64(value);
                    break;
                case 1:
                    Nombre = (string)value;
                    break;
                case 2:
                    FechaNacimiento = DateTime.ParseExact(value, "dd/MM/yyyy", null);
                    break;
                case 3:
                    Sexo = (Sexo)Sexo.Parse(typeof(Sexo), value.ToString());
                    break;
                case 4:
                    Telefono = Convert.ToInt64(value);
                    break;
                case 5:
                    Correo = (string)value;
                    break;
                case 6:
                    Direccion = (string)value;
                    break;
                case 7:
                    EstadoCivil = (EstadoCivil)EstadoCivil.Parse(typeof(EstadoCivil), value.ToString());
                    break;
                case 8:
                    IdCliente = Convert.ToInt32(value);
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

        public static void MenuClientes()
        {
            int option;

            LoadList();

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
                        Console.WriteLine("\n\t-- Crear clientes ---");
                        Cliente v = new Cliente();

                        Console.Write("\nCedula: ");
                        v.Cedula = Scanner.NextLong();

                        Console.Write("\nNombre: ");
                        v.Nombre = Scanner.NextLine();

                        Console.Write("\nFecha de nacimiento dd/MM/yyy: ");
                        v.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);

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

                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar clientes ---\nNúmero de cédula del clientes: ");
                        Int64 NCClientes = Scanner.NextLong();

                        if (Find(NCClientes))
                        {
                            Cliente vn = Search(NCClientes);
                            vn.Show();
                            Console.Write("\n¿Borrar cliente?\n\t1. Si.\n\t2. No.\n::");
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
