using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW14.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CW14.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult Student()
        {
            var db = new s16600Context();
            var s = db.Student.ToList();

            return Ok(s);
        }

        [HttpPost]
        public IActionResult Student(Student student)
        {
            var s = new Student();
            s.IndexNumber = student.IndexNumber;
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            s.BirthDate = student.BirthDate;
            s.IdEnrollment = student.IdEnrollment;
            
            var db = new s16600Context();
            db.Student.Add(s);
            db.SaveChanges();

            return Ok(s);

            /*
            {
	        "IndexNumber": "S166100",
	        "FirstName": "Jan",
            "LastName": "Winnicki",
            "BirthDate": "2001-01-25",
            "IdEnrollment": 1
            }
             */
        }

        [HttpPost("{Index}/{FirstName}")]
        public IActionResult Student(string Index, string FirstName)
        {
            var db = new s16600Context();
            var student = (from s in db.Student
                     where s.IndexNumber == Index
                     select s).First();

            student.FirstName = FirstName;
            db.SaveChanges();

            return Ok("Zmieniono imię studenta");
        }

        [HttpDelete("{Index}")]
        public IActionResult Student(string Index)
        {
            var db = new s16600Context();
            var s = db.Student.Where(s => s.IndexNumber.Equals(Index)).First();
            db.Student.Remove(s);
            db.SaveChanges();

            return Ok("Usunięto studenta");
        }
    }
}