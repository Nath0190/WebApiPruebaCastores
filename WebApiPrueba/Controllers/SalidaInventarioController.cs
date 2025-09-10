using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPrueba.Data;
using WebApiPrueba.Models;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaInventarioController : ControllerBase
    {
        private readonly SalidaInventarioData _salidaInventarioData;

        public SalidaInventarioController(SalidaInventarioData salidaInventarioData)
        {
            _salidaInventarioData = salidaInventarioData;
        }

        [HttpGet]
        public async Task<IActionResult> ListaProductoActivos()
        {
            List<Producto> lista = await _salidaInventarioData.ListaProductoActivos();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarStock([FromBody] Producto obj)
        {
            bool resp = await _salidaInventarioData.ActualizarStock(obj);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }

        [HttpPut("{movimiento}")]
        public async Task<IActionResult> ActualizarSalida([FromBody] Producto obj,string movimiento)
        {
            bool resp = await _salidaInventarioData.ActualizarSalida(obj,movimiento);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }
    }
}
