using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using SyncroCoder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Text;

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


        public static List<VentasCompleto> GetVentasCompleto()
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Select Venta.Id AS IdVenta, Venta.comentarios, Venta.idUsuario, PV.idProducto, PR.descripcion, PR.precioVenta From Venta Inner Join ProductoVendido PV on Venta.Id = PV.idVenta Inner Join Producto PR on Venta.idUsuario = PR.idUsuario";

            SqlCommand comando = new SqlCommand(query, conexion);
            SqlDataReader registro = comando.ExecuteReader();

            var listaVentacompleto = new List<VentasCompleto>();

            while (registro.Read())
            {
                var ventacompleto = new VentasCompleto();

                ventacompleto.Id = Convert.ToInt32(registro.GetValue(0));
                ventacompleto.comentarios = registro.GetValue(1).ToString();
                ventacompleto.idUsuario = Convert.ToInt32(registro.GetValue(2));
                ventacompleto.idProducto = Convert.ToInt32(registro.GetValue(3));
                ventacompleto.descripcion = registro.GetValue(4).ToString();
                ventacompleto.precioVenta = Convert.ToDouble(registro.GetValue(5));

                listaVentacompleto.Add(ventacompleto);
            }

            return listaVentacompleto;

            conexion.Close();
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
                comando = new    SqlCommand(query, conexion);

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

        public static void DeleteVenta(int idVenta)
        {
            List<ProductoVendido> idsdeproductos = ADO_ProductoVendido.GetProductoVendidoByIdVenta(idVenta);
            var listaIdsProducto = new List<int>();

            foreach(ProductoVendido producto in idsdeproductos)
            {
                listaIdsProducto.Add(producto.idProducto);
            }

            var ids = new List<int>(listaIdsProducto);
            var concatenoIds = new StringBuilder();
            foreach (var word in ids)
            {
                concatenoIds.Append(word).Append("','");
            }

            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();

            SqlCommand comandoUpdate = new SqlCommand();
            string UpdateProducto = string.Empty;
            UpdateProducto = "Update Producto set Stock = 0 Where id in ('" + concatenoIds + "')";
            SqlCommand comando = new SqlCommand(UpdateProducto, conexion);
            var registro = comando.ExecuteNonQuery();

            ProductoVendido productoVendido = new ProductoVendido();
            SqlCommand comandoDeletePV = new SqlCommand();
            string DeleteProductoVendido = string.Empty;
            DeleteProductoVendido = "Delete from ProductoVendido where idVenta = @idVenta";
            comandoDeletePV = new SqlCommand(DeleteProductoVendido, conexion);
            comandoDeletePV.Parameters.AddWithValue("@idVenta", idVenta);
            comandoDeletePV.ExecuteNonQuery();

            Venta venta = new Venta();
            SqlCommand comandoDeleteVenta = new SqlCommand();
            string DeleteVenta = string.Empty;
            DeleteVenta = "Delete From Venta where id = @id";
            comandoDeleteVenta = new SqlCommand(DeleteVenta, conexion);
            comandoDeleteVenta.Parameters.AddWithValue("@id", idVenta);
            comandoDeleteVenta.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
