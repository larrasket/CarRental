namespace Models.DataModels;

public class Bill : BaseModel
{
    public Bill()
    {
    }

    public Bill(TypeOfBill? type)
    {
        Type = type;
    }

    public string? Image { get; set; }
    public decimal Price { get; set; }

    public TypeOfBill? Type { get; set; }
}

public enum TypeOfBill
{
    Rent,
    Fine,
    HGS,
    Maintenance
}