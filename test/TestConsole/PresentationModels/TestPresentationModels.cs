using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Binding;
using Neptuo.PresentationModels.Binding.Converters;
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
using Neptuo.PresentationModels.TypeModels.ValueUpdates;

namespace TestConsole.PresentationModels
{
    class TestPresentationModels : TestClass
    {
        public static void Test()
        {
            AttributeMetadataReaderCollection metadataReaders = new AttributeMetadataReaderCollection()
                .Add<CompareAttribute>(new CompareMetadataReader())
                .Add<DataTypeAttribute>(new DataTypeMetadataReader())
                .Add<DefaultValueAttribute>(new DefaultValueMetadataReader())
                .Add<DescriptionAttribute>(new DescriptionMetadataReader())
                .Add<DisplayAttribute>(new DisplayMetadataReader())
                .Add<RequiredAttribute>(new RequiredMetadataReader())
                .Add<StringLengthAttribute>(new StringLengthMetadataReader());

            FieldMetadataValidatorCollection fieldMetadataValidators = new FieldMetadataValidatorCollection()
                .Add(null, null, "Required", new RequiredMetadataValidator())
                .Add(null, null, "MatchProperty", new MatchPropertyMetadataValidator());

            BindingConverterCollection bindingConverters = new BindingConverterCollection()
                //.Add(new TypeFieldType(typeof(bool)), new BoolBindingConverter())
                //.Add(new TypeFieldType(typeof(int)), new IntBindingConverter())
                //.Add(new TypeFieldType(typeof(double)), new DoubleBindingConverter())
                //.Add(new TypeFieldType(typeof(string)), new StringBindingConverter());
                .AddStandart();

            TypeModelDefinitionCollection modelDefinitions = new TypeModelDefinitionCollection()
                .AddReflectionSearchHandler(metadataReaders);

            IModelDefinition modelDefinition = modelDefinitions.Get<RegisterUserModel>();
            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider valueProvider = new ReflectionModelValueProvider(
                model, 
                new ReflectionValueUpdaterCollection()
                    .Add<ICollection<int>>(new ReflectionCollectionItemValueUpdater<int>())
            );

            IBindingModelValueStorage storage = new BindingDictionaryValueStorage()
                .Add("Username", "Pepa")
                .Add("Password", "XxYy")
                //.Add("PasswordAgain", "XxYy")
                //.Add("Age", "25")
                .Add("RoleIDs", "1,2,3,4,5,6");

            IModelValueGetter bindingGetter = new BindingModelValueGetter(storage, bindingConverters, modelDefinition);
            CopyModelValueProvider copyProvider = new CopyModelValueProvider(modelDefinition, true);
            Debug("Copy from dictionary", () => copyProvider.Update(valueProvider, bindingGetter));

            IValidationHandler<ModelValidatorContext> modelValidator = new FieldMetadataModelValidator(fieldMetadataValidators);
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
        [Compare("Password")]
        public string PasswordAgain { get; set; }

        [Required]
        public int? Age { get; set; }

        public ICollection<int> RoleIDs { get { return null; } }
    }
}
