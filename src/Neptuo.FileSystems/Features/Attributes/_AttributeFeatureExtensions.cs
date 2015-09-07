using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Attributes
{
    /// <summary>
    /// Attribute feature extensions for <see cref="IFile"/> and <see cref="IDirectory"/>.
    /// </summary>
    public static class _AttributeFeatureExtensions
    {
        #region File.AttributeReader

        public static bool TryWithAttributeReader(this IFile model, out IAttributeReader feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAttributeReader>(out feature);
        }

        public static IAttributeReader WithAttributeReader(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAttributeReader>();
        }

        #endregion

        #region File.AttributeUpdater

        public static bool TryWithAttributeUpdater(this IFile model, out IAttributeUpdater feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAttributeUpdater>(out feature);
        }

        public static IAttributeUpdater WithAttributeUpdater(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAttributeUpdater>();
        }

        #endregion

        #region DirectoryAttributeReader

        public static bool TryWithAttributeReader(this IDirectory model, out IAttributeReader feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAttributeReader>(out feature);
        }

        public static IAttributeReader WithAttributeReader(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAttributeReader>();
        }

        #endregion

        #region Directory.AttributeUpdater

        public static bool TryWithAttributeUpdater(this IDirectory model, out IAttributeUpdater feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAttributeUpdater>(out feature);
        }

        public static IAttributeUpdater WithAttributeUpdater(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAttributeUpdater>();
        }

        #endregion
    }
}
