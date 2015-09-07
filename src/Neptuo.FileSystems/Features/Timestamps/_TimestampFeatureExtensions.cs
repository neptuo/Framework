using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Timestamps
{
    /// <summary>
    /// Timestamp feature extensions for <see cref="IFile"/> and <see cref="IDirectory"/>.
    /// </summary>
    public static class _TimestampFeatureExtensions
    {
        #region File.CreatedAt

        public static bool TryWithCreatedAt(this IFile model, out ICreatedAt feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<ICreatedAt>(out feature);
        }

        public static ICreatedAt WithCreatedAt(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<ICreatedAt>();
        }

        #endregion

        #region File.ModefiedAt

        public static bool TryWithModefiedAt(this IFile model, out IModefiedAt feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IModefiedAt>(out feature);
        }

        public static IModefiedAt WithModefiedAt(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IModefiedAt>();
        }

        #endregion

        #region Directory.CreatedAt

        public static bool TryWithCreatedAt(this IDirectory model, out ICreatedAt feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<ICreatedAt>(out feature);
        }

        public static ICreatedAt WithCreatedAt(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<ICreatedAt>();
        }

        #endregion

        #region Directory.ModefiedAt

        public static bool TryWithModefiedAt(this IDirectory model, out IModefiedAt feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IModefiedAt>(out feature);
        }

        public static IModefiedAt WithModefiedAt(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IModefiedAt>();
        }

        #endregion
    }
}
