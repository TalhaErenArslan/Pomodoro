using System;
using System.ComponentModel;

namespace app.Models
{
    public class User
    {
        // public int Id { get; set; }
        public string ?Username { get; set; }
        public string ?Name { get; set; }
        public string ?Surname { get; set; }
        public string ?Password { get; set; }
    }
}
