using Models.DataModels;

namespace Models.Helpers;

public static class RentHelper
{
    public static double Days(this Rent r) => r.RentEnd.DayNumber - r.RentStart.DayNumber;
    
    public static bool Present(this Rent r) => r.RentEnd.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber > 0;

    public static bool Present(this Rent r, int n) =>
        r.RentEnd.DayNumber - DateOnly.FromDateTime(DateTime.Now.AddDays(n)).DayNumber > 0;
}