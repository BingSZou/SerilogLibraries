using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;

namespace SerilogTestConsole
{
    public class Customer
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TotalReturns { get; set; }
    }
    public class CustomerDbConfiguration// : DbConfiguration
    {

    }
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {
         //   this.use
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
