using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;
using T2507E_ASP.Services;

namespace T2507E_ASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : Controller
{   private readonly IPaymentService _paymentService;
    private readonly T2507EASPDbContext _dbContext;
    public StudentController([FromKeyedServices("momo")]IPaymentService paymentService,
        T2507EASPDbContext dbContext)
    {
        _paymentService = paymentService;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public ActionResult<List<Student>> GetAll()
    {
        var students = _dbContext.Students
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToList();
        return Ok(students);
    }

    [HttpGet("{id}")] // Route Parameter
    public IActionResult GetById(int id)
    {
        return Ok(new
        {
            Id = id,
            Name = "Hoa"
        });
    }

    [HttpGet("search")] // Query String /api/student?page=1&pageSize=20
    public IActionResult Search(int page, int pageSize)
    {
        return Ok(new{page,pageSize});
    }

    [HttpPost]
    public IActionResult Create(CreateStudentRequest request)
    {
        return Ok(request);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateStudentRequest request)
    {
        return Ok(new{id, request});
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}