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
    public class CarsInfoController : ControllerBase
    {
        //declare the db manager 
        DBManager WebDb = new DBManager();
        //join 2 tables and get list of all cars info and type
        // GET: api/CarsInfo
        [HttpGet]
        public ActionResult<object> GetAllCars()
        {
            try
            {
                var JoinedCarsTables = (from CarsType in WebDb.GetCarsType()
                                        join CarsInfo in WebDb.GetCarsInfo() on CarsType.CarId equals CarsInfo.CarId into NewList
                                        from NewCarsInfo in NewList.DefaultIfEmpty()
                                        select new
                                        {
                                            CarsType.CarId,
                                            CarsType.Model,
                                            CarsType.Manufacturer,
                                            CarsType.ManufacturerYear,
                                            CarsType.DayValue,
                                            CarsType.LateDayValue,
                                            CarsType.GearType,
                                            NewCarsInfo.CarType,
                                            NewCarsInfo.AvailableForRent,
                                            NewCarsInfo.ProperForRent,
                                            NewCarsInfo.Picture,
                                            NewCarsInfo.CurrectMileage,
                                        }).ToList(); ;
                return Ok(JoinedCarsTables);
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get cars list - error: " + ex.Message);
            }
        }
        //get list of cars info by car id
        // GET: api/CarsInfo/5
        [HttpGet("{id}", Name = "GetCarsInfo")]
        public ActionResult<CarsInfo>  GetCarById(int id)
        {
            CarsInfo result;
            try
            {
                result = WebDb.GetCarsInfo().FirstOrDefault(CarInfo => CarInfo.CarId == id);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get car info for id: "+ id + " error: "+ ex.Message);
            }
            return NotFound(result);
        }

        //create a new CarsInfo
        // POST: api/CarsInfo
        [HttpPost]
        public ActionResult<CarsInfo> CreateCarInfo([FromBody] CarsInfo value)
        {
            try
            {
                if (value != null)
                {
                    WebDb.AddCarsInfo(value);
                    WebDb.SaveChanges();
                    return Ok(value);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't post  car info - error: " + ex.Message);
            }
            return NotFound();
        }

        //edit the current CarsInfo by id
        // PUT: api/CarsInfo/5
        [HttpPut][Route("{id}/{value}")]
        public ActionResult<CarsInfo> UpdateCarInfo(int id, string value)
        {
            CarsInfo CurrentCar;
            try
            {
                CurrentCar = WebDb.GetCarsInfo().FirstOrDefault(CarInfo => CarInfo.CarId == id);
                if (CurrentCar != null)
                {
                    CurrentCar.AvailableForRent = value;
                    WebDb.UpdateCarsInfo(CurrentCar);
                    WebDb.SaveChanges();
                    return Ok(CurrentCar);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't edit  car info - error: " + ex.Message);
            }
            return NotFound();
        }
        //delete the current CarsInfo by id
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<CarsInfo> DeleteCarInfo(int id)
        {
            CarsInfo CurrentCar;
            try
            {
                CurrentCar = WebDb.GetCarsInfo().FirstOrDefault(CarInfo => CarInfo.CarId == id);
                if (CurrentCar != null)
                {
                    WebDb.DeleteCarsInfo(CurrentCar);
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
