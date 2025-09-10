using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPrueba.Data;
using WebApiPrueba.Models;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioData _inventarioData;

        public InventarioController(InventarioData inventarioData)
        {
            _inventarioData = inventarioData;
        }

        [HttpGet("{correo}/{contrasena}")] //int idUsuario, string correo, string contrasena
        public async Task<IActionResult> Session( string correo, string contrasena)
        {
            List<Session> lista = await _inventarioData.Session( correo, contrasena);
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> ListaProducto()
        {
            List<Producto> lista = await _inventarioData.ListaProducto();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo([FromBody] Producto obj)
        {
            bool resp = await _inventarioData.NuevoProducto(obj);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
             Producto objeto = await _inventarioData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEstatus([FromBody] ActualizarResp obj,int id)
        {
            bool resp = await _inventarioData.ActualizarEstatus(obj, id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }
    }
}
