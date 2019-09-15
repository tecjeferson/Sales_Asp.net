using System.Collections.Generic;
using aspnetapp.Models;

namespace aspnet.Services
{
    public interface ISalesPersonService
    {
        SalesPerson Create(SalesPerson salesperson);
        SalesPerson FindById(long? id);
        List<SalesPerson> FindAll();
        SalesPerson Update(SalesPerson salesperson);
        void Delete(long id);

    }
}