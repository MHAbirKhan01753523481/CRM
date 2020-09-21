using ProjectMaking.Helper;
using ProjectMaking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.UnitOfWorks
{
   public interface IUnitOfWork:IDisposable
    {
      
        IClientRepository Clients { get; }
        IManagementRepository Managers { get; }
        IProductRepository Products { get; }
        ISalesRepository Sales { get; }
        ISeheduleRepository Sehedules { get; }
        IServiceRepository Service { get; }
        ITradeRepository Trade { get; }
        IAccountingRepository Accounting { get; }
        ICategoryRepository Category { get; }
        IWorkTaskRepository WorkTask { get; }
        GetDropdownList dropdown { get; }
        DataTable dataTableList { get; }
       
        int Save();

    }
}
