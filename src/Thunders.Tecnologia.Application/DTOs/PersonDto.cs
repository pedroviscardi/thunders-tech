using System.ComponentModel.DataAnnotations;

namespace Thunders.Tecnologia.Application.DTOs;

/// <summary>
///     Data transfer object representing a person.
/// </summary>
public class PersonDto
{
    /// <summary>
    ///     Gets or sets the unique identifier of the person.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     Gets or sets the name of the person.
    /// </summary>
    /// <remarks>
    ///     This property is required and has a maximum length of 100 characters.
    /// </remarks>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public required string Name { get; set; }

    /// <summary>
    ///     Gets or sets the email address of the person.
    /// </summary>
    /// <remarks>
    ///     This property is required and must be a valid email address.
    /// </remarks>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    /// <summary>
    ///     Gets or sets the date of birth of the person.
    /// </summary>
    /// <remarks>
    ///     This property is required.
    /// </remarks>
    [Required(ErrorMessage = "Date of Birth is required.")]
    public required DateTime DateOfBirth { get; set; }
}