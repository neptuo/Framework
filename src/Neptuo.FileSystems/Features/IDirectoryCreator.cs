using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// New directory creator.
    /// </summary>
    public interface IDirectoryCreator : IActivator<IDirectory, string>
    { }
}
