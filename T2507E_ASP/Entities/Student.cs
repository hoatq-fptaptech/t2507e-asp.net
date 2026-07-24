namespace T2507E_ASP.Entities;

public class Student
{
    public int Id { get; set; } // PK
    public string StudentCode { get; set; } // not null
    public string Name { get; set; } = string.Empty; // not null default = ""
    public string? Email { get; set; } // cho phép column null
    public uint Age { get; set; } = 18;
}