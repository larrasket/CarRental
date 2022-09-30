using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS8618

namespace Models.DataModels;

public class Fine : BaseModel
{
    public Fine()
    {
    }

    public Fine(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public Vehicle Vehicle { get; set; }
    public Bill Bill { get; set; }
}