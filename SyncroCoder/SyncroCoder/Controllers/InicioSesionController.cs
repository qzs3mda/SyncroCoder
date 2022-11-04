using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;
using System.Reflection.PortableExecutable;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contraseña}/")]

        public List<Usuario> InicioSesionAPI(string nombreUsuario, string contraseña)
        {
            try
            {
                return ADO_Usuario.Login(nombreUsuario, contraseña);
            }
            catch (Exception)
            {
                return new List<Usuario>();
            }
            
        }
    }
}
