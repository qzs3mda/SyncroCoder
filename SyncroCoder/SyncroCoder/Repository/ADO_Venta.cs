using Microsoft.Extensions.Hosting;
using SyncroCoder.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace SyncroCoder.Repository
{
    public class ADO_Venta
    {
        public static List<Venta> GetVenta()
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "select * from Venta";
            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registro = comando.ExecuteReader();

            var listaVenta = new List<Venta>();

            while (registro.Read())
            {
                var venta = new Venta();
                listaVenta.Add(venta);

                venta.id = Convert.ToInt32(registro.GetValue(0));
                venta.comentarios = registro.GetValue(1).ToString();
                venta.idUsuario = Convert.ToInt32(registro.GetValue(2));
            }

            Console.WriteLine("Estas son las ventas: ");
            Console.WriteLine("-------------------");

            foreach (var venta in listaVenta)
            {
                Console.WriteLine("\nId venta: " + venta.id);
                Console.WriteLine("Comentarios: " + venta.comentarios);
                Console.WriteLine("Id usuario: " + venta.idUsuario);
                Console.WriteLine("-------------------");
            }

            conexion.Close();

            return listaVenta;
        }

        public static void CrearVenta(List <Producto> productos, string comentarios, int idUsuario)
        {
            Venta venta = new Venta();

            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Insert into Venta (comentarios, idUsuario) values (@comentarios, @idUsuario); select @@Identity";
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@comentarios", comentarios);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);

            int ventaId = 0;

            ventaId = Convert.ToInt32(comando.ExecuteScalar());


            foreach (Producto idProductos in productos)
            {
                query = "Insert into ProductoVendido (idProducto, Stock, idVenta) values (@idProducto, @Stock, @idVenta)";
                comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@idProducto", idProductos.id);
                comando.Parameters.AddWithValue("@Stock", idProductos.stock);
                comando.Parameters.AddWithValue("@idVenta", ventaId);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();

                
                SqlCommand comando2 = new SqlCommand();
                string UpdateProducto = string.Empty;
                UpdateProducto = "Update Producto set Stock = Stock - @Stock Where id = @id";
                comando2 = new SqlCommand(UpdateProducto, conexion);

                comando2.Parameters.AddWithValue("@Stock", idProductos.stock);
                comando2.Parameters.AddWithValue("@id", idProductos.id);
                comando2.ExecuteNonQuery();

            }
            conexion.Close();
        }


        public static void DeleteVenta(List<ProductoVendido> productosVendido, int id)
        {
            Venta venta = new Venta();

            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Delete from Venta where id = @id";
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@id", id);

            comando.ExecuteNonQuery();

            foreach (ProductoVendido idVentaProductoVendido in productosVendido)
            {
                query = "Delete from ProductoVendido where idVenta = @idVenta";
                comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@idVenta", idVentaProductoVendido.idVenta);

                comando.ExecuteNonQuery();

            }
            conexion.Close();
        }
    }
}
