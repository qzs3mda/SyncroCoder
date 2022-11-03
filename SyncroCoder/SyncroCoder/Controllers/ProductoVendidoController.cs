using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {

        [HttpGet(Name = "GetProductosVendidos")]

        public List<Models.ProductoVendido> Get(int IdUsuario)
        {
            return ADO_ProductoVendido.GetProductoVendido(IdUsuario);
        }
    }
}
