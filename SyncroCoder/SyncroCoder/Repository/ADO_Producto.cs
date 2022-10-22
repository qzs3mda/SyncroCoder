using SyncroCoder.Models;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;
using System.Text;

namespace SyncroCoder.Repository
{
    public class ADO_Producto
    {
        public static List<Producto> GetProducto(int idUsuario)
        {
            var listaProductos = GetProductoFromDatabase(idUsuario);
            //ImprimirProductos(listaProductos);

            return listaProductos;
        }


        public static List<Producto> GetProductoFromDatabase(int idUsuario)
        {
            //Creo la conexion a la bbdd
            SqlConnectionStringBuilder MiBuilder = new();
            MiBuilder.DataSource = "localhost\\SQLEXPRESS";
            MiBuilder.InitialCatalog = "SyncroCoder";
            MiBuilder.IntegratedSecurity = true;
            var cs = MiBuilder.ConnectionString;

            var listaProducto = new List<Producto>();

            using (SqlConnection conexion = new SqlConnection(cs))
            {
                conexion.Open();
                SqlCommand comando = conexion.CreateCommand();
                comando.CommandText = "Select * from Producto where idUsuario = @idUsuario";

                var parametro = new SqlParameter();
                parametro.ParameterName = "idUsuario";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.SqlValue = idUsuario;
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                var reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var dato = LeerProductoDelReader(reader);
                    listaProducto.Add(dato);
                }
            }

            return listaProducto;
        }


        public static void CrearProducto(string descripcion, double costo, double precioVenta, int stock, int idUsuario)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Insert into Producto (descripcion, costo, precioVenta, stock, idUsuario) values (@descripcion, @costo, @precioVenta, @stock, @idUsuario)";
            SqlCommand comando = new SqlCommand(query, conexion);

            var parametroDescripcion = new SqlParameter("descripcion", SqlDbType.VarChar);
            parametroDescripcion.Value = descripcion;

            var parametroCosto = new SqlParameter("costo", SqlDbType.Float);
            parametroCosto.Value = costo;

            var parametroprecioVenta = new SqlParameter("precioVenta", SqlDbType.Float);
            parametroprecioVenta.Value = precioVenta;

            var parametrostock = new SqlParameter("stock", SqlDbType.Int);
            parametrostock.Value = stock;

            var parametroidUsuario = new SqlParameter("idUsuario", SqlDbType.Int);
            parametroidUsuario.Value = idUsuario;

            comando.Parameters.AddWithValue("@descripcion", descripcion);
            comando.Parameters.AddWithValue("@costo", costo);
            comando.Parameters.AddWithValue("@precioVenta", precioVenta);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);

            comando.ExecuteNonQuery();

            conexion.Close();
        }

        public static Producto LeerProductoDelReader(SqlDataReader reader)
        {
            var dato = new Producto();
            dato.id = Convert.ToInt32(reader.GetValue(0));
            dato.descripcion = reader.GetValue(1).ToString();
            dato.costo = Convert.ToDouble(reader.GetValue(2));
            dato.precioVenta = Convert.ToDouble(reader.GetValue(3));
            dato.stock = Convert.ToInt32(reader.GetValue(4));
            dato.idUsuario = Convert.ToInt32(reader.GetValue(5));

            return dato;
        }


        public static void UpdateProducto(int id, string descripcion, double costo, double precioVenta, int stock, int idUsuario)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Update Producto set descripcion = @descripcion, costo = @costo, precioVenta = @precioVenta, stock = @stock, idUsuario = @idUsuario where id = @id";
            SqlCommand comando = new SqlCommand(query, conexion);

            var parametroId = new SqlParameter("id", SqlDbType.Int);
            parametroId.Value = id;

            var parametroDescripcion = new SqlParameter("descripcion", SqlDbType.VarChar);
            parametroDescripcion.Value = descripcion;

            var parametrocosto = new SqlParameter("costo", SqlDbType.Float);
            parametrocosto.Value = costo;

            var parametroprecioVenta = new SqlParameter("precioVenta", SqlDbType.Float);
            parametroprecioVenta.Value = precioVenta;

            var parametroStock = new SqlParameter("stock", SqlDbType.Int);
            parametroStock.Value = stock;

            var parametroidUsuario = new SqlParameter("idUsuario", SqlDbType.Int);
            parametroidUsuario.Value = idUsuario;

            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@descripcion", descripcion);
            comando.Parameters.AddWithValue("@costo", costo);
            comando.Parameters.AddWithValue("@precioVenta", precioVenta);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);

            var registro = comando.ExecuteNonQuery();

            conexion.Close();
        }


        /*public static void ImprimirProductos(List<Producto> listaProducto)
        {
            Console.WriteLine("Estos son los productos");
            Console.WriteLine("-------------------");

            foreach (var items in listaProducto)
            {
                Console.WriteLine("Id: " + items._id);
                Console.WriteLine("Descripción: " + items._descripcion);
                Console.WriteLine("Costo: " + items._costo);
                Console.WriteLine("Precio venta: " + items._precioVenta);
                Console.WriteLine("Stock: " + items._stock);
                Console.WriteLine("Id usuario:" + items._idUsuario);
                Console.WriteLine("-------------------");
            }
        }*/


        public static void EliminarProducto(int id)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Delete from Producto where id = @id";
            SqlCommand comando = new SqlCommand(query, conexion);
   
            comando.Parameters.AddWithValue("@id", id);
            
            var registro = comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}
