using System.ComponentModel.DataAnnotations;

namespace Thunders.Tecnologia.Application.DTOs;

public class TaskDto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required Guid IdPerson { get; set; }

    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    [StringLength(200)]
    public required string Description { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    public bool IsCompleted { get; set; }
}