using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CRUDtracking.Models;

namespace CRUDtracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeofenceController : ControllerBase
    {
        private SqlConnection getConnection()
        {
            return new SqlConnection("data source=104.217.253.86;initial catalog=Tracking;user id=alumno;password=12345678");
        }

        [HttpGet]
        public IEnumerable<Geofence> Get()
        {
            var result = new List<Geofence>();

            var connection = getConnection();
            connection.Open();
            var query = @"select id,isnull(name,''),isnull(description,''), isnull(latitude,0),
                            isnull(longitude,0), isnull(radius,0), isnull(enterpriseid,0),
                            isnull(geofencetypeid,0), isnull(active,'') 
		                  from geofence";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(
                    new Geofence
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Latitude = reader.GetDouble(3),
                        Longitude = reader.GetDouble(4),
                        Radius = reader.GetInt32(5),
                        Enterpriseid = reader.GetInt32(6),
                        Geofencetypeid = reader.GetInt32(7),
                        Active = reader.GetString(8)
                    }
                );
            }
            reader.Close();
            connection.Close();
            
            return result;
        }

        [HttpGet("{id}")]
        public Geofence Get(int id)
        {
            var result = new Geofence();

            var connection = getConnection();
            connection.Open();
            var query = @$"select id, isnull(name,''), isnull(description,''), isnull(latitude,0),
                            isnull(longitude,0), isnull(radius,0), isnull(enterpriseid,0),
                            isnull(geofencetypeid,0), isnull(active,'')
		                  from geofence
                          where id = {id}";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                result.Id = reader.GetInt32(0);
                result.Name = reader.GetString(1);
                result.Description = reader.GetString(2);
                result.Latitude = reader.GetDouble(3);
                result.Longitude = reader.GetDouble(4);
                result.Radius = reader.GetInt32(5);
                result.Enterpriseid = reader.GetInt32(6);
                result.Geofencetypeid = reader.GetInt32(7);
                result.Active = reader.GetString(8);
            }
            reader.Close();
            connection.Close();

            return result;
        }

        [HttpPost]
        public OkObjectResult Post([FromBody] Geofence geofence)
        {
            var connection = getConnection();
            connection.Open();
            var query =@$"insert into geofence(id,name,description,latitude,longitude,radius,enterpriseid,geofencetypeid,active)
                          select max(id)+1,'{geofence.Name}','{geofence.Description}',{geofence.Latitude},
                            {geofence.Longitude},{geofence.Radius},{geofence.Enterpriseid},
                            {geofence.Geofencetypeid},'{geofence.Active}' 
		                  from geofence";
            var command = new SqlCommand(query, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return Ok("No se pudo insertar: " + e.ToString());
            }
            connection.Close();
            return Ok("Insertado");
        }

        [HttpPut]
        public OkObjectResult Put([FromBody] Geofence geofence)
        {
            var connection = getConnection();
            connection.Open();
            var query = @$"update geofence
                           set name='{geofence.Name}',description='{geofence.Description}',
                                latitude={geofence.Latitude},longitude={geofence.Longitude},
                                radius={geofence.Radius},enterpriseid={geofence.Enterpriseid},
                                geofencetypeid={geofence.Geofencetypeid},active='{geofence.Active}'
		                   where id={geofence.Id}";
            var command = new SqlCommand(query, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return Ok("No se pudo editar: " + e.ToString());
            }
            connection.Close();
            return Ok("Editado exitosamente");
        }

        [HttpDelete("{id}")]
        public OkObjectResult Delete(int id)
        {
            var connection = getConnection();
            connection.Open();
            var query = @$"delete geofence
		                   where id={id}";
            var command = new SqlCommand(query, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return Ok("No se pudo borrar: " + e.ToString());
            }
            connection.Close();
            return Ok("Borrado exitosamente");
        }
    }
}
