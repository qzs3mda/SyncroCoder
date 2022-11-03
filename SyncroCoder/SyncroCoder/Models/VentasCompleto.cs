using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace SyncroCoder.Models
{
    public class VentasCompleto
    {
        //Modelo
        public int Id { get; set; }
        
        public string comentarios { get; set; }
        
        public int idUsuario { get; set; }

        public int idProducto { get; set; }

        public string descripcion { get; set; }

        public double precioVenta { get; set; }



        //Constructor
        public VentasCompleto()
        {
            Id = 0;
            comentarios = string.Empty;
            idUsuario = 0;
            idProducto = 0;
            descripcion = string.Empty;
            precioVenta = 0;
        }
    }
}
