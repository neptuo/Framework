using Neptuo.PresentationModels;
using Neptuo.PresentationModels.TypeModels;
using Neptuo.PresentationModels.TypeModels.DataAnnotations;
using Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators;
using Neptuo.PresentationModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    class TestPresentationModel
    {
        public static void Test()
        {
            MetadataReaderService readerService = new MetadataReaderService();
            readerService.Register(typeof(RequiredAttribute), new RequiredMetadataReader());
            readerService.Register(typeof(DescriptionAttribute), new DescriptionMetadataReader());
            readerService.Register(typeof(MatchPropertyAttribute), new MatchPropertyMetadataReader());

            MetadataValidatorCollection validators = new MetadataValidatorCollection();
            validators.Add(null, null, "Required", new SingletonFieldMetadataValidatorFactory(new RequiredMetadataValidator()));
            validators.Add(null, null, "MatchProperty", new SingletonFieldMetadataValidatorFactory(new MatchPropertyMetadataValidator()));

            IModelDefinition modelDefinition = new ReflectionModelDefinitionBuilder(typeof(RegisterUserModel), readerService).Build();
            RegisterUserModel model = new RegisterUserModel();
            model.Username = "pepa";
            model.Password = "x";
            model.PasswordAgain = "y";
            IModelValueProvider valueProvider = new ReflectionModelValueProvider(model);

            IModelValidator modelValidator = new MetadataModelValidator(modelDefinition, validators);
            IModelValidationResult validationResult = modelValidator.Validate(valueProvider);
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
    }
}
