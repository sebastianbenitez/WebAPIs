using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using CRUDtracking.DAL;
using CRUDtracking.DAL.DTO;
//using CRUDtracking.Models;

namespace CRUDtracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeofenceController : ControllerBase
    {
        private GeofenceDAO geofenceDAO;
        public GeofenceController()
        {
            geofenceDAO = new GeofenceDAO();
        }

        [HttpGet]
        public IEnumerable<GeofenceDTO> Get()
        {
            return geofenceDAO.GetAll();
        }

        [HttpGet("{id}")]
        public GeofenceDTO Get(int id)
        {
            return geofenceDAO.GetById(id);
        }

        [HttpPost]
        public ActionResult Post([FromBody] GeofenceDTO geofence)
        {
            try
            {
                var resp = geofenceDAO.Insert(geofence);
                return Ok(resp <= 0 ? "Ningun registro Afectado" : "Insercion exitosa");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al insertar: {e.Message}");
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] GeofenceDTO geofence)
        {
            try
            {
                var resp = geofenceDAO.Update(geofence);
                return Ok(resp <= 0 ? "Ningun registro Afectado" : "Edicion exitosa");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al editar: {e.Message}");
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] GeofenceDTO geofence)
        {
            try
            {
                var resp = geofenceDAO.Delete(geofence);
                return Ok(resp <= 0 ? "Ningun registro Afectado" : "Eliminacion exitosa");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar: {e.Message}");
            }
        }
    }
}
