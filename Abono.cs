using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Prog_III_2020_2_sesion_1
{
    class Abono
    {
        public static List<Abono> ListaAbonos;
        public static string path = "Files/Abono.txt";

        public int IdAbono { get; set; }
        public int IdCliente { get; set; }
        public int IdVenta { get; set; }
        public long Abn { get; set; }

        public Abono()
        {
            if (ListaAbonos.Count != 0)
                IdVenta = ListaAbonos.Last().IdAbono + 1;
            else
                IdVenta = 1;
        }

        public void Add()
        {
            if (ListaAbonos == null)
            {
                ListaAbonos = new List<Abono>();
            }

            ListaAbonos.Add(this);
            Save();
        }

        public void Save()
        {
            GestionArchivo gs = new GestionArchivo(path);
            gs.Save(GetLine());
        }

        private string GetLine()
        {
            return $"{IdAbono},{IdCliente},{IdVenta},{Abn}";
        }

        public void Delete()
        {
            if (Find(this.IdAbono))
            {
                ListaAbonos.Remove(this);
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

        public static void Update(int IdAbono, int IdVenta)
        {
            if (Find(IdAbono))
            {
                Abono v = Search(IdAbono);
                if (v != null)
                {
                    bool estadoCosto = false;
                    do
                    {
                        Abono abn = v;
                        Inventario item = Inventario.Search(Venta.Search(IdVenta).IdItem);
                        Console.Write("Ingrese la cantida a abonar\n:: ");
                        long cantidad = Scanner.NextLong();


                        if (cantidad <= item.PrecioVenta)
                        {
                            if (abn.Abn < item.PrecioVenta)
                            {
                                abn.Abn = cantidad + abn.Abn;
                                estadoCosto = true;
                                Edit(ListaAbonos.IndexOf(abn), 3, abn);
                            }
                            else
                            {
                                Console.WriteLine("\t¡Cuotas finalizadas!");
                                v.Delete();
                                Delete(v);
                            }

                        }
                        else
                        {
                            abn.Abn = 0;
                            Console.WriteLine("El abono excede el costo del valor del a pagar");
                        }

                    } while (estadoCosto);

                    
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }

        }

        public static void ToList()
        {
            Console.WriteLine("ID".PadRight(5) + "ID Cliente".PadRight(12) + 
                "ID Venta".PadRight(10) + "Abono".PadRight(7));

            foreach (Cliente v in Cliente.ListaClientes)
            {
                v.Show();
            }
        }

        public void Show()
        {
            Console.WriteLine(IdAbono.ToString().PadRight(5) + IdCliente.ToString().PadRight(12) +
                IdVenta.ToString().PadRight(10) + Abn.ToString().PadRight(7));
        }

        public override string ToString()
        {
            return (IdVenta.ToString() + "\t" + IdCliente.ToString() + "\t" + 
                IdVenta.ToString() + "\t" + Abn.ToString() + "\t" + "\n");
        }

        public static bool Find(int IdAbono)
        {
            foreach (Abono Abono in ListaAbonos)
            {
                if (Abono.IdAbono == IdAbono) return true;
            }
            return false;
        }

        public static Abono Search(int IdAbono)
        {
            foreach (Abono v in ListaAbonos)
            {
                if (v.IdAbono == IdAbono)
                {
                    return v;
                }
            }
            return null;
        }

        public static Abono SearchIdVenta(int IdVenta)
        {
            foreach (Abono v in ListaAbonos)
            {
                if (v.IdVenta == IdVenta)
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
                    IdAbono = Convert.ToInt32(value);
                    break;
                case 1:
                    IdCliente = Convert.ToInt32(value);
                    break;
                case 2:
                    IdVenta = Convert.ToInt32(value);
                    break;
                case 3:
                    Abn = Convert.ToInt64(value);
                    break;
            }
        }

        public static void LoadList()
        {
            ListaAbonos = new List<Abono>();
            if (File.Exists("Files/Abono.txt"))
            {
                StreamReader reader = new StreamReader("Files/Abono.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');
                    Abono v = new Abono();

                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaAbonos.Add(v);

                }

                reader.Close();
            }
        }

    }
}
