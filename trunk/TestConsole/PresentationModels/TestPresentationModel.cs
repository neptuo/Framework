using Neptuo.PresentationModels;
using Neptuo.PresentationModels.BindingConverters;
using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.TypeModels.DataAnnotations;
using Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators;
using Neptuo.PresentationModels.Validation;
using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    class TestPresentationModel : TestClass
    {
        public static void Test()
        {
            MetadataReaderService readerService = new MetadataReaderService()
                .Add(typeof(RequiredAttribute), new RequiredMetadataReader())
                .Add(typeof(DescriptionAttribute), new DescriptionMetadataReader())
                .Add(typeof(MatchPropertyAttribute), new MatchPropertyMetadataReader());

            MetadataValidatorCollection validators = new MetadataValidatorCollection()
                .Add(null, null, "Required", new SingletonFieldMetadataValidatorFactory(new RequiredMetadataValidator()))
                .Add(null, null, "MatchProperty", new SingletonFieldMetadataValidatorFactory(new MatchPropertyMetadataValidator()));

            BindingConverterCollection bindingConverters = new BindingConverterCollection()
                //.Add(new TypeFieldType(typeof(bool)), new BoolBindingConverter())
                //.Add(new TypeFieldType(typeof(int)), new IntBindingConverter())
                //.Add(new TypeFieldType(typeof(double)), new DoubleBindingConverter())
                //.Add(new TypeFieldType(typeof(string)), new StringBindingConverter());
                .AddStandart();

            IModelDefinition modelDefinition = new ReflectionModelDefinitionBuilder(typeof(RegisterUserModel), readerService).Build();
            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider valueProvider = new ReflectionModelValueProvider(model);

            IBindingModelValueStorage storage = new BindingDictionaryValueStorage()
                .Add("Username", "Pepa")
                .Add("Password", "XxYy")
                //.Add("PasswordAgain", "  ")
                .Add("Age", "25");

            IModelValueGetter bindingGetter = new BindingModelValueGetter(storage, bindingConverters, modelDefinition);
            CopyModelValueProvider copyProvider = new CopyModelValueProvider(modelDefinition);
            Debug("Copy from dictionary", () => copyProvider.Update(valueProvider, bindingGetter));

            IModelValidator modelValidator = new MetadataModelValidator(modelDefinition, validators);
            IValidationResult validationResult = Debug("Validate user", () => modelValidator.Validate(valueProvider));
            Console.WriteLine(validationResult);
        }
    }

    class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MatchProperty("Password")]
        public string PasswordAgain { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
