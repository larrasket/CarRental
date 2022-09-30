using Models.DataModels;

namespace Models.Helpers;

public static class RentHelper
{
    public static double Days(this Rent r) => r.RentEnd.DayNumber - r.RentStart.DayNumber;

    public static bool ValidStartDay(this Rent rent) =>
        !rent.Vehicle.Rents.Where(x =>
                x.Status != Status.Cancelled && x.Status != Status.Completed)
            .Any(r => rent.RentStart >= r.RentStart && rent.RentStart <= r.RentEnd);

    public static bool ValidEndDay(this Rent rent)
    {
        var next = rent.Vehicle.Rents.FirstOrDefault(x => x.Status != Status.Cancelled
                                                          && x.RentStart > rent.RentEnd);
        if (next == null) return true;
        return rent.RentEnd < next.RentStart;
    }

    public static bool InRent(this Vehicle vehicle)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        return vehicle.Rents.Any(r => today >= r.RentStart && today <= r.RentEnd);
    }
}