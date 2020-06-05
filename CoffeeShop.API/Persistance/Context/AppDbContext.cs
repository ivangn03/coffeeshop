using CoffeeShop.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.API.Persistance.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coffee> Coffee { get; set; }
        public DbSet<Tea> Tea { get; set; }
        public DbSet<Milk> Milk { get; set; }
        public DbSet<Topping> Topping { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
              #region Roles
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<Role>().HasKey(x=>x.Id);
        builder.Entity<Role>().Property(x =>x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Role>().Property(x=>x.Name).IsRequired().HasMaxLength(24);
        builder.Entity<Role>().HasMany(x=>x.UserRoles).WithOne(x=>x.Role);
        builder.Entity<Role>().HasData(
            new Role{ Id = 1, Name = "admin"},
            new Role{ Id = 2, Name = "customer"},
            new Role{ Id = 3, Name = "staff"}
        );
        #endregion
        #region Users
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30).ValueGeneratedOnAdd();
           // builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasMany(x=>x.UserRoles).WithOne(x=>x.User);
            builder.Entity<User>().HasData
            (
                new User 
                {
                    Id = 1, 
                    FirstName = "John", 
                    LastName= "Dou", 
                    Login="JackHorse",
                    Email="jackhorse@gmail.com",
                    Password="Pass1234",
                },
                new User 
                {
                    Id = 2, 
                    FirstName = "Veronika", 
                    LastName= "Jinton", 
                    Login="Jintonik",
                    Email="jintonik@gmail.com",
                    Password="drinkMore",
                }
            );
    #endregion
      
        #region UserRole
        builder.Entity<UserRole>().ToTable("UserRoles");
        builder.Entity<UserRole>().HasKey(x=> new{ x.UserId, x.RoleId});
        builder.Entity<UserRole>().HasData(
            new UserRole{UserId = 1, RoleId = 1},
            new UserRole{UserId = 1, RoleId = 2},
            new UserRole{UserId = 2, RoleId = 3}
        );
        #endregion
        #region Coffee
        builder.Entity<Coffee>().ToTable("Coffee");
        builder.Entity<Coffee>().HasKey(x=>x.Id);
        builder.Entity<Coffee>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Coffee>().HasData
        (
            new Coffee{Id = 1001, Name="Lavazza", CoffeeType = CoffeeType.Arabica, Price = 32.23, Quantity = 45, Rating = 3.5, Country = "Columbia", Sour = 4.2, Strength = 2.4, Saturation = 3.2, Aroma=1.9, ExpirationDate = new System.DateTime(2021,6,30)},
            new Coffee{Id = 1002, Name="Starbucks", CoffeeType = CoffeeType.Robusta, Price = 35.24, Quantity = 15, Rating = 1.2, Country = "Columbia", Sour = 4.2, Strength = 3.4, Saturation = 5.0, Aroma=5.0, ExpirationDate = new System.DateTime(2021,6,30)},
            new Coffee{Id = 1003, Name="Dallmayr", CoffeeType = CoffeeType.Arabica, Price = 105.4, Quantity = 17, Rating = 4.3, Country = "Columbia", Sour = 2.4, Strength = 2.0, Saturation = 2.4, Aroma=2.4, ExpirationDate = new System.DateTime(2021,12,9)},
            new Coffee{Id = 1004, Name="Café Bom Dia", CoffeeType = CoffeeType.Robusta, Price = 69.24, Quantity = 23, Rating = 5.0, Country = "Columbia", Sour = 3.2, Strength = 4.6, Saturation = 4.4, Aroma=3.6, ExpirationDate = new System.DateTime(2020,11,20)}
        );
        #endregion
        #region Tea
        builder.Entity<Tea>().ToTable("Tea");
        builder.Entity<Tea>().HasKey(x=>x.Id);
        builder.Entity<Tea>().HasData(
            new Tea{Id = 101, Name="Ten Fu Group", TeaType = TeaType.Black, Price = 32.23, Quantity = 45, Rating = 3.5, Country = "China",  ExpirationDate = new System.DateTime(2021,6,30)},
            new Tea{Id = 102, Name="Limtex", TeaType = TeaType.Fruit, Price = 35.24, Quantity = 15, Rating = 1.2, Country = "India",  ExpirationDate = new System.DateTime(2021,6,30)},
            new Tea{Id = 103, Name="TeaGschwendner", TeaType = TeaType.Green, Price = 105.4, Quantity = 17, Rating = 4.3, Country = "Germany",  ExpirationDate = new System.DateTime(2021,12,9)},
            new Tea{Id = 104, Name="Ito En", TeaType = TeaType.Red, Price = 69.24, Quantity = 23, Rating = 5.0, Country = "Japan", ExpirationDate = new System.DateTime(2020,11,20)},
            new Tea{Id = 105, Name="Douwe Egberts (Pickwick)", TeaType = TeaType.White, Price = 209.65, Quantity = 3, Rating = 5.0, Country = "Netherlands", ExpirationDate = new System.DateTime(2025,12,30)}
        );
        #endregion
        #region Milk
        builder.Entity<Milk>().ToTable("Milk");
        builder.Entity<Milk>().HasKey(x=>x.Id);
        builder.Entity<Milk>().HasData(
            new Milk{Id = 101, Name="Nestlé", MilkType = MilkType.Almond, Price = 32.23, Fattiness = 3.2, Quantity = 45, Rating = 3.5,   ExpirationDate = new System.DateTime(2021,6,30)},
            new Milk{Id = 102, Name="Lactalis", MilkType = MilkType.Buckwheat, Price = 35.24, Fattiness = 2.5, Quantity = 15, Rating = 1.2,   ExpirationDate = new System.DateTime(2021,6,30)},
            new Milk{Id = 103, Name="Danone", MilkType = MilkType.Cow, Price = 105.4, Fattiness = 8.4, Quantity = 17, Rating = 4.3,  ExpirationDate = new System.DateTime(2021,12,9)},
            new Milk{Id = 104, Name="Fonterra", MilkType = MilkType.Cow, Price = 69.24, Fattiness = 0.3, Quantity = 23, Rating = 5.0,  ExpirationDate = new System.DateTime(2020,11,20)},
            new Milk{Id = 105, Name="FrieslandCampina", MilkType = MilkType.Almond, Fattiness = 2.1, Price = 209.65, Quantity = 3, Rating = 5.0, ExpirationDate = new System.DateTime(2025,12,30)}
        );
        #endregion
          #region Topping
        builder.Entity<Topping>().ToTable("Topping");
        builder.Entity<Topping>().HasKey(x=>x.Id);
        builder.Entity<Topping>().HasData(
            new Topping{Id = 101, Name="Lolip", Price = 32.23,  Quantity = 45, Rating = 3.5,   ExpirationDate = new System.DateTime(2021,6,30)},
            new Topping{Id = 102, Name="CrackRick",  Price = 35.24,  Quantity = 15, Rating = 1.2,   ExpirationDate = new System.DateTime(2021,6,30)},
            new Topping{Id = 103, Name="KitKat",  Price = 105.4, Quantity = 17, Rating = 4.3,  ExpirationDate = new System.DateTime(2021,12,9)},
            new Topping{Id = 104, Name="Jack", Price = 69.24,  Quantity = 23, Rating = 5.0,  ExpirationDate = new System.DateTime(2020,11,20)},
            new Topping{Id = 105, Name="Salt caramel", Price = 209.65, Quantity = 3, Rating = 5.0, ExpirationDate = new System.DateTime(2025,12,30)}
        );
        #endregion
        }
    }
}