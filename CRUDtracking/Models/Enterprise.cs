using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDtracking.Models
{
    public class Enterprise
    {
        public Enterprise()
        {
            Name = "";
            Active = "";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Address { get; set; }
        public string Active { get; set; }
        public int Reseller { get; set; }
    }
}
