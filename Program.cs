using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_III_2020_2_sesion_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            int option;

            Vendedor.LoadList();
            Cliente.LoadList();
            Carro.LoadList();
            Inventario.LoadList();
            Venta.LoadList();
            Factura.LoadList();
            Abono.LoadList();

            do
            {
                Console.Clear();
                Console.Write("\n\t--- Menu principal ---\n\n" +
                    "\t1. Menú Vendedor\n" +
                    "\t2. Menú Clientes\n" +
                    "\t3. Menú Carros\n" +
                    "\t4. Menú Inventario\n" +
                    "\t5. Menú Venta\n" +
                    "\t6. Menú Factura\n" +
                    "\t7. Salir\n" +
                    "\t:: ");
                option = Scanner.NextInt();
                switch (option)
                {
                    case 1:
                        Vendedor.MenuAdminVendedor();
                        break;
                    case 2:
                        Cliente.MenuClientes();
                        break;
                    case 3:
                        Carro.MenuCarro();
                        break;
                    case 4:
                        Inventario.MenuInventario();
                        break;
                    case 5:
                        Venta.MenuVenta();
                        break;
                    case 6:
                        Factura.MenuFactura();
                        break;
                }
            } while (option != 7);
            Console.Read();
        }
    }
}
