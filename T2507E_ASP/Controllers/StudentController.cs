using Microsoft.AspNetCore.Mvc;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Services;

namespace T2507E_ASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : Controller
{   private readonly IPaymentService _paymentService;
    public StudentController([FromKeyedServices("momo")]IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new[]
        {
            new
            {
                Id = 1,
                Name="Hoa"
            },
            new
            {
                Id = 2,
                Name="Huang"
            }
        });
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