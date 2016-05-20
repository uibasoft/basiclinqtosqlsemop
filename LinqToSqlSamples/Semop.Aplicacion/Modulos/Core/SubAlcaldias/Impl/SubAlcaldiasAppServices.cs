using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Semop.Aplicacion.Modulos.Core.SubAlcaldias.Dto;
using Semop.Data.Model;
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
            bool result = false;
            try
            {
                if (dto == null)
                    return result;

                #region Validaciones


                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return result;

                #endregion

                var unitOfWork = RepositorySubAlcaldias.UnitOfWork as UnitOfWorkSimple;
                if (unitOfWork != null)
                {
                    var entity = unitOfWork.SubAlcaldias.SingleOrDefault(ele => ele.IdSubAlcaldia == dto.Id);
                    if (entity == null)
                        return false;
                    entity.Nombre = dto.Nombre;
                    entity.Direccion = dto.Direccion;
                    entity.NombreSubAlcalde = dto.NombreSubAlcalde;
                    entity.Telefono = dto.Telefono;
                    entity.Zona = entity.Zona;
                    unitOfWork.SubmitChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result = false;
            }
            return result;
        }

        public bool Eliminar(int[] ids)
        {
            bool result;
            try
            {
                result = RepositorySubAlcaldias.Eliminar(ids);
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result = false;
            }
            return result;
        }

        public bool Guardar(SubAlcaldiaDto dto)
        {
            bool result = false;
            try
            {
                if (dto == null)
                    return result;

                #region Validaciones


                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return result;

                #endregion

                var entity = new SubAlcaldia()
                {
                    Nombre = dto.Nombre,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono,
                    Zona = dto.Zona,
                    NombreSubAlcalde = dto.NombreSubAlcalde,
                };

                result = RepositorySubAlcaldias.Guardar(entity);
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result = false;
            }
            return result;
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
