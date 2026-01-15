using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Linq.Expressions;
using System.Collections;

namespace CapaDatos
{
    public class ClienteDAL
    {

        public int agregar(Cliente cliente)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_InsertarCliente";
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return f;
        }

        public int actualizar(Cliente cliente)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ActualizarCliente";
                    cmd.Parameters.AddWithValue("@IDCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarCliente";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime FechaRegistro = reader.IsDBNull(5) ? DateTime.MinValue : Convert.ToDateTime(reader[5]);

                            Cliente c = new Cliente(
                                Convert.ToInt32(reader["IdCliente"]),   //0
                                reader["Nombre"].ToString(),            //1
                                reader["Documento"].ToString(),         //2
                                reader["Telefono"].ToString(),          //3
                                reader["Email"].ToString(),             //4
                                FechaRegistro,                          //5
                                Convert.ToBoolean(reader["Activo"])     //6
                                );
                            lista.Add(c);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return lista;
        }

        public List<Cliente> buscar(string nombre)
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_BuscarCliente";
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime FechaRegistro = reader.IsDBNull(5) ? DateTime.MinValue : Convert.ToDateTime(reader[5]);

                            Cliente c = new Cliente(
                                Convert.ToInt32(reader["IdCliente"]),   //0
                                reader["Nombre"].ToString(),            //1
                                reader["Documento"].ToString(),         //2
                                reader["Telefono"].ToString(),          //3
                                reader["Email"].ToString(),             //4
                                FechaRegistro,                          //5
                                Convert.ToBoolean(reader["Activo"])     //6
                                );
                            lista.Add(c);
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return lista;
        }
    }
}
