using IronDome2.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IronDome2.Models;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using static IronDome2.Models.Status;
using static System.Net.Mime.MediaTypeNames;
using IronDome2.Middlewares;
using IronDome2.Middlewares.attack;
namespace IronDome2.Controllers
{
    //[ServiceFilter(typeof(AttackLogInMiddleWare))]
    [Route("api/[controller]")]
    [ApiController]
    public class AttacksController : ControllerBase
    {
        private readonly DbConnection _contection;
        public AttacksController(DbConnection dbConnection) { 
            this._contection = dbConnection;
        }

        //--רשימת תקיפות--
        [HttpGet]
        public IActionResult GetAttacks()
        {
            return StatusCode(
                StatusCodes.Status200OK,
                new
                {
                    success = true,
                    attacks = _contection.Attacks.ToArray()
                });
        }


        //--צור התקפה--
        [HttpPost]
        [Produces(contentType: "application/json")]
        [ServiceFilter(typeof(ValidationCreateAttackMiddleWare))]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        public IActionResult CreateAttack([FromBody] Attack attack)
        {
            
            attack.status = status.pending.ToString();
            _contection.Attacks.Add(attack);
            _contection.SaveChanges();

            return StatusCode(
                StatusCodes.Status201Created,
                new { success = true, attack = attack }
                );
        }


        //--התחל התקפה--
        //ToDo - change do to DbConnection!!!!!!!
        [HttpPost("{id}/start")]
        public IActionResult StartAttack(int id)
        {
            Attack attack = _contection.Attacks.FirstOrDefault(x => x.id == id);
            if (attack.status != status.pending.ToString())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Erroe = "cannot start an attact that has already been started!" });
            }

            Task attackTask = Task.Run(() =>
            {
                attack.status = status.inProgress.ToString();
                attack.time = DateTime.Now;
                Task.Delay(1000);
            });
            
            return StatusCode(
            StatusCodes.Status200OK,
            new { messege = "attack started", attackID = attackTask.Id }
            );
        }


        //--סטטוס התקפה--
        [HttpGet("{id}/status")]
        public IActionResult GetAttackStatus(int id)
        {
            Attack attack = _contection.Attacks.FirstOrDefault(x => x.id == id);
            if (attack == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { messsege = "attack was not found!" });
            }
            return StatusCode(StatusCodes.Status200OK, new
            {
                id = attack.id,
                status = attack.status,
                startedAt = attack.time
            });
        }


        //--זהה טילי התקפה--
        //ToDo - change do to DbConnection!!!!!!!
        [HttpPut("{id}/missiles")]
        [Produces(contentType: "application/json")]
        public IActionResult DefineMissile(int id) 
        {
            Attack attack = _contection.Attacks.FirstOrDefault(x => x.id == id);

            return StatusCode(StatusCodes.Status200OK, new {
                id = attack.id,
                missileCount = attack.type.Length,
                missileTypes = attack.type,
                status = "missile defined"});
        }


        //--סוגי טלים--
        [HttpGet("types")]
        public IActionResult GetAttackstype()
        {       
            string json = "[{";
            foreach (Attack attack in _contection.Attacks)
            {
                json += $"id: {attack.id}, type: [";
                foreach (var type in attack.type)
                {
                     json += $"{type}, ";                   
                }
                json = json.Substring(0, json.Length - 1);
                json += "]}, ";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            var result = JsonConvert.SerializeObject(json);
            return StatusCode(StatusCodes.Status200OK, new
            {
                result = json
            });
        }


        //--סוגי טלים--
        [HttpGet("origine")]
        public IActionResult GetAttacksOrigine()
        {
            string json = "[";
            foreach (Attack attack in _contection.Attacks)
            {
                json += "{";
                json += $"id: {attack.id}, origine: ";               
                json += $"{attack.origine}";
                json = json.Substring(0, json.Length - 1);
                json += "}, ";
            }
            
            json += "]";
            var result = JsonConvert.SerializeObject(json);
            return StatusCode(StatusCodes.Status200OK, new
            {
                result = json
            });
        }


        //--התחלת יירוט--
        //ToDo - change do to DbConnection!!!!!!!
        [HttpPost("{id}/Intercept")]
        public IActionResult InterceptAttack(int id)
        {
            Attack attack = _contection.Attacks.FirstOrDefault(x => x.id == id);
            if (attack == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { success = false, messeage = "attack was not found" });
            }
            if (attack.status != status.inProgress.ToString())
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Erroe = "cannot intercept an attact that has already been completed!" });
            }

            Task attackTask = Task.Run(() =>
            {
                attack.status = status.Compleated.ToString();
                attack.time = DateTime.Now;
                Task.Delay(1000);
            });
            return StatusCode(
            StatusCodes.Status200OK,
            new { messege = "attack intercepted", status = "success" }
            );
        }


        //--מחק התקפה--
        [HttpDelete("{id}")]
        public IActionResult DeletetAttack(int id)
        {
            Attack attack = _contection.Attacks.FirstOrDefault(x => x.id == id);
            if (attack == null) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new { success = false, messeage = "attack was not found" });
            }
            if (attack.status != status.pending.ToString())
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Erroe = "cannot delete an attact that has already been initiated!" });
                }

            _contection.Attacks.Remove(attack);
            _contection.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, new { message = "Attack deleted successfully." });
        }
    }
}
