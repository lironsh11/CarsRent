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
    public class CarsTypeController : ControllerBase
    {
        //declare the db manager 
        DBManager WebDb = new DBManager();
        //get list of CarsTypes
        // GET: api/CarsType
        [HttpGet]
        public ActionResult<CarsType> GetAllCars()
        {
            try
            {
                return Ok(WebDb.GetCarsType());
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get cars list - error: " + ex.Message);
            }
        }
        //get list of CarsTypes by car id
        // GET: api/CarsType/5
        [HttpGet("{id}", Name = "GetCarsType")]
        public ActionResult<CarsType> GetCarById(string model)
        {
            CarsType result;
            try
            {
                result = WebDb.GetCarsType().FirstOrDefault(CarType => CarType.Model == model);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get car  for model: " + model + " error: " + ex.Message);
            }
            return NotFound();
        }
        //create a new CarsType
        // POST: api/CarsType
        [HttpPost]
        public ActionResult<CarsType> CreateCarType([FromBody] CarsType value)
        {
            try
            {
                if (value != null)
                {
                    WebDb.AddCarsType(value);
                    WebDb.SaveChanges();
                    return Ok(value);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't post  car  - error: " + ex.Message);
            }
            return NotFound();
        }
        //edit the current CarsType by id
        // PUT: api/CarsType/5

        [HttpPut]
        [Route("{id}/{carid}/{Model}/{Manufacturer}/{ManufacturerYear}/{GearType}/{DayValue}/{LateDayValue}")]
        public ActionResult<CarsType> UpdateCarType(int id, int carid, string Model, string Manufacturer, string ManufacturerYear, string GearType, string DayValue, string LateDayValue)
        {
            CarsType CurrentCar;
            try
            {
                CurrentCar = WebDb.GetCarsType().FirstOrDefault(CarType => CarType.CarId == id);
                if (CurrentCar != null)
                {
                    CurrentCar.CarId = carid;
                    CurrentCar.Model = Model;
                    CurrentCar.Manufacturer = Manufacturer;
                    CurrentCar.ManufacturerYear = ManufacturerYear;
                    CurrentCar.GearType = GearType;
                    CurrentCar.DayValue = DayValue;
                    CurrentCar.LateDayValue = LateDayValue;
                    WebDb.UpdateCarsType(CurrentCar);
                    WebDb.SaveChanges();
                    return Ok(CurrentCar);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't edit  car  - error: " + ex.Message);
            }
            return NotFound();
        }

        //delete the current CarsType by id
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<CarsType> DeleteCarType(int id)
        {
            CarsType CurrentCar;
            try
            {
                CurrentCar = WebDb.GetCarsType().FirstOrDefault(CarType => CarType.CarId == id);
                if (CurrentCar != null)
                {
                    WebDb.DeleteCarsType(CurrentCar);
                    WebDb.SaveChanges();
                    return Ok(CurrentCar);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't delete  car  -  error: " + ex.Message);
            }
            return NotFound();
        }
    }
}
