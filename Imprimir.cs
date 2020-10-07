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
            Console.WriteLine("NoFactura: " + factura.IdFactura.ToString() + "\t\t\t" + "Fecha: " + factura.Vent.Item.FechaSalida.ToString() + "\t");
            Console.WriteLine("Cliente");
            Console.WriteLine("Cédula: " + factura.Vent.Client.Cedula.ToString() + "\t" + "Nombre: " + factura.Vent.Client.Nombre);
            Console.WriteLine("Dirección: " + factura.Vent.Client.Direccion + "\t" + "Teléfono: " + factura.Vent.Client.Telefono.ToString());
            Console.WriteLine("Detalles");
            Console.WriteLine("NoItem" + "\t" + "Descripcion" + "\t" + "Valor/Unidad");
            Console.WriteLine(factura.Vent.Item.IdInventario.ToString() + "\t" + factura.Vent.Item.Car.Marca + "\t" + factura.Vent.Item.PrecioVenta.ToString() + "\t");
            Console.WriteLine();
            Console.WriteLine("Vendedor: " + factura.Vent.Vend.Nombre + "\t\t" + "SubTotal: " + factura.Vent.Item.PrecioVenta.ToString());
            Console.WriteLine("\t\t\t\t" + "IVA: 19%");
            Console.WriteLine("\t\t\t\t" + "Total: " + (factura.Vent.Item.PrecioVenta + (factura.Vent.Item.PrecioVenta*0.19)).ToString());
        }

    }
}
