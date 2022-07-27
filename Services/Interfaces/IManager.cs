using Models.DataModels;

namespace Services.Interfaces;

public interface IManager<T> where T : BaseModel
{
    public IQueryable<T> All();
    public Task<int> Add(T i);
    public Task<int> Edit(T i);
    public Task<int> Remove(T i);
}