using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class Contact
    {
        public long ContactId { get; set; }

        public string Name { get; set; } = default!;

        public string Telephone { get; set; } = default!;
    }
}
