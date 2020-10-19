using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Prog_III_2020_2_sesion_1
{
    public class Vendedor : Persona
    {
        /// <summary>
        /// static
        /// </summary>
        public static List<Vendedor> ListaVendedor;

        

        public int IdVendedor
        {
            get;
            set;
        }

        public DateTime FechaIngreso
        {
            get;
            set;
        }

        public int Salario
        {
            get;
            set;
        }

        public string Profesion
        {
            get;
            set;
        }

        public int Calificacion
        {
            get;
            set;
        }



        public void Add()
        {
            if (ListaVendedor==null)
            {
                ListaVendedor = new List<Vendedor>();
            }

            ListaVendedor.Add(this);

            Save();
        }

        private void Save()
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter("Files/Vendedor.txt", true);

            writer.WriteLine(Cedula.ToString() + "," + Nombre + "," + FechaNacimiento.ToShortDateString() + "," +
                Sexo.ToString() + "," + Telefono.ToString() + "," + Correo + "," + Direccion + "," +
                EstadoCivil.ToString() + "," + IdVendedor.ToString() + "," + FechaIngreso.ToShortDateString() + "," + 
                Salario.ToString() + "," + Profesion + "," + Calificacion.ToString());

            writer.Close();
        }

        public void Delete()
        {
            if (Find(this.IdVendedor))
            {
                ListaVendedor.Remove(this);
            }
        }

        public static void Delete(object data)
        {
            using (StreamWriter fileWrite = new StreamWriter("Files/temp.txt",true))
            {
                using (StreamReader fielRead = new StreamReader("Files/Vendedor.txt"))
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
            File.Delete("Files/Vendedor.txt");
            File.Move("Files/temp.txt", "Files/Vendedor.txt");
        }
        public static void Edit(int linea, int i, object data, string Archivo)
        {
            string[] All = File.ReadAllLines(Archivo);
            string[] Lines = (All[linea]).Split(',');
            string[] date = (data.ToString()).Split('\t');

            /*int calificacion = Convert.ToInt32(date[12]);
            date[12] = calificacion.ToString();*/

            Lines[i] = date[i];
            string dataText = "";
            for (int j = 0; j < Lines.Length; j++)
            {
                if(Lines[j] != "\n")
                    dataText += Lines[j];
                if (j < Lines.Length-1) 
                    dataText += ",";
            }

            All[linea] = dataText;

            File.WriteAllLines(Archivo, All);
        }

        public static Vendedor Parse(string value)
        {
            Vendedor a = new Vendedor();
            String[] values = value.Split('\t');

            if (values[0] != "")
            {
                a.Cedula = Convert.ToInt64(values[0]);

                a.Nombre = (string)values[1];

                //a.FechaNacimiento = DateTime.ParseExact(values[2], "d/MM/yyyy", null);
                a.FechaNacimiento = DateTime.Parse(values[2]);
                a.Sexo = (Sexo)Sexo.Parse(typeof(Sexo), values[3].ToString());

                a.Telefono = Convert.ToInt64(values[4]);

                a.Correo = (string)values[5];

                a.Direccion = (string)values[6];

                a.EstadoCivil = (EstadoCivil)EstadoCivil.Parse(typeof(EstadoCivil), values[7].ToString());

                a.IdVendedor = Convert.ToInt32(values[8]);

                //a.FechaIngreso = DateTime.ParseExact(values[9], "d/MM/yyyy", null);
                a.FechaIngreso = DateTime.Parse(values[9]);
                a.Salario = Convert.ToInt32(values[10]);

                a.Profesion = values[11].ToString();

                a.Calificacion = Convert.ToInt32(values[12]);
            }
            return a;
        }

        public static void printVector(string[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                Console.WriteLine(v[i]);
            }
        }

        public static void Update(long CedVendedor, int NDato)
        {
            if (Find(CedVendedor))
            {
                Vendedor v = Search(CedVendedor);

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
                            Console.Write("\nNueva Fecha de nacimiento dd/MM/yyy");
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
                        case 9:
                            NDato = 10;
                            Console.Write("\nNueva Fecha de ingreso dd/MM/yyy");
                            v.FechaIngreso = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);
                            break;
                        case 10:
                            NDato = 11;
                            Console.Write("\nNuevo Salario: ");
                            v.Salario = Scanner.NextInt();
                            break;
                        case 11:
                            NDato = 12;
                            Console.Write("\nNueva Profesión: ");
                            v.Profesion = Scanner.NextLine();
                            break;
                        case 12:
                            NDato = 13;
                            Console.Write("\nNueva Calificación de 1 a 10: ");
                            v.Calificacion = Scanner.NextInt();
                            break;
                    }

                    Edit(ListaVendedor.IndexOf(v), NDato - 1, v, "Files/Vendedor.txt");
                }
                else Console.WriteLine("¡Oooops, A ocurrido un erro!");
            }
            
        }

        /// <summary>
        /// Muestra los datos de todos los vendedores
        /// </summary>
        
        public static void ToList()
        {

            foreach (Vendedor v in Vendedor.ListaVendedor)
            {
                v.Show();
            }
        }

        //public string List()
        //{
        //    string todos = "";
        //    foreach (Vendedor vendedor in ListaVendedor)
        //    {
        //        todos += vendedor.ToString();
        //    }
        //    return todos;
        //}
        /// <summary>
        /// Muestra los datos de un vendedor
        /// </summary>
        public void Show()
        {
            Console.WriteLine(Cedula.ToString().PadRight(12)+ Nombre.PadRight(35)+ FechaNacimiento.ToShortDateString().PadLeft(12)+
                Sexo.ToString().PadRight(12).PadLeft(15)+ Telefono.ToString().PadLeft(12).PadRight(15)+ Correo.PadRight(40)+ Direccion.PadRight(40)+
                EstadoCivil.ToString().PadRight(12) + IdVendedor.ToString().PadLeft(5)+ FechaIngreso.ToShortDateString().PadLeft(12)+
                Salario.ToString().PadLeft(10)+ Profesion.PadLeft(20).PadRight(17)+ Calificacion.ToString().PadLeft(5));
        }

        public override string ToString()
        {
            return (Cedula.ToString() + "\t" + Nombre + "\t" + FechaNacimiento.ToShortDateString() + "\t" +
                Sexo.ToString() + "\t" + Telefono.ToString() + "\t" + Correo + "\t" + Direccion + "\t" +
                EstadoCivil.ToString() + "\t" + IdVendedor.ToString() + "\t" + FechaIngreso.ToShortDateString() + "\t" +
                Salario.ToString() + "\t" + Profesion + "\t" + Calificacion.ToString());
        }

        public static bool Find(int CodVendedor)
        {
            foreach (Vendedor vendedor in ListaVendedor)
            {
                if (vendedor.IdVendedor == CodVendedor) return true;
            }
            return false;
        }

        public static bool Find(long CedVendedor)
        {
            foreach (Vendedor vendedor in ListaVendedor)
            {
                if (vendedor.Cedula == CedVendedor) return true;
            }
            return false;
        }

        public static Vendedor Search(long CedVendedor = 0, int IdVendedor = 0, int Telefono = 0)
        {
            foreach (Vendedor v in ListaVendedor)
            {           
                if (v.Cedula == CedVendedor | v.IdVendedor == IdVendedor | v.Telefono == Telefono)
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
                    FechaNacimiento = DateTime.ParseExact(value, "d/MM/yyyy", null);
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
                    IdVendedor = Convert.ToInt32(value);
                    break;
                case 9:
                    FechaIngreso = DateTime.ParseExact(value, "d/MM/yyyy", null);
                    break;
                case 10:
                    Salario = Convert.ToInt32(value);
                    break;
                case 11:
                    Profesion = value.ToString();
                    break;
                case 12:
                    Calificacion = Convert.ToInt32(value);
                    break;
            }
        }

        public static void LoadList()
        {
            ListaVendedor = new List<Vendedor>();
            if (File.Exists("Files/Vendedor.txt"))
            {
                StreamReader reader = new StreamReader("Files/Vendedor.txt");

                while (!reader.EndOfStream)
                {
                    string[] var = reader.ReadLine().Split(',');

                    Vendedor v = new Vendedor();
                    for (int i = 0; i < var.Length; i++)
                    {
                        v.SetItems(i, var[i]);
                    }

                    ListaVendedor.Add(v);

                }

                reader.Close();
            }
        }

        public static void MenuAdminVendedor()
        {
            int option;
            
            do
            {
                Console.Write("\n\tBienvenido al menú de Vendedores\n" +
                    "\t1. Crear vendedor.\n" +
                    "\t2. Eliminar vendedor.\n" +
                    "\t3. Editar vendedor.\n" +
                    "\t4. Listar vendedores.\n" +
                    "\t5. Buscar vendedor.\n" +
                    "\t6. Salir.\n" +
                    "\t:: ");
                
                option = Scanner.NextInt();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Crear vendedor ---");
                        Vendedor v = new Vendedor();

                        Console.Write("\nCedula: ");
                        v.Cedula = Scanner.NextLong();

                        Console.Write("\nNombre: ");
                        v.Nombre = Scanner.NextLine();

                        Console.Write("\nFecha de nacimiento dd/MM/yyy: ");
                        v.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

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
                                break;
                            };
                        }

                        

                        Console.Write("\nFecha de ingreso dd/MM/yyy: ");
                        v.FechaIngreso = DateTime.ParseExact(Console.ReadLine(), "d/MM/yyyy", null);

                        Console.Write("\nSalario: ");
                        v.Salario = Scanner.NextInt();

                        Console.Write("\nProfesión: ");
                        v.Profesion = Scanner.NextLine();

                        Console.Write("\nCalificación de 1 a 10: ");
                        v.Calificacion = Scanner.NextInt();

                        if (ListaVendedor.Count != 0)
                            v.IdVendedor = ListaVendedor.Last().IdVendedor + 1;
                        else
                            v.IdVendedor = 1;
                        

                        v.Add();

                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("\n\t--- Eliminar vendedor ---\n\nNúmero de cédula del Vendedor: ");
                        Int64 NCVendedor = Scanner.NextLong();

                        if (Find(NCVendedor))
                        {
                            Vendedor vn = Search(NCVendedor);
                            Console.WriteLine();
                            vn.Show();
                            Console.Write("\n¿Borrar Vendedor?\n\t1. Si.\n\t2. No.\n:: ");
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
                        Console.Write("\n\t--- Editar datos del Vendedor ---\nNúmero de cédula del Vendedor: ");
                        Int64 NCedVendedor = Scanner.NextLong();
                        Search(NCedVendedor).Show();
                        Console.Write("\n\tOpciones a editar:\n" +
                           "\t1.  Cédula.\n" +
                           "\t2.  Nombre.\n" +
                           "\t3.  Fecha de nacimiento.\n" +
                           "\t4.  Sexo.\n" +
                           "\t5.  Teléfono.\n" +
                           "\t6.  Correo.\n" +
                           "\t7.  Dirección.\n" +
                           "\t8.  Estado civil.\n" +
                           "\t9.  Fecha de ingreso.\n" +
                           "\t10. Salario.\n" +
                           "\t11. Profesión.\n" +
                           "\t12. Calificación.\n" +
                           "\t:: ");

                        Update(NCedVendedor, Scanner.NextInt());

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Lista de vendedores ---\n");
                        ToList();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("\n\t-- Buscar vendedor ---");
                        Console.Write("\nSeleccione el método de busqueda\n" +
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
                                Search(0, 0, Scanner.NextInt()).Show();
                                break;
                        }
                        break;
                        
                }

            } while (option != 6);
        }
    }
}