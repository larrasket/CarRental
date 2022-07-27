namespace Models.DataModels;

#pragma warning disable CS8618
public class Rent : BaseModel
{
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }
    public Vehicle Vehicle { get; set; }
    public Bill Contract { get; set; }
}