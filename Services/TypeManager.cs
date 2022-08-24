using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Services.Interfaces;

namespace Services;

public class TypeManager<T> : IManager<T> where T : BaseModel
{
    public readonly CycleContext Db;
    public TypeManager() => Db = new CycleContext();

    public IQueryable<T> All(Expression<Func<T, object>>? includeExp = null)
    {
        if (includeExp == null)
            return Db.Set<T>().AsQueryable();
        return Db.Set<T>().Include(includeExp).AsQueryable();
    }


    public IEnumerable<T> Where(Func<T, bool> func, Expression<Func<T, Object>>? includeExp = null)
    {
        return includeExp is null ? Db.Set<T>().Where(func) : Db.Set<T>().Include(includeExp).Where(func);
    }


    public Task<T?> First(Expression<Func<T, bool>> func, Expression<Func<T, Object>>? includeExp = null)
    {
        return Task.FromResult(includeExp == null
            ? Db.Set<T>().FirstOrDefault(func)
            : Db.Set<T>().Include(includeExp).FirstOrDefault(func));
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


    public async Task<int> Save() => await Db.SaveChangesAsync();

    public async Task<T?> Find(long i, Expression<Func<T, Object>>? includeExp = null)
    {
        if (includeExp is null)
            return await Db.Set<T>().FindAsync(i);
        return Db.Set<T>().Include(includeExp).First(x => x.Id == i);
    }

    public Task<int> Remove(T i)
    {
        Db.Set<T>().Remove(i);
        return Db.SaveChangesAsync();
    }
}