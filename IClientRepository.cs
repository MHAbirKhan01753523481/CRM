using ProjectMaking.BaseManager;
using ProjectMaking.Models.EntityModel;
using ProjectMaking.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public interface IClientRepository : IBaseManager<Client>
    {
        List<vmClient> GetAllClient();
        dynamic TotalLead();
        dynamic LostLead();
        dynamic ActiveLead();
        List<Client> LeadManager();
    }
}
