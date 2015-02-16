using System;
using AutoMapper;

namespace BGGAPI
{
    public static class AutoMapperExtensions
    {
        public static void NullSafeConvertUsing<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression, Func<TSource, TDestination> mappingFunction)
        {
            mappingExpression.ConvertUsing(src => src == null ? default(TDestination) : mappingFunction(src));
        }
    }
}
