using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Linq.Expressions;

namespace CapaDatos
{
    public class CategoriaDAL
    {

        public int agregar (Categoria categoria)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AgregarCategoria";
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex) 
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public int actualizar (Categoria categoria)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ActualizarCategoria";
                    cmd.Parameters.AddWithValue("@IDCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public int Desactivar (Categoria categoria)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_DesactivarCategoria";
                    cmd.Parameters.AddWithValue("@IDCategoria", categoria.IdCategoria);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex) 
                { 
                    throw new Exception (ex.Message);
                }
            }
            return f;
        }
        public int Reactivar(Categoria categoria)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ReactivarCategoria";
                    cmd.Parameters.AddWithValue("@IDCategoria", categoria.IdCategoria);
                    cn.Open();
                    f = cmd.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return f;
        }

        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarCategoria";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria c = new Categoria(
                            Convert.ToInt32(reader["IdCategoria"]),
                            reader["Nombre"].ToString(),
                            reader["Descripcion"].ToString(),
                            Convert.ToBoolean(reader["Activo"])
                            );
                            lista.Add(c);
                        }
                        
                    }
                    cn.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception (ex.Message);
                }
            }
            return lista;
        }

    }
}
