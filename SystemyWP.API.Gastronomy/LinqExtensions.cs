using System.Linq;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy;

public static class LinqExtensions
{
    public static IQueryable<T> FilterAllowedEntity<T>(this IQueryable<T> source, long id,
        string accessKey) where T : BaseModel
    {
        return source.Where(baseModel => baseModel.Id == id && baseModel.AccessKey == accessKey);
    }

    public static IQueryable<T> FilterAllowedEntity<T>(this IQueryable<T> source,
        ResourceAccessPass resourceAccessPass) where T : BaseModel
    {
        return source.Where(baseModel =>
            baseModel.Id == resourceAccessPass.Id && baseModel.AccessKey == resourceAccessPass.AccessKey);
    }

    public static IQueryable<T> FilterAllowedEntities<T>(this IQueryable<T> source,
        string accessKey) where T : BaseModel
    {
        return source.Where(baseModel => baseModel.AccessKey == accessKey);
    }
}