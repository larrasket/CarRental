namespace Models.DataModels;

public class Bill : BaseModel
{
    public string? Image { get; set; }
    public decimal Price { get; set; }
}