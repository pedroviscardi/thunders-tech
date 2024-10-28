using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thunders.Tecnologia.Domain.Entities;

[Table("Persons")]
public class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    public required DateTime DateOfBirth { get; set; }

    [EmailAddress]
    public required string Email { get; set; }
}