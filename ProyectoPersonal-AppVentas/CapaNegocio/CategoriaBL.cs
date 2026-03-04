using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CategoriaBL
    {

        CategoriaDAL categoriaDAL = new CategoriaDAL();

        public int GestionarCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new Exception("Categoría inválida");

            if (categoria.IdCategoria < 0)
                throw new Exception("ID inválido");

            if (categoria.IdCategoria == 0)
                return categoriaDAL.agregar(categoria);
            else
                return categoriaDAL.actualizar(categoria);
        }


        public int CambiarEstadoCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new Exception("Categoría inválida");

            if (categoria.IdCategoria <= 0)
                throw new Exception("ID inválido");

            if (categoria.Activo)
                return categoriaDAL.Desactivar(categoria.IdCategoria);
            else
                return categoriaDAL.Reactivar(categoria.IdCategoria);
        }

        public List<Categoria> ListarCategoriaConFiltro()
        {
            return categoriaDAL.listarConFiltro();
        }

        public List<Categoria> ListarCategoriaSinFiltro()
        {
            return categoriaDAL.listarSinFiltro();
        }

    }
}
