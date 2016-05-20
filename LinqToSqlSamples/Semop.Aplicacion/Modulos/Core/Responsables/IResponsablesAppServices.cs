using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Aplicacion.Modulos.Core.Responsables.Dtos;

namespace Semop.Aplicacion.Modulos.Core.Responsables
{
    public interface IResponsablesAppServices
    {
        bool Guardar(ResponsableDto dto);
    }
}
