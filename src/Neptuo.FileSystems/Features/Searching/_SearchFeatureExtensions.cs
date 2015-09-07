using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Searching feature extensions for <see cref="IFile"/> and <see cref="IDirectory"/>.
    /// </summary>
    public static class _SearchFeatureExtensions
    {
        #region DirectoryNameSearch

        public static bool TryWithDirectoryNameSearch(this IDirectory model, out IDirectoryNameSearch feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryNameSearch>(out feature);
        }

        public static IDirectoryNameSearch WithDirectoryNameSearch(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryNameSearch>();
        }

        #endregion

        #region DirectoryPathSearch

        public static bool TryWithDirectoryPathSearch(this IDirectory model, out IDirectoryPathSearch feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryPathSearch>(out feature);
        }

        public static IDirectoryPathSearch WithDirectoryPathSearch(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryPathSearch>();
        }

        #endregion

        #region FileNameSearch

        public static bool TryWithFileNameSearch(this IDirectory model, out IFileNameSearch feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileNameSearch>(out feature);
        }

        public static IFileNameSearch WithFileNameSearch(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileNameSearch>();
        }

        #endregion

        #region FilePathSearch

        public static bool TryWithFilePathSearch(this IDirectory model, out IFilePathSearch feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFilePathSearch>(out feature);
        }

        public static IFilePathSearch WithFilePathSearch(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFilePathSearch>();
        }

        #endregion
    }
}
