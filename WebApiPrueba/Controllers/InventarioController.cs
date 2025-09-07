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

        [HttpPut]
        public async Task<IActionResult> ActualizarProducto([FromBody] Producto obj)
        {
            bool resp = await _inventarioData.ActualizarProducto(obj);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }

        [HttpPut("{idProducto}/{estatus}/{idUsuario}/{movimiento}")]
        public async Task<IActionResult> ActualizarEstatus(int idProducto, int estatus, int idUsuario, string movimiento)
        {
            bool resp = await _inventarioData.ActualizarEstatus(idProducto, estatus, idUsuario, movimiento);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }
    }
}
