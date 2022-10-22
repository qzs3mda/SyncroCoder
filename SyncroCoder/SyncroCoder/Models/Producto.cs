using Microsoft.AspNetCore.Http.Features;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace SyncroCoder.Models
{
    public class Producto
    {
        //Modelo        
        public int id { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }
        public double precioVenta { get; set; }
        public int stock { get; set; }
        public int idUsuario { get; set; }

        //Constructor
        public Producto()
        {
            id = 0;
            descripcion = string.Empty;
            costo = 0;
            precioVenta = 0;
            stock = 0;
            idUsuario = 0;
        }
    }
}