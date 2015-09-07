using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Common feature extensions for <see cref="IFile"/>.
    /// </summary>
    public static class _DirectoryFeatureExtensions
    {
        #region AbsolutePath

        public static bool TryWithAbsolutePath(this IDirectory model, out IAbsolutePath feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAbsolutePath>(out feature);
        }

        public static IAbsolutePath WithAbsolutePath(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAbsolutePath>();
        }

        #endregion

        #region AncestorEnumerator

        public static bool TryWithAncestorEnumerator(this IDirectory model, out IAncestorEnumerator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAncestorEnumerator>(out feature);
        }

        public static IAncestorEnumerator WithAncestorEnumerator(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAncestorEnumerator>();
        }

        #endregion

        #region DirectoryCreator

        public static bool TryWithDirectoryCreator(this IDirectory model, out IDirectoryCreator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryCreator>(out feature);
        }

        public static IDirectoryCreator WithDirectoryCreator(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryCreator>();
        }

        #endregion

        #region DirectoryDeleter

        public static bool TryWithDirectoryDeleter(this IDirectory model, out IDirectoryDeleter feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryDeleter>(out feature);
        }

        public static IDirectoryDeleter WithDirectoryDeleter(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryDeleter>();
        }

        #endregion

        #region DirectoryEnumerator

        public static bool TryWithDirectoryEnumerator(this IDirectory model, out IDirectoryEnumerator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryEnumerator>(out feature);
        }

        public static IDirectoryEnumerator WithDirectoryEnumerator(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryEnumerator>();
        }

        #endregion

        #region DirectoryRenamer

        public static bool TryWithDirectoryRenamer(this IDirectory model, out IDirectoryRenamer feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IDirectoryRenamer>(out feature);
        }

        public static IDirectoryRenamer WithDirectoryRenamer(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IDirectoryRenamer>();
        }

        #endregion

        #region FileCreator

        public static bool TryWithFileCreator(this IDirectory model, out IFileCreator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileCreator>(out feature);
        }

        public static IFileCreator WithFileCreator(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileCreator>();
        }

        #endregion

        #region FileEnumerator

        public static bool TryWithFileEnumerator(this IDirectory model, out IFileEnumerator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileEnumerator>(out feature);
        }

        public static IFileEnumerator WithFileEnumerator(this IDirectory model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileEnumerator>();
        }

        #endregion
    }
}
