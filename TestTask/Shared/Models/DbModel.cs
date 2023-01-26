using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Shared.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; } = default!;
        [Required, StringLength(150)]
        public string CustomerAddress { get; set; } = default!;
        [Required, StringLength(20)]
        public string Contact { get; set; } = default!;
        [Required, StringLength(50)]
        public string Email { get; set; } = default!;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        [Required, StringLength(50)]
        public string PaymentMethodName { get; set; } = default!;
        public bool Active { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
    public class Order
    {
        public int OrderId { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public Payment Payment { get; set; } = default!;

    }

    public class Payment
    {
        public int PaymentId { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime PaymentTime { get; set; }
        [Required, ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }= default!;
        public PaymentMethod PaymentMethod { get; set; }=default!;
    }
    /// <summary>
    /// //////////////////////
    /// PizzaSpecial
    /// </summary>
    public class PizzaSpecial
    {
        public int PizzaSpecialId { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal BasePrice { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;
        [Required, StringLength(150)]
        public string Description { get; set; } = default!;
        [Required, StringLength(350)]
        public string ImageUrl { get; set; } = default!;
    }
    public class Topping
    {
        public int ToppingId { get; set;}
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set;} 
    }
    public class PizzaAppDbContext : DbContext
    {
        public PizzaAppDbContext(DbContextOptions<PizzaAppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(a => a.Payment)
                .WithOne(b => b.Order)
                .HasForeignKey<Payment>(b => b.OrderId);
        }
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PizzaSpecial> PizzaSpecials { get; set; } = default!;
        public DbSet<Topping> Toppings { get; set; } = default!;
    }

}
