namespace Sales.Domain.Models
{
     using System.Data.Entity;
     using Common.Moldes;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
