using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace SyncroCoder.Models
{
    public class ProductoVendido
    {

        //Modelo
        public int id { get; set; }
        public int idProducto { get; set; }
        public int stock { get; set; }
        public int idVenta { get; set; }


        //Constructor
        public ProductoVendido()
        {
            id = 0;
            idProducto = 0;
            stock = 0;
            idVenta = 0;
        }
    }
}