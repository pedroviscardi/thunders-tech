using System.ComponentModel.DataAnnotations;

namespace Thunders.Tecnologia.Application.DTOs;

public class PersonDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required DateTime BirthDate { get; set; }
}