using SyncroCoder.Models;
using System.Data.SqlClient;

namespace SyncroCoder.Repository
{
    public class ADO_Login
    {
        public static void Login()
        {
            //Acá use otra manera de llamar a la base de datos, me parecio mucho más simple
            SqlConnection conexion = new SqlConnection("server=localhost\\SQLEXPRESS ; database=SyncroCoder ; integrated security = true");
            conexion.Open();

            int count = 0;
            bool LoginOk = false;

            Console.WriteLine("Para que el login funcione hay que ingresar Usuario:dgonzalez | Contraseña:1234");
            Console.WriteLine("Si el login es invalido 3 veces sale del ciclo\n");

            while (!LoginOk)
            {
                //Use metodo para que reciba todo en minuscula, y descubrí el trim para limpiar espacios adelante y atras del string que pueda tipear por error
                Console.WriteLine("Ingrese su usuario: ");
                string inputUsuario = Console.ReadLine().ToLower().Trim();

                Console.WriteLine("\nIngrese su contraseña: ");
                string inputContraseña = Console.ReadLine().ToLower().Trim();

                //Acá me fallaba cuando el usuario en la contraseña tipeaba letras, hasta que me di cuenta que me faltaban las comillas simples
                string query = "select * from Usuario where nombreUsuario='" + inputUsuario + "' and contraseña='" + inputContraseña + "'";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader registro = comando.ExecuteReader();

                //Acá lee lo que devuelve el query y creo un objeto usuario para usarlo adentro del if 
                registro.Read();
                var usuario = new Usuario();

                //En este if descrubrí el hasrows, entiendo que si hay una fila ejecuta el true
                if (registro.HasRows)
                {
                    usuario.nombre = registro.GetValue(1).ToString();
                    usuario.apellido = registro.GetValue(2).ToString();
                    usuario.nombreUsuario = registro.GetValue(3).ToString();
                    usuario.mail = registro.GetValue(5).ToString();
                    Console.WriteLine("\n             Bienvenido " + usuario.nombre + " " + usuario.apellido);
                    Console.WriteLine("\n             Nombre de usuario: " + usuario.nombreUsuario);
                    Console.WriteLine("\n             Email: " + usuario.mail);
                    LoginOk = true;
                    break;
                }

                //Con esto si falla el login 3 veces saco del ciclo al usuario
                if (count == 2)
                {
                    Console.WriteLine("\n             Ha agotado los intentos de validación\n");
                    break;
                }

                else
                {
                    Console.WriteLine("\n             Usuario o contraseña incorrecta\n");
                }
                count++;
                registro.Close();
            }
            conexion.Close();
        }
    }
}
