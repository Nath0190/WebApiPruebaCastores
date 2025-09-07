namespace WebApiPrueba.Models
{
    public class Rel_Rol_Usuario
    {
        public int idRel_Rol_Usuario { get; set; }
        public int idRol { get; set; }
        public int idModulo { get; set; }
        public int idUsuario { get; set; }
        public int estatus { get; set; }
        public int permiso { get; set; }
        
    }
}
