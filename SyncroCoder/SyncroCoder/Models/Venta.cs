using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace SyncroCoder.Models
{
    public class Venta
    {
        //Modelo
        public int id { get; set; }
        public string comentarios { get; set; }
        public int idUsuario { get; set; }


        //Constructor
        public Venta()
        {
            id = 0;
            comentarios = string.Empty;
            idUsuario = 0;
        }
    }
}
