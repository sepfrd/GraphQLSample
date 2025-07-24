namespace Application.Abstractions;

public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource? source);

    TDestination Map<TSource, TDestination>(TSource? source, TDestination destination);
}