using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;
using T2507E_ASP.Models;
using T2507E_ASP.Services;

namespace T2507E_ASP.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : Controller
{   private readonly IPaymentService _paymentService;
    private readonly T2507EASPDbContext _dbContext;
    private readonly IStudentService _studentService;
    public StudentController([FromKeyedServices("momo")]IPaymentService paymentService,
        T2507EASPDbContext dbContext)
    {
        _paymentService = paymentService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<PagedResult<StudentResponse>>>> 
        GetAll([FromQuery] StudentQueryParameters parameters)
    {
        var allowedSortFields = new[] {  "name", "code" };
        var allowedSortDirections = new[] {"desc", "asc"};
        if (!allowedSortFields.Contains(parameters.SortBy)
            || !allowedSortDirections.Contains(parameters.SortDirection))
        {
            return BadRequest(ApiResult<string>.Failure("BAD_SORT_DIRECTION"));
        }
        var result = await _studentService.GetAllAsync(parameters);
        return Ok(result);
    }

    [HttpGet("{id}")] // Route Parameter
    public ActionResult<Student> GetById(int id)
    {
        var student = _dbContext.Students
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        return Ok(student);
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