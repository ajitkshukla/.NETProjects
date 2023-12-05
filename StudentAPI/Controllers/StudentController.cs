using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;
using StudentAPI.Model.Context;
using StudentAPI.Model.Requests;
using System.Xml.Linq;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly StudentAPIDbContext _context;
        public StudentController(StudentAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/student/getall")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            

            return Ok(students);
        }
        [HttpGet]
        [Route("api/student/get/{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            return Ok(student);
        }

        [HttpPost]
        [Route("api/student/add")]
        public async Task<IActionResult> Add(AddStudentRequest request)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DawgTag = request.DawgTag,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = new Address
                {
                    ZipCode = request.Address.ZipCode,
                    BuildingNumber = request.Address.BuildingNumber,
                    AptNo = request.Address.AptNo,
                    Street = request.Address.Street,
                    City = request.Address.City,
                    State = request.Address.State,
                    Country = request.Address.Country,
                }
            };
            await _context.Addresses.AddAsync(student.Address);
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut]
        [Route("api/student/edit/{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, UpdateStudentRequest request)
        {            
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            if (student == null)
                return NotFound();
            student.Name = request.Name;
            student.DawgTag = request.DawgTag;
            student.DateOfBirth = request.DateOfBirth;
            student.PhoneNumber = request.PhoneNumber;
            student.Email = request.Email;
            if(request.Address != null)
            {
                student.Address = new Address
                {
                    ZipCode = request.Address.ZipCode,
                    BuildingNumber = request.Address.BuildingNumber,
                    AptNo = request.Address.AptNo,
                    Street = request.Address.Street,
                    City = request.Address.City,
                    State = request.Address.State,
                    Country = request.Address.Country,
                };
            }
            
            _context.Update(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete]
        [Route("api/student/delete/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
            if (student == null) return NotFound();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }
    }
}
