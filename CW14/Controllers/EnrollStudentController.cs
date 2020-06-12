using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW14.DTOs;
using CW14.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CW14.Controllers
{
    [Route("api/enrollstudent")]
    [ApiController]
    public class EnrollStudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult EnrollStudent()
        {
            return Ok("Enr1");
        }
        
        [HttpPost]
        public IActionResult EnrollStudent(EnrollmentDTO enrolment)
        {
            var db = new s16600Context();

            //if (db.Studies.Count(s => s.Name == e.Studies) == 0)
            if (!db.Studies.Any(s => s.Name == enrolment.Studies))
           
            {
                return BadRequest("Brak takiego kierunku studiów");
            }

            if (db.Student.Any(s => s.IndexNumber == enrolment.IndexNumber))
            {
                return BadRequest("Student o takim numerze indeksu już istnieje");
            }

            var ids = db.Studies.Where(s => s.Name == enrolment.Studies).First().IdStudy;
            if (!db.Enrollment.Any(e => (e.Semester == 1) && (e.IdStudy == ids) ))
            {
                var e = new Enrollment();
                e.IdEnrollment = db.Enrollment.Max(e => e.IdEnrollment) + 1;
                e.Semester = 1;
                e.IdStudy = ids;
                e.StartDate = DateTime.Now;

                db.Enrollment.Add(e);
                db.SaveChanges();
                
                //return Ok("Dodano rekrutację");
            }

            var s = new Student();
            s.IndexNumber = enrolment.IndexNumber;
            s.FirstName = enrolment.FirstName;
            s.LastName = enrolment.LastName;
            s.BirthDate = Convert.ToDateTime(enrolment.BirthDate);
            s.IdEnrollment = db.Enrollment.Where(e => (e.Semester == 1) && (e.IdStudy == ids)).First().IdEnrollment;

            db.Student.Add(s);
            db.SaveChanges();

            return Ok("Dodano studenta");

            /*
            {
                "IndexNumber": "s12345",
                "FirstName": "Andrzej",
                "LastName": "Malewski",
                "BirthDate": "30.03.1993",
                "Studies": "IT"
            }*/

        }
    }
}
 