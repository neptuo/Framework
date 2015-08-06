using Neptuo.Activators;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Timestamps;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Common base class for <see cref="LocalFile"/> and <see cref="LocalDirectory"/>.
    /// </summary>
    public abstract class LocalItemBase : CollectionFeatureModel, IAbsolutePath, IActivator<IAncestorEnumerator>
    {
        public string AbsolutePath { get; private set; }

        protected LocalItemBase(string absolutePath)
            :  base(true)
        {
            AbsolutePath = absolutePath;

            this
                .Add<IAbsolutePath>(this)
                .AddFactory<IAncestorEnumerator>(this);
        }

        IAncestorEnumerator IActivator<IAncestorEnumerator>.Create()
        {
            return new LocalAncestorEnumerator(AbsolutePath);
        }
    }
}
