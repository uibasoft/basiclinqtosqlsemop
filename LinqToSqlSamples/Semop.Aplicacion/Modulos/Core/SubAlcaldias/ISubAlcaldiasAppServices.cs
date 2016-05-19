using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Aplicacion.Modulos.Core.SubAlcaldias.Dto;
using PagedList;

namespace Semop.Aplicacion.Modulos.Core.SubAlcaldias
{
    public interface ISubAlcaldiasAppServices
    {
        SubAlcaldiaDto Obtener(int id);
        bool Guardar(SubAlcaldiaDto dto);
        bool Editar(SubAlcaldiaDto dto);
        bool Eliminar(int[] ids);
        List<SubAlcaldiaDto> Listar(string nombre, string direccion, int? pageIndex, int pageSize);
    }
}
