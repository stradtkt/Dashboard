using Microsoft.EntityFrameworkCore;
 
namespace Dashboard.Models
{
    public class DashContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DashContext(DbContextOptions<DashContext> options) : base(options) { }

        public DbSet<User> users {get;set;}
        public DbSet<Message> messages {get;set;}
        public DbSet<Comment> comments {get;set;}
        public DbSet<Product> products {get;set;}
        public DbSet<Order> orders {get;set;}
        public DbSet<Category> categories {get;set;}
        public DbSet<OrdersProducts> orders_has_products {get;set;}
        public DbSet<ProductsCategories> products_has_categories  {get;set;}
    }
}