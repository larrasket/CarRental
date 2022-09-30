using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618
namespace Models.DataModels;

public class Vehicle : BaseModel
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Number { get; set; }
    public string Color { get; set; }
    public List<Rent> Rents { get; set; }
    public List<Fine>Fines { get; set; }
    public List<Maintenance>Maintenances { get; set; }
}
