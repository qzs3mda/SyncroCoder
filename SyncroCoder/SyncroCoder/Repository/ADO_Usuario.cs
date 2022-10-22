using SyncroCoder.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Hosting;

namespace SyncroCoder.Repository
{
    public class ADO_Usuario
    {
        public static void GetUsuario(string nombreUsuario)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Select * from Usuario where nombreUsuario = @nombreUsuario";
            SqlCommand comando = new SqlCommand(query, conexion);

            var parametroNombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
            parametroNombreUsuario.Value = parametroNombreUsuario;

            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario);

            var reader = comando.ExecuteReader();
            
            conexion.Close();
        }

        public static void CrearUsuario(Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Insert into Usuario (nombre, apellido, nombreUsuario, contraseña, mail) values (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@id", usuario.id);
            comando.Parameters.AddWithValue("@nombre", usuario.nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.apellido);
            comando.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
            comando.Parameters.AddWithValue("@contraseña", usuario.contraseña);
            comando.Parameters.AddWithValue("mail", usuario.mail);

            var registro = comando.ExecuteNonQuery();

            conexion.Close();
        }

        public static void UpdateUsuario(Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Update Usuario set nombre = @nombre, apellido = @apellido, nombreUsuario = @nombreUsuario, contraseña = @contraseña, mail = @mail where id = @id";
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@id", usuario.id);
            comando.Parameters.AddWithValue("@nombre", usuario.nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.apellido);
            comando.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
            comando.Parameters.AddWithValue("@contraseña", usuario.contraseña);
            comando.Parameters.AddWithValue("mail", usuario.mail);

            var registro = comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}   
