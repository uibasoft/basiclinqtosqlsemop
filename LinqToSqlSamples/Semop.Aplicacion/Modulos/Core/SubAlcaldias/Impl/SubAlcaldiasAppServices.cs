using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Semop.Aplicacion.Modulos.Core.SubAlcaldias.Dto;
using Semop.Data.Modulos.Core.SubAlcaldias;

namespace Semop.Aplicacion.Modulos.Core.SubAlcaldias.Impl
{
    public class SubAlcaldiasAppServices : ISubAlcaldiasAppServices
    {
        protected readonly IRepositorySubAlcaldias RepositorySubAlcaldias;

        public SubAlcaldiasAppServices(IRepositorySubAlcaldias pRepositorySubAlcaldias)
        {
            if (pRepositorySubAlcaldias == null)
                throw new ArgumentNullException(nameof(pRepositorySubAlcaldias));
            RepositorySubAlcaldias = pRepositorySubAlcaldias;

        }

        public bool Editar(SubAlcaldiaDto dto)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int[] ids)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(SubAlcaldiaDto dto)
        {
            throw new NotImplementedException();
        }

        public List<SubAlcaldiaDto> Listar(string nombre, string direccion, int? pageIndex, int pageSize)
        {
            var result = new List<SubAlcaldiaDto>();
            try
            {
                var list = RepositorySubAlcaldias.Listar();

                if (!string.IsNullOrWhiteSpace(nombre))
                    list = list.Where(ele => ele.Nombre.Contains(nombre));
                if (!string.IsNullOrWhiteSpace(direccion))
                    list = list.Where(ele => ele.Direccion.Contains(direccion));

                var pagSize = pageSize;
                var pagNumber = (pageIndex ?? 1);
                var lista = list.ToPagedList(pagNumber, pagSize);

                result.AddRange(lista.Select(e => new SubAlcaldiaDto()
                {
                    Id = e.IdSubAlcaldia,
                    Nombre = e.Nombre,
                    Direccion = e.Direccion,
                    Zona = e.Zona,
                    Telefono = e.Telefono,
                    NombreSubAlcalde = e.NombreSubAlcalde
                }));

                //var pageSiz = lista.PageSize;
                //var pageCount = lista.PageCount;
                //var pageNumb = lista.PageNumber;
                // Asignar en un Futuro la Paginacion

            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }

            return result;

        }

        public SubAlcaldiaDto Obtener(int id)
        {
            var result = new SubAlcaldiaDto();
            try
            {
                var subAlcaldia = RepositorySubAlcaldias.Obtener(id);
                if (subAlcaldia == null)
                    result = null;
                else
                {
                    result.Id = subAlcaldia.IdSubAlcaldia;
                    result.Nombre = subAlcaldia.Nombre;
                    result.Direccion = subAlcaldia.Direccion;
                    result.Telefono = subAlcaldia.Telefono;
                    result.Zona = subAlcaldia.Zona;
                    result.NombreSubAlcalde = subAlcaldia.NombreSubAlcalde;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return result;
        }

    }
}
