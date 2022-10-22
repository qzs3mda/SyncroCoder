using System.Collections.Specialized;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace SyncroCoder.Models
{
    public class Usuario
    {
        //Modelo
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string contraseña { get; set; }
        public string mail { get; set; }


        //Constructor
        public Usuario()
        {
            id = 0;
            nombre = string.Empty;
            apellido = string.Empty;
            nombreUsuario = string.Empty;
            contraseña = string.Empty;
            mail = string.Empty;
        }
    }
}

