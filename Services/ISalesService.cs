using System.Collections.Generic;
using aspnetapp.Models;

namespace aspnet.Services
{
    public interface ISalesService
    {
        Sales Create(Sales sales);
        Sales FindById(long? id);
        List<Sales> FindAll();
        Sales Update(Sales sales);
        void Delete(long id);
    }
}