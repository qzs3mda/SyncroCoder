using SyncroCoder.Models;
using System.Data.SqlClient;
using System.Text;

namespace SyncroCoder.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> GetProductoVendido(int IdUsuario)
        {
            //Producto idUsuarioproducto = new Producto();

            List<Producto> productos = ADO_Producto.GetProductoFromDatabase(IdUsuario);
            var listaIdsProducto = new List<int>();

            foreach (Producto producto in productos)
            {
                listaIdsProducto.Add(producto.id);
            }

            //Como los ids de los productos los tenia en una lista, necesitaba convertirlos a un string para pasarselos al query
            var ids = new List<int>(listaIdsProducto);
            var concatenoIds = new StringBuilder();
            foreach (var word in ids)
            {
                concatenoIds.Append(word).Append("','");
            }

            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Select * from ProductoVendido where idProducto in ('" + concatenoIds + "')";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registro = comando.ExecuteReader();

            var listaProductoVendido = new List<ProductoVendido>();

            while (registro.Read())
            {
                var productoVendido = new ProductoVendido();
                listaProductoVendido.Add(productoVendido);

                productoVendido.id = Convert.ToInt32(registro.GetValue(0));
                productoVendido.idProducto = Convert.ToInt32(registro.GetValue(1));
                productoVendido.stock = Convert.ToInt32(registro.GetValue(2));
                productoVendido.idVenta = Convert.ToInt32(registro.GetValue(3));
            }

            /*Console.WriteLine("Estas son los productos vendidos, usando los productosid que devolvio el metodo GetProducto que hace un \nselect * from producto where usuarioid = 1");
            Console.WriteLine("-------------------");

            foreach (var productosVendidos in listaProductoVendido)
            {
                Console.WriteLine("\nId: " + productosVendidos.id);
                Console.WriteLine("Id producto: " + productosVendidos.idProducto);
                Console.WriteLine("Stock: " + productosVendidos.stock);
                Console.WriteLine("Id venta: " + productosVendidos.idVenta);
                Console.WriteLine("-------------------");
            }*/

            return listaProductoVendido;
        }

        public static List<ProductoVendido> GetProductoVendidoByIdVenta(int idVenta)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Select idProducto from ProductoVendido where idVenta = @idVenta";
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@idVenta", idVenta);
            SqlDataReader registro = comando.ExecuteReader();

            var listaDeIdProducto = new List<ProductoVendido>();

            while (registro.Read())
            {
                ProductoVendido productoVendido = new ProductoVendido();
                listaDeIdProducto.Add(productoVendido);
                productoVendido.idProducto = Convert.ToInt32(registro.GetValue(0));
            }

            return listaDeIdProducto;
        }
    }
}
