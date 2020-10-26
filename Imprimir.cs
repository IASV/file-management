using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_III_2020_2_sesion_1
{
    class Imprimir
    {
        
        public static void Imp(Factura factura)
        {
            Venta venta = Venta.Search(factura.IdVenta);
            Inventario item = Inventario.Search(venta.IdItem);
            Cliente cliente = Cliente.Search(0,venta.IdClient);
            Vendedor vendedor = Vendedor.Search(0,venta.IdVendedor);
            Carro carro = Carro.Search(item.IdCar);
            Abono abono = Abono.SearchIdVenta(factura.IdVenta);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t" + "Factura".PadLeft(36).PadRight(72));
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\tNoFactura: " + factura.IdFactura.ToString().PadRight(30) + "Fecha: " + item.FechaSalida.ToShortDateString().PadRight(15));
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\tCliente");
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\tCédula: " + cliente.Cedula.ToString().PadRight(33) + "Nombre: " + cliente.Nombre.PadRight(35));
            Console.WriteLine("\tDirección: " + cliente.Direccion.PadRight(30) + "Teléfono: " + cliente.Telefono.ToString().PadRight(15));
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\tDetalles");
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\t" + "NoItem".PadRight(10) + "Descripcion".PadRight(15) + "Valor/Unidad".PadRight(15));
            Console.WriteLine("\t" + item.IdInventario.ToString().PadRight(10) + carro.Marca.PadRight(15) + item.PrecioVenta.ToString().PadRight(15));
            if(venta.formaPago == FormaPago.Credito)
            {
                Console.WriteLine("\n\tForma de pago".PadRight(18) + "Valor".PadRight(10));
                Console.WriteLine("\t" + venta.formaPago.ToString().PadRight(18) + abono.Abn.ToString().PadRight(10));
            }
            else
            {
                Console.WriteLine("\n\tForma de pago".PadRight(10));
                Console.WriteLine("\t" + venta.formaPago.ToString().PadRight(10));
            }
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\t" + "Vendedor: " + vendedor.Nombre.PadRight(35) + "SubTotal: " + item.PrecioVenta.ToString().PadRight(15));
            Console.WriteLine("\t------------------------------------------------------------------------");
            Console.WriteLine("\t" + "IVA: 19%".PadLeft(53));
            Console.WriteLine("\t" + "Total: ".PadLeft(52) + factura.precioFinal.ToString());
        }

    }//Poner un for y buscar por cédula e impimir los item asociados al cliente o comprador.
}
