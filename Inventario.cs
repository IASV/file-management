using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    class Inventario
    {
        public static List<Inventario> ListaInventario;
        public static string path = "Files/Inventario.txt";
        DateTime fechaSalida = DateTime.Parse("01/01/0001");

        public int IdInventario { get; set; }
        public int IdCar { get; set; }       
        public int Cantidad { get; set; }
        public long PrecioBase { get; set; }
        public long PrecioVenta { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get => fechaSalida; set => fechaSalida = value; }

        public void Add()
        {
            if (ListaInventario == null)
            {
                ListaInventario = new List<Inventario>();
            }

            ListaInventario.Add(this);

            Save();
        }

        private void Save()
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Save(this.GetLine());
        }

        private string GetLine()
        {
            return $"{IdInventario},{IdCar},{Cantidad}," +
                $"{PrecioBase},{PrecioVenta},{FechaIngreso.ToShortDateString()},{FechaSalida.ToShortDateString()}";
        }

        public void Delete()
        {
            if (Find(this.IdInventario))
            {
                ListaInventario.Remove(this);
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
        public static Inventario Parse(string value)
        {
            Inventario a = new Inventario();
            string[] values = value.Split('\t');
            //string car = "";
            //for (int i = 1; i < 8; i++)
            //{
            //    car += values[i];
            //    if (i < 8) car += "\t";
            //}

            //a.Car = Carro.Parse(car);
            a.IdInventario = Convert.ToInt32(values[0]);

            a.IdCar = value[1];

            a.Cantidad = Convert.ToInt32(values[2]);

            a.PrecioBase = Convert.ToInt64(values[3]);

            a.PrecioVenta = Convert.ToInt64(values[4]);

            //FechaIngreso = DateTime.ParseExact(value, "dd/MM/yyyy", null);
            a.FechaIngreso = DateTime.Parse(values[5]);

            //FechaSalida = DateTime.ParseExact(value, "dd/MM/yyyy", null);
            a.FechaSalida = DateTime.Parse(values[6]);

            return a;
        }
        public static void CarsShowItems()
        {
            Console.WriteLine("ID".PadRight(5) + "VIN".PadRight(10) + "Modelo".PadRight(10) + "Color".PadRight(10) + "Marca".PadRight(10) +
                "Combustible".PadRight(15) + "Transmisión".PadRight(15));
            foreach (Inventario item in ListaInventario)
            {
                Carro.Search(item.IdCar).Show();
            }
        }
        public static void Update(int IdInventario, int NDato)
        {
            if (Find(IdInventario))
            {
                Inventario v = Search(IdInventario);
                if (v != null)
                {
                    switch (NDato)
                    {
                        case 1:
                            Console.WriteLine("\nNueva Cantidad: ");
                            v.Cantidad = Scanner.NextInt();
                            break;

                        case 2:
                            Console.Write("\nNuevo Precio Base: ");
                            v.PrecioBase = Scanner.NextLong();
                            break;
                        case 3:
                            Console.Write("\nNuevo Precio de venta: ");
                            v.PrecioVenta = Scanner.NextLong();
                            break;
                        case 4:
                            Console.Write("\nFecha de ingreso dd/MM/yyy: ");
                            v.FechaIngreso = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);
                            break;

                        case 5:
                            Console.Write("\nFecha de Salida dd/MM/yyy: ");
                            v.FechaSalida = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);
                            break;

                    }

                    Edit(ListaInventario.IndexOf(v), NDato+1, v);
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        /// <summary>
        /// Muestra los datos de todos los Inventarioes
        /// </summary>

        public static void ToList()
        {
            Console.WriteLine("ID".PadRight(5) + "ID CAR".PadRight(10) + "Nº".PadRight(5) + "Precio Base".PadRight(15) + 
                "Precio Venta".PadRight(15) + "Fecha Ingreso".PadRight(15) );

            foreach (Inventario v in Inventario.ListaInventario)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Inventario Inventario in ListaInventario)
        //    {
        //        todos += Inventario.ToString();
        //    }
        //    return todos;
        //}

        /// <summary>
        /// Muestra los datos de un Inventario
        /// </summary>
        public void Show()
        {
            Console.WriteLine(IdInventario.ToString().PadRight(5) + IdCar.ToString().PadRight(10) + Cantidad.ToString().PadRight(5) + PrecioBase.ToString().PadRight(15) + 
                PrecioVenta.ToString().PadRight(15) + FechaIngreso.ToShortDateString().ToString().PadRight(15) );
           
        }

        public override string ToString()
        {
            return (IdInventario.ToString() + "\t" + IdCar.ToString() + "\t" + Cantidad.ToString() + "\t" + PrecioBase.ToString() + "\t" +
                PrecioVenta.ToString() + "\t" + FechaIngreso.ToString() + "\t" + FechaSalida.ToString());
        }

        public static bool Find(int IdInventario)
        {
            foreach (Inventario Inventario in ListaInventario)
            {
                if (Inventario.IdInventario == IdInventario) return true;
            }
            return false;
        }

        public static bool itemExistValues(Inventario v)
        {
            foreach (Inventario item in ListaInventario)
            {
                if(item.IdCar == v.IdCar)
                {
                    if(item.PrecioBase == v.PrecioBase && item.PrecioVenta == v.PrecioVenta && item.FechaIngreso == v.FechaIngreso && item.FechaSalida == v.FechaSalida)
                    {
                        item.Cantidad++;
                        return true;
                    }
                }
            }
            return false;
        }

        public static Inventario Search(int IdInventario)
        {
            foreach (Inventario v in ListaInventario)
            {
                if (v.IdInventario == IdInventario)
                {
                    return v;
                }
            }
            return null;
        }

        public static Inventario SearchForCar(int IdCar)
        {
            foreach (Inventario item in ListaInventario)
            {
                if (item.IdCar == IdCar)
                {
                    return item;
                }
            }
            return null;
        }

        public void SetItems(int i, string value)
        {
            switch (i)
            {
                case 0:
                    IdInventario = Convert.ToInt32(value);
                    break;
                case 1:
                    IdCar = Convert.ToInt32(value);
                    break;
                case 2:
                    Cantidad = Convert.ToInt32(value);
                    break;
                case 3:
                    PrecioBase = Convert.ToInt64(value);
                    break;
                case 4:
                    PrecioVenta = Convert.ToInt64(value);
                    break;
                case 5:
                    //FechaIngreso = DateTime.ParseExact(value, "dd/MM/yyyy", null);
                    FechaIngreso = DateTime.Parse(value);
                    break;
                case 6:
                    //FechaSalida = DateTime.ParseExact(value, "dd/MM/yyyy", null);
                    FechaSalida = DateTime.Parse(value);
                    break;


            }
        }

        public static void LoadList()
        {
            ListaInventario = new List<Inventario>();
            if (File.Exists("Files/Inventario.txt"))
            {
                StreamReader reader = new StreamReader("Files/Inventario.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Inventario v = new Inventario();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaInventario.Add(v);

                }

                reader.Close();
            }
        }

        public static void MenuInventario()
        {
            int option;

            do
            {
                Console.Write("\n\tBienvenido al menú de Inventario\n" +
                    "\t1. Crear Item.\n" +
                    "\t2. Eliminar Item.\n" +
                    "\t3. Editar Item.\n" +
                    "\t4. Listar Item.\n" +
                    "\t5. Buscar Item.\n" +
                    "\t6. Salir.\n" +
                    "\t:: ");

                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Crear Item ---");
                        Inventario v = new Inventario();

                        Carro.LoadList();
                        Console.WriteLine("\t--- Elija un carro para el Item ---");
                        Console.WriteLine("\t--- Lista de carros ---");
                        Carro.ToList();
                        Console.Write("\t Ingrese ID del carro\n:: ");
                        v.IdCar = Scanner.NextInt();

                        Console.Write("\nCantidad: ");
                        v.Cantidad = Scanner.NextInt();

                        Console.Write("\nPrecio Base: ");
                        v.PrecioBase = Scanner.NextLong();

                        Console.Write("\nPrecio de Venta: ");
                        v.PrecioVenta = Scanner.NextLong();

                        Console.Write("\nFecha de ingreso: ");
                        //v.FechaIngreso = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        v.FechaIngreso = DateTime.Parse(Console.ReadLine());

                        //Console.Write("\nFecha de salida: ");
                        //v.FechaSalida = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                        //v.fechaSalida = DateTime.Parse(Console.ReadLine());

                        if (ListaInventario.Count != 0)
                            v.IdInventario = ListaInventario.Last().IdInventario + 1;
                        else
                            v.IdInventario = 1;

                        v.Show();

                        if (!itemExistValues(v))
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
