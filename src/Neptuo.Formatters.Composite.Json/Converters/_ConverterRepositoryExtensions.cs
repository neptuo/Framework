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
            bool isSuccess = false;

            if (context.SourceType == typeof(JToken))
            {
                if (JsonPrimitiveConverter.Supported.Contains(context.TargetType))
                    isSuccess = true;
            }
            else if (context.TargetType == typeof(JToken))
            {
                if (JsonPrimitiveConverter.Supported.Contains(context.SourceType))
                    isSuccess = true;
            }

            if (isSuccess)
                converter = new JsonPrimitiveConverter();
            else
                converter = null;

            return isSuccess;
        }

        public static IConverterRepository AddJsonEnumSearchHandler(this IConverterRepository repository, JsonEnumConverterType converterType = JsonEnumConverterType.UseInderlayingValue)
        {
            Ensure.NotNull(repository, "repository");
            return repository.AddSearchHandler(new TryGetJsonEnumConverter(converterType).TryFind);
        }

        private class TryGetJsonEnumConverter
        {
            private readonly JsonEnumConverterType converterType;

            public TryGetJsonEnumConverter(JsonEnumConverterType converterType)
            {
                Ensure.NotNull(converterType, "converterType");
                this.converterType = converterType;
            }

            public bool TryFind(ConverterSearchContext context, out IConverter converter)
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

                if (isSuccess)
                    converter = new JsonEnumConverter(converterType);
                else
                    converter = null;

                return isSuccess;
            }
        }

        public static IConverterRepository AddJsonObjectSearchHandler(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");
            return repository.AddSearchHandler(TryGetJsonObjectConverter);
        }

        private static bool TryGetJsonObjectConverter(ConverterSearchContext context, out IConverter converter)
        {
            bool isSuccess = false;
            if(context.SourceType == typeof(JToken))
            {
                if (context.TargetType.IsClass && !context.TargetType.IsAbstract)
                    isSuccess = true;
            }
            else if (context.TargetType == typeof(JToken))
            {
                if (context.SourceType.IsClass && !context.SourceType.IsAbstract)
                    isSuccess = true;
            }

            if (isSuccess)
                converter = new JsonObjectConverter();
            else
                converter = null;

            return isSuccess;
        }
    }
}
