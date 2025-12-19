using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practical_17.Data;
using Practical_17.Models;
using Practical_17.Repositories;

namespace Practical_17.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public StudentAPIController(AppDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
        }

        // GET: api/StudentAPI
        [HttpGet]
        public ActionResult<IEnumerable<Students>> GetStudents()
        {
            var students = _studentRepository.GetAllStudents();
            return Ok(students);
        }
        // GET: api/StudentAPI/5
        [HttpGet("{id}")]
        public ActionResult<Students> GetStudents(int id)
        {
            var students = _studentRepository.GetStudentById(id);

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // PUT: api/StudentAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Students student)
        {
            if (id != student.StudentId)
                return BadRequest("Student ID mismatch");

            var existingStudent = _studentRepository.GetStudentById(id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Email = student.Email;
            existingStudent.Gender = student.Gender;

            _studentRepository.UpdateStudent(existingStudent);
            _studentRepository.SaveChanges();

            return NoContent();
        }

        // POST: api/StudentAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Students> CreateStudent(Students student)
        {
            _studentRepository.AddStudent(student);
            _studentRepository.SaveChanges();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/StudentAPI/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            if (student == null)
                return NotFound();

            _studentRepository.DeleteStudent(id);
            _studentRepository.SaveChanges();

            return NoContent();
        }
    }
}
