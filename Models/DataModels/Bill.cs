namespace Models.DataModels;

public class Bill : BaseModel
{
    public Bill()
    {
        Creation = null;
    }

    public string? Image { get; set; }
    public decimal Price { get; set; }
}

public enum TypeOfMaintenance
{
    Regular,
    Cycle,
}