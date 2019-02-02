using Neptuo.PresentationModels;
using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.TypeModels.DataAnnotations;
using Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators;
using Neptuo.PresentationModels.Validators;
using Neptuo.Validators;
using Neptuo.Validators.Handlers;
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
                .Add(new CompareMetadataReader())
                .Add(new DataTypeMetadataReader())
                .Add(new DefaultValueMetadataReader())
                .Add(new DescriptionMetadataReader())
                .Add(new DisplayMetadataReader())
                .Add(new RequiredMetadataReader())
                .Add(new StringLengthMetadataReader());

            FieldMetadataValidatorCollection fieldMetadataValidators = new FieldMetadataValidatorCollection()
                .Add(null, null, "Required", new RequiredMetadataValidator())
                .Add(null, null, "MatchProperty", new MatchPropertyMetadataValidator());

            ReflectionValueUpdaterCollection valueUpdaters = new ReflectionValueUpdaterCollection()
                    .Add<ICollection<int>>(new CollectionItemReflectionValueUpdater<int>());

            TypeModelDefinitionCollection modelDefinitions = new TypeModelDefinitionCollection()
                .AddReflectionSearchHandler(metadataReaders);

            IModelDefinition modelDefinition = modelDefinitions.Get<RegisterUserModel>();
            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider valueProvider = new ReflectionModelValueProvider(model, valueUpdaters);

            CopyModelValueProvider copyProvider = new CopyModelValueProvider(modelDefinition, true);

            Console.WriteLine("RoleIDs: {0}", String.Join(", ", model.RoleIDs));

            IValidationHandler<ModelValidatorContext> modelValidator = new FieldMetadataModelValidator(fieldMetadataValidators);
            Task<IValidationResult> validationResult = Debug("Validate user", () => modelValidator.HandleAsync(new ModelValidatorContext(modelDefinition, valueProvider)));
            if (!validationResult.IsCompleted)
                validationResult.RunSynchronously();

            Console.WriteLine(validationResult.Result);
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

        private ICollection<int> roleIDs;
        public ICollection<int> RoleIDs { get { return roleIDs; } }

        public RegisterUserModel()
        {
            roleIDs = new List<int>();
        }
    }
}
