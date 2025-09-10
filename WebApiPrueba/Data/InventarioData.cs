using WebApiPrueba.Models;
using System.Data;
using System.Data.SqlClient;


namespace WebApiPrueba.Data
{
    public class InventarioData
    {
        private readonly string connection;

        public InventarioData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("connectionLocal")!;
        }

        public async Task<List<Session>> Session(string correo, string contrasena)
        {
            List<Session> lista = new List<Session>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_sesion", con);
                cmd.Parameters.AddWithValue("@p_correo", correo);
                cmd.Parameters.AddWithValue("@p_contrasena", contrasena);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Session
                        {
                            idModulo = Convert.ToInt32(reader["idModulo"]),
                            nombreModulo = reader["nombreModulo"].ToString(),
                            permiso = Convert.ToInt32(reader["permiso"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<List<Producto>> ListaProducto()
        {
            List<Producto> listaProducto = new List<Producto>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ListaProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        listaProducto.Add(new Producto
                        {
                            idProducto = Convert.ToInt32(reader["idProducto"]),
                            estatus = Convert.ToInt32(reader["estatus"]),
                            nombre = reader["nombre"].ToString(),
                            nombreEstatus = reader["nombreEstatus"].ToString(),
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            precio = Convert.ToDecimal(reader["precio"])
                        });
                    }
                }
            }
            return listaProducto;
        }

        public async Task<bool> NuevoProducto(Producto obj)
        {
            bool Resp = true;
            using (var conn = new SqlConnection(connection))
            {
                SqlCommand comm = new SqlCommand("sp_NuevoProducto", conn);
                comm.Parameters.AddWithValue("@p_estatus", obj.estatus);
                comm.Parameters.AddWithValue("@p_nombre", obj.nombre);
                comm.Parameters.AddWithValue("@p_cantidad", obj.cantidad);
                comm.Parameters.AddWithValue("@p_precio", obj.precio);

                comm.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conn.OpenAsync();
                    Resp = await comm.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    Resp = false;
                }
            }
            return Resp;
        }

        public async Task<Producto> Obtener(int Id)
        {
            Producto objeto = new Producto();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerProducto", con);
                cmd.Parameters.AddWithValue("@p_idProducto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Producto
                        {
                            idProducto = Convert.ToInt32(reader["idProducto"]),
                            nombre = reader["nombre"].ToString(),
                            nombreEstatus = reader["nombreEstatus"].ToString(),
                            estatus = Convert.ToInt32(reader["estatus"].ToString()),
                            cantidad = Convert.ToInt32(reader["cantidad"].ToString()),
                            precio = Convert.ToDecimal(reader["precio"]),
                        };
                    }
                }
            }
            return objeto;
        }

       

        public async Task<bool> ActualizarEstatus(ActualizarResp obj, int id )
        {
            bool Resp = true;
            using (var conn = new SqlConnection(connection))
            {
                SqlCommand comm = new SqlCommand("sp_CambiarEstatusProducto", conn);
                comm.Parameters.AddWithValue("@p_idProducto", id);
                comm.Parameters.AddWithValue("@p_estatus", obj.estatus);
                comm.Parameters.AddWithValue("@p_idUsuario", obj.idUsuario);
                comm.Parameters.AddWithValue("@p_movimiento", obj.tipoMovimiento);

                comm.CommandType = CommandType.StoredProcedure;

                try
                {
                    await conn.OpenAsync();
                    Resp = await comm.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    Resp = false;
                }
            }
            return Resp;
        }


    }
}
