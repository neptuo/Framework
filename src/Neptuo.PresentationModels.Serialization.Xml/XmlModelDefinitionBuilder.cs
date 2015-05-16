﻿using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML source based implementation of model definition builder.
    /// XML format must valid againts <see cref="XmlNameDefinition.XmlnsUri"/>.
    /// </summary>
    public class XmlModelDefinitionBuilder : IActivator<IModelDefinition>
    {
        private readonly XmlModelDefinitionFactory factory;
        private readonly IReadOnlyFile xmlFile;

        public XmlModelDefinitionBuilder(XmlTypeMappingCollection typeMappings, IReadOnlyFile xmlFile)
        {
            Ensure.NotNull(xmlFile, "xmlFile");
            this.factory = new XmlModelDefinitionFactory(typeMappings);
            this.xmlFile = xmlFile;
        }

        public XmlModelDefinitionBuilder(IReadOnlyFile xmlFile)
            : this(new XmlTypeMappingCollection(), xmlFile)
        { }

        public IModelDefinition Create()
        {
            return factory.Create(xmlFile);
        }
    }
}
