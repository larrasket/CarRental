namespace Models.DataModels;

public class CycleMaintenance : BaseModel
{
    public string Name { get; set; }
    // if null, then it is not cycle
    public int? DaysToRenew { get; set; }
    public List<Bill> Bills { get; set; }
}