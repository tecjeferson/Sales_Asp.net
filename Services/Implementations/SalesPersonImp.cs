using System;
using System.Collections.Generic;
using System.Threading;
using aspnetapp.Models;
using System.Linq;

namespace aspnet.Services.Implementations
{
    public class SalesPersonImp : ISalesPersonService
    {

        public SalesContext _contextSalesperson;
        public SalesPersonImp(SalesContext context)
        {
            _contextSalesperson = context;

            if (_contextSalesperson.SalesData.Count() == 0)
            {
                _contextSalesperson.AddRange(new SalesPerson
                {
                    Salesperson = "Charlie"
                }, new SalesPerson
                {
                    Salesperson = "Doug",
                });
                _contextSalesperson.SaveChanges();
            }

        }

        public SalesPerson Create(SalesPerson salesperson)
        {
            try
            {
                _contextSalesperson.SalespersonData.Add(salesperson);
                _contextSalesperson.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return salesperson;
        }


        public void Delete(long id)
        {
            var result = _contextSalesperson.SalespersonData.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _contextSalesperson.Remove(result);
                    _contextSalesperson.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SalesPerson> FindAll()
        {
            return _contextSalesperson.SalespersonData.ToList();
        }

        public SalesPerson FindById(long? id)
        {
            return _contextSalesperson.SalespersonData.SingleOrDefault(p => p.Id.Equals(id));
        }

        public SalesPerson Update(SalesPerson salesperson)
        {
            if (!Exist(salesperson.Id)) return new SalesPerson();
            var result = _contextSalesperson.SalespersonData.SingleOrDefault(p => p.Id.Equals(salesperson.Id));
            try
            {
                _contextSalesperson.Entry(result).CurrentValues.SetValues(salesperson);
                _contextSalesperson.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw ex;
            }
            return salesperson;
        }

        private bool Exist(long? id)
        {
            return _contextSalesperson.SalespersonData.Any(p => p.Id.Equals(id));
        }
    }
}