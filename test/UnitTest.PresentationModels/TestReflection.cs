using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.PresentationModels.Models;
using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.TypeModels.DataAnnotations;
using Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators;
using Neptuo.PresentationModels.TypeModels.ValueUpdates;
using Neptuo.PresentationModels.Validation;
using Neptuo.PresentationModels.Validators;
using Neptuo.PresentationModels.Validators.Handlers;
using Neptuo.Validators;
using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    [TestClass]
    public class TestReflection
    {
        private AttributeMetadataReaderCollection metadataReaders;
        private FieldMetadataValidatorCollection fieldMetadataValidators;
        private ReflectionValueUpdaterCollection valueUpdaters;
        private TypeModelDefinitionCollection modelDefinitions;

        public TestReflection()
        {
            metadataReaders = new AttributeMetadataReaderCollection()
               .Add(new CompareMetadataReader())
               .Add(new DataTypeMetadataReader())
               .Add(new DefaultValueMetadataReader())
               .Add(new DescriptionMetadataReader())
               .Add(new DisplayMetadataReader())
               .Add(new RequiredMetadataReader())
               .Add(new StringLengthMetadataReader());

            fieldMetadataValidators = new FieldMetadataValidatorCollection()
               .Add(null, null, "Required", new RequiredMetadataValidator())
               .Add(null, null, "MatchProperty", new MatchPropertyMetadataValidator());

            valueUpdaters = new ReflectionValueUpdaterCollection()
               .Add<ICollection<int>>(new CollectionItemReflectionValueUpdater<int>());

            modelDefinitions = new TypeModelDefinitionCollection()
                .AddReflectionSearchHandler(metadataReaders);
        }

        [TestMethod]
        public void BuildModelDefinition()
        {
            IModelDefinition modelDefinition = modelDefinitions.Get<RegisterUserModel>();
            Assert.AreEqual(5, modelDefinition.Fields.Count());
        }

        [TestMethod]
        public void GetValues()
        {
            IModelDefinition modelDefinition = modelDefinitions.Get<RegisterUserModel>();

            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider reflection = new ReflectionModelValueProvider(model, valueUpdaters);
            DictionaryModelValueProvider dictionary = new DictionaryModelValueProvider();

            CopyModelValueProvider copyProvider = new CopyModelValueProvider(modelDefinition, true);
            copyProvider.Update(dictionary, reflection);

            object value;
            Assert.IsTrue(dictionary.TryGetValue(nameof(RegisterUserModel.Username), out value));
            Assert.AreEqual(model.Username, value);

            Assert.IsTrue(dictionary.TryGetValue(nameof(RegisterUserModel.Password), out value));
            Assert.AreEqual(model.Password, value);

            Assert.IsTrue(dictionary.TryGetValue(nameof(RegisterUserModel.PasswordAgain), out value));
            Assert.AreEqual(model.PasswordAgain, value);
        }
    }
}
