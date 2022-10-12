using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class TypeSimpleManager<T> where T : class
{
    private readonly CycleContext _db;
    public TypeSimpleManager() => _db = new();

    public async Task<int>
        AddWithMapping(T i, Expression<Func<T, bool>> func) // params Expression<Func<T, Object>>[]? includeExp) 
    {
        if (await First(func) == null)
            await _db.Set<T>().AddAsync(i);
        return await _db.SaveChangesAsync();
    }

    public Task<T?> First(Expression<Func<T, bool>> func)
    {
        var dbSet = _db.Set<T>();
        return Task.FromResult(dbSet.FirstOrDefault(func));
    }

    public async Task Add(T i)
    {
        await _db.AddAsync(i);
    }
}