using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyApplication.Models;
using static System.Collections.Specialized.BitVector32;

namespace TechnologyApplication.Controllers
{
    [Route("api/school")]
    [ApiController]
    public class SchoolDataController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<School>>> AddData([FromBody] List<School> schoolData)
        {
            using (var sch = new TechnologyContext())
            {
                try
                {
                    foreach (var data in schoolData)
                    {
                        sch.Schools.Add(new School()
                        {
                            SchoolId = data.SchoolId,
                            SchoolName = data.SchoolName,
                            SchoolAddress = data.SchoolAddress,
                           
                            SchoolType = data.SchoolType,
                            SchoolLocation = data.SchoolLocation,
                            StudentID = data.StudentID,
                            Student = new Student()
                            {
                                StudentID = data.Student.StudentID,
                                StudentName = data.Student.StudentName,
                                DateOfBirth = data.Student.DateOfBirth,
                                Height = data.Student.Height,
                                Weight = data.Student.Weight,

                                Grade = new Grade()
                                {
                                    GradeId = data.Student.Grade.GradeId,
                                    GradeName = data.Student.Grade.GradeName,
                                    Section = data.Student.Grade.Section
                                }
                            }
                        });
                        sch.SaveChanges();
                    }
                    return Ok(schoolData);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<School>> GetData(int id)
        {
            try
            {
                using (var schoo = new TechnologyContext())
                {
                    IList<School> schooldata = new List<School>();
                    schooldata = schoo.Schools.Where(sc => sc.SchoolId == id).ToList();
                    //if (schooldata.Count == 0)
                    //{
                    //    return BadRequest();
                    //}
                    //return Ok(schooldata);


                    //    IList<Student> studentdata = new List<Student>();
                    var studentdata = from student in schoo.Students.Where(s => s.StudentID == id).ToList()
                                      join school in schoo.Schools on student.StudentID equals school.SchoolId
                                      join grade in schoo.Grades on student.Grade.GradeId equals grade.GradeId
                                      select new
                                      {
                                          studentId = student.StudentID,
                                          StudentSchoolName = school.SchoolName,
                                          StudentGrade = grade.GradeName
                                      };

                    //    if (studentdata.Count == 0)
                    //    {
                    //        return BadRequest();
                    //    }
                    return Ok(studentdata);
                    //}
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

       
    }
}
