using Neptuo.ComponentModel;
using Neptuo.FileSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Factory for <see cref="IModelValueGetter"/> from collection of XML elements.
    /// </summary>
    public class XmlModelValueGetterFactory : IEnumerable<IModelValueGetter>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IReadOnlyList<XElement> elements;

        /// <summary>
        /// Returns count of matched elements/model value instances.
        /// </summary>
        public int Count
        {
            get { return elements.Count(); }
        }

        /// <summary>
        /// Creates new instance for models of type <paramref name="modelDefinition"/> from collection of <paramref name="elements"/>.
        /// <paramref name="elements"/> are filtered by <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="elements">Enumeration of elements to be used (at first, they are filtered by <paramref name="modelDefinition"/>).</param>
        public XmlModelValueGetterFactory(IModelDefinition modelDefinition, IEnumerable<XElement> elements)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(elements, "elements");
            this.modelDefinition = modelDefinition;
            this.elements = new List<XElement>(elements.Where(e => e.Name.LocalName == modelDefinition.Identifier));
        }

        /// <summary>
        /// Returns model value getter at position <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Zero based index to the collection of model value getters.</param>
        /// <returns>Model value getter at position <paramref name="index"/>.</returns>
        public IModelValueGetter Getter(int index)
        {
            return new XmlModelValueGetter(modelDefinition, elements[index]);
        }

        #region IEnumerable<IModelValueGetter>

        public IEnumerator<IModelValueGetter> GetEnumerator()
        {
            return elements.Select(e => new XmlModelValueGetter(modelDefinition, e)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
