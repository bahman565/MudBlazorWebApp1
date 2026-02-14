using System.ComponentModel.DataAnnotations;

namespace MudBlazorWebApp1.Models;

public enum ChildGender
{
    Male = 1,
    Female = 2
}

public class Child
{
    public int Id { get; set; }

    [Required]
    public string OwnerUserId { get; set; } = default!; // AspNetUsers.Id

    [Required, StringLength(80)]
    public string Name { get; set; } = default!;

    [Required]
    public ChildGender Gender { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }  // تاریخ تولد (بعداً UI شمسی می‌کنیم)

    public List<Measurement> Measurements { get; set; } = new();
}