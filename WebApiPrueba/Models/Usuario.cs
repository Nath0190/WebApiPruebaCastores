namespace WebApiPrueba.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public int idRol { get; set; }
        public int estatus { get; set; }

    }
}
