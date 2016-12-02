using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semop.Data.Modulos.Core.Responsables
{
    public interface IRepositoryResponsables : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
