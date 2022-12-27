using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyApplication.Models;

namespace TechnologyApplication.Controllers
{
    [ApiController]
    [Route("api/technology")]
    public class TechnologyController : ControllerBase
    {
        private static List<Course> courses  = new List<Course>
        { 
            new Course { CourseId = 1, CourseName = "Angular", CourseType = "Development", CourseDuration = "3months", CourseCost = 1000 },
            new Course { CourseId = 2, CourseName = "React", CourseType = "Development", CourseDuration = "4months", CourseCost = 1250}
        };

        public int CourseId { get; private set; }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get()
        {
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            var course = courses.Find(c => c.CourseId == CourseId);
            if (course == null)
            {
                return BadRequest("No Records are Inserted");
            }
            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult<List<Course>>> PostAddCourse([FromBody]Course tech)
        {
            courses.Add(tech);
            return Ok(courses);
        }
        [HttpPut]
        public async Task<ActionResult<List<Course>>> PutUpdateCourse( Course technology)
        {
            var course = courses.Find(c =>c.CourseId == technology.CourseId);
            if(course == null)
                return BadRequest("not data cannot be found");
            course.CourseName = technology.CourseName;
            course.CourseType = technology.CourseType;
            course.CourseDuration = technology.CourseDuration;
            course.CourseCost = technology.CourseCost;

            return Ok(course);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Course>>> DeleteMethod(int id)
        {
            var Courses = courses.Find(c => c.CourseId==id);
            if (Courses == null)
                return BadRequest("no records deleted");

            return Ok(courses);

        }

    }
}
