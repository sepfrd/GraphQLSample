using Application.Abstractions;
using Mapster;

namespace Infrastructure.Services.Mapping;

public class MappingService : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource? source) =>
        source.Adapt<TDestination>();

    public TDestination Map<TSource, TDestination>(TSource? source, TDestination destination) =>
        source.Adapt(destination);
}