using Models.DataModels;
using Services.Interfaces;

namespace Services;

public class TypeManager<T> : IManager<T> where T : BaseModel
{
    public readonly CycleContext Db;
    public TypeManager() => Db = new CycleContext();

    public IQueryable<T> All()
    {
        return Db.Set<T>().AsQueryable();
    }



    public async Task<int> Add(T i)
    {
        await Db.Set<T>().AddAsync(i);
        return await Db.SaveChangesAsync();
    }

    public async Task<int> Edit(T i)
    {
#pragma warning disable CS8604
        Db.Entry<T>(await Db.Set<T>().FindAsync(i.Id)).CurrentValues.SetValues(i);
#pragma warning restore CS8604
        return await Db.SaveChangesAsync();
    }


    public async Task<T?> Find(long i)
    {
        return await Db.Set<T>().FindAsync(i);
    }

    public Task<int> Remove(T i)
    {
        Db.Set<T>().Remove(i);
        return Db.SaveChangesAsync();
    }
}