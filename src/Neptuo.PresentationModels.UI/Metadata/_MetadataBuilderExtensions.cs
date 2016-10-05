using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Metadata
{
    public static class _MetadataBuilderExtensions
    {
        #region GridColumnIndex

        public static IMetadataBuilder AddGridColumnIndex(this IMetadataBuilder builder, int value)
        {
            return builder.Add("GridColumnIndex", value);
        }

        public static bool TryGetGridColumnIndex(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            return metadata.TryGet("GridColumnIndex", out value);
        }

        public static int GetGridColumnIndex(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("GridColumnIndex");
        }

        public static int GetGridColumnIndex(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("GridColumnIndex", defaultValue);
        }

        #endregion

        #region GridColumnSpan

        public static IMetadataBuilder AddGridColumnSpan(this IMetadataBuilder builder, int? value)
        {
            return builder.Add("GridColumnSpan", value);
        }

        public static bool TryGetGridColumnSpan(this IReadOnlyKeyValueCollection metadata, out int? value)
        {
            return metadata.TryGet("GridColumnSpan", out value);
        }

        public static int? GetGridColumnSpan(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int?>("GridColumnSpan");
        }

        public static int? GetGridColumnSpan(this IReadOnlyKeyValueCollection metadata, int? defaultValue)
        {
            return metadata.Get("GridColumnSpan", defaultValue);
        }

        #endregion

        #region GridRowIndex

        public static IMetadataBuilder AddGridRowIndex(this IMetadataBuilder builder, int value)
        {
            return builder.Add("GridRowIndex", value);
        }

        public static bool TryGetGridRowIndex(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            return metadata.TryGet("GridRowIndex", out value);
        }

        public static int GetGridRowIndex(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("GridRowIndex");
        }

        public static int GetGridRowIndex(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("GridRowIndex", defaultValue);
        }

        #endregion

        #region GridRowSpan

        public static IMetadataBuilder AddGridRowSpan(this IMetadataBuilder builder, int? value)
        {
            return builder.Add("GridRowSpan", value);
        }

        public static bool TryGetGridRowSpan(this IReadOnlyKeyValueCollection metadata, out int? value)
        {
            return metadata.TryGet("GridRowSpan", out value);
        }

        public static int? GetGridRowSpan(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int?>("GridRowSpan");
        }

        public static int? GetGridRowSpan(this IReadOnlyKeyValueCollection metadata, int? defaultValue)
        {
            return metadata.Get("GridRowSpan", defaultValue);
        }

        #endregion
    }
}
