using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Linq;

namespace WebAPI.Controllers;
///Users/glenkink/WebAPI/WebAPI/Controllers/AppealsController.cs
[Route("api/[controller]")]
[ApiController]

public class AppealsController : ControllerBase
{
    private readonly IAppeals _appeals;

    public AppealsController(IAppeals appeals)
    {
        _appeals = appeals;
    }

    [HttpGet]
    public ActionResult<List<Appeal>> List()
    {
        var appeals = _appeals.GetAppeals().Where(a => !a.IsDone).ToList();
       


        return Ok(appeals);
    }
    private string DetermineColor(DateTime deadline) {
        return deadline <= DateTime.Now.AddHours(1) ? "color:red" : "color:black";
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] AppealsViewModel viewModel)
    {
        var appeal = new Appeal
        {
            Description = viewModel.Description,
            EntryDate = DateTime.Now,
            DeadlineDate = viewModel.DeadlineDate,
            Color = DetermineColor(viewModel.DeadlineDate) // Set color based on deadline
        };

        _appeals.AddAppeal(appeal);
        _appeals.SortDeadlineDate();

        return CreatedAtAction(nameof(List), new { id = appeal.Id }, appeal);
    }

    [HttpPost("markAsDone")]
    public IActionResult MarkAsDone([FromQuery] Guid appealId)
    {
        var appeal = _appeals.GetAppeals().FirstOrDefault(a => a.Id == appealId);
        if (appeal == null) return NotFound();

        if (appeal.IsDone) return BadRequest("Appeal is already marked as done.");

        appeal.IsDone = true;
        appeal.DoneDate = DateTime.Now;
        appeal.Color = DetermineColor(appeal.DeadlineDate); // Ensure color is updated

        return Ok(appeal); // Return the updated appeal
    }

    [HttpPost("remove")]
    public IActionResult Remove([FromQuery] Guid appealId)
    {
        var appeal = _appeals.GetAppeals().FirstOrDefault(a => a.Id == appealId);
        if (appeal == null) return NotFound();

        _appeals.DeleteAppeal(appealId);
        return NoContent();
    }

    [HttpGet("doneList")]
    public ActionResult<List<Appeal>> DoneList()
    {
        var doneAppeals = _appeals.GetAppeals().Where(a => a.IsDone).ToList();
        return Ok(new AppealsViewModel
        {
            Appeals = doneAppeals,
            DoneAppealsCount = doneAppeals.Count
        });
    }
}
