using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

namespace Services;

public static class ExtManager
{
    public static IQueryable<Rent> Going(this DataManager<Rent> dataManager,
        Expression<Func<Rent, Object>>? includeExp = null)
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        return includeExp is null
            ? dataManager.Db.Set<Rent>().Where(x => x.Status == Status.Waiting || x.RentEnd > now)
            : dataManager.Db.Set<Rent>().Include(includeExp).Where(x => x.Status == Status.Waiting || x.RentEnd > now);
    }


}