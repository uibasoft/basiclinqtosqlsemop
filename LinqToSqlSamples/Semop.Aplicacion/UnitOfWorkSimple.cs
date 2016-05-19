using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Semop.Data;
using Semop.Data.Model;

namespace Semop.Aplicacion
{
    public class UnitOfWorkSimple : AlcaldiaContextDbDataContext, IUnitOfWork
    {
        public UnitOfWorkSimple(string stringConexion): base(stringConexion)
        {
                        
        }
        public UnitOfWorkSimple(IDbConnection dbConnection) : base(dbConnection)
        {

        }
    }
}
