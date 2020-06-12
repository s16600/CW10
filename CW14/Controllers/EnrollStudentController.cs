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
        [HttpPost]
        public IActionResult EnrollStudent(EnrollmentPromotion ep)
        {
            var db = new s16600Context();

            /*
            var enrollment = (from e in db.Enrollment
                              where e.)
            */

            return Ok("Enr");
        }
    }
}