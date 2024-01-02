using System;
using System.ComponentModel;

namespace app.Models
{
    public class Todo
    {
        public int userId { get; set; }
        public string ?task { get; set; }
        public TimeSpan ?time { get; set; }
    }
}
