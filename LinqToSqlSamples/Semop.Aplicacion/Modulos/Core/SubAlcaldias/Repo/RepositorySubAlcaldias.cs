using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Aplicacion;
using Semop.Data;
using Semop.Data.Model;
using Semop.Data.Modulos.Core.SubAlcaldias;

namespace Semop.Aplicacion.Modulos.Core.SubAlcaldias.Repo
{
    public class RepositorySubAlcaldias : IRepositorySubAlcaldias
    {
        private readonly UnitOfWorkSimple _unitOfWorkSimple;
        public RepositorySubAlcaldias(IUnitOfWork unitOfWork)
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
        public SubAlcaldia Obtener(int id)
        {
            SubAlcaldia result = null;
            try
            {
                var entidad = (from ele in _unitOfWorkSimple.SubAlcaldias
                              where ele.IdSubAlcaldia == id
                              select ele).ToList().FirstOrDefault();

                result = entidad;
                return result;

            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return result;

        }
        public bool Guardar(SubAlcaldia alcaldia)
        {
            bool result;
            try
            {
                _unitOfWorkSimple.SubAlcaldias.InsertOnSubmit(alcaldia);
                _unitOfWorkSimple.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public IEnumerable<SubAlcaldia> Listar()
        {
            IEnumerable<SubAlcaldia> result = null;
            try
            {
                var list = (from ele in _unitOfWorkSimple.SubAlcaldias
                               select ele);
                result = list;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return result;
        }
        public bool Eliminar(int[] ids)
        {
            bool result;
            try
            {
                var entities = from ele in _unitOfWorkSimple.SubAlcaldias
                               where ids.Contains(ele.IdSubAlcaldia)
                               select ele;
                _unitOfWorkSimple.SubAlcaldias.DeleteAllOnSubmit(entities);
                _unitOfWorkSimple.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                result = false;
            }
            return result;
        }
        public void Dispose()
        {
            _unitOfWorkSimple.Dispose();
        }

    }
}
