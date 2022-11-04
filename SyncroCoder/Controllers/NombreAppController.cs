using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncroCoder.Models;
using SyncroCoder.Repository;

namespace SyncroCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NombreAppController : ControllerBase
    {
        [HttpGet]
        public string NombreDeMiApp()
        {
            return "SyncroCoder - Este es el nombre de mi App";
        }
    }
}
