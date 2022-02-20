namespace SystemyWP.Lib.Shared.Abstractions.DataRelated;

public interface IRepositoryBase
{
    Task<int> SaveChanges();
}