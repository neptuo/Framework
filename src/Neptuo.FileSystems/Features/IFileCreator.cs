using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// New file creator.
    /// </summary>
    public interface IFileCreator : IActivator<IFile, string>
    { }
}
