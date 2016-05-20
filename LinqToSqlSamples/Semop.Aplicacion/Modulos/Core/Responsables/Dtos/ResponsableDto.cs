using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semop.Aplicacion.Modulos.Core.Responsables.Dtos
{
    public class ResponsableDto : PersonaDto
    {
        public DateTime FechaAsignacion { get; set; }
    }
}
