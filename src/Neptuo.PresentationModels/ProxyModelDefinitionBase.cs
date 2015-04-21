using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Model definition that dynamically refreshes model parts as requested.
    /// </summary>
    public abstract class ProxyModelDefinitionBase : IModelDefinition
    {
        /// <summary>
        /// Cached model identifier.
        /// </summary>
        private string identifier;

        /// <summary>
        /// Cached model fields.
        /// </summary>
        private IEnumerable<IFieldDefinition> fields;

        /// <summary>
        /// Cached model metadata.
        /// </summary>
        private IReadOnlyKeyValueCollection metadata;

        /// <summary>
        /// If <c>true</c>, next access to identifier, fields or metadata forces refresh.
        /// </summary>
        protected bool IsRefreshRequired { get; set; }

        public string Identifier
        {
            get
            {
                if (IsRefreshRequired)
                {
                    identifier = RefreshIdentifier();
                    IsRefreshRequired = false;
                }
                return identifier;
            }
        }

        public IEnumerable<IFieldDefinition> Fields
        {
            get
            {
                if (IsRefreshRequired)
                {
                    fields = RefreshFields();
                    IsRefreshRequired = false;
                }
                return fields;
            }
        }

        public IReadOnlyKeyValueCollection Metadata
        {
            get
            {
                if (IsRefreshRequired)
                {
                    metadata = RefreshMetadata();
                    IsRefreshRequired = false;
                }
                return metadata;
            }
        }

        public ProxyModelDefinitionBase()
        {
            IsRefreshRequired = true;
        }

        /// <summary>
        /// Provides model identifier.
        /// </summary>
        /// <returns>Model identifier.</returns>
        protected abstract string RefreshIdentifier();

        /// <summary>
        /// Provides model fields.
        /// </summary>
        /// <returns>Model fields.</returns>
        protected abstract IEnumerable<IFieldDefinition> RefreshFields();

        /// <summary>
        /// Provides model metadata.
        /// </summary>
        /// <returns>Model metadata.</returns>
        protected abstract IReadOnlyKeyValueCollection RefreshMetadata();
    }
}
