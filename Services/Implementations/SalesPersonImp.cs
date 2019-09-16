using System;
using System.Collections.Generic;
using System.Threading;
using aspnetapp.Models;
using System.Linq;

namespace aspnet.Services.Implementations
{
    public class SalesPersonImp //: ISalesPersonService
    {

        // public SalesContext _contextSalesperson;
        // public SalesPersonImp(SalesContext context)
        // {
        //     _contextSalesperson = context;



        // }

        // public SalesPerson Create(SalesPerson salesperson)
        // {
        //     try
        //     {
        //         _contextSalesperson.Add(salesperson);
        //         _contextSalesperson.SaveChanges();

        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     return salesperson;
        // }


        // public void Delete(long id)
        // {
        //     var result = _contextSalesperson.SalesData.SingleOrDefault(p => p.Id.Equals(id));
        //     try
        //     {
        //         if (result != null)
        //         {
        //             _contextSalesperson.Remove(result);
        //             _contextSalesperson.SaveChanges();
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         throw e;
        //     }
        // }

        // public List<Sales> FindAll()
        // {
        //     return _contextSalesperson.SalesData.ToList();
        // }

        // public SalesPerson FindById(long? id)
        // {
        //     return _contextSalesperson.SalesData.SingleOrDefault(p => p.Id.Equals(id));
        // }

        // public Sales Update(Sales sales)
        // {
        //     if (!Exist(sales.Id)) return new Sales();
        //     var result = _contextSalesperson.SalesData.SingleOrDefault(p => p.Id.Equals(sales.Id));
        //     try
        //     {
        //         _contextSalesperson.Entry(result).CurrentValues.SetValues(sales);
        //         _contextSalesperson.SaveChanges();
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.Write(ex);
        //         throw ex;
        //     }
        //     return sales;
        // }

        // private bool Exist(long? id)
        // {
        //     return _contextSalesperson.SalesData.Any(p => p.Id.Equals(id));
        // }
    }
}