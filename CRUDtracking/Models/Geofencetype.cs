using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDtracking.Models
{
    public class Geofencetype
    {
        public Geofencetype()
        {
            Name = "";
            Icon = "";
            Colour = "rgb(0,0,0)";
            Enterprise = new Enterprise();
            Active = "";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Colour { get; set; }
        public int Enterpriseid { get; set; }
        public Enterprise Enterprise { get; set; }
        public string Active { get; set; }
    }
}
