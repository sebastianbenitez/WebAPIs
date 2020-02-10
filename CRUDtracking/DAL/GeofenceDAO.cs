using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CRUDtracking.DAL.DTO;

namespace CRUDtracking.DAL
{
    public class GeofenceDAO : DAOBase
    {
        private GeofenceDTO GetGeofenceDTO(SqlDataReader reader)
        {
            return new GeofenceDTO
            {
                Id = reader[reader.GetOrdinal("g.id")] as int? ?? default,
                Name = reader[reader.GetOrdinal("g.name")] as string,
                Description = reader[reader.GetOrdinal("g.description")] as string,
                Latitude = reader[reader.GetOrdinal("g.latitude")] as double? ?? default,
                Longitude = reader[reader.GetOrdinal("g.longitude")] as double? ?? default,
                Radius = reader[reader.GetOrdinal("g.radius")] as int? ?? default,
                Active = reader[reader.GetOrdinal("g.active")] as string,
                Enterprise =
                {
                    Id = reader[reader.GetOrdinal("e.id")] as int? ?? default,
                    Name = reader[reader.GetOrdinal("e.name")] as string,
                    Address = reader[reader.GetOrdinal("e.address")] as int? ?? default,
                    Active = reader[reader.GetOrdinal("e.active")] as string,
                    Reseller = reader[reader.GetOrdinal("e.reseller")] as int? ?? default
                },
                Geofencetype =
                {
                    Id = reader[reader.GetOrdinal("gt.id")] as int? ?? default,
                    Name = reader[reader.GetOrdinal("gt.name")] as string,
                    Icon = reader[reader.GetOrdinal("gt.icon")] as string,
                    Colour = reader[reader.GetOrdinal("gt.colour")] as string,
                    Active = reader[reader.GetOrdinal("gt.active")] as string
                }
            };
        }

        private string selectAllQuery = @"
            select g.id 'g.id', g.name 'g.name', g.description 'g.description', g.latitude 'g.latitude',
	            g.longitude 'g.longitude', g.radius 'g.radius', g.active 'g.active',
	            e.id 'e.id', e.name 'e.name', e.address 'e.address', e.active 'e.active', e.reseller 'e.reseller',
	            gt.id 'gt.id', gt.name 'gt.name', gt.icon 'gt.icon', gt.colour 'gt.colour', gt.active 'gt.active'
            from geofence g
	            left join enterprise e on g.enterpriseid = e.id
	            left join geofencetype gt on g.geofencetypeid = gt.id
        ";

        public IEnumerable<GeofenceDTO> GetAll()
        {
            var result = new List<GeofenceDTO>();
            var reader = GetReader(selectAllQuery);
            while (reader.Read())
            {
                result.Add(GetGeofenceDTO(reader));
            }
            reader.Close();
            return result;
        }

        public GeofenceDTO GetById(int id)
        {
            var result = new GeofenceDTO();
            var query = @$"{selectAllQuery}
                            where g.id = {id}";
            var reader = GetReader(query);
            if (reader.HasRows)
            {
                reader.Read();
                result = GetGeofenceDTO(reader);
            }
            reader.Close();
            return result;
        }

        public int Insert(GeofenceDTO geofence)
        {
            var query = @$"insert into geofence(id,name,description,latitude,longitude,radius,enterpriseid,geofencetypeid,active)
                          select max(id)+1,'{geofence.Name}','{geofence.Description}',{geofence.Latitude},
                            {geofence.Longitude},{geofence.Radius},{geofence.Enterprise.Id},
                            {geofence.Geofencetype.Id},'{geofence.Active}' 
		                  from geofence";
            return GetNonQueryResponse(query);
        }

        public int Update(GeofenceDTO geofence)
        {
            var query = @$"update geofence
                           set name='{geofence.Name}',description='{geofence.Description}',
                                latitude={geofence.Latitude},longitude={geofence.Longitude},
                                radius={geofence.Radius},enterpriseid={geofence.Enterprise.Id},
                                geofencetypeid={geofence.Geofencetype.Id},active='{geofence.Active}'
		                   where id={geofence.Id}";
            return GetNonQueryResponse(query);
        }

        public int Delete(GeofenceDTO geofence)
        {
            var query = @$"delete geofence
		                   where id={geofence.Id}";
            return GetNonQueryResponse(query);
        }
    }
}
