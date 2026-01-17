using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Data.Common;

namespace CapaDatos
{
    public class UsuarioDAL
    {

        public int agregar(Usuario usuario)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_InsertarUsuario";
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@IDRrol", usuario.rol.IdRol);
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

        public int actualizar(Usuario usuario)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ActualizarUsuario";
                    cmd.Parameters.AddWithValue("@IDUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@IDRol", usuario.rol.IdRol);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public int desactivar (int id)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DesactivarUsuario";
                    cmd.Parameters.AddWithValue("@IDUsuario", id);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public int reactivar(int id)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ReactivarUsuario";
                    cmd.Parameters.AddWithValue("@IDUsuario", id);
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

        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarUsuarios";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) {
                            Rol rol = new Rol()
                            {
                                NombreRol = reader["NombreRol"].ToString()
                            };

                            Usuario usuario = new Usuario(
                                Convert.ToInt32(reader["IdUsuario"].ToString()),
                                reader["Usuario"].ToString(),
                                rol,
                                Convert.ToBoolean(reader["Activo"])
                                );

                            lista.Add(usuario);
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

        public Usuario login(string nombre, string clave)
        {
            Usuario usuario = null;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_LoginUsuario";
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombre);
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        usuario = new Usuario();

                        usuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString());
                        usuario.NombreUsuario = dr["Usuario"].ToString();
                        usuario.Activo = Convert.ToBoolean(dr["Activo"]);

                        usuario.rol = new Rol()
                        {
                            NombreRol = dr["NombreRol"].ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return usuario;
        }

    }
}
