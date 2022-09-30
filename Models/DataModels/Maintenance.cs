namespace Models.DataModels;

public class Maintenance : BaseModel
{
    public Maintenance()
    {
    }

    public Maintenance(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public Maintenance(TypeOfMaintenance type, Vehicle vehicle) : this()
    {
        Type = type;
        Vehicle = vehicle;
    }

    public Vehicle Vehicle { get; set; }
    public Bill Bill { get; set; }
    public TypeOfMaintenance Type { get; set; }
}