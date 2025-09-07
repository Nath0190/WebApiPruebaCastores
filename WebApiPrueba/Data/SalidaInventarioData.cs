using System.Data;
using System.Data.SqlClient;
using WebApiPrueba.Models;

namespace WebApiPrueba.Data
{
    public class SalidaInventarioData
    {
        private readonly string connection;

        public SalidaInventarioData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("connectionLocal")!;
        }

        public async Task<List<Producto>> ListaProductoActivos()
        {
            List<Producto> listaProducto = new List<Producto>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ListaProductosActivos", con);
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
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            precio = Convert.ToDecimal(reader["precio"])
                        });
                    }
                }
            }
            return listaProducto;
        }

        public async Task<bool> ActualizarStock(int idProducto, int cantidad)
        {
            bool Resp = true;
            using (var conn = new SqlConnection(connection))
            {
                SqlCommand comm = new SqlCommand("sp_ActualizarStock", conn);
                comm.Parameters.AddWithValue("@p_idProducto", idProducto);
                comm.Parameters.AddWithValue("@p_cantidad", cantidad);

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
