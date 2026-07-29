using System.ComponentModel.DataAnnotations;

namespace T2507E_ASP.DTOs.Student;

public class StudentQueryParameters
{
    public string? Keyword { get; set; }
    public string SortBy { get; set; } = "StudentCode";
    public string SortDirection { get; set; } = "asc";
    [Range(1, int.MaxValue, 
        ErrorMessage = "Please enter a number min: 1")]
    public int Page { get; set; }
    [Range(1, 200,
        ErrorMessage = "Please enter a number min:1 - max: 200")]
    public int PageSize { get; set; }
}