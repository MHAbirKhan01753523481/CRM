using ProjectMaking.Data;
using ProjectMaking.Helper;
using ProjectMaking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
      
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
           
        }

    
        private IWorkTaskRepository _WorkTask;
        private IClientRepository _client;
        public IManagementRepository _Managers;

        public IProductRepository _Products;

        public ISalesRepository _Sales;

        public ISeheduleRepository _Sehedules;

        public IServiceRepository _Service;

        public ITradeRepository _Trade;

        public IAccountingRepository _Accounting;

        public ICategoryRepository _Category;

        private GetDropdownList _Dropdown;

        private DataTable _dataTable;
     
        public IWorkTaskRepository WorkTask {

            get
            {
                return _WorkTask = _WorkTask ?? new WorkTaskRepository(_context);
            }
        
        }

        public ISalesRepository Sales
        {
            get
            {
                return _Sales = _Sales ?? new SalesRepository(_context);
            }
        }
        public IManagementRepository Managers
        {
            get
            {
                return _Managers = _Managers ?? new ManagementRepository(_context);
            }
        }
        public ICategoryRepository Category
        {
            get
            {
                return _Category = _Category ?? new CategoryRepository(_context);
            }
        }
        public ITradeRepository Trade
        {
            get
            {
                return _Trade = _Trade ?? new TradeRepository(_context);
            }
        }
        public ISeheduleRepository Sehedules
        {
            get
            {
                return _Sehedules = _Sehedules ?? new SeheduleRepository(_context);
            }
        }
        public IServiceRepository Service
        {
            get
            {
                return _Service = _Service ?? new ServiceRepository(_context);
            }
        }
        public IProductRepository Products
        {
            get
            {
                return _Products = _Products ?? new ProductRepository(_context);
            }
        }
    
        public IAccountingRepository Accounting
        {
            get
            {
                return _Accounting = _Accounting ?? new AccountingRepository(_context);

            }
        }
        public IClientRepository Clients
        {
            get
            {
                return _client = _client ?? new ClientRepository(_context);
            }
        }
        public GetDropdownList dropdown 
        {
            get
            {
                return _Dropdown = _Dropdown ?? new GetDropdownList();
            }
        
        }

        public DataTable dataTableList 
        {

            get
            {
                return _dataTable = _dataTable ?? new DataTable(_context);
            }
        
        }


        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
