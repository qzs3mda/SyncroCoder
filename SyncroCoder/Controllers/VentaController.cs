using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        [HttpGet("GetVentas")]

        public List<Models.Venta> GetVentaAPI()
        {
            return ADO_Venta.GetVenta();
        }


        [HttpGet("GetVentasCompleto")]

        public List<VentasCompleto> GetVentaCompletoAPI()
        {
            return ADO_Venta.GetVentasCompleto();
        }


        [HttpPost(Name = "CrearVentas")]

        public void CrearVentaAPI(List<Producto> productos, string comentarios, int idUsuario)
        {
            ADO_Venta.CrearVenta(productos, comentarios, idUsuario);
        }


        [HttpDelete(Name = "DeleteVentas")]

        public void DeleteVentaAPI([FromBody] int idVenta)
        {
            ADO_Venta.DeleteVenta(idVenta);
        }
    }
}
