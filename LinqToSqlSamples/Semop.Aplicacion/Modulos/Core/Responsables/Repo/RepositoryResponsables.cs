using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Data;
using Semop.Data.Modulos.Core.Responsables;

namespace Semop.Aplicacion.Modulos.Core.Responsables.Repo
{
    public class RepositoryResponsables : IRepositoryResponsables
    {
        private readonly UnitOfWorkSimple _unitOfWorkSimple;
        public RepositoryResponsables(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWorkSimple = unitOfWork as UnitOfWorkSimple;
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWorkSimple;
            }
        }
        public void Dispose()
        {
            _unitOfWorkSimple.Dispose();
        }
    }
}
