using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDtracking.Models
{
    public class Geofence
    {
        public Geofence()
        {
            Name = "";
            Description = "";
            Active = "";
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Radius { get; set; }

        public int Enterpriseid { get; set; }

        public int Geofencetypeid { get; set; }

        public string Active { get; set; }
        
    }
}
