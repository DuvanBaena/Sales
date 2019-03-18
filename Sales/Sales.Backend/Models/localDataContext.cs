namespace Sales.Backend.Models
{
    using Domain.Models;

    public class LocalDataContext   : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Common.Moldes.Product> Products { get; set; }
    }
}