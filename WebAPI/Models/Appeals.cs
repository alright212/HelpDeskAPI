namespace WebAPI.Models;



public class Appeals : IAppeals
{
    private static List<Appeal> _appeals = [];

    public List<Appeal> GetAppeals()
    {
        return _appeals;
    }

    public void DeleteAppeal(Guid appealId) // Accept a Guid instead of an int
    {
        var appeal = _appeals.FirstOrDefault(a => a.Id == appealId);
        if (appeal == null) return;
        _appeals.Remove(appeal);
    }

    public void AddAppeal(Appeal appeal)
    {
        _appeals.Add(appeal);
    }

    public void SortDeadlineDate()
    {
        _appeals = _appeals.OrderByDescending(a => a.DeadlineDate).ToList();
    }
}

public interface IAppeals
{
    List<Appeal> GetAppeals();
    void AddAppeal(Appeal appeal);
    void SortDeadlineDate();
    void DeleteAppeal(Guid appealId); // Accept a Guid instead of an int

}