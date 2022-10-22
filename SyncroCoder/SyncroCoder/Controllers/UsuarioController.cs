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

        public void GetUsuarioAPI(string nombreUsuario)
        {
            ADO_Usuario.GetUsuario(nombreUsuario);
        }


        [HttpPost(Name = "CrearUsuario")]

        public void CrearUsuarioAPI(Usuario usuario)
        {
            ADO_Usuario.CrearUsuario(usuario);
        }


        [HttpPut(Name = "UpdateUsuario")]

        public void UpdateUsuarioAPI(Usuario usuario)
        {
            ADO_Usuario.UpdateUsuario(usuario);
        }
    }
}
