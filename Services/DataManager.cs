using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models.DataModels;
using Services.Interfaces;

namespace Services;

public class DataManager<T> : IManager<T> where T : BaseModel
{
    public readonly CycleContext Db;
    public readonly Creator? Creator;

    public DataManager(Creator? creator)
    {
        Creator = creator;
        Db = new();
    }

    public IQueryable<T> All(Expression<Func<T, Object>>? includeExp = null)
    {
        return includeExp == null ? Db.Set<T>().AsQueryable() : Db.Set<T>().Include(includeExp).AsQueryable();
    }

    public IQueryable<T> All(params Expression<Func<T, Object>>[]? includeExp)
    {
        if (includeExp == null)
            return Db.Set<T>().AsQueryable();

        if (includeExp.Length == 1)
            return Db.Set<T>().Include(includeExp[0]).AsQueryable();

        IIncludableQueryable<T, object>? tdb = null;
        HandleExpression(ref tdb, includeExp);
        return tdb!.AsQueryable();
    }


    public IEnumerable<T> Where(Func<T, bool> func, Expression<Func<T, Object>>? includeExp = null)
    {
        return includeExp is null ? Db.Set<T>().Where(func) : Db.Set<T>().Include(includeExp).Where(func);
    }


    public Task<T?> First(Expression<Func<T, bool>> func, params Expression<Func<T, Object>>[]? includeExp)
    {
        var dbSet = Db.Set<T>();
        if (includeExp == null || includeExp.Length == 0)
            return Task.FromResult(dbSet.FirstOrDefault(func));
        IIncludableQueryable<T, object>? tdb = null;
        HandleExpression(ref tdb, includeExp);
        return Task.FromResult(tdb!.FirstOrDefault(func));
    }

    private void HandleExpression(ref IIncludableQueryable<T, object>? tdb,
        params Expression<Func<T, Object>>[] includeExp)
    {
        var first = true;
        foreach (var expression in includeExp)
        {
            if (first)
                tdb = Db.Set<T>().Include(expression);
            first = false;
            tdb = tdb!.Include(expression);
        }
    }

    // TODO implement created by
    public async Task<int> Add(T i)
    {
        // i.Creation = new Creator();
        // if (Creator is not null)
        // {
        //     i.Creation.UserId = Creator.User.Id;
        // }

        // await Db.Set<Creator>().AddAsync(i.Creation);
        // await Db.SaveChangesAsync();
        // long id = i.Creation.Id;
        // i.Creation.Id = id;

        if (i.Creation != null)
        {
            var id = i.Creation.User.Id;
            var newUser = await Db.ClientUsers.FindAsync(id);
            if (newUser != null)
            {
                i.Creation.User = newUser;
            }
        }

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