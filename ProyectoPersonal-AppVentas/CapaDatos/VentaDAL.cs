using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad.DTO;

namespace CapaDatos
{
    public class VentaDAL
    {

        public int RegistrarVenta(int idCliente, List<DetalleVentaDTO> detalles)
        {
            int resultado = 0;

            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarVenta_Multiple", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                    // TVP
                    DataTable dt = new DataTable();
                    dt.Columns.Add("IdProducto", typeof(int));
                    dt.Columns.Add("Cantidad", typeof(int));

                    foreach (var d in detalles)
                    {
                        dt.Rows.Add(d.IdProducto, d.Cantidad);
                    }


                    SqlParameter tvp = cmd.Parameters.AddWithValue("@Detalle", dt);
                    tvp.SqlDbType = SqlDbType.Structured;
                    tvp.TypeName = "TVP_DetalleVenta";

                    cn.Open();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return resultado;
        }

        public List<VentaListadoDTO> listarHoy()
        {
            List<VentaListadoDTO> lista = new List<VentaListadoDTO>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarVentasHoy";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                Nombre = reader["Cliente"].ToString()
                            };

                            DateTime FechaVenta = Convert.ToDateTime(reader["FechaVenta"].ToString());

                            lista.Add(new VentaListadoDTO
                            {
                                IdVenta = Convert.ToInt32(reader["IdVenta"].ToString()),
                                cliente = cliente,
                                FechaVenta = FechaVenta,
                                Total = Convert.ToDecimal(reader["Total"].ToString()),
                            });
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

        public decimal TotalVentasHoy()
        {
            decimal total = 0;

            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_TotalVentasHoy";

                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    total = result != null ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return total;
        }

        public List<VentaListadoDTO> listarMes()
        {
            List<VentaListadoDTO> lista = new List<VentaListadoDTO>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarVentasMes";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                Nombre = reader["Cliente"].ToString()
                            };

                            DateTime FechaVenta = Convert.ToDateTime(reader["FechaVenta"].ToString());

                            lista.Add(new VentaListadoDTO
                            {
                                IdVenta = Convert.ToInt32(reader["IdVenta"].ToString()),
                                cliente = cliente,
                                FechaVenta = FechaVenta,
                                Total = Convert.ToDecimal(reader["Total"].ToString()),
                            });
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

        public decimal TotalVentasMes()
        {
            decimal total = 0;

            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_TotalVentasMes";

                    cn.Open();
                    object result = cmd.ExecuteScalar();
                    total = result != null ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return total;
        }

        public List<VentaListadoDTO> listarAdmin() //ESTE LISTA TODAS LAS VENTAS, SOLO LO PUEDE USAR EL ADMINISTRADOR
        {
            List<VentaListadoDTO> lista = new List<VentaListadoDTO>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ListarVentas";
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                Nombre = reader["Cliente"].ToString()
                            };

                            DateTime FechaVenta = Convert.ToDateTime(reader["FechaVenta"].ToString());

                            lista.Add(new VentaListadoDTO
                            {
                                IdVenta = Convert.ToInt32(reader["IdVenta"].ToString()),
                                cliente = cliente,
                                FechaVenta = FechaVenta,
                                Total = Convert.ToDecimal(reader["Total"].ToString()),
                            });
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

        public List<VentaListadoDTO> BuscarAdmin(string nombreCliente) 
        {
            List<VentaListadoDTO> lista = new List<VentaListadoDTO>();
            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_BuscarVenta";
                    cmd.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente()
                            {
                                Nombre = reader["Cliente"].ToString()
                            };

                            DateTime FechaVenta = Convert.ToDateTime(reader["FechaVenta"].ToString());

                            lista.Add(new VentaListadoDTO
                            {
                                IdVenta = Convert.ToInt32(reader["IdVenta"].ToString()),
                                cliente = cliente,
                                FechaVenta = FechaVenta,
                                Total = Convert.ToDecimal(reader["Total"].ToString()),
                            });
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

        public Venta detalles(int id)
        {
            Venta venta = null;
            List<DetalleVenta> detalles = new List<DetalleVenta>();

            using (SqlConnection cn = new SqlConnection(ConexionBD.cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DetalleVenta", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDVenta", id); 

                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (venta == null)
                            {
                                Cliente c = new Cliente()
                                {
                                    Nombre = reader["Cliente"].ToString()
                                };

                                DateTime fechaVenta = Convert.ToDateTime(reader["FechaVenta"]);

                                venta = new Venta(
                                    Convert.ToInt32(reader["IdVenta"]),
                                    c,
                                    fechaVenta,
                                    Convert.ToDecimal(reader["Total"]),
                                    detalles
                                );
                            }

                            Producto p = new Producto()
                            {
                                Nombre = reader["Producto"].ToString()
                            };

                            DetalleVenta detalle = new DetalleVenta(
                                p,
                                Convert.ToInt32(reader["Cantidad"]),
                                Convert.ToDecimal(reader["PrecioUnitario"]),
                                Convert.ToDecimal(reader["SubTotal"])
                            );

                            detalles.Add(detalle);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return venta;
        }

    }
}
