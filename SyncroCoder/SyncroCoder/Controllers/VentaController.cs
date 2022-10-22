using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;
using System.ComponentModel;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        [HttpGet(Name = "GetVentas")]

        public List<Models.Venta> GetVentaAPI()
        {
            return ADO_Venta.GetVenta();
        }


        [HttpPost(Name = "CrearVentas")]

        public void CrearVentaAPI(List<Producto> productos, string comentarios, int idUsuario)
        {
            ADO_Venta.CrearVenta(productos, comentarios, idUsuario);
        }


        [HttpDelete(Name = "DeleteVentas")]

        public void DeleteVentaAPI([FromBody] List<ProductoVendido> productosVendido, int id)
        {
            ADO_Venta.DeleteVenta(productosVendido, id);
        }
    }
}
