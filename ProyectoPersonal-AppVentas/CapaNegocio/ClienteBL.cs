using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ClienteBL
    {

        ClienteDAL clienteDAL = new ClienteDAL();

        public int GestionarCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new Exception("Categoría inválida");

            if (cliente.IdCliente < 0)
                throw new Exception("ID inválido");

            if (cliente.IdCliente == 0)
                return clienteDAL.agregar(cliente);
            else
                return clienteDAL.actualizar(cliente);   
        }

        public List<Cliente> ListarClientes()
        {
            return clienteDAL.listar();
        }

        public List<Cliente> BuscarClientes(string nombre)
        {
            return clienteDAL.buscar(nombre);
        }

    }
}
