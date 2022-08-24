using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DataModels;

#pragma warning disable CS8618
public class Rent : BaseModel
{
    public DateOnly RentStart { get; set; }
    public DateOnly RentEnd { get; set; }
    public Vehicle Vehicle { get; set; }
    public long VehicleId { get; set; }
    public Bill Contract { get; set; }
    public string Driver { get; set; }
    public long BillId { get; set; }
    public Status Status { get; set; }
}

public enum Status
{
    Cancelled,
    Completed,
    Waiting
}