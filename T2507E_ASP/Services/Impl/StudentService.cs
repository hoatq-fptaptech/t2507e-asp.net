using AutoMapper;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;
using T2507E_ASP.Models;
using T2507E_ASP.Repositories;

namespace T2507E_ASP.Services.Impl;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<StudentService> _logger;
    private readonly IMapper _mapper;
    public StudentService(IStudentRepository studentRepository,
        ILogger<StudentService> logger, 
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResult<PagedResult<StudentResponse>>> GetAllAsync(StudentQueryParameters parameters)
    {
        var (items, totalItems) = await _studentRepository
                            .GetAllAsync(parameters);
        var totalPages = totalItems == 0 ? 0 : 
            (int)Math.Ceiling((double)totalItems / parameters.PageSize);
        var pagedResult = new PagedResult<StudentResponse>
        {
            // Items = items.Select(s=>_mapper.Map<StudentResponse>(s)).ToList(),
            Items = _mapper.Map<List<StudentResponse>>(items),
            TotalPages = totalPages,
            PageNumber = parameters.Page,
            PageSize = parameters.PageSize,
            TotalItems = totalItems
        };
        return ApiResult<PagedResult<StudentResponse>>.Success(pagedResult);
    }

    public async Task<ApiResult<StudentResponse>> GetByIdAsync(int id)
    {
        var student =  await _studentRepository.GetByIdAsync(id);
        return student is null ? 
                ApiResult<StudentResponse>.Failure("NOT FOUND")
            : ApiResult<StudentResponse>.Success(_mapper.Map<StudentResponse>(student));
    }

    public async Task<ApiResult<StudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        _logger.LogInformation("Create student with code: {StudentCode}",request.Code);
        var code = request.Code.Trim().ToUpperInvariant();
        var codeExists = await _studentRepository.ExistsByCodeAsync(code);
        if (codeExists)
        {
            _logger.LogWarning("Code already exists: {StudentCode}",request.Code);
            return ApiResult<StudentResponse>.Failure("CODE_EXISTS");
        }

        // var student = new Student
        // {
        //     StudentCode = code,
        //     Name = request.Name,
        //     Email = request.Email,
        //     Age = request.Age
        // };
        var student = _mapper.Map<Student>(request);
        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();
        _logger.LogInformation("Create student with code: {StudentCode} and id: {Id}",request.Code,student.Id);
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public async Task<ApiResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
        {
            return ApiResult<StudentResponse>.Failure("NOT FOUND");
        }
        student.Name = request.Name;
        student.Age = request.Age;
        await _studentRepository.SaveChangesAsync();
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public async Task<ApiResult<StudentResponse>> DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);
        if (student is null)
        {
            return ApiResult<StudentResponse>.Failure("NOT FOUND");
        }
        _studentRepository.Remove(student);
        await _studentRepository.SaveChangesAsync();
        return ApiResult<StudentResponse>.Success(MapToResponse(student));
    }

    public static StudentResponse MapToResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            FullName = student.Name,
            Email = student.Email,
            Age = student.Age,
        };
    }
}