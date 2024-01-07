using System;
using System.ComponentModel;

namespace app.Models
{
    public class Todo
    {
        public string username { get; set; }
        public string ?task { get; set; }
        public TimeSpan ?time { get; set; }
    }
}
