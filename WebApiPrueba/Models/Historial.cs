namespace WebApiPrueba.Models
{
    public class Historial
    {
        public int idHistorial { get; set; }
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public string ? tipoMovimiento { get; set; }
        public string ? nombreUsuario { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
