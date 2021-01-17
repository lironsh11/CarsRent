using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManager;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("Policy1")]
    public class RentInfoController : ControllerBase
    {
        //declare the db manager 
        DBManager WebDb = new DBManager();
        //get list of RentInfo
        // GET: api/RentInfo
        [HttpGet]
        public IActionResult GetAllRents()
        {
            try
            {
                return Ok(WebDb.GetRentInfo());
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get rent list - error: " + ex.Message);
            }
        }
        //get list of RentInfo by rent id
        // GET: api/RentInfo/5
        [HttpGet("{id}", Name = "GetRentInfo")]
        public ActionResult<RentInfo> GetRentById(int id)
        {
            RentInfo result;
            try
            {
                result = WebDb.GetRentInfo().FirstOrDefault(RentInfo => RentInfo.RentId == id);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get rent info  for id: " + id + " error: " + ex.Message);
            }
            return NotFound();
        }
        //create a new RentInfo
        // POST: api/RentInfo
        [HttpPost]
        public IActionResult CreateRentInfo(RentInfo value)
        {
            try
            {
                if (value != null)
                {
                    WebDb.AddRentInfo(value);
                    WebDb.SaveChanges();
                    return Created("rentinfo/" + value.RentId, value);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't post  rent info  - error: " + ex.Message);
            }
            return NotFound();
        }

        //edit the RentInfo CarsType by id
        // PUT: api/RentInfo/5
        [HttpPut]
        [Route("{id}/{value}")]
        public ActionResult<RentInfo> UpdateRentInfo(int id, string value)
        {
            RentInfo CurrentRent;
            try
            {
                CurrentRent = WebDb.GetRentInfo().FirstOrDefault(RentInfo => RentInfo.RentId == id);

                if (CurrentRent != null)
                {
                    CurrentRent.RealReturnDate = value;
                    WebDb.UpdateRentInfo(CurrentRent);
                    WebDb.SaveChanges();
                    return Ok(CurrentRent);
                }
            }
            catch (Exception ex)
            {

                return NotFound("Couldn't edit  rent info  - error: " + ex.Message);
            }
            return NotFound();
        }


        //delete the current RentInfo by id
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<RentInfo> DeleteRentInfo(int id)
        {
            RentInfo CurrentRent;
            try
            {
                CurrentRent = WebDb.GetRentInfo().FirstOrDefault(RentInfo => RentInfo.RentId == id);
                if (CurrentRent != null)
                {
                    WebDb.DeleteRentInfo(CurrentRent);
                    WebDb.SaveChanges();
                    return Ok(CurrentRent);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't delete  rent info  -  error: " + ex.Message);
            }
            return NotFound();
        }
    }
}
