using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;
using T2507E_ASP.Models;
using T2507E_ASP.Repositories;

namespace T2507E_ASP.Services.Impl;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<ApiResult<List<StudentResponse>>> GetAllAsync()
    {
        var students = await _studentRepository.GetAllAsync();
        return ApiResult<List<StudentResponse>>.Success(
            students.Select(MapToResponse).ToList());
    }

    public Task<ApiResult<StudentResponse>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<StudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<StudentResponse>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public static StudentResponse MapToResponse(Student student)
    {
        return new StudentResponse
        {
            Id = student.Id,
            StudentCode = student.StudentCode,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
        };
    }
}