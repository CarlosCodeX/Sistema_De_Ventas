using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ProductoDAL
    {

        public int agregar(Producto producto)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_AgregarProducto";
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@IDCategoria", producto.categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
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

        public int actualizar(Producto producto)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ActualizarProducto";
                    cmd.Parameters.AddWithValue("@IDProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@IDCategoria", producto.categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
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

        public int eliminar(int id)
        {
            int f = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_EliminarProducto";
                    cmd.Parameters.AddWithValue("@IDProducto", id);
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

        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarProducto";
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria()
                        {
                            IdCategoria= int.Parse(reader[2].ToString()),
                        };

                        DateTime FechaRegistro = reader.IsDBNull(5) ? DateTime.MinValue : Convert.ToDateTime(reader[5]);

                        Producto producto = new Producto(
                            Convert.ToInt32(reader["IdProducto"].ToString()),   //0
                            reader["Nombre"].ToString(),                        //1
                            categoria,                                          //2
                            Convert.ToInt32(reader["Stock"].ToString()),        //3
                            Convert.ToDecimal(reader["Precio"].ToString()),     //4
                            FechaRegistro,                                      //5
                            Convert.ToBoolean(reader["Activo"])                 //6g
                            );
                    }
                    
            }
        }
            
    }
}
