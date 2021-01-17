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
    public class UsersController : ControllerBase
    {
        //declare the db manager 
        DBManager WebDb = new DBManager();

        //get list of all users
        // GET: api/Users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(WebDb.GetUsers());
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get users list - error: " + ex.Message);
            }
        }

        //get list of user by id
        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUsers")]
        public IActionResult GetUserById(string id)
        {
            Users CurrentUser;
            try
            {
                CurrentUser = WebDb.GetUsers().FirstOrDefault(User => User.Id == id);
                if (CurrentUser != null)
                {
                    return Ok(CurrentUser);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't get user  for id: " + id + " error: " + ex.Message);
            }
            return NotFound();
        }
        //create a new user

        // POST: api/Users
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] Users value)
        {
            try
            {
                if (value != null)
                {
                    WebDb.AddUsers(value);
                    WebDb.SaveChanges();
                    return Created("Users/" + value.Id, value);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't add  new user - error: " + ex.Message);
            }
            return NotFound();
        }
        //edit the current user by id
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Users value)
        {
            try
            {
                if (value != null)
                {
                    WebDb.UpdateUsers(value);
                    WebDb.SaveChanges();
                    return Ok(value);
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't add  new user - error: " + ex.Message);
            }
            return NotFound();
        }



        //delete the current user by id
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            Users CurrentUser;
            try
            {
                CurrentUser = WebDb.GetUsers().FirstOrDefault(User => User.Id == id);
                if (CurrentUser != null)
                {
                    WebDb.DeleteUsers(CurrentUser);
                    WebDb.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound("Couldn't delete  user -  error: " + ex.Message);
            }
            return NotFound();
        }

    }
}
