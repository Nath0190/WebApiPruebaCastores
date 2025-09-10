namespace WebApiPrueba.Models
{
    public class Producto
    {
        public int idProducto { get; set; }
        public int estatus { get; set; }
        public string ? nombre { get; set; }
        public string  ? nombreEstatus { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public int idUsuarioModificacion { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
