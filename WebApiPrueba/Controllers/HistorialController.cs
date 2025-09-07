using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPrueba.Data;
using WebApiPrueba.Models;

namespace WebApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly HistorialData _historialData;
        public HistorialController(HistorialData historialData)
        {
            _historialData = historialData;
        }

        [HttpGet ("{idUsuario}/{tipoMovimiento}")]
        public async Task<IActionResult> ListaProductoActivos(int idUsuario, string tipoMovimiento)
        {
            List<Historial> lista = await _historialData.ListadoHistorial( idUsuario, tipoMovimiento);
            return StatusCode(StatusCodes.Status200OK, lista);
        }

    }
}
