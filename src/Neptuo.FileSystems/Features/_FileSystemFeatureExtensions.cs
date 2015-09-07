using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Common feature extensions for <see cref="IFileSystem"/>.
    /// </summary>
    public static class _FileSystemFeatureExtensions
    {
        #region FileSystemConstant

        public static bool TryWithFileSystemConstant(this IFileSystem model, out IFileSystemConstant feature)
        {
            Ensure.NotNull(model, "model");
            return model.TryWith<IFileSystemConstant>(out feature);
        }

        public static IFileSystemConstant WithFileSystemConstant(this IFileSystem model)
        {
            Ensure.NotNull(model, "model");
            return model.With<IFileSystemConstant>();
        }

        #endregion
    }
}
