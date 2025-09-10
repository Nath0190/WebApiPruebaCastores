using System.Data;
using System.Data.SqlClient;
using WebApiPrueba.Models;

namespace WebApiPrueba.Data
{
    public class HistorialData
    {
        private readonly string connection;

        public HistorialData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("connectionLocal")!;
        }

        public async Task<List<Historial>> ListadoHistorial(string tipoMovimiento)
        {
            List<Historial> listadoHistorial = new List<Historial>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_ListadoHistorial", con);
                cmd.Parameters.AddWithValue("@p_movimiento", tipoMovimiento);
                
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        listadoHistorial.Add(new Historial
                        {
                            tipoMovimiento = reader["tipoMovimiento"].ToString(),
                            idUsuario = Convert.ToInt32(reader["idUsuario"]),
                            nombreUsuario = reader["nombreUsuario"].ToString(),
                            fechaModificacion = Convert.ToDateTime(reader["fechaModificacion"]),
                        });
                    }
                }
            }
            return listadoHistorial;
        }

    }
}
