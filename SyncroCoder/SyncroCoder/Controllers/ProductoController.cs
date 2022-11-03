using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "GetProductos")]

        public List<Models.Producto> GetProductoAPI(int idUsuario)
        {
            return ADO_Producto.GetProducto(idUsuario);
        }


        [HttpPost(Name = "CrearProductos")]

        public void CrearProductoAPI(string descripcion, double costo, double precioVenta, int stock, int idUsuario)
        {
            ADO_Producto.CrearProducto(descripcion, costo, precioVenta, stock, idUsuario);
        }


        [HttpPut("{id}")]

        public void UpdateProductoAPI(int id, string descripcion, double costo, double precioVenta, int stock, int idUsuario)
        {
            ADO_Producto.UpdateProducto(id, descripcion, costo, precioVenta, stock, idUsuario);
        }


        [HttpDelete(Name = "EliminarProducto")]

        public void EliminarProductoAPI([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);
        }
    }
}
