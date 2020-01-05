namespace Test.ECommerce.Data.Mapping
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}