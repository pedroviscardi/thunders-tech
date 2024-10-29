using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thunders.Tecnologia.Domain.Entities;

[Table("Tasks")]
public class Tasks
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("IdPerson")]
    public virtual Person? Person { get; set; }

    [Required]
    public required Guid IdPerson { get; set; }

    [Required]
    [StringLength(100)]
    public required string Title { get; set; }

    [StringLength(200)]
    public required string Description { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsCompleted { get; set; }
}