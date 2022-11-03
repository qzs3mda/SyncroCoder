using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet(Name = "GetUsuarios")]

        public List<Models.Usuario> GetUsuarioAPI(string nombreUsuario)
        {
            return ADO_Usuario.GetUsuario(nombreUsuario);
        }


        [HttpPost(Name = "CrearUsuario")]

        public static void CrearUsuarioAPI(Usuario usuario)
        {
            ADO_Usuario.CrearUsuario(usuario);
        }


        [HttpPut(Name = "UpdateUsuario")]

        public void UpdateUsuarioAPI(Usuario usuario)
        {
            ADO_Usuario.UpdateUsuario(usuario);
        }


        [HttpDelete(Name = "EliminarUsuario")]

        public IActionResult EliminarUsuarioAPI([FromBody] int id)
        {
            try
            {
                ADO_Usuario.EliminarUsuario(id);
                return Ok(new { mesagge = "Usuario eliminado correctamente"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
