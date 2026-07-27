using System.ComponentModel.DataAnnotations;

namespace T2507E_ASP.DTOs.Student;

public class CreateStudentRequest
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } // abstract property
    [Range(18, 50)]
    public uint Age { get; set; }
    [Required]
    [RegularExpression("^[a-zA-Z]+$")]
    public string Code { get; set; }
    [Required]
    [RegularExpression("^[a-zA-Z]+$")]
    public string Email { get; set; }
}