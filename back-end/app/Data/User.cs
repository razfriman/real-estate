using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string Email { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Property> Properties { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageURL { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
