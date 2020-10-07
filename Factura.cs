﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    class Factura
    {
        public static List<Factura> ListaFactura;
        public Venta Vent { get; set; }
        public long precioFinal { get; set; }
        public int IdFactura { get; set; }


        public void Add()
        {
            if (ListaFactura == null)
            {
                ListaFactura = new List<Factura>();
            }

            ListaFactura.Add(this);

            Save();
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter("Files/Factura.txt", true);

            writer.WriteLine(Vent.ToString() + "," + precioFinal.ToString() + "," + IdFactura.ToString());

            writer.Close();
        }

        public void Delete()
        {
            if (Find(this.IdFactura))
            {
                ListaFactura.Remove(this);
            }
        }
        public static void Delete(object data)
        {
            using (StreamWriter fileWrite = new StreamWriter("Files/temp.txt", true))
            {
                using (StreamReader fielRead = new StreamReader("Files/Factura.txt"))
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
            File.Delete("Files/Factura.txt");
            File.Move("Files/temp.txt", "Files/Factura.txt");
        }

        /// <summary>
        /// Muestra los datos de todos los Facturaes
        /// </summary>

        public static void ToList()
        {

            foreach (Factura v in ListaFactura)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Factura Factura in ListaFactura)
        //    {
        //        todos += Factura.ToString();
        //    }
        //    return todos;
        //}

        /// <summary>
        /// Muestra los datos de un Factura
        /// </summary>
        public void Show()
        {
            Console.WriteLine(Vent.ToString().PadRight(5) + precioFinal.ToString().PadLeft(2).PadRight(4)
                 + IdFactura.ToString().PadRight(2).PadLeft(4));
        }

        public override string ToString()
        {
            return (Vent.ToString() + "\t" + precioFinal.ToString() + "\t" + IdFactura.ToString());
        }

        public static bool Find(int IdFactura)
        {
            foreach (Factura Factura in ListaFactura)
            {
                if (Factura.IdFactura == IdFactura) return true;
            }
            return false;
        }

        public static Factura Search(int IdFactura)
        {
            foreach (Factura v in ListaFactura)
            {
                if (v.IdFactura == IdFactura)
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
                    Vent = Venta.Parse(value);
                    break;
                case 1:
                    precioFinal = Convert.ToInt64(value);
                    break;
                case 2:
                    IdFactura = Convert.ToInt32(value);
                    break;
            }
        }

        public static void LoadList()
        {
            ListaFactura = new List<Factura>();
            if (File.Exists("Files/Factura.txt"))
            {
                StreamReader reader = new StreamReader("Files/Factura.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Factura v = new Factura();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaFactura.Add(v);

                }

                reader.Close();
            }
        }

        public static void MenuFactura()
        {
            int option;

            Vendedor.LoadList();
            Cliente.LoadList();
            Carro.LoadList();
            Venta.LoadList();
            LoadList();

            do
            {
                Console.Write("\n\tBienvenido al menú de Facturas\n" +
                    "\t1. Crear Factura.\n" +
                    "\t2. Eliminar Factura.\n" +
                    "\t3. Listar Facturas.\n" +
                    "\t4. Buscar Factura.\n" +
                    "\t5. Salir.\n" +
                    "\t:: ");

                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Crear Factura ---");
                        Factura v = new Factura();

                        Console.Write("Ingrese el ID de la Venta: ");
                        int IdVenta = Scanner.NextInt();
                        if (Venta.Find(IdVenta))
                        {
                            v.Vent = Venta.Search(IdVenta);
                        }

                        v.precioFinal = v.Vent.Item.PrecioVenta;

                        if (ListaFactura.Count != 0)
                            v.IdFactura = ListaFactura.Last().IdFactura + 1;
                        else
                            v.IdFactura = 1;

                        v.Show();

                        v.Add();

                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar Factura ---\nNúmero de ID de la Factura: ");
                        int Factura = Scanner.NextInt();

                        if (Find(Factura))
                        {
                            Factura vn = Search(Factura);
                            vn.Show();
                            Console.Write("\n¿Borrar Factura?\n\t1. Si.\n\t2. No.\n:: ");
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
                        Console.WriteLine("\n\t-- Lista de Facturas ---\n");
                        ToList();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Buscar Factura ---\n");

                        Console.Write("\nIngrese el ID de la Factura.\n:: ");
                        int Idfactura = Scanner.NextInt();
                        Factura f = Search(Idfactura);
                        f.Show();
                        Imprimir.Imp(f);
                        break;
                }

            } while (option != 5);
        }

    }
}