using Models.DataModels;

namespace Services;

public static class ExtManager
{
    public static IQueryable<Rent> Going(this TypeManager<Rent> typeManager) =>
        typeManager.Db.Set<Rent>().Where(x => x.Status == Status.Waiting);
}