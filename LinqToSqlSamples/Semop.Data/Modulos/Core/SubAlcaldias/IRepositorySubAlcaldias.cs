using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Data.Model;

namespace Semop.Data.Modulos.Core.SubAlcaldias
{
    public interface IRepositorySubAlcaldias
    {
        SubAlcaldia Obtener(int id);
        bool Guardar(SubAlcaldia alcaldia);
        IEnumerable<SubAlcaldia> Listar();        
        bool Eliminar(int[] ids);
    }
}
