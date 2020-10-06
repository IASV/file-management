using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    class Venta
    {
        public static List<Venta> ListaVentas;
        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public Inventario Item { get; set; }
        public int IdVenta { get; set; }

        public void Add()
        {
            if (ListaVentas == null)
            {
                ListaVentas = new List<Venta>();
            }

            ListaVentas.Add(this);

            Save();
        }

        private void Save()
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter("Files/Venta.txt", true);

            writer.WriteLine(Cliente.ToString() + "," + Vendedor.ToString() + "," + Item.ToString() + "," + IdVenta.ToString());

            writer.Close();
        }

        public void Delete()
        {
            if (Find(this.IdVenta))
            {
                ListaVentas.Remove(this);
            }
        }
        public static void Delete(object data)
        {
            using (StreamWriter fileWrite = new StreamWriter("Files/temp.txt", true))
            {
                using (StreamReader fielRead = new StreamReader("Files/Venta.txt"))
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
            File.Delete("Files/Venta.txt");
            File.Move("Files/temp.txt", "Files/Venta.txt");
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

        public static void Update(int IdVenta, int NDato)
        {
            Cliente.LoadList();
            Vendedor.LoadList();
            Inventario.LoadList();

            if (Find(IdVenta))
            {
                Venta v = Search(IdVenta);
                if (v != null)
                {
                    switch (NDato)
                    {
                        case 1:
                            
                            Console.WriteLine("\nNuevo Cliente: ");

                            v.VIN = Scanner.NextLine();
                            break;

                        case 2:
                            Console.Write("\nNueva Modelo: ");
                            v.Modelo = Scanner.NextLine();
                            break;
                        case 3:
                            Console.Write("\nNuevo Color: ");
                            v.Color = Scanner.NextLine();
                            break;
                        case 4:
                            Console.Write("\nNuevo Marca: ");
                            v.Marca = Scanner.NextLine();
                            break;

                        case 5:
                            Console.Write("\nNuevo Combustible\n1. Gasolina.\n2. Biodiésel.\n3. Gas Natural.\n4. Diésel.\n:: ");
                            int tipo = Scanner.NextInt();
                            for (int i = 0; i < 4; i++)
                            {
                                if (tipo - 1 == i)
                                {
                                    v.TipoCombustible = (Combustible)i;
                                    break;
                                };
                            }
                            break;
                        case 6:
                            Console.Write("\nNueva Transmisión\n1. Manual.\n2. Automatica.\n3. CVT.\n:: ");
                            int transmision = Scanner.NextInt();
                            for (int i = 0; i < 3; i++)
                            {
                                if (transmision - 1 == i)
                                {
                                    v.TipoTransmision = (Transmision)i;
                                    break;
                                };
                            }
                            break;

                    }

                    Edit(ListaVentas.IndexOf(v), NDato - 1, v, "Files/Venta.txt");
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        /// <summary>
        /// Muestra los datos de todos los Ventaes
        /// </summary>

        public static void ToList()
        {

            foreach (Venta v in Venta.ListaVentas)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Venta Venta in ListaVentas)
        //    {
        //        todos += Venta.ToString();
        //    }
        //    return todos;
        //}

        /// <summary>
        /// Muestra los datos de un Venta
        /// </summary>
        public void Show()
        {
            Console.WriteLine(Cliente.ToString().PadRight(5) + Vendedor.ToString().PadLeft(2).PadRight(4) 
                + Item.ToString().PadRight(10) + IdVenta.ToString().PadRight(2).PadLeft(4));
        }

        public override string ToString()
        {
            return (Cliente.ToString() + "\t" + Vendedor.ToString() + "\t" + Item.ToString() + "\t" + IdVenta.ToString());
        }

        public static bool Find(int IdVenta)
        {
            foreach (Venta Venta in ListaVentas)
            {
                if (Venta.IdVenta == IdVenta) return true;
            }
            return false;
        }

        public static Venta Search(int IdVenta)
        {
            foreach (Venta v in ListaVentas)
            {
                if (v.IdVenta == IdVenta)
                {
                    return v;
                }
            }
            return null;
        }

        public static Venta Search(Cliente cliente)
        {
            foreach (Venta v in ListaVentas)
            {
                if (v.Cliente == cliente)
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
                    Cliente = Cliente.Parse(value);
                    break;
                case 1:
                    Vendedor = Vendedor.Parse(value);
                    break;
                case 2:
                    Item = Inventario.Parse(value);
                    break;
                case 3:
                    IdVenta = Convert.ToInt32(value);
                    break;
            }
        }

        public static void LoadList()
        {
            ListaVentas = new List<Venta>();
            if (File.Exists("Files/Venta.txt"))
            {
                StreamReader reader = new StreamReader("Files/Venta.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Venta v = new Venta();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaVentas.Add(v);

                }

                reader.Close();
            }
        }

        public static void MenuVenta()
        {
            int option;

            LoadList();
            Vendedor.LoadList();
            Cliente.LoadList();
            Carro.LoadList();
            Inventario.LoadList();

            do
            {
                Console.Write("\n\tBienvenido al menú de Ventas\n" +
                    "\t1. Crear Venta.\n" +
                    "\t2. Eliminar Venta.\n" +
                    "\t3. Editar Venta.\n" +
                    "\t4. Listar Ventas.\n" +
                    "\t5. Buscar Venta.\n" +
                    "\t6. Salir.\n" +
                    "\t:: ");

                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Crear Venta ---");
                        Venta v = new Venta();

                        Console.Write("Ingrese cédula del cliente: ");
                        long cedCliente = Scanner.NextLong();
                        if (Cliente.Find(cedCliente))
                        {
                            v.Cliente = Cliente.Search(cedCliente);
                        }
                        else
                        {
                            Console.WriteLine("Cliente no encontrado o no registrado.\nIngrese al menú crear cliente.");
                            Cliente.MenuClientes();
                            if (Cliente.Find(cedCliente))
                            {
                                v.Cliente = Cliente.Search(cedCliente);
                            }
                            else Console.WriteLine("¡Ooops, Cliente no creado!");
                        }

                        Console.Write("Ingrese cédula del vendedor que realizo la venta\n:: ");
                        long cedVendedor = Scanner.NextLong();

                        if (Vendedor.Find(cedVendedor))
                        {
                            v.Vendedor = Vendedor.Search(cedVendedor);
                        }
                        else Console.WriteLine("¡Ooops, Vendedor no encontrado!");

                        Inventario.CarsShowItems();
                        int IdCar = Scanner.NextInt();

                        Inventario item = Inventario.SearchForCar(IdCar);

                        if (Inventario.Find(item.IdInventario))
                        {
                            v.Item = item;
                        }

                        Console.Write("Ingrese fecha de venta. dd/MM/yyyy\n:: ");
                        v.Item.FechaSalida = DateTime.Parse(Scanner.NextLine());

                        if (ListaVentas.Count != 0)
                            v.IdVenta = ListaVentas.Last().IdVenta + 1;
                        else
                            v.IdVenta = 1;

                        v.Show();

                        //if (!itemExistValues(v))
                        v.Add();

                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar Item ---\nNúmero de ID del Item: ");
                        int Item = Scanner.NextInt();

                        if (Find(Item))
                        {
                            Inventario vn = Search(Item);
                            vn.Show();
                            Console.Write("\n¿Borrar Carro?\n\t1. Si.\n\t2. No.\n:: ");
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
                        Console.Write("\n\t--- Editar datos del Item ---\nNúmero de ID del Item: ");
                        int IdItem = Scanner.NextInt();
                        Search(IdItem).Show();
                        Console.Write("\n\tOpciones a editar:\n" +
                           "\t1.  Cantidad.\n" +
                           "\t2.  Precio Base.\n" +
                           "\t3.  Precio de Venta.\n" +
                           "\t4.  Fecha de ingreso.\n" +
                           "\t5.  Fecha de salida.\n" +
                           "\t:: ");

                        Update(IdItem, Scanner.NextInt());

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Lista de Items ---\n");
                        ToList();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Buscar Item ---\n");

                        Console.Write("\nIngrese el ID del Item.\n:: ");
                        Search(Scanner.NextInt()).Show();
                        break;
                }

            } while (option != 6);
        }

    }
}
