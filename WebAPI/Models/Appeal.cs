using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Appeal : BaseEntity
{
    [StringLength(1000, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 1000 characters long.")]
    public string? Description { get; set; }

    public DateTime DeadlineDate { get; set; }

   
    public string Color { get; set; } = "color:black";
    public DateTime EntryDate { get; set; } = DateTime.Now;
    public bool IsDone { get; set; } = false;
    public DateTime DoneDate { get; set; } = DateTime.Now;
}