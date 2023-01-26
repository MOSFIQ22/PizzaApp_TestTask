using Microsoft.VisualBasic;
using TestTask.Shared.Models;

namespace TestTask.Server.HostedServices
{
    public class PizzaAppDbDataSeeder
    {
        private readonly PizzaAppDbContext db;
        public PizzaAppDbDataSeeder(PizzaAppDbContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }
        public async Task SeedAsync()
        {
            var toppings = new Topping[]
            {
            new Topping()
            {
                    Name = "Extra cheese",
                    Price = 2.50m,
            },
            new Topping()
            {
                    Name = "American bacon",
                    Price = 2.99m,
            },
            new Topping()
            {
                    Name = "British bacon",
                    Price = 2.99m,
            },
            new Topping()
            {
                    Name = "Canadian bacon",
                    Price = 2.99m,
            },
            new Topping()
            {
                    Name = "Tea and crumpets",
                    Price = 5.00m
            },
            new Topping()
            {
                    Name = "Fresh-baked scones",
                    Price = 4.50m,
            },
            new Topping()
            {
                    Name = "Bell peppers",
                    Price = 1.00m,
            },
            new Topping()
            {
                    Name = "Onions",
                    Price = 1.00m,
            },
            new Topping()
            {
                    Name = "Mushrooms",
                    Price = 1.00m,
            },
            new Topping()
            {
                    Name = "Pepperoni",
                    Price = 1.00m,
            },
            new Topping()
            {
                    Name = "Duck sausage",
                    Price = 3.20m,
            },
            new Topping()
            {
                    Name = "Venison meatballs",
                    Price = 2.50m,
            },
            new Topping()
            {
                    Name = "Served on a silver platter",
                    Price = 250.99m,
            },
            new Topping()
            {
                    Name = "Lobster on top",
                    Price = 64.50m,
            },
            new Topping()
            {
                    Name = "Sturgeon caviar",
                    Price = 101.75m,
            },
            new Topping()
            {
                    Name = "Artichoke hearts",
                    Price = 3.40m,
            },
            new Topping()
            {
                    Name = "Fresh tomatoes",
                    Price = 1.50m,
            },
            new Topping()
            {
                    Name = "Basil",
                    Price = 1.50m,
            },
            new Topping()
            {
                    Name = "Steak (medium-rare)",
                    Price = 8.50m,
            },
            new Topping()
            {
                    Name = "Blazing hot peppers",
                    Price = 4.20m,
            },
            new Topping()
            {
                    Name = "Buffalo chicken",
                    Price = 5.00m,
            },
            new Topping()
            {
                    Name = "Blue cheese",
                    Price = 2.50m
            }
            };
            var specials = new PizzaSpecial[]
            {
            new PizzaSpecial()
            {
                    Name = "Basic Cheese Pizza",
                    Description = "It's cheesy and delicious. Why wouldn't you want one?",
                    BasePrice = 9.99m,
                    ImageUrl = "cheese.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "The Baconatorizor",
                    Description = "It has EVERY kind of bacon",
                    BasePrice = 11.99m,
                    ImageUrl = "bacon.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "Classic pepperoni",
                    Description = "It's the pizza you grew up with, but Blazing hot!",
                    BasePrice = 10.50m,
                    ImageUrl = "pepperoni.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "Buffalo chicken",
                    Description = "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up",
                    BasePrice = 12.75m,
                    ImageUrl = "meaty.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "Mushroom Lovers",
                    Description = "It has mushrooms. Isn't that obvious?",
                    BasePrice = 11.00m,
                    ImageUrl = "mushroom.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "The Brit",
                    Description = "When in London...",
                    BasePrice = 10.25m,
                    ImageUrl = "brit.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "Veggie Delight",
                    Description = "It's like salad, but on a pizza",
                    BasePrice = 11.50m,
                    ImageUrl = "salad.jpg",
            },
            new PizzaSpecial()
            {

                    Name = "Margherita",
                    Description = "Traditional Italian pizza with tomatoes and basil",
                    BasePrice = 9.99m,
                    ImageUrl = "margherita.jpg",
            },
            };
            //
            PaymentMethod pm1 = new PaymentMethod { PaymentMethodName = "Visa Card", Active = true };
            Customer c1 = new Customer { CustomerName = "Customer 1", Contact = "01710XXXXXX", Email = "cust1@mail.com", CustomerAddress = "Mirpur, Dhaka" };
            Order o1 = new Order {  Customer = c1, OrderDate = DateTime.Now.AddDays(-24 * 10) };
            o1.Payment = new Payment { PaymentMethod = pm1, PaymentTime = DateTime.Now.AddDays(-24 * 10).AddSeconds(70) };
           
            if(!db.Customers.Any())
                await db.PaymentMethods.AddAsync(pm1);
            if (!db.Orders.Any())
               await db.Orders.AddAsync(o1);
            //await db.Payments.AddAsync(p1);
            if (!db.Toppings.Any())
                await db.Toppings.AddRangeAsync(toppings);
            if (!db.PizzaSpecials.Any())
                await db.PizzaSpecials.AddRangeAsync(specials);
           
            
            await db.SaveChangesAsync();
        }
    }
}
