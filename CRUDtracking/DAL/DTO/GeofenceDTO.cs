using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDtracking.DAL.DTO
{
    public class GeofenceDTO
    {
        public GeofenceDTO()
        {
            Name = "";
            Description = "";
            Active = "";
            Enterprise = new EnterpriseDTO();
            Geofencetype = new GeofencetypeDTO();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; }
        public int Enterpriseid { get; set; }
        public int Geofencetypeid { get; set; }
        public EnterpriseDTO Enterprise { get; set; }
        public GeofencetypeDTO Geofencetype { get; set; }
        public string Active { get; set; }
    }
}
