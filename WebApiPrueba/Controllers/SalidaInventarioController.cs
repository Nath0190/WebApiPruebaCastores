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

        [HttpPut("{idProducto}/{cantidad}")]
        public async Task<IActionResult> ActualizarEstatus(int idProducto, int cantidad)
        {
            bool resp = await _salidaInventarioData.ActualizarStock(idProducto, cantidad);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = resp });
        }
    }
}
