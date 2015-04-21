using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Binding;
using Neptuo.PresentationModels.BindingConverters;
using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.TypeModels.DataAnnotations;
using Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators;
using Neptuo.PresentationModels.Validators;
using Neptuo.Pipelines.Validators;
using Neptuo.Pipelines.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.PresentationModels.Validators.Handlers;
using Neptuo.PresentationModels.Validation;

namespace TestConsole.PresentationModels
{
    class TestPresentationModels : TestClass
    {
        public static void Test()
        {
            AttributeMetadataReaderCollection readerService = new AttributeMetadataReaderCollection()
                .Add(typeof(RequiredAttribute), new RequiredMetadataReader())
                .Add(typeof(DescriptionAttribute), new DescriptionMetadataReader())
                .Add(typeof(MatchPropertyAttribute), new MatchPropertyMetadataReader());

            FieldMetadataValidatorCollection validators = new FieldMetadataValidatorCollection()
                .Add(null, null, "Required", new RequiredMetadataValidator())
                .Add(null, null, "MatchProperty", new MatchPropertyMetadataValidator());

            BindingConverterCollection bindingConverters = new BindingConverterCollection()
                //.Add(new TypeFieldType(typeof(bool)), new BoolBindingConverter())
                //.Add(new TypeFieldType(typeof(int)), new IntBindingConverter())
                //.Add(new TypeFieldType(typeof(double)), new DoubleBindingConverter())
                //.Add(new TypeFieldType(typeof(string)), new StringBindingConverter());
                .AddStandart();

            IModelDefinition modelDefinition = new ReflectionModelDefinitionBuilder(typeof(RegisterUserModel), readerService).Create();
            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider valueProvider = new ReflectionModelValueProvider(model);

            IBindingModelValueStorage storage = new BindingDictionaryValueStorage()
                .Add("Username", "Pepa")
                .Add("Password", "XxYy")
                .Add("PasswordAgain", "XxYy")
                .Add("Age", "25")
                .Add("RoleIDs", "1,2,3,4,5,6");

            IModelValueGetter bindingGetter = new BindingModelValueGetter(storage, bindingConverters, modelDefinition);
            CopyModelValueProvider copyProvider = new CopyModelValueProvider(modelDefinition);
            Debug("Copy from dictionary", () => copyProvider.Update(valueProvider, bindingGetter));

            IValidationHandler<ModelValidatorContext> modelValidator = new FieldMetadataModelValidator(validators);
            IValidationResult validationResult = Debug("Validate user", () => modelValidator.Handle(new ModelValidatorContext(modelDefinition, valueProvider)));
            Console.WriteLine(validationResult);
            validationResult = Debug("Validate user with binding", () => modelValidator.Handle(new ModelValidatorContext(modelDefinition, bindingGetter)));
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
        public int? Age { get; set; }

        public IEnumerable<int> RoleIDs { get; set; }
    }
}
