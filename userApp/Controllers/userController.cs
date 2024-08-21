using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using userApp.Models;

namespace userApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private static List<User> _users = new List<User>();


        //-- רשימת משתמשים
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _users.ToArray();
        }

        //-- משתמש לפי id
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

              
        //-- יצירה של משתמש
        [HttpPost]
        public ActionResult<User> SignUp(User user)
        {           
            _users.Add(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        //-- משתמש לפי שם 
        [HttpGet("name/{FirstName}")]
        public ActionResult<User> GetUserByName(string FirstName)
        {
            User user = _users.FirstOrDefault(x => x.FirstName == FirstName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //-- מחיקה
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            User User = _users.FirstOrDefault(User => User.Id == id);
            if (User == null)
            {
                return NotFound();
            }
            _users.Remove(User);
            return NoContent(); 
        }

        //--עידכון
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            User existinguser = _users.FirstOrDefault(u => u.Id == id);
            if (existinguser == null)
            {
                return NotFound();
            }
            existinguser.FirstName = user.FirstName;
            existinguser.LastName = user.LastName;
            existinguser.Email = user.Email;
            return NoContent();
        }

    }    
}
