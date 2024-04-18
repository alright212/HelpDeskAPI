namespace WebAPI.Models;

public class AppealsViewModel
{
    public DateTime DeadlineDate { get; set; }
    public List<Appeal>? Appeals { get; set; }
    public string? Description { get; set; }
    public int DoneAppealsCount { get; set; }

}