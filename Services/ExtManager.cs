using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

namespace Services;

public static class ExtManager
{
    public static IQueryable<Rent> Going(this TypeManager<Rent> typeManager,
        Expression<Func<Rent, Object>>? includeExp = null)
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        return includeExp is null
            ? typeManager.Db.Set<Rent>().Where(x => x.Status == Status.Waiting || x.RentEnd > now)
            : typeManager.Db.Set<Rent>().Include(includeExp).Where(x => x.Status == Status.Waiting || x.RentEnd > now);
    }
}