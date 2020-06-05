using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Token {get; set;}
        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}