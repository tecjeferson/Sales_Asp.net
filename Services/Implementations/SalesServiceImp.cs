using System;
using System.Collections.Generic;
using System.Threading;
using aspnetapp.Models;
using System.Linq;

namespace aspnet.Services.Implementations
{
    public class SalesServiceImp : ISalesService
    {
        private SalesContext _context;
        public SalesServiceImp(SalesContext context)
        {
            _context = context;


            if (_context.SalesData.Count() == 0)
            {
                _context.SalesData.AddRange(new Sales
                {
                    CustomerName = "Aveva Inc.",
                    Salesperson = "Charlie",
                    hasPayment = true,
                    Price = 199
                }, new Sales
                {
                    CustomerName = "Netflix",
                    Salesperson = "Doug",
                    hasPayment = false,
                    Price = 13
                });
                _context.SaveChanges();
            }
            
        }

        public Sales Create(Sales sales)
        {
            try
            {
                _context.Add(sales);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sales;
        }

        public void Delete(long id)
        {
            var result = _context.SalesData.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.SalesData.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Trocar aqui no 8 pelo lenght do dado _context

        public List<Sales> FindAll()
        {
            return _context.SalesData.ToList();
        }



        public Sales FindById(long? id)
        {
            return _context.SalesData.SingleOrDefault(p => p.Id.Equals(id));

        }

        public Sales Update(Sales sales)
        {

            if (!Exist(sales.Id)) return new Sales();

            var result = _context.SalesData.SingleOrDefault(p => p.Id.Equals(sales.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(sales);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sales;



        }

        private bool Exist(long? id)
        {
            return _context.SalesData.Any(p => p.Id.Equals(id));
        }
    }
}