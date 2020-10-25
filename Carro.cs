using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    class Carro
    {
        public static List<Carro> ListaCarros;
        public static string path = "Files/Carro.txt";

        public int IdCarro { get; set; }
        public string VIN { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public Combustible TipoCombustible { get; set; }
        public Transmision TipoTransmision { get; set; }
        

        public void Add()
        {
            if (ListaCarros == null)
            {
                ListaCarros = new List<Carro>();
            }

            ListaCarros.Add(this);

            Save();
        }

        private void Save()
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Save(this.GetLine());
        }

        private string GetLine()
        {
            return $"{IdCarro},{VIN},{Modelo},{Color},{Marca}," +
                $"{TipoCombustible},{TipoTransmision}";
        }

        public void Delete()
        {
            if (Find(this.IdCarro))
            {
                ListaCarros.Remove(this);
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

        public static Carro Parse(string value)
        {
            Carro a = new Carro();
            string[] values = value.Split('\t');

            a.IdCarro = Convert.ToInt32(values[0]);

            a.VIN = (string)values[1];

            a.Modelo = (string)values[2];

            a.Color = (string)values[3];

            a.Marca = (string)values[4];

            a.TipoCombustible = (Combustible)Combustible.Parse(typeof(Combustible), values[5].ToString());

            a.TipoTransmision = (Transmision)Transmision.Parse(typeof(Transmision), values[6].ToString());

            return a;
        }

        public static void Update(int IdCarro, int NDato)
        {
            if (Find(IdCarro))
            {
                Carro v = Search(IdCarro);
                if (v != null)
                {
                    switch (NDato)
                    {
                        case 1:
                            Console.WriteLine("\nNuevo Número de Chasis: ");
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

                    Edit(ListaCarros.IndexOf(v), NDato, v);
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        /// <summary>
        /// Muestra los datos de todos los Carroes
        /// </summary>

        public static void ToList()
        {
            Console.WriteLine("ID".PadRight(5) + "VIN".PadRight(10) + "Modelo".PadRight(10) + "Color".PadRight(10) + "Marca".PadRight(10) +
                "Combustible".PadRight(15) + "Transmisión".PadRight(15));

            foreach (Carro v in Carro.ListaCarros)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Carro Carro in ListaCarros)
        //    {
        //        todos += Carro.ToString();
        //    }
        //    return todos;
        //}

        /// <summary>
        /// Muestra los datos de un Carro
        /// </summary>
        public void Show()
        {
            Console.WriteLine(IdCarro.ToString().PadRight(5) + VIN.PadRight(10) + Modelo.PadRight(10) + Color.PadRight(10) + Marca.PadRight(10) + 
                TipoCombustible.ToString().PadRight(15) + TipoTransmision.ToString().PadRight(15));
        }

        public override string ToString()
        {
            return (IdCarro.ToString() + "\t" + VIN.ToString() + "\t" + Modelo + "\t" + Color + "\t" + Marca + "\t" +
                TipoCombustible.ToString() + "\t" + TipoTransmision.ToString());
        }

        public static bool Find(int IdCarro)
        {
            foreach (Carro Carro in ListaCarros)
            {
                if (Carro.IdCarro == IdCarro) return true;
            }
            return false;
        }

        public static bool Find(string VIN)
        {
            foreach (Carro Carro in ListaCarros)
            {
                if (Carro.VIN.ToLower() == VIN.ToLower()) return true;
            }
            return false;
        }

        public static Carro Search(int IdCarro)
        {
            foreach (Carro v in ListaCarros)
            {
                if (v.IdCarro == IdCarro)
                {
                    return v;
                }
            }
            return null;
        }

        public static Carro Search(string VIN)
        {
            foreach (Carro v in ListaCarros)
            {
                if (v.VIN.ToLower() == VIN.ToLower())
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
                    IdCarro = Convert.ToInt32(value);
                    break;
                case 1:
                    VIN = (string)value;
                    break;
                case 2:
                    Modelo = (string)value;
                    break;
                case 3:
                    Color = (string)value;
                    break;
                case 4:
                    Marca = (string)value;
                    break;
                case 5:
                    TipoCombustible = (Combustible)Combustible.Parse(typeof(Combustible), value.ToString());
                    break;
                case 6:
                    TipoTransmision = (Transmision)Transmision.Parse(typeof(Transmision), value.ToString());
                    break;

            }
        }

        public static void LoadList()
        {
            ListaCarros = new List<Carro>();
            if (File.Exists("Files/Carro.txt"))
            {
                StreamReader reader = new StreamReader("Files/Carro.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Carro v = new Carro();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaCarros.Add(v);

                }

                reader.Close();
            }
        }

        public static void MenuCarro()
        {
            int option;

            do
            {
                Console.Write("\n\tBienvenido al menú de Carros\n" +
                    "\t1. Crear Carro.\n" +
                    "\t2. Eliminar Carro.\n" +
                    "\t3. Editar Carro.\n" +
                    "\t4. Listar Carros.\n" +
                    "\t5. Buscar Carro.\n" +
                    "\t6. Salir.\n" +
                    "\t:: ");

                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Crear Carro ---");
                        Carro v = new Carro();

                        Console.Write("\nCódigo de chasis: ");
                        v.VIN = Scanner.NextLine();

                        Console.Write("\nModelo: ");
                        v.Modelo = Scanner.NextLine();

                        Console.Write("\nColor: ");
                        v.Color = Scanner.NextLine();

                        Console.Write("\nMarca: ");
                        v.Marca = Scanner.NextLine();

                        Console.Write("\nCombustible\n1. Gasolina.\n2. Biodiésel.\n3. Gas Natural.\n4. Diésel.\n:: ");
                        int tipo = Scanner.NextInt();
                        for (int i = 0; i < 4; i++)
                        {
                            if (tipo - 1 == i)
                            {
                                v.TipoCombustible = (Combustible)i;
                                break;
                            };
                        }

                        Console.Write("\nTransmisión\n1. Manual.\n2. Automatica.\n3. CVT.\n:: ");
                        int transmision = Scanner.NextInt();
                        for (int i = 0; i < 3; i++)
                        {
                            if (transmision - 1 == i)
                            {
                                v.TipoTransmision = (Transmision)i;
                            };
                        }

                        if (ListaCarros.Count != 0)
                            v.IdCarro = ListaCarros.Last().IdCarro + 1;
                        else
                            v.IdCarro = 1;

                        v.Show();
                        v.Add();
                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar Carro ---\nCódigo de chasis del Carro: ");
                        string VIN = Scanner.NextLine();

                        if (Find(VIN))
                        {
                            Carro vn = Search(VIN);
                            vn.Show();
                            Console.Write("\n¿Borrar Carro?\n\t1. Si.\n\t2. No.\n::");
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
                        Console.Write("\n\t--- Editar datos del Carro ---\nNúmero de ID del Carro: ");
                        int IdCarro  = Scanner.NextInt();
                        Search(IdCarro).Show();
                        Console.Write("\n\tOpciones a editar:\n" +
                           "\t1.  VIN.\n" +
                           "\t2.  Modelo.\n" +
                           "\t3.  Color.\n" +
                           "\t4.  Marca.\n" +
                           "\t5.  Combustible.\n" +
                           "\t6.  Transamisión.\n" +
                           "\t:: ");

                        Update(IdCarro, Scanner.NextInt());

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Lista de Carro ---\n");
                        ToList();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Buscar Carro ---\n");

                        Console.Write("\nIngrese el ID del carro.\n:: ");
                        Search(Scanner.NextInt()).Show();
                        break;
                }

            } while (option != 6);
        }

    }

}
