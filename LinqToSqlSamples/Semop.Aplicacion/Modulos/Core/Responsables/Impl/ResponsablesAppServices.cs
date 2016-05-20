using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Aplicacion.Modulos.Core.Responsables.Dtos;
using Semop.Data.Model;
using Semop.Data.Modulos.Core.Responsables;

namespace Semop.Aplicacion.Modulos.Core.Responsables.Impl
{
    public class ResponsablesAppServices : IResponsablesAppServices
    {
        protected readonly IRepositoryResponsables RepositoryResponsables;

        public ResponsablesAppServices(IRepositoryResponsables pRepositoryResponsables)
        {
            if (pRepositoryResponsables == null)
                throw new ArgumentNullException(nameof(pRepositoryResponsables));
            RepositoryResponsables = pRepositoryResponsables;

        }

        public bool Guardar(ResponsableDto dto)
        {
            bool result = false;
            try
            {
                if (dto == null)
                    return result;

                #region Validaciones

                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    return result;

                #endregion

                var unitOfWork = RepositoryResponsables.UnitOfWork as UnitOfWorkSimple;
                if (unitOfWork != null)
                {
                    var entity = new Responsable()
                    {
                        FechaAsignacion = dto.FechaAsignacion,
                        Persona = new Persona()
                        {
                            FirstName = dto.FirstName,
                            LastName = dto.LastName
                        }
                    };
                    unitOfWork.Responsables.InsertOnSubmit(entity);
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
    }
}
