using Neptuo.Converters;
using Neptuo.Formatters.Internals;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// Registration extensions of <see cref="IConverterRepository"/>.
    /// </summary>
    public static class _ConverterRepositoryExtensions
    {
        public static IConverterRepository AddJsonPrimitivesSearchHandler(this IConverterRepository repository) 
        {
            Ensure.NotNull(repository, "repository");
            return repository.AddSearchHandler(TryGetJsonPrimitiveConverter);
        }

        private static bool TryGetJsonPrimitiveConverter(ConverterSearchContext context, out IConverter converter)
        {
            if (context.SourceType == typeof(JToken) || context.TargetType == typeof(JToken))
            {
                converter = new JsonPrimitiveConverter();
                return true;
            }

            converter = null;
            return false;
        }

        public static IConverterRepository AddJsonEnumSearchHandler(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");
            return repository.AddSearchHandler(TryGetJsonEnumConverter);
        }

        private static bool TryGetJsonEnumConverter(ConverterSearchContext context, out IConverter converter)
        {
            bool isSuccess = false;

            if (context.SourceType == typeof(JToken))
            {
                if (context.TargetType.IsEnum)
                    isSuccess = true;
                else if (context.TargetType.IsNullableType() && context.TargetType.GetGenericArguments()[0].IsEnum)
                    isSuccess = true;
            }
            else if (context.TargetType == typeof(JToken))
            {
                if (context.SourceType.IsEnum)
                    isSuccess = true;
                else if (context.SourceType.IsNullableType() && context.SourceType.GetGenericArguments()[0].IsEnum)
                    isSuccess = true;
            }

            // TODO: Continue here...
            if (isSuccess)
                converter = new JsonEnumConverter(JsonEnumConverterType.UseInderlayingValue);
            else
                converter = null;

            return isSuccess;    
        }
    }
}
