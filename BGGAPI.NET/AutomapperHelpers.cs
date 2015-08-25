using System;

namespace BGGAPI
{
    internal class AutomapperHelpers
    {
        internal static TMember FormatExceptionSafeMapping<TSource, TMember>(TSource src, Func<TSource, TMember> sourceMapping)
        {
            try
            {
                return sourceMapping(src);
            }
            catch (FormatException)
            {
                return default(TMember);
            }
        }
    }
}
