using SyncroCoder.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System.Reflection.PortableExecutable;

namespace SyncroCoder.Repository
{
    public class ADO_Usuario
    {
        public static List<Usuario> Login(string nombreUsuario, string contraseña)
        {
            var login =  ADO_Usuario.GetUsuario(nombreUsuario);

            if (login.Count == 0)
            {
                throw new Exception("No existe usuario");
            }

            else
            {
                var listaUsuario = new List<Usuario>();

                SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
                conexion.Open();
                string query = "Select * from Usuario where nombreUsuario = @nombreUsuario and contraseña = @contraseña";
                SqlCommand comando = new SqlCommand(query, conexion);

                var parametroNombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
                parametroNombreUsuario.Value = parametroNombreUsuario;

                var parametroContraseña = new SqlParameter("contraseña", SqlDbType.VarChar);
                parametroContraseña.Value = parametroContraseña;

                comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("contraseña", contraseña);

                var reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    var usuario = new Usuario();
                    usuario.id = Convert.ToInt32(reader.GetValue(0));
                    usuario.nombre = reader.GetValue(1).ToString();
                    usuario.apellido = reader.GetValue(2).ToString();
                    usuario.nombreUsuario = reader.GetValue(3).ToString();
                    usuario.contraseña = reader.GetValue(4).ToString();
                    usuario.mail = reader.GetValue(5).ToString();
                    listaUsuario.Add(usuario);
                }
                conexion.Close();

                return listaUsuario;
            }
        }


        public static List<Usuario> GetUsuario(string nombreUsuario)
        {
            var listaUsuario = new List<Usuario>();

            SqlConnection conexion = new SqlConnection("Data Source=.\\SQLEXPRESS ; Initial Catalog = SyncroCoder; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conexion.Open();
            string query = "Select * from Usuario where nombreUsuario = @nombreUsuario";
            SqlCommand comando = new SqlCommand(query, conexion);

            var parametroNombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar);
            parametroNombreUsuario.Value = parametroNombreUsuario;

            comando.Parameters.AddWithValue("nombreUsuario", nombreUsuario);

            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                var usuario = new Usuario();
                usuario.id = Convert.ToInt32(reader.GetValue(0));
                usuario.nombre = reader.GetValue(1).ToString();
                usuario.apellido = reader.GetValue(2).ToString();
                usuario.nombreUsuario = reader.GetValue(3).ToString();
                usuario.contraseña = reader.GetValue(4).ToString();
                usuario.mail = reader.GetValue(5).ToString();
                listaUsuario.Add(usuario);
            }

            conexion.Close();

            return listaUsuario;
        }


        public static void CrearUsuario(Usuario usuario)
        {
            var usuarioRepetido = ADO_Usuario.GetUsuario(usuario.nombreUsuario);

            if (usuarioRepetido.Count != 0)
            {
               throw new Exception("El usuario ya existe");
            }
            else
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


        public static void EliminarUsuario(int id)
        {
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();
            string query = "Delete from Usuario where id = @id";
            SqlCommand comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@id", id);

            var registro = comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}   
