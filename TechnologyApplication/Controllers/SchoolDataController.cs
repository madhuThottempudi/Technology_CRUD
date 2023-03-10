using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
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
            using(var schoo = new TechnologyContext())
            {
                try
                {
                    var rsult = (from sc in schoo.Schools.Where(sc => sc.SchoolId == id).ToList()
                                 join st in schoo.Students
                                 on sc.StudentID equals st.StudentID
                                 select new
                                 {
                                     SchoolName = sc.SchoolName,
                                     StudentName = st.StudentName
                                 }).ToList();
                    // Lamda Expressions
                    //var school_data = schooldata.Join(studentdata,sc => sc.StudentID, st => st.StudentID,(sc,st)=> new
                    //{
                    //    SchoolId = sc.SchoolId,
                    //    SchoolName = sc.SchoolName,
                    //    SchoolAddress = sc.SchoolAddress,
                    //    StudentID = st.StudentID,
                    //    StudentName = st.StudentName,
                    //    Height = st.Height

                    //});
                    return Ok(rsult);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("second/list")]
        public async Task<ActionResult<Student>> GetAllData(int id)
        {
            using (var data = new TechnologyContext())
            {

                var Result = (from s in data.Students.Where(s => s.Grade.GradeId == id).ToList()
                              join g in data.Grades
                              on s.Grade.GradeId equals g.GradeId
                              select new
                              {
                                  StudentName = s.StudentName,
                                  GradeName = g.GradeName
                              }).ToList();
                return Ok(Result);

            }

        }
        [HttpPut]
        [Route("update/list")]
        public async Task<ActionResult<Student>> updateValue(int id,Student valueUpdate)
        {
            using(var data = new TechnologyContext())
            {

                try
                {
                    //Single value Update at 1 Table

                    //var existingStudent = data.Students.Where(c => c.StudentID == valueUpdate.StudentID).FirstOrDefault();

                    // if (existingStudent != null)
                    // {

                    //     existingStudent.StudentName = valueUpdate.StudentName;
                    //     existingStudent.DateOfBirth = valueUpdate.DateOfBirth;
                    //     existingStudent.Height = valueUpdate.Height;

                    //     data.SaveChanges();

                    // }



                    // else
                    // {
                    //     return BadRequest("No Records have Found That Id");
                    // }
                    // return Ok();



                    //multi values Update at 2 Tables


                   var retriveData = data.Students.Where(st => st.StudentID == id).FirstOrDefault();
                    if (retriveData != null)
                    {
                        var updateRecord = data.Grades.FirstOrDefault(x => x.GradeId == retriveData.Grade.GradeId);

                        if (updateRecord != null)
                        {
                            updateRecord.GradeName = valueUpdate.Grade.GradeName;
                            updateRecord.Section = valueUpdate.Grade.Section;

                            var gradeUpdate = data.Grades.Update(updateRecord);

                            retriveData.StudentName = valueUpdate.StudentName;
                            retriveData.DateOfBirth = valueUpdate.DateOfBirth;
                            retriveData.Height = valueUpdate.Height;

                            var studentUpdate = data.Students.Update(retriveData);

                            return StatusCode(StatusCodes.Status200OK);
                        }

                        else
                        {
                            return StatusCode(StatusCodes.Status204NoContent, "no record");
                        }

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent, "no content found");
                    }

                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            using (var stud = new TechnologyContext())
            {
                var student = stud.Students.Where(c => c.StudentID == id).FirstOrDefault();
                stud.Remove(student);
                stud.SaveChanges();
            
            }
            return Ok();
        }

        [HttpGet]
       [Route("separate/List")]

        public async Task<ActionResult<List<School>>> GetSchoolData(int id,string name)
        {
            using (var sch = new TechnologyContext())
            {
                IList<School> schools = new List<School>();

                // Where && oftype


                schools = sch.Schools.Where(s => s.SchoolId == id)
                          .Select(s => new School()
                          {
                              SchoolId = s.SchoolId,
                              SchoolName = s.SchoolName,
                              SchoolAddress = s.SchoolAddress,
                              SchoolType = s.SchoolType,
                              StudentID = s.StudentID

                          }).ToList<School>();
                schools = (from s in sch.Schools.Where(s => s.SchoolId == id && s.SchoolName == name).ToList<School>() select s).ToList();



                // orderby && then by


                schools = (from s in sch.Schools
                           orderby s.SchoolName
                           select s).ToList<School>();

                schools = (from sc in sch.Schools
                           orderby sc.SchoolName descending
                           select sc).ToList<School>();

                schools = sch.Schools.OrderByDescending(s => s.StudentID).ToList();


                //IEnumerable<IGrouping<string, School>> schls = sch.Schools.GroupBy(s => s.SchoolName).ToList<School>();



                // Group By

                ////IEnumerable<IQueryable<String, School>> groupByQuery = from sc in schools
                //                                                       group sc by sc.SchoolName.ToList<School>();

                //foreach (var newGroup in groupByQuery)
                //{
                //    Console.WriteLine($"Key: {nameGroup.Key}");

                //    foreach (var sc in nameGroup)
                //    {
                //        Console.WriteLine($"\t{sc.SchoolName}");
                //    }
                //}


                //Select


                schools = sch.Schools.Where(sc => sc.SchoolId == id)
                           .Select(sc => new School()
                           {
                               SchoolName = sc.SchoolName

                           }).ToList<School>();

                schools = (from s in sch.Schools.Where(s => s.SchoolId == id)
                           select s).ToList<School>();

                //schools = from s in sch.Schools.Where(s => s.SchoolId == id).ToList<School>()
                //          .Select(s => new
                //          {
                //              SchoolAddress = s.SchoolAddress
                //          }).ToList();

                //schools = from s in sch.Schools
                //            .select s => new School()
                //            {
                //                SchoolAddress = "MR." + s.SchoolAddress
                //            }.ToList<School>();

                foreach (var item in schools)
                {
                    Console.WriteLine("SchoolName: {0}", item.SchoolAddress);
                }


                //Any && All

                bool isschools = sch.Schools.Any(s => s.SchoolId == id);
                                      

                //Using Query Syntax
                //var ResultQS = (from num in schools
                //                select num).Any();



                if (schools.Count == 0) 
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,"Data not Found");

                }
                return Ok(isschools);
            }

        }

    }
}