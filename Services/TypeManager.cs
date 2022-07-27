using Models.DataModels;
using Services.Interfaces;

namespace Services;

public class TypeManager<T> : IManager<T> where T : BaseModel
{
    private readonly CycleContext _db;
    public TypeManager() => _db = new CycleContext();

    public IQueryable<T> All() => _db.Set<T>().AsQueryable();

    public async Task<int> Add(T i)
    {
        await _db.Set<T>().AddAsync(i);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Edit(T i)
    {
#pragma warning disable CS8604
        _db.Entry<T>(await _db.Set<T>().FindAsync(i)).CurrentValues.SetValues(i);
#pragma warning restore CS8604
        return await _db.SaveChangesAsync();
    }

    public Task<int> Remove(T i)
    {
        _db.Set<T>().Remove(i);
        return _db.SaveChangesAsync();
    }
}