using Models.DataModels;

namespace Models.Helpers;

public static class RentHelper
{
    public static double Days(this Rent r) => r.RentEnd.DayNumber - r.RentStart.DayNumber;

    public static bool ValidStartDay(this Rent rent)
    {
        return !rent.Vehicle.Rents.Where(x => x.RentEnd > rent.RentStart || x.RentStart > rent.RentStart)
            // .Any(f => rent.RentStart >= f.RentStart && (rent.RentEnd != default && rent.RentEnd <= f.RentEnd));
            .Any(r => rent.RentStart >= r.RentStart && rent.RentStart <= r.RentEnd);
    }

    public static bool ValidEndDay(this Rent rent)
    {
        var next = rent.Vehicle.Rents.FirstOrDefault(x => x.RentStart > rent.RentEnd);
        if (next == null) return true;
        return rent.RentEnd < next.RentStart;
    }
}