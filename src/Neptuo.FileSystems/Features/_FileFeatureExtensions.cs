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
    public static class _FileFeatureExtensions
    {
        #region AbsolutePath

        public static bool TryWithAbsolutePath(this IFile model, out IAbsolutePath feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAbsolutePath>(out feature);
        }

        public static IAbsolutePath WithAbsolutePath(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAbsolutePath>();
        }

        #endregion

        #region AncestorEnumerator

        public static bool TryWithAncestorEnumerator(this IFile model, out IAncestorEnumerator feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IAncestorEnumerator>(out feature);
        }

        public static IAncestorEnumerator WithAncestorEnumerator(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IAncestorEnumerator>();
        }

        #endregion

        #region FileContentAppender

        public static bool TryWithFileContentAppender(this IFile model, out IFileContentAppender feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileContentAppender>(out feature);
        }

        public static IFileContentAppender WithFileContentAppender(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileContentAppender>();
        }

        #endregion

        #region FileContentReader

        public static bool TryWithFileContentReader(this IFile model, out IFileContentReader feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileContentReader>(out feature);
        }

        public static IFileContentReader WithFileContentReader(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileContentReader>();
        }

        #endregion

        #region FileContentSize

        public static bool TryWithFileContentSize(this IFile model, out IFileContentSize feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileContentSize>(out feature);
        }

        public static IFileContentSize WithFileContentSize(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileContentSize>();
        }

        #endregion

        #region FileContentUpdater

        public static bool TryWithFileContentUpdater(this IFile model, out IFileContentUpdater feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileContentUpdater>(out feature);
        }

        public static IFileContentUpdater WithFileContentUpdater(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileContentUpdater>();
        }

        #endregion

        #region FileDeleter

        public static bool TryWithFileDeleter(this IFile model, out IFileDeleter feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileDeleter>(out feature);
        }

        public static IFileDeleter WithFileDeleter(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileDeleter>();
        }

        #endregion

        #region FileRenamer

        public static bool TryWithFileRenamer(this IFile model, out IFileRenamer feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileRenamer>(out feature);
        }

        public static IFileRenamer WithFileRenamer(this IFile model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileRenamer>();
        }

        #endregion
    }
}
